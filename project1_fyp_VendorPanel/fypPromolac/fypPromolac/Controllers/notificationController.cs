using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using fypPromolac.Models;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Firebase.Notification;
using System.Net;
using System.Text;

namespace fypPromolac.Controllers
{
    public class notificationController : Controller
    {
      
/*IFirebaseConfig config = new FirebaseConfig
{
    AuthSecret = "XrO8HZD-SsUSld64BI62NgD7SabTXOMA_k6z_kq1ORM",
    BasePath = "https://xdadeveloperes.firebaseio.com/"

};*/

// GET: notification
public ActionResult notify()
        {
            

            var id = vendorId(User.Identity.Name);
          

            notificationModel tempmodel = new notificationModel();
            tempmodel.detail = Getarea(id);
            tempmodel.vendorId = id;
            // return View(detail);
            return View(tempmodel);

        }
      


        [HttpPost]
        public ActionResult notify(notificationModel n)
        {
            //string path = uploadimage(notimage);
            using (var context = new promoLacDbEntities())
            {
                if (ModelState.IsValid)
                {
                    {
                        foreach (var x in n.areaName)
                        {
                            notification not = new notification()
                            {

                                notificationTitle = n.notificationTitle,
                                notificationDescription = n.notificationDescription,
                                notificationAreaId = getAreaId(x),
                                notificationStatus = "s",
                                vendorId = n.vendorId,
                                notificationTime = DateTime.Now
                            };


                           


                            string towards = getHashCode(not.notificationAreaId);

                            context.notifications.Add(not);
                            updatemessagesent();

                            context.SaveChanges();


                            string SERVER_API_KEY = "AAAAtlCjlu4:APA91bGGwQ8HSMWLf7dVoD3c1XB3O7IBmhet0im9YCbFuxDF5pVui8IBZ8iCXcnUv2K32XVLnoAFwU17CdeYs3SGW6IX01dKeRSXAht8cS7WPg21fU_WIrkX8o-1FpWyQjlPxFj_mp8R";
                            var SENDER_ID = "783036946158";

                            WebRequest tRequest;
                            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                            tRequest.Method = "post";
                            tRequest.ContentType = "application/json";
                            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
                            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));


                            var data = new
                            {
                                notification = new
                                {
                                    body = not.notificationDescription,
                                    title = not.notificationTitle
                                }
                                  ,
                                to = "/topics/" + towards
                            };

                            var serializer = new JavaScriptSerializer();
                            var json = serializer.Serialize(data);
                            //logger.Info("json: " + json);
                            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                            tRequest.ContentLength = byteArray.Length;

                            Stream dataStream = tRequest.GetRequestStream();
                            dataStream.Write(byteArray, 0, byteArray.Length);
                            dataStream.Close();

                            WebResponse tResponse = tRequest.GetResponse();
                            dataStream = tResponse.GetResponseStream();
                            StreamReader tReader = new StreamReader(dataStream);
                            String sResponseFromServer = tReader.ReadToEnd();

                            tReader.Close();
                            dataStream.Close();
                            tResponse.Close();

                           /* notificationModel nt = new notificationModel();
                            nt.notificationAreaId = not.notificationAreaId.ToString();
                            nt.notificationDescription = n.notificationDescription;
                            nt.notificationId = not.notificationId.ToString();
                            nt.notificationStatus = "s";
                            nt.notificationTitle = n.notificationTitle;



                            IFirebaseConfig mes = new FirebaseConfig
                            {
                                AuthSecret = "CwnE0VGXMqxNxcY5YSWVNcL7IAM3hfqRuSiNjWm5",
                                BasePath = "https://xdadeveloperes.firebaseio.com/"
                            };

                            IFirebaseClient mfc;
                            mfc = new FireSharp.FirebaseClient(mes);

                            var set = mfc.Set(@"Messages/" + not.notificationId, nt);
                            */

                        }
                    }
                   
                }
            }

            return Json(new { success = true, message = "dddd" }, JsonRequestBehavior.AllowGet);
        }
        public void updatemessagesent()
        {
            using (var context= new promoLacDbEntities())
            {
                int id = vendorId(User.Identity.Name);


                var message = context.userPackages.FirstOrDefault(x => x.vendorId == id);

                if(message != null)
                {
                    message.notificationSent++;
                }
                context.SaveChanges();
               
            }
           
        }
        public string uploadimage(HttpPostedFileBase notimage)
        {
                Random r = new Random();

                string path = "-1";

                int random = r.Next();

                if (notimage != null && notimage.ContentLength > 0)

                {
                    string extension = Path.GetExtension(notimage.FileName);

                    if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))

                    {

                        try

                        {
                            path = Path.Combine(Server.MapPath("~/Content/images"), random + Path.GetFileName(notimage.FileName));

                            notimage.SaveAs(path);

                            path = "~/Content/upload/" + random + Path.GetFileName(notimage.FileName);

                        }

                        catch (Exception ex)

                        {
                            path = "-1";

                        }

                    }

                    else

                    {

                        Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");

                    }

                }
                else
                {
                    Response.Write("<script>alert('Please select a file'); </script>");

                    path = "-1";
                }

                return path;
            }

        /* public List<area> Getarea(int id)
         {
             using (var context = new userAuthenticationEntities())
             {
                 var result = context.areas.Where(x => x.areaId == id).Select(x => new area()
                 {
                     areaName = x.areaName

                 }).ToList();

                 return result;
             }



         }*/



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
        public string getHashCode(int id)
        {
            using (var context = new promoLacDbEntities())
            {
                var result = context.areas.Where(x => x.areaId == id).Select(x => new areaModel()
                {
                    areaHashCode=x.areaHashCode

                }).FirstOrDefault();

                return result.areaHashCode;
            }
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


    }



}