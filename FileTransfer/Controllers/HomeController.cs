using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataEntites.FileTransfer;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace FileTransfer.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index(int? _sayfaNo)
        {
            using (var db = new FileTransferEntitiess())
            {
                if (UserContext.Current != null)
                {
                    TempData["FileCount"] = db.FileDb.Where(ss => ss.ManagerId == UserContext.Current.User.Id).ToList().Count.ToString();
                    int _sayfa = _sayfaNo ?? 1;
                    var result = db.FileDb.Where(ss => ss.ManagerId == UserContext.Current.User.Id).OrderBy(ss => ss.CreatedDate).ToPagedList<FileDb>(_sayfa, 10);
                    return View(result);
                }
                else
                {
                    return Redirect("/giris");
                }
            }
        }
        public ActionResult files()
        {
            using (var db = new FileTransferEntitiess())
            {
                var result = db.FileDb.Where(ss => ss.ManagerId == UserContext.Current.User.Id).OrderByDescending(ss => ss.CreatedDate).ToList();
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
                    if (FileType != null || dosya.TextType != null)
                    {

                        if (FileType != null)
                        {
                            if (FileType.ContentLength > 0)
                            {
                                var file = Request.Files[0];
                                if (FileType != null && FileType.ContentLength > 0)
                                {
                                    var fi = new FileInfo(FileType.FileName);
                                    var fileName = Path.GetFileName(FileType.FileName);
                                    fileName = fileName + Guid.NewGuid().ToString() + fi.Extension;
                                    var path = "/Files/" + fileName;
                                    file.SaveAs(Server.MapPath(path));
                                    dosya.FileType = path;
                                }

                            }
                        }
                        dosya.CreatedDate = DateTime.Now;
                        db.FileDb.Add(dosya);
                        db.SaveChanges();
                        TempData["m"] = "1";
                    }
                    else
                    {
                        TempData["m"] = "none";
                    }
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
            return Redirect("/");

        }

    }
}