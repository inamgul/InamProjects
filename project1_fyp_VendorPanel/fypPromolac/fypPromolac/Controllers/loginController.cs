using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fypPromolac.Models;
using System.Web.Security;
namespace fypPromolac.Controllers
{
    
    public class loginController : Controller
    {
        
        // GET: login
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(vendorModel model)
        {
            using(var context= new promoLacDbEntities())
            {
                bool isvalidVendor = context.vendors.Any(x => x.vendorUserName == model.vendorUserName && x.vendorPassword == model.vendorPassword);
                if (isvalidVendor)
                {
                    FormsAuthentication.SetAuthCookie(model.vendorUserName, false);
                    int id = vendorId(model.vendorUserName);
                    var result = context.userPackages.Where(x => x.vendorId == id).FirstOrDefault();
                    int comp = DateTime.Compare(result.packageEndTime, DateTime.Now);
                    if (comp<0)
                    {
                        result.packageStatus = "NotActive";
                        context.SaveChanges();
                        ModelState.AddModelError("", "Package Expired");
                    }
                    else
                    {
                        return RedirectToAction("controlPanel", "controlPanel");
                    }


                    
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User Name and Password");
                }
                
                    
                
            }
            return View();
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

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }
       
    }
}