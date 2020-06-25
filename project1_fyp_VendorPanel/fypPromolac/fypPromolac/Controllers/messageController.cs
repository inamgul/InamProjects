using FireSharp.Config;
using FireSharp.Interfaces;
using fypPromolac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fypPromolac.Controllers
{
    public class messageController : Controller
    {
        // GET: message
        public ActionResult createMessage()
        {
            var id = vendorId(User.Identity.Name);


            messageModel tempmodel = new messageModel();
            tempmodel.detail = Getarea(id);
            tempmodel.vendorID = id;
            // return View(detail);
            return View(tempmodel);

        }



        [HttpPost]
        public ActionResult createMessage(messageModel m)
        {
            var context = new promoLacDbEntities();
            messages_ obj = new messages_();

            foreach (var x in m.areaName)
            {


                obj.vendorID = m.vendorID;
                obj.messageDescription = m.messageDescription;
                obj.messageTitle = m.messageTitle;
                obj.areaId = getAreaId(x);
                obj.messageStatus = "s";

                context.messages_.Add(obj);
                context.SaveChanges();

                tempMessage objM = new tempMessage();


                objM.notificationStatus = "s";
                objM.notificationAreaId = obj.areaId.ToString();
                objM.notificationDescription = obj.messageDescription;
                objM.notificationTitle = obj.messageTitle;
                objM.notificationTime = DateTime.Now.ToString();
                objM.notificationId = obj.messageId.ToString();
                IFirebaseConfig mes = new FirebaseConfig
                {
                    AuthSecret = "CwnE0VGXMqxNxcY5YSWVNcL7IAM3hfqRuSiNjWm5",
                    BasePath = "https://xdadeveloperes.firebaseio.com/"
                };

                IFirebaseClient mfc;
                mfc = new FireSharp.FirebaseClient(mes);

                var set = mfc.Set(@"Messages/" + obj.messageId, objM);
            }

            return Json(new { success = true, message = "dddd" }, JsonRequestBehavior.AllowGet);

        }

        public int getAreaId(string name)
        {
            var context = new promoLacDbEntities();
            var x = context.areas.Where(z => z.areaName == name).FirstOrDefault();
            return x.areaId;

        }
        public int vendorId(string name)
        {

            using (var context = new promoLacDbEntities())
            {
                var result = context.vendors.Where(x => x.vendorUserName == name).Select(x => new vendorModel()
                {
                    vendorId = x.vendorId

                }).FirstOrDefault();

                return result.vendorId;
            }

        }
        public List<areaAssignedModel> Getarea(int id)

        {
            using (var context = new promoLacDbEntities())
            {
                var result = context.areaAssigneds.Where(x => x.vendorId == id).Select(x => new areaAssignedModel()
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

        
    }
}