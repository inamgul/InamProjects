using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fypPromolac.Models;

namespace fypPromolac.Controllers
{
    public class memberAddController : Controller
    {
      /*  // GET: memberAdd
        [Authorize(Roles ="admin")]
        public ActionResult Memberadd()
        {
                return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Memberadd(subUserModel member)
        {
            using (var context = new promoLacDbEntities())
            {
                subUser m = new subUser()
                {
                    subUserFirstName=member.subUserFirstName,
                    subUserLastName=member.subUserLastName,
                    subUserEmail=member.subUserEmail,
                    subUserPassword=member.subUserPassword,
                    subUserPhoneNumber=member.subUserPhoneNumber,
                    subUserRegisterTimestamp
                };
                context.members.Add(m);
                context.SaveChanges();

            }
            ViewBag.Message = "new non admin user created ";
            return RedirectToAction("../ControlPanel/ControlPanel");
        }*/
    }
}