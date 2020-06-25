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

namespace fypPromolac.Controllers
{
    [Authorize]
    public class dealsController : Controller
    {
        // GET: deals
        public ActionResult createDeal()
        {
            var context = new promoLacDbEntities();
            var y = context.dealCategories.ToList();
            dealsModel x = new dealsModel();
            x.dealCategoryLists = y;

            
            
            return View(x);
        }
        [HttpPost]
        public ActionResult createDeal(dealsModel d, HttpPostedFileBase dealsImage)
        {
            string path = uploadimage(dealsImage);


            var context = new promoLacDbEntities();
            deal obj = new deal();



            obj.vendorId = getvendorId(User.Identity.Name);

            HttpClient httpClient = new HttpClient();
            string baseUrl = "https://localhost:44371/";
            string path1 = "C:\\Users\\DELL\\source\\repos\\fypPromolac\\fypPromolac\\Content\\images\\53757052inam-picture.jpeg";

            HttpResponseMessage response = httpClient.GetAsync(baseUrl + "api2/fileFirebase/GetFileUpload?id=" + path + "&vId=" + obj.vendorId + "&fileName=" + dealsImage.FileName).Result;

           
            
                string stateInfo = response.Content.ReadAsStringAsync().Result;

            stateInfo = stateInfo.Substring(1, stateInfo.Count()-2);

            obj.dealCategory = getDealCategoryId(d.dealCategory);
            obj.dealDescription = d.dealDescription;
            obj.dealName = d.dealName;
            obj.dealPrice = d.dealPrice;
            obj.latitude = d.latitude;
            obj.longitude = d.longitude;
            obj.dealImage = path;
            obj.dealTime = DateTime.Now.Date;
            obj.area = d.AreaName;
            
           
            context.deals.Add(obj);
            context.SaveChanges();
            tempDeal ddd = new tempDeal();
            
            
            ddd.dealimg = stateInfo;
            ddd.dealprice = d.dealPrice.ToString();
            ddd.latitude = d.latitude.ToString();
            ddd.longitute = d.longitude.ToString();
            ddd.rating = 2.ToString();
            ddd.dealtitle = d.dealName;
           IFirebaseConfig mes = new FirebaseConfig
            {
                AuthSecret = "CwnE0VGXMqxNxcY5YSWVNcL7IAM3hfqRuSiNjWm5",
                BasePath = "https://xdadeveloperes.firebaseio.com/"
            };
            IFirebaseClient mfc;
            mfc = new FireSharp.FirebaseClient(mes);
            var data = ddd;
            PushResponse r = mfc.Push(d.dealCategory + "/", data);


            
            return Json(new {success=true,message="dddd" },JsonRequestBehavior.AllowGet);
        }

        public int getDealCategoryId(string name)
        {
            var context = new promoLacDbEntities();
            var y = context.dealCategories.Where(x => x.categoryName == name).FirstOrDefault();
            return y.categoryId;
        }
        public int getvendorId(string name)
        {
            var context = new promoLacDbEntities();
            var y = context.vendors.Where(x => x.vendorUserName == name).FirstOrDefault();
            return y.vendorId;
        }
        public string uploadimage(HttpPostedFileBase dealsImage)
        {

            Random r = new Random();

            string path = "-1";

            int random = r.Next();

            if (dealsImage != null && dealsImage.ContentLength > 0)

            {
                string extension = Path.GetExtension(dealsImage.FileName);

                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))

                {

                    try

                    {
                        path = Path.Combine(Server.MapPath("~/Content/images"), random + Path.GetFileName(dealsImage.FileName));

                        dealsImage.SaveAs(path);

                        path = "C:/Users/DELL/source/repos/fypPromolac/fypPromolac/Content/images/" + random + Path.GetFileName(dealsImage.FileName);

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




    }
}