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
using FireSharp.Config;
using fypPromolac.Models;
using System.IO;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using System.Diagnostics;
using System.Net.Http;
using Vereyon.Web;

namespace fypPromolac.Controllers
{
    public class geoFenceController : Controller
    {
        // GET: geoFence
        public ActionResult createFence()
        {
            int id = vendorId(User.Identity.Name);

            fenceModel m = new fenceModel();
            m.vendorId = id;

            var context = new promoLacDbEntities();

            var r = context.userPackages.Where(x => x.vendorId == id).FirstOrDefault();

            m.allowedFenceHours = Convert.ToInt32(r.remainingFencingHours);
            return View(m);
        }
        [HttpPost]
        public ActionResult createFence(fenceModel f)
        {

            tempFence tf = new tempFence();
            tf.Latitude = f.latitude.ToString();
            tf.Longitude = f.longitude.ToString();
            tf.Notification_ = f.notification;
            tf.TimeStamp = DateTime.Now.AddHours(f.hr).ToString("MM/dd/yyyy HH:mm:ss");

            tf.Radius = f.radius.ToString();
            IFirebaseConfig mes = new FirebaseConfig
            {
                AuthSecret = "CwnE0VGXMqxNxcY5YSWVNcL7IAM3hfqRuSiNjWm5",
                BasePath = "https://xdadeveloperes.firebaseio.com/"
            };

            IFirebaseClient mfc;
            mfc = new FireSharp.FirebaseClient(mes);
             var data = tf;
            PushResponse r = mfc.Push("Fences_" + "/", data);

            updateFencingHours(Convert.ToInt32(f.vendorId),f.hr);

            var context = new promoLacDbEntities();
            fence fence_ = new fence();
            fence_.latitude = f.latitude;
            fence_.longitude = f.longitude;
            fence_.startTime = DateTime.Now;
            fence_.endTime = DateTime.Now.AddHours(f.hr);
            fence_.notification = f.notification;
            fence_.radius = f.radius;
            fence_.vendorId = f.vendorId;
            fence_.area = f.AreaName;

            context.fences.Add(fence_);
            context.SaveChanges();

            return Json(new { success = true, message = "dddd" }, JsonRequestBehavior.AllowGet);

        }
        public void updateFencingHours(int id,int hr)
        {
            
            var context = new promoLacDbEntities();
            var result = context.userPackages.Where(x => x.vendorId ==id).FirstOrDefault();
            result.remainingFencingHours = result.remainingFencingHours - hr;
            result.fenceCreated = result.fenceCreated + 1;
            context.SaveChanges();
        }
        public int getVendorStatus(int id)
        {
            var context = new promoLacDbEntities();
            var result = context.vendors.Where(x => x.vendorId == id).FirstOrDefault();
            if (result.isHead == "Y")
            {
                return result.vendorId;
            }
            else
            {
                id = Convert.ToInt32(result.headId);
                return id;
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
    }
}