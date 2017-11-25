﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyDoIT.Models.NLP;
using StudyDoIT.Models.Common;
using System.IO;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Data;
using System.Data.OleDb;
using System.Xml;

namespace StudyDoIT.Areas.AdminIT.Controllers
{
    public class LoadCommentController : Controller
    {

        lCMSData db = new lCMSData();

        public ActionResult Index()
        {
            //LoadCommentController obj = new LoadCommentController();
            //Load comment 
            //string pathRead = "D:/Giao trinh CH/Luan van/ReviewTest";
            //obj.ReadFolder(@pathRead, "", 2);

            ////Tách câu
            //obj.SplitComment();

            //Xóa các câu ngắn có rating 5
            //obj.DelSentenseShort(5);

            //
            var data = db.Comments.ToList();
            return View(data);
        }

        public ActionResult List(string id = "")
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);

            IEnumerable<GroupComent> data = (IEnumerable<GroupComent>)db.GroupComents.ToList();
            if (id == "")
            {
                ViewBag.GroupComment = new SelectList(data, "Id", "Name");
            }
            else
            {
                ViewBag.GroupComment = new SelectList(data, "Id", "Name", id);
            }

            var data2 = db.Comments.Where(e => e.GroupCommentId == id).ToList();
            ViewBag.Id = id;
            return View(data2);
        }

        private void ReadFile(string path, string idfolder, int kk)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = "";
                    int k = 1;
                    int j = 0;
                    int s = 5;
                    string img = "";

                    Comment cm = new Comment();
                    string idc = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            if (k == (s + j))
                            {
                                cm.Name = line.Split('>')[1];
                                if (cm.Name == "")
                                {
                                    s = 8;
                                }
                            }
                            else if (k == (s + j + 1))
                            {
                                cm.Comment1 = line.Split('>')[1].Trim();
                            }
                            else if (k == (s + j + 2))
                            {
                                cm.Date = line.Split('>')[1].Trim();
                            }
                            else if (k == (s + j + 3))
                            {
                                img = line.Split('>')[1].Trim();
                            }
                            else if ((img == "" && k == (s + j + 6)) || (img != "" && k == (s + j + 5)))
                            {
                                cm.Rating = decimal.Parse(line.Split('>')[1]);

                                idc = Public.GetID();
                                while (db.Comments.Where(e => e.Id == idc).Count() > 0)
                                {
                                    idc = Public.GetID();
                                }
                                cm.Id = idc;
                                cm.GroupCommentId = idfolder;
                                db.Comments.Add(cm);
                                db.SaveChanges();
                            }
                            else if ((img == "" && k == (s + j + 7)) || (img != "" && k == (s + j + 6)))
                            {
                                //cm.Value = int.Parse(line.Split('>')[1]);
                                GroupWordComment gwc = new GroupWordComment();
                                string idgwc = Public.GetID();
                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                {
                                    idgwc = Public.GetID();
                                }
                                gwc.Id = idgwc;
                                gwc.CommentId = idc;
                                gwc.GroupWordId = "160421084754919";
                                gwc.Score = int.Parse(line.Split('>')[1]);
                                db.GroupWordComments.Add(gwc);
                                db.SaveChanges();

                            }
                            else if ((img == "" && k == (s + j + 8)) || (img != "" && k == (s + j + 7)))
                            {
                                //cm.Rooms = int.Parse(line.Split('>')[1]);
                                GroupWordComment gwc = new GroupWordComment();
                                string idgwc = Public.GetID();
                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                {
                                    idgwc = Public.GetID();
                                }
                                gwc.Id = idgwc;
                                gwc.CommentId = idc;
                                gwc.GroupWordId = "160421084754914";
                                gwc.Score = int.Parse(line.Split('>')[1]);
                                db.GroupWordComments.Add(gwc);
                                db.SaveChanges();
                            }
                            else if ((img == "" && k == (s + j + 9)) || (img != "" && k == (s + j + 8)))
                            {
                                //cm.Location = int.Parse(line.Split('>')[1]);
                                GroupWordComment gwc = new GroupWordComment();
                                string idgwc = Public.GetID();
                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                {
                                    idgwc = Public.GetID();
                                }
                                gwc.Id = idgwc;
                                gwc.CommentId = idc;
                                gwc.GroupWordId = "160421084754915";
                                gwc.Score = int.Parse(line.Split('>')[1]);
                                db.GroupWordComments.Add(gwc);
                                db.SaveChanges();
                            }
                            else if ((img == "" && k == (s + j + 10)) || (img != "" && k == (s + j + 9)))
                            {
                                //cm.Cleanliness = int.Parse(line.Split('>')[1]);
                                GroupWordComment gwc = new GroupWordComment();
                                string idgwc = Public.GetID();
                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                {
                                    idgwc = Public.GetID();
                                }
                                gwc.Id = idgwc;
                                gwc.CommentId = idc;
                                gwc.GroupWordId = "160421084754916";
                                gwc.Score = int.Parse(line.Split('>')[1]);
                                db.GroupWordComments.Add(gwc);
                                db.SaveChanges();
                            }
                            else if ((img == "" && k == (s + j + 11)) || (img != "" && k == (s + j + 10)))
                            {
                                //cm.CheckinFrontDesk = int.Parse(line.Split('>')[1]);
                                GroupWordComment gwc = new GroupWordComment();
                                string idgwc = Public.GetID();
                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                {
                                    idgwc = Public.GetID();
                                }
                                gwc.Id = idgwc;
                                gwc.CommentId = idc;
                                gwc.GroupWordId = "160421084754917";
                                gwc.Score = int.Parse(line.Split('>')[1]);
                                db.GroupWordComments.Add(gwc);
                                db.SaveChanges();
                            }
                            else if ((img == "" && k == (s + j + 12)) || (img != "" && k == (s + j + 11)))
                            {
                                //cm.Service = int.Parse(line.Split('>')[1]);
                                GroupWordComment gwc = new GroupWordComment();
                                string idgwc = Public.GetID();
                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                {
                                    idgwc = Public.GetID();
                                }
                                gwc.Id = idgwc;
                                gwc.CommentId = idc;
                                gwc.GroupWordId = "160421084754918";
                                gwc.Score = int.Parse(line.Split('>')[1]);
                                db.GroupWordComments.Add(gwc);
                                db.SaveChanges();
                            }
                            else if ((img == "" && k == (s + j + 13)) || (img != "" && k == (s + j + 12)))
                            {
                                //cm.BusinessService = int.Parse(line.Split('>')[1]);
                                GroupWordComment gwc = new GroupWordComment();
                                string idgwc = Public.GetID();
                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                {
                                    idgwc = Public.GetID();
                                }
                                gwc.Id = idgwc;
                                gwc.CommentId = idc;
                                gwc.GroupWordId = "160421084754920";
                                gwc.Score = int.Parse(line.Split('>')[1]);
                                db.GroupWordComments.Add(gwc);
                                db.SaveChanges();

                                j = k;
                                s = 2;

                                idc = "";
                                cm = new Comment();
                            }
                            k++;
                        }
                        catch
                        {
                            var gc = db.GroupComents.Find(idfolder);
                            gc.Note = "error";
                            db.Entry(gc).State = EntityState.Modified;
                            db.SaveChanges();
                            break;
                        }
                    }
                }
            }
            catch
            {
                var gc = db.GroupComents.Find(idfolder);
                gc.Note = "error";
                db.Entry(gc).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void ReadFolder(string path, string idfolder, int k)
        {
            try
            {
                DirectoryInfo drInfo = new DirectoryInfo(path);
                FileInfo[] files = drInfo.GetFiles();
                //doc ten cac file
                foreach (FileInfo f in files)
                {
                    ReadFile(f.FullName, idfolder, k);
                }
                //lay cac folder con
                DirectoryInfo[] folders = drInfo.GetDirectories();
                foreach (DirectoryInfo fol in folders)
                {
                    GroupComent gc = new GroupComent();
                    string name = fol.FullName.Split('\\')[4].Trim();
                    var gcc = db.GroupComents.Where(e => e.Name.Equals(name)).ToList();
                    if (gcc.Count <= 0)
                    {
                        string idgc = Public.GetID();
                        while (db.GroupComents.Where(e => e.Id == idgc).Count() > 0)
                        {
                            idgc = Public.GetID();
                        }
                        gc.Id = idgc;
                        gc.Name = fol.FullName.Split('\\')[4].Trim();
                        db.GroupComents.Add(gc);
                        db.SaveChanges();
                        ReadFolder(fol.FullName, idgc, 1);
                    }
                    else
                    {
                        ReadFolder(fol.FullName, gcc.FirstOrDefault().Id, 2);
                    }
                }
            }
            catch { }
        }

        private void SplitComment(string idgc)
        {
            var sen2 = db.Sentenses.Where(e => e.Comment.GroupCommentId == idgc).ToList();
            if (sen2.Count <= 0)
            {
                var cm = db.Comments.Where(e => e.GroupCommentId == idgc).ToList();
                try
                {
                    foreach (var item in cm)
                    {
                        try
                        {
                            var sen = db.Sentenses.Where(e => e.CommentId == item.Id).ToList();
                            if (sen.Count <= 0)
                            {
                                string[] str = item.Comment1.Split('.', '!', '?');

                                foreach (var s in str)
                                {
                                    try
                                    {
                                        if (s.Trim() != "" && s.Length > 2)
                                        {
                                            string ids = Public.GetID();
                                            while (db.Sentenses.Where(e => e.Id == ids).Count() > 0)
                                            {
                                                ids = Public.GetID();
                                            }
                                            Sentens se = new Sentens();
                                            se.Id = ids;
                                            se.ContentReview = s;
                                            se.CommentId = item.Id;
                                            db.Sentenses.Add(se);
                                            db.SaveChanges();
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }

        //private void DelSentenseShort(decimal rating)
        //{
        //    var sen = db.Sentenses.Where(e=>e.Comment.Rating==rating).ToList();
        //    foreach(var itemS in sen){
        //        if (itemS.ContentReview.Count() < 30)
        //        {
        //            var sen2 = db.Sentenses.Find(itemS.Id);
        //            db.Sentenses.Remove(sen2);
        //            db.SaveChanges();
        //        }
        //    }
        //}

        public ActionResult LoadListComment(string idgc)
        {
            var data = db.Comments.Where(e => e.GroupCommentId == idgc).ToList();
            return PartialView("_ListComment", data);
        }

        public ActionResult Create(string id = "")
        {
            Session["current_url"] = Request.Url.AbsoluteUri;
            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);

            IEnumerable<Product> data = (IEnumerable<Product>)db.Products.ToList();
            if (id == "")
            {
                ViewBag.Product = new SelectList(data, "Id", "Name");
            }
            else
            {
                ViewBag.Product = new SelectList(data, "Id", "Name", id);
            }

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(FormCollection collection, Comment model)
        {
            //try
            //{
            GroupComent gc = new GroupComent();
            string idgc = "";
            string idp = "";
            idp = collection["ProductId"];
            if (collection["typePage"] == "2")
            {
                //Thêm sản phẩm
                string name = collection["NameGC"];
                idgc = Public.GetID();
                while (db.GroupComents.Where(e => e.Id == idgc).Count() > 0)
                {
                    idgc = Public.GetID();
                }
                gc.Id = idgc;
                gc.Name = name;
                db.GroupComents.Add(gc);
                db.SaveChanges();

                //Thêm bình luận sản phẩm
                string idcm = Public.GetID();
                while (db.Comments.Where(e => e.Id == idcm).Count() > 0)
                {
                    idcm = Public.GetID();
                }
                model.Id = idcm;
                model.Name = collection["Name"];
                model.Comment1 = collection["Comment1"];
                model.Rating = int.Parse(collection["Comment1"]);
                //model.Rooms = int.Parse(collection["Comment1"]);
                //model.Location = int.Parse(collection["Comment1"]);
                //model.Cleanliness = int.Parse(collection["Comment1"]);
                //model.CheckinFrontDesk = int.Parse(collection["Comment1"]);
                //model.Service = int.Parse(collection["Comment1"]);
                //model.Value = int.Parse(collection["Comment1"]);
                //model.BusinessService = int.Parse(collection["Comment1"]);
                model.GroupCommentId = idgc;
                //model.TilteComment = "";
                model.Date = DateTime.Now.ToShortDateString();

                db.Comments.Add(model);
                db.SaveChanges();
                TempData["success"] = "Thêm thành công.";
            }
            else
            {
                string fileName2 = "";
                string path = "";
                var files2 = Request.Files["fileProduct"];

                if (files2 != null && files2.ContentLength > 0)
                {
                    try
                    {
                        if (Request.Browser.Browser == "IE")
                            fileName2 = Path.GetFileName(files2.FileName);
                        else
                            fileName2 = files2.FileName;

                        path = Path.Combine(Server.MapPath("~/Uploads/files"), fileName2);
                        files2.SaveAs(path);
                    }
                    catch (Exception ex)
                    {
                        TempData["error"] = "Thêm file đính kèm lỗi.";
                        return RedirectToAction("Create");
                    }

                    //if (idgc == "")
                    //{
                    var gcc = db.GroupComents.Find(idgc);
                    string name = fileName2.Split('.')[0].ToString();

                    if (gcc == null)
                    {
                        idgc = Public.GetID();
                        while (db.GroupComents.Where(e => e.Id == idgc).Count() > 0)
                        {
                            idgc = Public.GetID();
                        }
                        gc.Id = idgc;
                        gc.Name = name;
                        gc.ProductId = idp;
                        db.GroupComents.Add(gc);
                        db.SaveChanges();
                        ReadFile(path, idgc, 1);
                    }
                    else
                    {
                        ReadFile(path, gcc.Id, 2);
                    }
                    //}                       

                    TempData["success"] = "Thêm thành công.";
                }
                else
                {
                    TempData["info"] = "Chưa chọn file đính kèm.";
                    return RedirectToAction("Create");
                }
            }

            //Tách câu
            //SplitComment(idgc);

            return RedirectToAction("List", new { id = idgc });
            //}
            //catch
            //{
            //    TempData["error"] = "Thêm lỗi.";
            //    return RedirectToAction("List");
            //}
        }
        public ActionResult AutoCreat()
        {
            string idp = "";
            idp = "170319111826335";

            //Load comment 
            string pathRead = "D:\\hoctap\\DoAnTotNghiep\\soucecode\\filetestv1";
            //string pathRead="E:/MsTu/Data/DataHotel";
            DirectoryInfo drInfo = new DirectoryInfo(pathRead);
            FileInfo[] files = drInfo.GetFiles();
            //doc ten cac file
            foreach (FileInfo f in files)
            {
                GroupComent gc = new GroupComent();
                string name = f.FullName.Split('\\')[5].ToString().Split('.')[0].ToString();
                var gcc = db.GroupComents.Where(e => e.Name.Trim().Equals(name.Trim())).ToList();
                if (gcc.Count <= 0)
                {
                    string idgc = Public.GetID();
                    while (db.GroupComents.Where(e => e.Id == idgc).Count() > 0)
                    {
                        idgc = Public.GetID();
                    }
                    gc.Id = idgc;
                    gc.Name = name;
                    gc.ProductId = idp;
                    db.GroupComents.Add(gc);
                    db.SaveChanges();
                    ReadFile(f.FullName, idgc, 1);
                    //SplitComment(idgc);
                }
                
            }
            return PartialView("AutoCreat");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateJson(FormCollection collection, Comment model)
        {
            //try
            //{
            //string idgc = "";
            string idp = "";
            idp = collection["ProductId"];

            //Load comment 
            string pathRead = "D:/Giao trinh CH/Luan van/DataHotel/ReviewTestv2";
            //string pathRead="E:/MsTu/Data/DataHotel";
            DirectoryInfo drInfo = new DirectoryInfo(pathRead);
            FileInfo[] files = drInfo.GetFiles();
            //doc ten cac file
            foreach (FileInfo f in files)
            {
                GroupComent gc = new GroupComent();
                string name = f.FullName.Split('\\')[5].ToString().Split('.')[0].ToString();
                var gcc = db.GroupComents.Where(e => e.Name.Trim().Equals(name.Trim())).ToList();
                if (gcc.Count <= 0)
                {
                    string idgc = Public.GetID();
                    while (db.GroupComents.Where(e => e.Id == idgc).Count() > 0)
                    {
                        idgc = Public.GetID();
                    }
                    gc.Id = idgc;
                    gc.Name = name;
                    gc.ProductId = idp;
                    db.GroupComents.Add(gc);
                    db.SaveChanges();
                    ReadFile(f.FullName, idgc, 1);
                    //SplitComment(idgc);
                }
            }

            return RedirectToAction("List", new { id = "" });
            //}
            //catch
            //{
            //    TempData["error"] = "Thêm loi.";
            //    return RedirectToAction("List");
            //}
        }

        //public ActionResult CreateJson(FormCollection collection, Comment model)
        //{
        //    try
        //    {
        //        //string idgc = "";
        //        string idp = "";
        //        idp = collection["ProductId"];
        //        var kc = db.GroupWords.Where(e => e.ProductId == idp).ToList();
        //        //Load comment 
        //        //string pathRead = "D:/Giao trinh CH/Luan van/DataBeer";
        //        string pathRead = "E:/MsTu/Data/DataBeer";
        //        DirectoryInfo drInfo = new DirectoryInfo(pathRead);
        //        FileInfo[] files = drInfo.GetFiles();
        //        //doc ten cac file
        //        foreach (FileInfo f in files)
        //        {
        //            using (StreamReader r = new StreamReader(f.FullName))
        //            {
        //                string json = r.ReadToEnd();
        //                List<BeerJson> items = JsonConvert.DeserializeObject<List<BeerJson>>(json);

        //                foreach (var item in items)
        //                {
        //                    try
        //                    {
        //                        GroupComent gc = new GroupComent();
        //                        string name = item.BeerName.Trim().ToLower();                              
        //                        Comment cm = new Comment();
        //                        var gcc = db.GroupComents.Where(e => e.Name.Trim().ToLower().Equals(name)).ToList();
        //                        if (gcc.Count <= 0)
        //                        {
        //                            string idgc = Public.GetID();
        //                            while (db.GroupComents.Where(e => e.Id == idgc).Count() > 0)
        //                            {
        //                                idgc = Public.GetID();
        //                            }
        //                            gc.Id = idgc;
        //                            gc.Name = item.BeerName;
        //                            gc.Note = item.BeerStyle;
        //                            gc.ProductId = idp;
        //                            db.GroupComents.Add(gc);
        //                            db.SaveChanges();

        //                            string idc = Public.GetID();
        //                            while (db.Comments.Where(e => e.Id == idc).Count() > 0)
        //                            {
        //                                idc = Public.GetID();
        //                            }
        //                            cm.Id = idc;
        //                            cm.Comment1 = item.ReviewText;
        //                            cm.Rating = decimal.Parse(item.ReviewOverall);
        //                            cm.Name = item.UserProfileName;
        //                            cm.Date = item.ReviewTimeUnix;
        //                            cm.GroupCommentId = idgc;
        //                            db.Comments.Add(cm);
        //                            db.SaveChanges();

        //                            int kk=0;
        //                            foreach (var itemKC in kc)
        //                            {
        //                                GroupWordComment gwc = new GroupWordComment();
        //                                string idgwc = Public.GetID();
        //                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
        //                                {
        //                                    idgwc = Public.GetID();
        //                                }

        //                                gwc.Id = idgwc;
        //                                gwc.GroupWordId = itemKC.Id;
        //                                gwc.CommentId = idc;
        //                                if (kk==0)
        //                                {
        //                                    gwc.Score = double.Parse(item.ReviewAppearance);
        //                                }
        //                                else if (kk == 1)
        //                                {
        //                                    gwc.Score = double.Parse(item.ReviewPalate);
        //                                }
        //                                else if (kk == 2)
        //                                {
        //                                    gwc.Score = double.Parse(item.ReviewTaste);
        //                                }
        //                                else if (kk == 3)
        //                                {
        //                                    gwc.Score = double.Parse(item.ReviewAroma);
        //                                }

        //                                db.GroupWordComments.Add(gwc);
        //                                db.SaveChanges();
        //                                kk++;
        //                            }

        //                        }
        //                        else
        //                        {
        //                            string idc = Public.GetID();
        //                            while (db.Comments.Where(e => e.Id == idc).Count() > 0)
        //                            {
        //                                idc = Public.GetID();
        //                            }
        //                            cm.Id = idc;
        //                            cm.Comment1 = item.ReviewText;
        //                            cm.Rating = decimal.Parse(item.ReviewOverall);
        //                            cm.Name = item.BeerName;
        //                            cm.Date = item.ReviewTimeUnix;
        //                            cm.GroupCommentId = gcc.FirstOrDefault().Id;
        //                            db.Comments.Add(cm);
        //                            db.SaveChanges();

        //                            int kk = 0;
        //                            foreach (var itemKC in kc)
        //                            {
        //                                GroupWordComment gwc = new GroupWordComment();
        //                                string idgwc = Public.GetID();
        //                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
        //                                {
        //                                    idgwc = Public.GetID();
        //                                }

        //                                gwc.Id = idgwc;
        //                                gwc.GroupWordId = itemKC.Id;
        //                                gwc.CommentId = idc;
        //                                if (kk == 0)
        //                                {
        //                                    gwc.Score = double.Parse(item.ReviewAppearance);
        //                                }
        //                                else if (kk == 1)
        //                                {
        //                                    gwc.Score = double.Parse(item.ReviewPalate);
        //                                }
        //                                else if (kk == 2)
        //                                {
        //                                    gwc.Score = double.Parse(item.ReviewTaste);
        //                                }
        //                                else if (kk == 3)
        //                                {
        //                                    gwc.Score = double.Parse(item.ReviewAroma);
        //                                }

        //                                db.GroupWordComments.Add(gwc);
        //                                db.SaveChanges();
        //                                kk++;
        //                            }
        //                        }

        //                        //SplitComment(idgc);
        //                    }
        //                    catch { }
        //                }

        //            }
        //        }

        //        return RedirectToAction("List", new { id = "" });
        //    }
        //    catch
        //    {
        //        TempData["error"] = "Thêm loi.";
        //        return RedirectToAction("List");
        //    }
        //}

        public ActionResult LoadDataJsonHotel(FormCollection collection, Comment model)
        {
            try
            {
                //string idgc = "";
                string idp = "";
                idp = collection["ProductId"];
                var kc = db.GroupWords.Where(e => e.ProductId == idp).ToList();
                //Load comment 
                string pathRead = "D:/Giao trinh CH/Luan van/DataHotel";
                //string pathRead = "E:/MsTu/Data/DataHotel";
                DirectoryInfo drInfo = new DirectoryInfo(pathRead);
                FileInfo[] files = drInfo.GetFiles();
                //doc ten cac file
                foreach (FileInfo f in files)
                {
                    using (StreamReader r = new StreamReader(f.FullName))
                    {
                        string json = r.ReadToEnd();
                        List<BeerJson> items = JsonConvert.DeserializeObject<List<BeerJson>>(json);

                        foreach (var item in items)
                        {
                            try
                            {
                                GroupComent gc = new GroupComent();
                                string name = item.BeerName.Trim().ToLower();
                                Comment cm = new Comment();
                                var gcc = db.GroupComents.Where(e => e.Name.Trim().ToLower().Equals(name)).ToList();
                                if (gcc.Count <= 0)
                                {
                                    string idgc = Public.GetID();
                                    while (db.GroupComents.Where(e => e.Id == idgc).Count() > 0)
                                    {
                                        idgc = Public.GetID();
                                    }
                                    gc.Id = idgc;
                                    gc.Name = item.BeerName;
                                    gc.Note = item.BeerStyle;
                                    gc.ProductId = idp;
                                    db.GroupComents.Add(gc);
                                    db.SaveChanges();

                                    string idc = Public.GetID();
                                    while (db.Comments.Where(e => e.Id == idc).Count() > 0)
                                    {
                                        idc = Public.GetID();
                                    }
                                    cm.Id = idc;
                                    cm.Comment1 = item.ReviewText;
                                    cm.Rating = decimal.Parse(item.ReviewOverall);
                                    cm.Name = item.UserProfileName;
                                    cm.Date = item.ReviewTimeUnix;
                                    cm.GroupCommentId = idgc;
                                    db.Comments.Add(cm);
                                    db.SaveChanges();

                                    int kk = 0;
                                    foreach (var itemKC in kc)
                                    {
                                        GroupWordComment gwc = new GroupWordComment();
                                        string idgwc = Public.GetID();
                                        while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                        {
                                            idgwc = Public.GetID();
                                        }

                                        gwc.Id = idgwc;
                                        gwc.GroupWordId = itemKC.Id;
                                        gwc.CommentId = idc;
                                        if (kk == 0)
                                        {
                                            gwc.Score = double.Parse(item.ReviewAppearance);
                                        }
                                        else if (kk == 1)
                                        {
                                            gwc.Score = double.Parse(item.ReviewPalate);
                                        }
                                        else if (kk == 2)
                                        {
                                            gwc.Score = double.Parse(item.ReviewTaste);
                                        }
                                        else if (kk == 3)
                                        {
                                            gwc.Score = double.Parse(item.ReviewAroma);
                                        }

                                        db.GroupWordComments.Add(gwc);
                                        db.SaveChanges();
                                        kk++;
                                    }

                                }
                                else
                                {
                                    string idc = Public.GetID();
                                    while (db.Comments.Where(e => e.Id == idc).Count() > 0)
                                    {
                                        idc = Public.GetID();
                                    }
                                    cm.Id = idc;
                                    cm.Comment1 = item.ReviewText;
                                    cm.Rating = decimal.Parse(item.ReviewOverall);
                                    cm.Name = item.BeerName;
                                    cm.Date = item.ReviewTimeUnix;
                                    cm.GroupCommentId = gcc.FirstOrDefault().Id;
                                    db.Comments.Add(cm);
                                    db.SaveChanges();

                                    int kk = 0;
                                    foreach (var itemKC in kc)
                                    {
                                        GroupWordComment gwc = new GroupWordComment();
                                        string idgwc = Public.GetID();
                                        while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                        {
                                            idgwc = Public.GetID();
                                        }

                                        gwc.Id = idgwc;
                                        gwc.GroupWordId = itemKC.Id;
                                        gwc.CommentId = idc;
                                        if (kk == 0)
                                        {
                                            gwc.Score = double.Parse(item.ReviewAppearance);
                                        }
                                        else if (kk == 1)
                                        {
                                            gwc.Score = double.Parse(item.ReviewPalate);
                                        }
                                        else if (kk == 2)
                                        {
                                            gwc.Score = double.Parse(item.ReviewTaste);
                                        }
                                        else if (kk == 3)
                                        {
                                            gwc.Score = double.Parse(item.ReviewAroma);
                                        }

                                        db.GroupWordComments.Add(gwc);
                                        db.SaveChanges();
                                        kk++;
                                    }
                                }

                                //SplitComment(idgc);
                            }
                            catch { }
                        }

                    }
                }

                return RedirectToAction("List", new { id = "" });
            }
            catch
            {
                TempData["error"] = "Thêm loi.";
                return RedirectToAction("List");
            }
        }

        public ActionResult Edit(string id)
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);

            var data = db.GroupComents.Find(id);

            IEnumerable<GroupComent> data2 = (IEnumerable<GroupComent>)db.GroupComents.ToList();
            ViewBag.Category = new SelectList(data2, "Id", "Name", id);

            return View(data);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(FormCollection collection, string id)
        {
            try
            {
                var model = db.Comments.Find(id);
                model.Name = collection["Name"];
                model.Comment1 = collection["Description"];

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                TempData["success"] = "Sửa thành công.";

                return RedirectToAction("List");
            }
            catch
            {
                TempData["error"] = "Sửa lỗi.";
                return RedirectToAction("List");
            }
        }

        public ActionResult Delete()
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            var cmt = db.Comments.Where(e => e.GroupCommentId == "170602090344212").ToList();
            try
            {
                foreach (var item in cmt)
                {
                    var gwc = db.GroupWordComments.Where(e => e.CommentId == item.Id).ToList();
                    foreach (var itemGWC in gwc)
                    {
                        var gwcd = db.GroupWordComments.Find(itemGWC.Id);
                        db.GroupWordComments.Remove(gwcd);

                    }
                    db.SaveChanges();

                    if (gwc.Count > 0)
                    {
                        var cmtd = db.Comments.Find(item.Id);
                        db.Comments.Remove(cmtd);

                    }
                }

                db.SaveChanges();
            }
            catch
            {

            }

            //var data = db.Comments.Find(id);
            //ViewBag.Meg = data.Name;
            return View();
        }

        //
        // POST: /AdminIT/Tags/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            { 
                var c3 = db.SeKeyWords.Where(e => e.Sentens.CommentId == id).Select(e => e.Id);
                foreach (string s in c3)
                {
                    var cn = db.SeKeyWords.Find(s);
                    db.SeKeyWords.Remove(cn);
                }
                db.SaveChanges();

                var c4 = db.Sentenses.Where(e => e.CommentId == id).Select(e => e.Id);
                foreach (string s in c4)
                {
                    var cn = db.Sentenses.Find(s);
                    db.Sentenses.Remove(cn);
                }
                db.SaveChanges();

                var data = db.Comments.Find(id);
                string idgc = data.GroupCommentId;
                db.Comments.Remove(data);

                db.SaveChanges();

                TempData["success"] = "Xóa thành công.";

                return RedirectToAction("List", new { id = idgc });
            }
            catch
            {
                TempData["error"] = "Xóa lỗi.";
                return RedirectToAction("List");
            }
        }

        public ActionResult LoadExcel()
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            return View();
        }

        [HttpPost]
        public ActionResult LoadExcel(HttpPostedFileBase file)
        {
            try
            {
                DataSet ds = new DataSet();
                if (Request.Files["file"].ContentLength > 0)
                {
                    string fileExtension =
                                         System.IO.Path.GetExtension(Request.Files["file"].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {

                            System.IO.File.Delete(fileLocation);
                        }
                        Request.Files["file"].SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {

                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(ds);
                        }
                    }
                    if (fileExtension.ToString().ToLower().Equals(".xml"))
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                        // DataSet ds = new DataSet();
                        ds.ReadXml(xmlreader);
                        xmlreader.Close();
                    }


                    //var model = db.GroupWordComments.Find(ds.Tables[0].Rows[i][0].ToString());
                    //model.Score = int.Parse(ds.Tables[0].Rows[i][3].ToString());

                    //db.Entry(model).State = EntityState.Modified;
                    //db.SaveChanges();
                    //string conn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                    //SqlConnection con = new SqlConnection(conn);
                    //string query = "Insert into Person(Name,Email,Mobile) Values('" + ds.Tables[0].Rows[i][0].ToString() + "','" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "')";
                    //con.Open();
                    //SqlCommand cmd = new SqlCommand(query, con);
                    //cmd.ExecuteNonQuery();
                    //con.Close();
                    var kc = db.GroupWords.Where(e => e.ProductId == "170329112813869").ToList();

                    GroupComent gc = new GroupComent();
                    string name = "Test Beer";
                    var gcc = db.GroupComents.Where(e => e.Name.Trim().Equals(name.Trim())).ToList();
                    if (gcc.Count <= 0)
                    {
                        string idgc = Public.GetID();
                        while (db.GroupComents.Where(e => e.Id == idgc).Count() > 0)
                        {
                            idgc = Public.GetID();
                        }
                        gc.Id = idgc;
                        gc.Name = name;
                        gc.ProductId = "170329112813869";
                        db.GroupComents.Add(gc);
                        db.SaveChanges();
                        for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Comment cm = new Comment();
                            string idc = Public.GetID();
                            while (db.Comments.Where(e => e.Id == idc).Count() > 0)
                            {
                                idc = Public.GetID();
                            }
                            cm.Id = idc;
                            cm.Comment1 = ds.Tables[0].Rows[i][8].ToString();
                            cm.Rating = decimal.Parse(ds.Tables[0].Rows[i][7].ToString());
                            cm.Name = ds.Tables[0].Rows[i][1].ToString();
                            cm.Date = ds.Tables[0].Rows[i][2].ToString();
                            cm.GroupCommentId = idgc;
                            db.Comments.Add(cm);
                            db.SaveChanges();

                            int kk = 0;
                            foreach (var itemKC in kc)
                            {
                                GroupWordComment gwc = new GroupWordComment();
                                string idgwc = Public.GetID();
                                while (db.GroupWordComments.Where(e => e.Id == idgwc).Count() > 0)
                                {
                                    idgwc = Public.GetID();
                                }

                                gwc.Id = idgwc;
                                gwc.GroupWordId = itemKC.Id;
                                gwc.CommentId = idc;
                                if (kk == 0)
                                {
                                    gwc.Score = Math.Round(double.Parse(ds.Tables[0].Rows[i][3].ToString()), 0);
                                }
                                else if (kk == 1)
                                {
                                    gwc.Score = Math.Round(double.Parse(ds.Tables[0].Rows[i][5].ToString()), 0);
                                }
                                else if (kk == 2)
                                {
                                    gwc.Score = Math.Round(double.Parse(ds.Tables[0].Rows[i][6].ToString()), 0);
                                }
                                else if (kk == 3)
                                {
                                    gwc.Score = Math.Round(double.Parse(ds.Tables[0].Rows[i][4].ToString()), 0);
                                }
                                else if (kk == 4)
                                {
                                    gwc.Score = Math.Round(double.Parse(ds.Tables[0].Rows[i][7].ToString()), 0);
                                }

                                db.GroupWordComments.Add(gwc);
                                db.SaveChanges();
                                kk++;
                            }
                            //SplitComment(idgc);
                        }

                    }

                }
                return View();
            }
            catch
            {
                TempData["error"] = "Load lỗi.";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public ActionResult LoadExcelSentense(HttpPostedFileBase file)
        {
            try
            {
                DataSet ds = new DataSet();
                if (Request.Files["file"].ContentLength > 0)
                {
                    string fileExtension =
                                         System.IO.Path.GetExtension(Request.Files["file"].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {

                            System.IO.File.Delete(fileLocation);
                        }
                        Request.Files["file"].SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {

                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(ds);
                        }
                    }
                    if (fileExtension.ToString().ToLower().Equals(".xml"))
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                        // DataSet ds = new DataSet();
                        ds.ReadXml(xmlreader);
                        xmlreader.Close();
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string comment = ds.Tables[0].Rows[i][2].ToString();
                        string id = ds.Tables[0].Rows[i][3].ToString();
                        if (comment != "" && comment != null)
                        {
                            string ids = Public.GetID();
                            while (db.Sentenses.Where(e => e.Id == ids).Count() > 0)
                            {
                                ids = Public.GetID();
                            }
                            Sentens se = new Sentens();
                            se.Id = ids;
                            se.ContentReview = comment;
                            se.CommentId = id;
                            db.Sentenses.Add(se);
                            db.SaveChanges();
                        }
                    }

                }
                return View();
            }
            catch
            {
                TempData["error"] = "Load lỗi.";
                return RedirectToAction("List");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
