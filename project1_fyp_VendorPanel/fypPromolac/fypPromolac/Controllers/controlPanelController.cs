using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using fypPromolac.Models;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace fypPromolac.Controllers
{
    public class controlPanelController : Controller
    {
        // GET: controlPanel
        public ActionResult controlPanel()
        {
            
            using (var context = new promoLacDbEntities())
            {
                int id = vendorId(User.Identity.Name);
               userPackage d = new userPackage();

                var result = context.userPackages.Where(x => x.vendorId == id).Select(x => new userPackageModel()
                {


                    notificationSent = x.notificationSent,
                    packageId=x.packageId,
                    dividedShareOfMessages=x.dividedShareOfMessages,
                    messagesRemaining=x.dividedShareOfMessages-x.notificationSent,
                    remainingFencingHours=x.remainingFencingHours

                }).FirstOrDefault();
                /*d.messagesSent = result.messagesSent;
                d.messagesAllowed = messagesAllowed(result.packageId);
                d.messagesRemaining = d.messagesAllowed-d.messagesSent;
               
                    */
                var result1 = context.vendors.Where(x => x.vendorId == id).First();
                List<DataPoint> dataPointsNot = new List<DataPoint>();
                DateTime time = result1.registerTimestamp;
                
                int y = DateTime.Compare(time, DateTime.Now);
                int count = 0;
                while (y!=1)
                {
                    var dt = time.Date;
                    var z = context.notifications.Where(a => (a.vendorId == id) && (a.notificationTime == dt)).Count();
                    dataPointsNot.Add(new DataPoint(count,z));
                    time=time.AddDays(1);
                    y = DateTime.Compare(time.Date, DateTime.Now.Date);
                    count++;
                }
                
                ViewData["name"]= "Inam";
                ViewBag.DataPointsNotification = JsonConvert.SerializeObject(dataPointsNot);


                List<DataPoint> dataPointsDeal = new List<DataPoint>();
                time = result1.registerTimestamp;
                count = 0;
                y = DateTime.Compare(time, DateTime.Now);
                while (y != 1)
                {
                    var dt = time.Date;
                    var z = context.deals.Where(a => (a.vendorId == id) && (a.dealTime == dt)).Count();
                    dataPointsDeal.Add(new DataPoint(count, z));
                    time = time.AddDays(1);
                    y = DateTime.Compare(time.Date, DateTime.Now.Date);
                    count++;
                }
                ViewData["name"] = "Inam";
                ViewBag.DataPointsDeal = JsonConvert.SerializeObject(dataPointsDeal);
                List<DataPoint> dataPointsFence = new List<DataPoint>();
                time = result1.registerTimestamp;
                count = 0;
                y = DateTime.Compare(time, DateTime.Now);
                while (y != 1)
                {
                    var dt = time.Date;
                    var z = context.fences.Where(a => (a.vendorId == id) && (DbFunctions.TruncateTime(a.startTime)== dt)).Count();
                    dataPointsFence.Add(new DataPoint(count, z));
                    time = time.AddDays(1);
                    y = DateTime.Compare(time.Date, DateTime.Now.Date);
                    count++;
                }
                ViewData["name"] = "Inam";
                ViewBag.DataPointsFence = JsonConvert.SerializeObject(dataPointsFence);


                return View(result);
            }
         
        }
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
