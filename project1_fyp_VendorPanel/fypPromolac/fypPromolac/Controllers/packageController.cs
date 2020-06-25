using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fypPromolac.Models;

namespace fypPromolac.Controllers
{
    public class packageController : Controller
    {
        // GET: package
        public ActionResult packageDetails()
        {
            using (var context = new promoLacDbEntities())
            {
                int id = vendorId(User.Identity.Name);
                packageModel details = new packageModel();
                var subresult = context.userPackages.Where(x => x.vendorId ==id ).Select(y => new packageModel()
                {
                   packagesId=y.packageId


                }).FirstOrDefault();
                //details.detail = Getarea(1);
                var result= context.packages.Where(x => x.packagesId == subresult.packagesId).Select(x => new packageModel()
                {
                    packageName=x.packageName,
                    packageDurationDays=x.packageDurationDays,
                    subUsersAllowed=x.subUsersAllowed,
                    messagesAllowed=x.messagesAllowed,
                    packageDescription=x.packageDescription,
                    fencingHours=x.fencingHours,
                    
                }).FirstOrDefault();

                var r = context.userPackages.Where(x => x.vendorId == id).FirstOrDefault();

                result.endtime = r.packageEndTime;
               
                return View(result);
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