using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;
using Console = System.Console;
using java.util;
using java.io;
using StudyDoIT.Models.NLP;
using StudyDoIT.Models.Common;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StudyDoIT.Areas.AdminIT.Controllers
{
    [Authorize(Roles = "Administrator, Manager, Writer, Writers")]
    public class SentensesController : Controller
    {
        lCMSData db = new lCMSData();
        //
        // GET: /AdminIT/Sentenses/
        public ActionResult Index()
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);

            return RedirectToAction("List");
        }

        public ActionResult ReplaceList()
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);

            IEnumerable<Product> data = (IEnumerable<Product>)db.Products.ToList();
            ViewBag.Product = new SelectList(data, "Id", "Name");

            var datalist = db.Sentenses.ToList();
            return PartialView("List", datalist);
        }

        public ActionResult List()
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);

            IEnumerable<Product> data = (IEnumerable<Product>)db.Products.ToList();
            ViewBag.Product = new SelectList(data, "Id", "Name");

            var datalist = db.Sentenses.ToList();
            return PartialView("List", datalist);
        }

        public ActionResult LoadListSentenses(string idgc)
        {
            var data = db.Sentenses.Where(e => e.Comment.GroupCommentId == idgc).ToList();
            return PartialView("_ListSentenses", data);
        }
        private void standarsetenword()
        {
            var stopword = db.StopWords.ToList();
            Dictionary<string, string> dicstopword = new Dictionary<string, string>();
            foreach (var kvp in stopword)
            {
                dicstopword.Add(kvp.StopWord1, kvp.StopWord1);
            }

            var listcore = db.CoreWords.ToList();
            Dictionary<string, string> diccore = new Dictionary<string, string>();
            foreach (var kvp in listcore)
            {
                //diccore.Add(kvp.core_word, kvp.core_word);
                string[] strcore = kvp.core_word.Split(',');
                foreach (var s in strcore)
                {
                    diccore.Add(s, s);
                }
            }
            string urlRoot = System.IO.Path.Combine(Server.MapPath("~/Uploads/english-left3words"), "english-left3words-distsim.tagger");
            var tagger = new MaxentTagger(urlRoot);
            var sen2 = db.Sentenses.ToList();
            if (sen2.Count >= 1)
            {

                var cm = sen2;

                //try
                //{
                if (cm.Count > 1)
                {
                    foreach (var item in cm)
                    {
                        var text = item.ContentReview;
                        //string str = "";
                        var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(text)).toArray();
                        foreach (ArrayList sentence in sentences)
                        {
                            string strse = sentence.toString();
                            strse = strse.Replace(",", "");
                            strse = strse.Replace("[", "");
                            strse = strse.Replace("]", "");
                            string ids = item.Id;

                            var taggedSentence = tagger.tagSentence(sentence);
                            string[] str1 = taggedSentence.ToString().Split(',', '[', ']');
                            string strtmp = "";
                            foreach (var item2 in str1)
                            {
                                string linei = "";
                                if (item2.Trim() != "")
                                {
                                    string[] str2 = item2.ToString().Split('/');
                                    try
                                    {
                                        if(str2.Count()==2 && str2[1].Trim() != "")
                                        {
                                            if ((str2[1].Trim() != "NN"
                                                && !diccore.TryGetValue(str2[0].Trim().ToLower(), out linei)
                                                && str2[1].Trim() != "NNP" && str2[1].Trim() != "NNS") || dicstopword.TryGetValue(str2[0].Trim().ToLower(), out linei))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                strtmp += " " + str2[0].Trim().ToLower();
                                            }                                      
                                        }
                                   
                                    }
                                    catch { }
                                }
                            }

                            Sentensesnotword senotopword = new Sentensesnotword();
                           /* while (db.Sentensesnotwords.Where(e => e.Id == ids).Count() > 0)
                            {
                                ids = Public.GetID();
                            }*/
                            if (strtmp.Trim() != "" && db.Sentensesnotwords.Where(e => e.Id == ids).Count() == 0)
                            {
                                senotopword.Id = ids;
                                senotopword.ContentReview = strtmp.Trim();
                                senotopword.CommentId = item.CommentId;
                                db.Sentensesnotwords.Add(senotopword);
                                db.SaveChanges();
                            }
                           
                        }
                    }
                }
                //}
                //catch { }
          
        }
      }
        [HttpPost]
        public ActionResult SplitSentenses(FormCollection collection)
        {
            string idp = collection["ProductId"];
            //var cat = db.Categories.ToList();
            //var cm = db.Comments.ToList();
            var p = db.GroupComents.Where(e => e.ProductId == idp).ToList();
            foreach (var pp in p)
            {
                //try
                //{

               //SplitComment(pp.Id);
                
                //}
                //catch { }
            }
            standarsetenword();
            var data = db.Sentenses.ToList();
            return PartialView("_ListSentenses", data);
        }
        private void SplitComment(string idgc)
        {
            var sen2 = db.Sentenses.Where(e => e.Comment.GroupCommentId == idgc).ToList();
            if (sen2.Count <= 0)
            {
                var cm = db.Comments.Where(e => e.GroupCommentId == idgc).ToList();

                //try
                //{
                if (cm.Count > 1)
                {
                    foreach (var item in cm)
                    {
                        //Tiền xử lý
                        //string[] str = item.Comment1.Split('...');
                        //try
                        //{
                        var sen = db.Sentenses.Where(e => e.CommentId == item.Id).ToList();
                        if (sen.Count <= 0)
                        {
                            // string strtest = "Not too heavy, definately not watery. Not very good at describing it other than it's good.";
                            string[] str = item.Comment1.Split('.', '!', '?');
                            //string[] str = item.Split('.', '!', '?');

                            for (int i = 0; i < str.Length; i++)
                            {
                                //try
                                //{
                                if (str[i].Trim() != "" && str[i].Length > 2)
                                {
                                    string str2 = str[i];
                                    int j = i + 1;
                                    if (j < str.Length)
                                    {
                                        while (str[j].Length <= 0)
                                        {
                                            j++;
                                            if (j >= str.Length) break;
                                        }
                                    }
                                    //char s = str[j].Trim().ToCharArray()[0];
                                    if (j < str.Length)
                                    {
                                        try
                                        {
                                            if (!Char.IsUpper(str[j].Trim().ToCharArray()[0]))
                                            {
                                                str2 += " " + str[j];
                                                i = j;
                                            }
                                        }
                                        catch{

                                        }
                                        
                                    }
                                    string ids = Public.GetID();
                                    while (db.Sentenses.Where(e => e.Id == ids).Count() > 0)
                                    {
                                        ids = Public.GetID();
                                    }
                                    Sentens se = new Sentens();
                                    se.Id = ids;
                                    se.ContentReview = str2.Trim();
                                    se.CommentId = item.Id;
                                    db.Sentenses.Add(se);
                                    db.SaveChanges();

                                }
                                //}
                                //catch { }
                            }
                        }
                        //}
                        //catch { }
                    }
                }
                //}
                //catch { }
            }
        }

        public ActionResult Delete(string id)
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);

            var data = db.Sentenses.Find(id);
            ViewBag.Meg = data.ContentReview;
            return View();
        }

        public ActionResult DeleteError()
        {
            var data = db.GroupComents.Where(e=>e.Note=="error").ToList();
            foreach (var itemGC in data)
            {
                var cmt = db.Comments.Where(e => e.GroupCommentId == itemGC.Id).ToList();
                foreach (var itemC in cmt)
                {
                    var gwc = db.GroupWordComments.Where(e => e.CommentId == itemC.Id).ToList();
                    foreach (var itemGWC in gwc)
                    {
                        var del = db.GroupWordComments.Find(itemGWC.Id);
                        db.GroupWordComments.Remove(del);
                        db.SaveChanges();
                    }

                    var sen = db.Sentenses.Where(e => e.CommentId == itemC.Id).ToList();
                    foreach (var itemS in sen)
                    {
                        var del = db.Sentenses.Find(itemS.Id);
                        db.Sentenses.Remove(del);
                        db.SaveChanges();
                    }

                    var del5 = db.Comments.Find(itemC.Id);
                    db.Comments.Remove(del5);
                    db.SaveChanges();
                }

                var del6 = db.GroupComents.Find(itemGC.Id);
                db.GroupComents.Remove(del6);
                db.SaveChanges();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var data = db.Sentenses.Find(id);
                db.Sentenses.Remove(data);

                db.SaveChanges();

                TempData["success"] = "Xóa thành công.";

                return RedirectToAction("List");
            }
            catch
            {
                TempData["error"] = "Xóa lỗi.";
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
