using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataEntites.FileTransfer;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace TextTransfer.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult files()
        {
            using (var db = new FileTransferEntitiess())
            {
                TempData["FileCount"] = db.FileDb.Where(ss => ss.ManagerId == UserContext.Current.User.Id).ToList().Count.ToString();
                var result = db.FileDb.Where(ss => ss.ManagerId == UserContext.Current.User.Id && ss.TextType !=null).OrderByDescending(ss => ss.CreatedDate).ToList();
                return View(result);
            }
        }


        [HttpPost]
        public ActionResult create(FileDb dosya, HttpPostedFileBase FileType)
        {


            try
            {
                using (var db = new FileTransferEntitiess())
                {
                        dosya.CreatedDate = DateTime.Now;
                        db.FileDb.Add(dosya);
                        db.SaveChanges();
                        TempData["m"] = "1";
                }
                return Redirect("/dosyagonder");
            }
            catch (Exception e)
            {
                TempData["m"] = "0";
                throw e;
            }
        }

        public ActionResult FileDelete(int Id)
        {
            try
            {
                using (var db = new FileTransferEntitiess())
                {
                    var deletedFile = db.FileDb.FirstOrDefault(ss => ss.Id == Id);
                    db.FileDb.Remove(deletedFile);
                    db.SaveChanges();
                    if (System.IO.File.Exists(Server.MapPath(deletedFile.FileType)))
                    {
                        System.IO.File.Delete(Server.MapPath(deletedFile.FileType));
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return Redirect("/dosyagonder");

        }

    }
}