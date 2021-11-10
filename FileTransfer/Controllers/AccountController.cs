using DataEntites.FileTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FileTransfer.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        public void createCurrentUserContext(Manager user)
        {
            HttpContext.Session["UserSessionText"] = new UserContext(user);
        }
        [HttpPost]
        public ActionResult Login(Manager userLogin)
        {
            try
            {
                using (var db = new FileTransferEntitiess())
                {
                    var userdetail = db.Manager.Where(ss => ss.UserName == userLogin.UserName && ss.Password == userLogin.Password).FirstOrDefault();
                    if (userdetail==null)
                    {
                        
                        TempData["login"] = "0";
                        return Redirect("/giris");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie("KullaniciId", false);
                        Session["KullaniciId"] = userdetail.Id;
                        createCurrentUserContext(userdetail);
                        return Redirect("/dosyalistesi");
                    }
                    
                }
            }
            catch (Exception e)
            {
                TempData["login"] = "0";
                return Redirect("/giris");
            }
        }




        public ActionResult SignOut()
        {
                HttpContext.Session["UserSessionText"] = null;
                FormsAuthentication.SignOut();
                Session.Abandon(); // sessionları oldurur.
                return Redirect("/giris");
        }
    }
}