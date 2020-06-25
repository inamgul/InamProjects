using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using fypPromolac;
using fypPromolac.Models;

namespace fypPromolac.Controllers
{
    [Authorize]
    public class userInfoesController : Controller
    {
        private promoLacDbEntities db = new promoLacDbEntities();

        // GET: userInfoes
        public ActionResult Index()
        {
            return View(db.vendors.ToList());
        }
        /*========GET MEMBERS UNDER VENDOR==========*/
        public ActionResult memberdetails()
        {
            using (var context = new promoLacDbEntities())
            {
                int id = vendorId(User.Identity.Name);
               // subUserModel details = new subUserModel();
                var myresult = context.vendors.Where(x => x.headId == id).Select(x => new userPackageModel()
                {
                    vendorId = x.vendorId,
                    userName=x.vendorUserName
                }).ToList();


                foreach (userPackageModel x in myresult)
                {
                    var t = context.userPackages.Where(z => z.vendorId == x.vendorId).FirstOrDefault();


                    x.packageId = t.packageId;
                    x.dividedShareOfMessages = t.dividedShareOfMessages;
                    x.notificationSent = t.notificationSent;
                    x.dealsCreated = t.dealsCreated;
                    x.fenceCreated = t.fenceCreated;
                    x.detail = Getarea(x.vendorId);


                   
                }
                return View(myresult);

            }
        }
        public ActionResult changePassword()
        {
            using (var context=new promoLacDbEntities())
            {
                var myresult = context.vendors.Where(x => x.vendorUserName == User.Identity.Name).Select(x => new passwordChange()
                {
                    password = x.vendorPassword

                }).FirstOrDefault();
                ViewBag.password = myresult.password;
                return View();
            }
           
        }

        public ActionResult notificationHistory(int id)
        {
            var context = new promoLacDbEntities();
            var result = context.notifications.Where(x => x.vendorId == id).Select(y => new notificationModel() {
                notificationId = y.notificationId.ToString(),
                notificationDescription = y.notificationDescription,
                notificationTitle = y.notificationTitle,
                notificationAreaId=y.notificationAreaId.ToString(),
                vendorId=Convert.ToInt32(y.vendorId),
                notificationTime=y.notificationTime
                
              
            }).ToList();

            foreach(var x in result)
            {
                x.areas = getAreaName(Convert.ToInt32(x.notificationAreaId));
                x.userName = getUserName(Convert.ToInt32(x.vendorId));
            }
            return View(result);
        }


        [HttpPost]
        public ActionResult changePassword(passwordChange p)
        {
            using (var context = new promoLacDbEntities())
            {

                var myresult = context.vendors.Where(x=>x.vendorUserName==User.Identity.Name).First();

                myresult.vendorPassword = p.newPassword;

                context.SaveChanges();
                return RedirectToAction("../ControlPanel/ControlPanel");

            }

           

        }
        /* public int mesallowed(int id)
         {
             using (var context = new userAuthenticationEntities())
             {
                 var result1 = context.messages.Where(x => x.messageid == id).Select(x => new message()
                 {
                     m_allowed = x.m_allowed

                 }).FirstOrDefault();

                 return result1.m_allowed;
             }
         }*/
        //get vendorId through vendor name from vendor table
        public int vendorId (string name){
         
            using (var context = new promoLacDbEntities())
            {
                var result = context.vendors.Where(x => x.vendorUserName == name).Select(x => new vendorModel()
                {
                    vendorId=x.vendorId

                }).FirstOrDefault();

                return result.vendorId;
            }
           
            }



        /*public int messent(int id)
        {
            using (var context = new userAuthenticationEntities())
            {
                var result1 = context.messages.Where(x => x.messageid == id).Select(x => new message()
                {
                    m_sent = x.m_sent

                }).FirstOrDefault();

                return result1.m_sent;
            }
        }*/
        public List<areaAssignedModel> Getarea(int id)

        {
            using (var context = new promoLacDbEntities())
            {
                var result = context.areaAssigneds.Where(x => x.vendorId == id).Select(x => new areaAssignedModel()
                {
                    areaId=x.areaId

                }).ToList();
                foreach(areaAssignedModel var in result)
                {
                    var.areaName = getAreaName(var.areaId);
                }
                return result;
            }



        }
        /*=========Return Area Name to function Get Area ==========*/

        public string getAreaName(int id)
        {
            using (var context = new promoLacDbEntities())
            {
                var result = context.areas.Where(x => x.areaId == id).Select(x => new areaModel()
                {
                    areaName = x.areaName

                }).FirstOrDefault();
                
                return result.areaName;
            }
        }
        public int getAreaId(string name)
        {
            using (var context = new promoLacDbEntities())
            {
                var result = context.areas.Where(x => x.areaName == name).Select(x => new areaModel()
                {
                    areaId = x.areaId

                }).FirstOrDefault();

                return result.areaId;
            }
        }

        /*============Return Messages Allowed to Package Details function===========*/
        public int messagesAllowed(int z)
        {
            using (var context = new promoLacDbEntities())
            {
                var result = context.packages.Where(x => x.packagesId == z).Select(x => new packageModel()
                {
                    messagesAllowed = x.messagesAllowed

                }).FirstOrDefault();

                return result.messagesAllowed;


            }

        }

        /*=============Return Messages Sent to Package Details function==============*/
        public int messagesSent(int z)
        {
            using (var context = new promoLacDbEntities())
            {
                var result = context.userPackages.Where(x => x.vendorId == z).Select(x => new userPackageModel()
                {
                    notificationSent=x.notificationSent

                }).FirstOrDefault();

                return result.notificationSent;


            }

        }
        // GET: userInfoes/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userInfo userInfo = db.userInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }*/

        /*=======Provide list of areas to be added for Subuser under Vendor==========*/
        public ActionResult Addarea(int id)
        {
            areaAssignedModel obj = new areaAssignedModel();
            using (var context = new promoLacDbEntities())
            {
                
                obj.detail = Getarea(vendorId(User.Identity.Name));
                obj.vendorId = id;
                
                
               
              
                return View(obj);
            }
                
        }
        [HttpPost]
        public ActionResult Addarea(areaAssignedModel u)
        {
            using (var context = new promoLacDbEntities())
            {

                areaAssigned a = new areaAssigned()
                {
                    vendorId = u.vendorId,
                    areaId = 1

                    };

                    context.areaAssigneds.Add(a);
                    context.SaveChanges();
                
                
            }
            return RedirectToAction("../controlPanel/controlPanel");

        }
            // GET: userInfoes/Create
            public ActionResult Create()
        {
            var context = new promoLacDbEntities();
            int id = vendorId(User.Identity.Name);
            var result2 = context.userPackages.Where(x => x.vendorId == id).FirstOrDefault();

            vendorModel v = new vendorModel();
            var detail = context.areaAssigneds.Where(x => x.vendorId == id).Select(y=> new areaAssignedModel()
            {
                areaId=y.areaId
            }).ToList();

            foreach(var z in detail)
            {
                z.areaName = getAreaName(z.areaId);
            }
            v.detail = detail;
            v.messagesStock = result2.dividedShareOfMessages;
            v.fencingHoursStock = Convert.ToInt32(result2.remainingFencingHours);
            v.vendorId = id;
            return View(v);
        }

        // POST: userInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        /*  public ActionResult Create([Bind(Include = "userId,userName,password")] subUser user)
          {
              if (ModelState.IsValid)
              {
                  db.subUsers.Add(user);
                  db.SaveChanges();
                  return RedirectToAction("Index");
              }
              return RedirectToAction("Index");
              //return View(userInfo);
          }*/


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(vendorModel user)
        {
           
            using (var context = new promoLacDbEntities())
            {


                {
                    vendor info = new vendor()
                    {
                        firstName = user.firstName,
                        lastName = user.lastName,
                        vendorEmail = user.vendorEmail,
                        vendorPassword = user.vendorPassword,
                        registerTimestamp = DateTime.Now,
                        phoneNumber = user.phoneNumber,
                        vendorUserName = user.vendorUserName,
                        headId = vendorId(User.Identity.Name),
                        isHead = "N",
                        vendorStatus = "A",
                        vendorAddress = vendorAddress(vendorId(User.Identity.Name)),
                        vendorCompanyName = vendorCompanyName(vendorId(User.Identity.Name))

                    };


                    context.vendors.Add(info);
                    context.SaveChanges();
                    var result = context.userPackages.Where(x => x.vendorId == info.headId).FirstOrDefault();
                    userPackage u = new userPackage();
                    u.vendorId = vendorId(user.vendorUserName);
                    u.packageStartTime = result.packageStartTime;
                    u.packageEndTime = result.packageEndTime;
                    u.packageId = result.packageId;
                    u.dividedShareOfMessages = user.messagesAllowed;
                    u.packageStatus = "active";
                    u.notificationSent = 0;
                    u.remainingFencingHours = user.fencingHours;
                    context.userPackages.Add(u);
                    context.SaveChanges();

                    foreach (var x in user.areas)
                    {
                        areaAssigned ar = new areaAssigned()
                        {
                            vendorId = vendorId(user.vendorUserName),
                            areaId = Convert.ToInt32(x)

                    };
                    context.areaAssigneds.Add(ar);
                    context.SaveChanges();
                  
                    }
                    var result1 = context.userPackages.Where(x => x.vendorId == info.headId).FirstOrDefault();
                    result1.dividedShareOfMessages = result1.dividedShareOfMessages - user.messagesAllowed;
                    result1.remainingFencingHours = result1.remainingFencingHours - user.fencingHours;
                    context.SaveChanges();
                        
                    }
                    
                
                return RedirectToAction("../ControlPanel/ControlPanel");
            }
        }



        /*  public ActionResult changePassword(int id)
          {
              using (var context = new promoLacDbEntities())
              {
                  user details = new user();
                  var myresult = context.vendors.Where(x => x.vendorId==id).Select(x => new vendorModel()
                  {

                      vendorId=x.vendorId,
                      vendorUserName=x.vendorUserName,
                      vendorPassword=x.vendorPassword

                  }).FirstOrDefault();
                  return View(myresult);

              }


          }
  [HttpPost]
          public ActionResult changePassword(vendorModel v)
          {


              using (var context = new promoLacDbEntities())
              {

                  var myresult = context.vendors.Where(x => x.vendorId == v.vendorId).First();

                  myresult.vendorPassword = v.vendorPassword;

                  context.SaveChanges();
                  return RedirectToAction("../ControlPanel/ControlPanel");

              }

          }*/

            public string vendorAddress(int id)
        {
            var context = new promoLacDbEntities();
            var r = context.vendors.Where(x => x.vendorId == id).FirstOrDefault();

            return r.vendorAddress;
        }


        public string vendorCompanyName(int id)
        {
            var context = new promoLacDbEntities();
            var r = context.vendors.Where(x => x.vendorId == id).FirstOrDefault();

            return r.vendorCompanyName;
        }


        // GET: userInfoes/Edit/5
        public JsonResult IsUserNameAvailable(string vendorUserName)
        {
            return Json(!db.vendors.Any(u => u.vendorUserName == vendorUserName), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //userInfo userInfo = db.userInfo.Find(id);
            using (var context =  new promoLacDbEntities())
            {
                //subUserModel details = new subUserModel();
                var myresult = context.vendors.Where(x => x.vendorId==id).Select(x => new vendorModel()
                {

                    vendorUserName=x.vendorUserName,
                    headId=x.headId,
                    vendorId=x.vendorId,
                    vendorPassword=x.vendorPassword
                    
                }).FirstOrDefault();


                myresult.detail = GetareaSubUser(myresult.vendorId);
                /*details.subUserId= myresult.subUserId;
                details.subUserUserName = myresult.subUserUserName;
                details.subUserPassword= myresult.subUserPassword;
                */

                   //myresult.detail = Getarea(myresult.userId);
                  

                
                return View(myresult);

            }

            /*if (userInfo == null)
            {
                return HttpNotFound();
            }*/
            //return View(userInfo);
        }
        public List<areaAssignedModel> GetareaSubUser(int id)

        {
            using (var context = new promoLacDbEntities())
            {
                var result = context.areaAssigneds.Where(x =>x.vendorId== id).Select(x => new areaAssignedModel()
                {
                    areaId = x.areaId

                }).ToList();
                foreach (areaAssignedModel var in result)
                {
                    var.areaName = getAreaName(var.areaId);
                }
                return result;
            }



        }

        // POST: userInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        /* public ActionResult Edit(user u)
         {



             using (var context = new userAuthenticationEntities())
             {
                 user details = new user();
                 var myresult = context.userInfo.Where(x => x.userAdmin == User.Identity.Name).Select(x => new user()
                 {

                     userId = x.userId,
                     userName = x.userName,

                 }).ToList();


                 foreach (user x in myresult)
                 {
                     x.detail = Getarea(x.userId);
                     x.m_Allowed = mesallowed(x.userId);
                     x.m_sent = messent(x.userId);

                 }
                 return View(myresult);

             }






             /*if (ModelState.IsValid)
             {

                 db.Entry(u).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }
             return View(u);
         }*/
        public ActionResult RemoveArea(string areaa,int id )
        {
            using (var context = new promoLacDbEntities())
            {
                int area_Id = getAreaId(areaa);
                var myresult = context.areaAssigneds.Where(x=>(x.areaId==area_Id)&&(x.vendorId==id)).First();
                    //.Where(o=>(o.areaId == id )&&(o.areaName==areaa)).SingleOrDefault();


                
                context.areaAssigneds.Remove(myresult);
                context.SaveChanges();
                return RedirectToAction("../ControlPanel/ControlPanel");


            }
        }
        public string getUserName(int id)
        {
            var context = new promoLacDbEntities();
            var result = context.vendors.Where(x => x.vendorId == id).FirstOrDefault();
            return result.vendorUserName;
        }
        // GET: userInfoes/Delete/5
       /* public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userInfo userInfo = db.userInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }*/

        // POST: userInfoes/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]*/
        /* public ActionResult DeleteConfirmed(int id)
         {
             userInfo userInfo = db.userInfo.Find(id);
             db.userInfo.Remove(userInfo);
             db.SaveChanges();
             return RedirectToAction("Index");
         }*/
       

        public ActionResult vendorFenceList(int id)
        {
            var f = getFenceList(id);
            return View(f);
        }
        public ActionResult vendorDealList(int id)
        {
            var f = getDealList(id);
            return View(f);
        }
        public ActionResult vendorNotificationList(int id)
        {
            var f = getNotificationList(id);
            return View(f);
        }
        public List<fence> getFenceList(int id)
        {
            var context = new promoLacDbEntities();
            
                var r = context.fences.Where(x => x.vendorId == id).ToList();
                return r;
           
               
           

        }
        public List<deal> getDealList(int id)
        { 
            var context = new promoLacDbEntities();
           
                var r = context.deals.Where(x => x.vendorId == id).ToList();
                return r;
          
           
            

        }
        public List<notification> getNotificationList(int id)
        {
            var context = new promoLacDbEntities();
           
                var r = context.notifications.Where(x => x.vendorId == id).ToList();
                return r;
           
           

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
