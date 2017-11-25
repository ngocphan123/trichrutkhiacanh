﻿using System;
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
    public class VocabularyController : Controller
    {
        lCMSData db = new lCMSData();

        public ActionResult Index()
        {
            var jarRoot = @"D:\HsnSky\NLPTokenzie\stanford-corenlp-full-2015-12-09\edu\stanford\nlp";
            var modelsDirectory = jarRoot + @"\models\pos-tagger\english-left3words";

            // Loading POS Tagger
            //var tagger = new MaxentTagger(modelsDirectory + @"\wsj-0-18-bidirectional-nodistsim.tagger");
            var tagger = new MaxentTagger(modelsDirectory + @"\english-left3words-distsim.tagger");
            // Text for tagging
            var data3 = db.Sentenses.ToList();
            foreach (var item2 in data3)
            {
                var text = item2.ContentReview;
                var sen = db.Sentenses.Find(item2.Id);
                string str = "";
                //var text = "Quality hotel at great price Very clean. Free breakfast with good selection. Staff friendly and most helpful. A grat stay!";

                var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(text)).toArray();
                foreach (ArrayList sentence in sentences)
                {
                    try
                    {
                        var taggedSentence = tagger.tagSentence(sentence);
                        string[] str1 = taggedSentence.ToString().Split(',', '[', ']');

                        //Kiểm tra từ ghép
                        for (int i = 0; i < str1.Length; i++)
                        {
                            try
                            {
                                string[] str2 = str1[i].ToString().Split('/');
                                string[] str3 = str1[i + 1].ToString().Split('/');
                                string str4 = "";
                                string type = "JJ";

                                if (str2[1].Trim() == "JJ" && (str3[1].Trim() == "NN" || str3[1].Trim() == "NNS")) //Luật 1
                                {
                                    str4 = str2[0].Trim() + " " + str3[0].Trim();
                                    str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                }
                                else if ((str2[1].Trim() == "RB" || str2[1].Trim() == "RBR" || str2[1].Trim() == "RBS") && str3[1].Trim() == "JJ") //Luật 2
                                {
                                    str4 = str2[0].Trim() + " " + str3[0].Trim();
                                    str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                }
                                else if (str2[1].Trim() == "JJ" && str3[1].Trim() == "JJ") //Luật 3
                                {
                                    str4 = str2[0].Trim() + " " + str3[0].Trim();
                                    str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                }
                                else if ((str2[1].Trim() == "NN" || str2[1].Trim() == "NNS") && str3[1].Trim() == "JJ") //Luật 4
                                {
                                    str4 = str2[0].Trim() + " " + str3[0].Trim();
                                    str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                }
                                else if ((str2[1].Trim() == "RB" || str2[1].Trim() == "RBR" || str2[1].Trim() == "RBS")
                                    && (str3[1].Trim() == "VB" || str3[1].Trim() == "VBD" || str3[1].Trim() == "VBN" || str3[1].Trim() == "VBG")) //Luật 5
                                {
                                    str4 = str2[0].Trim() + " " + str3[0].Trim();
                                    str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                }
                                else if (str2[1].Trim() == "JJ" || str2[1].Trim() == "NN" || str2[1].Trim() == "VB" || str2[1].Trim() == "RB"  //Luật 6
                                            || str2[1].Trim() == "VBZ" || str2[1].Trim() == "NNP" || str2[1].Trim() == "NNS")
                                {
                                    str4 = str2[0].Trim();
                                    type = str2[1].Trim();
                                    str += str2[0].Trim() + "|" + str2[1].Trim() + " ;";
                                }

                                //logs tách từ
                                //str += str2[0].Trim() + "|" + str2[1].Trim() + " ;";

                                //cập nhật từ điển
                                var data2 = db.Vocabularies.Where(e => e.Word.Trim().ToLower().Equals(str4)).ToList();
                                if (data2.Count <= 0)
                                {
                                    Vocabulary data = new Vocabulary();
                                    string idv = Public.GetID();
                                    while (db.Vocabularies.Where(e => e.Id == idv).Count() > 0)
                                    {
                                        idv = Public.GetID();
                                    }
                                    data.Id = idv;
                                    data.Word = str4.Trim();
                                    data.TypeWord = type.Trim();
                                    db.Vocabularies.Add(data);
                                    db.SaveChanges();
                                }
                            }
                            catch { }
                        }


                        //foreach (var item in str1)
                        //{
                        //    try
                        //    {
                        //        if (item.Trim() != "")
                        //        {
                        //            string[] str2 = item.ToString().Split('/');
                        //            if (str2[1].Trim() == "JJ" || str2[1].Trim() == "NN" || str2[1].Trim() == "VB" || str2[1].Trim() == "RB"
                        //                || str2[1].Trim() == "VBZ" || str2[1].Trim() == "NNP" || str2[1].Trim() == "NNS")
                        //            {
                        //                string str3 = str2[0].Trim().ToLower();
                        //                //logs tách từ
                        //                str += str2[0].Trim() + "|" + str2[1].Trim() + " ;";

                        //                //cập nhật từ điển
                        //                var data2 = db.Vocabularies.Where(e => e.Word.Trim().ToLower().Equals(str3)).ToList();
                        //                if (data2.Count <= 0)
                        //                {
                        //                    Vocabulary data = new Vocabulary();
                        //                    string idv = Public.GetID();
                        //                    while (db.Vocabularies.Where(e => e.Id == idv).Count() > 0)
                        //                    {
                        //                        idv = Public.GetID();
                        //                    }
                        //                    data.Id = idv;
                        //                    data.Word = str2[0].Trim();
                        //                    data.TypeWord = str2[1].Trim();
                        //                    db.Vocabularies.Add(data);
                        //                    db.SaveChanges();
                        //                }
                        //            }
                        //        }
                        //    }
                        //    catch { }
                        //}
                    }
                    catch { }
                }
                sen.Logs = str;
                db.Entry(sen).State = EntityState.Modified;
                db.SaveChanges();

            }
            return View();
        }

        //public ActionResult LoadVocabulary(string idgc, string typew)
        //{ 
        //    //var jarRoot = @"D:\HsnSky\NLPTokenzie\stanford-corenlp-full-2015-12-09\edu\stanford\nlp";
        //    string urlRoot = System.IO.Path.Combine(Server.MapPath("~/Uploads/english-left3words"), "english-left3words-distsim.tagger");
        //    //var jarRoot = @"\stanford-corenlp-full-2015-12-09\edu\stanford\nlp";
        //    //var modelsDirectory = urlRoot + jarRoot + @"\models\pos-tagger\english-left3words";

        //    // Loading POS Tagger
        //    //var tagger = new MaxentTagger(urlRoot + @"\english-left3words-distsim.tagger");
        //    var tagger = new MaxentTagger(urlRoot);
        //    // Text for tagging
        //    var data = db.Sentenses.Where(e=>e.Comment.GroupCommentId==idgc).ToList();
        //    //var data = db.Comments.Where(e => e.GroupCommentId == idgc).ToList();

        //    foreach (var item in data)
        //    {
        //        var text = item.ContentReview;
        //        var sen = db.Sentenses.Find(item.Id);
        //        string str = "";
        //        //var text = "Quality hotel at great price Very clean. Free breakfast with good selection. Staff friendly and most helpful. A grat stay!";

        //        var sentences = MaxentTagger.tokenizeText(new StringReader(text)).toArray();
        //        foreach (ArrayList sentence in sentences)
        //        {
        //            try
        //            {
        //                var taggedSentence = tagger.tagSentence(sentence);
        //                string[] str1 = taggedSentence.ToString().Split(',', '[', ']');
        //                if (typew == "2")
        //                {
        //                    #region Tách có tạo từ ghép
        //                    //Kiểm tra từ ghép
        //                    for (int i = 0; i < str1.Length; i++)
        //                    {
        //                        try
        //                        {
        //                            string[] str2 = str1[i].ToString().Split('/');
        //                            string[] str3 = str1[i + 1].ToString().Split('/');
        //                            string str4 = "";
        //                            string type = "JJ";

        //                            if (str2[1].Trim() == "JJ" && (str3[1].Trim() == "NN" || str3[1].Trim() == "NNS")) //Luật 1
        //                            {
        //                                str4 = str2[0].Trim() + " " + str3[0].Trim();
        //                                str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
        //                            }
        //                            else if ((str2[1].Trim() == "RB" || str2[1].Trim() == "RBR" || str2[1].Trim() == "RBS") && str3[1].Trim() == "JJ") //Luật 2
        //                            {
        //                                str4 = str2[0].Trim() + " " + str3[0].Trim();
        //                                str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
        //                            }
        //                            else if (str2[1].Trim() == "JJ" && str3[1].Trim() == "JJ") //Luật 3
        //                            {
        //                                str4 = str2[0].Trim() + " " + str3[0].Trim();
        //                                str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
        //                            }
        //                            else if ((str2[1].Trim() == "NN" || str2[1].Trim() == "NNS") && str3[1].Trim() == "JJ") //Luật 4
        //                            {
        //                                str4 = str2[0].Trim() + " " + str3[0].Trim();
        //                                str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
        //                            }
        //                            else if ((str2[1].Trim() == "RB" || str2[1].Trim() == "RBR" || str2[1].Trim() == "RBS")
        //                                && (str3[1].Trim() == "VB" || str3[1].Trim() == "VBD" || str3[1].Trim() == "VBN" || str3[1].Trim() == "VBG")) //Luật 5
        //                            {
        //                                str4 = str2[0].Trim() + " " + str3[0].Trim();
        //                                str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
        //                            }
        //                            else if (str2[1].Trim() == "JJ" || str2[1].Trim() == "NN" || str2[1].Trim() == "VB" || str2[1].Trim() == "RB"  //Luật 6
        //                                        || str2[1].Trim() == "VBZ" || str2[1].Trim() == "NNP" || str2[1].Trim() == "NNS")
        //                            {
        //                                str4 = str2[0].Trim();
        //                                type = str2[1].Trim();
        //                                str += str2[0].Trim() + "|" + str2[1].Trim() + " ;";
        //                            }

        //                            //cập nhật từ điển
        //                            if (str4.Trim().Count() > 1)
        //                            {
        //                                var data2 = db.Vocabularies.Where(e => e.Word.Trim().ToLower().Equals(str4) && e.GroupCommentId == idgc).ToList();
        //                                if (data2.Count <= 0)
        //                                {
        //                                    Vocabulary data3 = new Vocabulary();
        //                                    string idv = Public.GetID();
        //                                    while (db.Vocabularies.Where(e => e.Id == idv).Count() > 0)
        //                                    {
        //                                        idv = Public.GetID();
        //                                    }
        //                                    data3.Id = idv;
        //                                    data3.Word = str4.Trim();
        //                                    data3.TypeWord = type.Trim();
        //                                    data3.GroupCommentId = idgc;
        //                                    db.Vocabularies.Add(data3);
        //                                    db.SaveChanges();
        //                                }
        //                            }
        //                        }
        //                        catch { }
        //                    }
        //                    #endregion
        //                }
        //                else
        //                {
        //                    #region Tách không tạo từ ghép
        //                    foreach (var item2 in str1)
        //                    {
        //                        try
        //                        {
        //                            if (item2.Trim() != "")
        //                            {
        //                                string[] str2 = item2.ToString().Split('/');
        //                                if (str2[1].Trim() == "JJ" || str2[1].Trim() == "NN" || str2[1].Trim() == "VB" || str2[1].Trim() == "RB"
        //                                    || str2[1].Trim() == "VBZ" || str2[1].Trim() == "NNP" || str2[1].Trim() == "NNS")
        //                                {
        //                                    string str3 = str2[0].Trim().ToLower();
        //                                    //logs tách từ
        //                                    str += str2[0].Trim() + "|" + str2[1].Trim() + " ;";

        //                                    //cập nhật từ điển
        //                                    if (str2[0].Trim().Count() > 1)
        //                                    {
        //                                        var data2 = db.Vocabularies.Where(e => e.Word.Trim().ToLower().Equals(str3) && e.GroupCommentId == idgc).ToList();
        //                                        if (data2.Count <= 0)
        //                                        {
        //                                            Vocabulary data4 = new Vocabulary();
        //                                            string idv = Public.GetID();
        //                                            while (db.Vocabularies.Where(e => e.Id == idv).Count() > 0)
        //                                            {
        //                                                idv = Public.GetID();
        //                                            }
        //                                            data4.Id = idv;
        //                                            data4.Word = str2[0].Trim();
        //                                            data4.TypeWord = str2[1].Trim();
        //                                            data4.GroupCommentId = idgc;
        //                                            db.Vocabularies.Add(data4);
        //                                            db.SaveChanges();
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        catch { }
        //                    }
        //                    #endregion
        //                }
        //            }
        //            catch { }
        //        }

        //        sen.Logs = str;
        //        db.Entry(sen).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }

        //    CountVocabulary(idgc);

        //    //IEnumerable<GroupComent> data6 = (IEnumerable<GroupComent>)db.GroupComents.ToList();
        //    //ViewBag.GroupComent = new SelectList(data, "Id", "Name", idgc);
        //    var data5 = db.Vocabularies.Where(e => e.GroupCommentId == idgc).ToList();
        //    return PartialView("_ListVocabulary", data5);
        //}

        public ActionResult LoadVocabulary()
        {
            string idgc = "";
            string typew = "1";
            Stopwatch sw = Stopwatch.StartNew();
            FileStream fs = new FileStream("D:\\hoctap\\DoAnTotNghiep\\soucecode\\stopWord_1.txt", FileMode.Open);
            StreamReader rd = new StreamReader(fs, Encoding.UTF8);
            string line = "";
            Dictionary<string, string> stopword = new Dictionary<string, string>();
            while ((line = rd.ReadLine()) != null)
            {
                stopword.Add(line, line);
            } 

            string urlRoot = System.IO.Path.Combine(Server.MapPath("~/Uploads/english-left3words"), "english-left3words-distsim.tagger");
            var tagger = new MaxentTagger(urlRoot);
            // Text for tagging

            var dataGC = db.GroupComents.Where(e => e.ProductId == "170319111826335").ToList();

            foreach (var itemGC in dataGC)
            {
                idgc = itemGC.Id;
                var data = db.Comments.Where(e => e.GroupCommentId == idgc).ToList();

                foreach (var item in data)
                {
                    var text = item.Comment1;
                    //string str = "";
                    var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(text)).toArray();
                    foreach (ArrayList sentence in sentences)
                    {
                        try
                        {
                            var taggedSentence = tagger.tagSentence(sentence);
                            string[] str1 = taggedSentence.ToString().Split(',', '[', ']');
                            if (typew == "2")
                            {
                                #region Tách có tạo từ ghép
                                //Kiểm tra từ ghép
                                for (int i = 0; i < str1.Length; i++)
                                {
                                    try
                                    {
                                        string[] str2 = str1[i].ToString().Split('/');
                                        string[] str3 = str1[i + 1].ToString().Split('/');
                                        string str4 = "";
                                        string type = "JJ";

                                        if (str2[1].Trim() == "JJ" && (str3[1].Trim() == "NN" || str3[1].Trim() == "NNS")) //Luật 1
                                        {
                                            str4 = str2[0].Trim() + " " + str3[0].Trim();
                                            //str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                        }
                                        else if ((str2[1].Trim() == "RB" || str2[1].Trim() == "RBR" || str2[1].Trim() == "RBS") && str3[1].Trim() == "JJ") //Luật 2
                                        {
                                            str4 = str2[0].Trim() + " " + str3[0].Trim();
                                            //str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                        }
                                        else if (str2[1].Trim() == "JJ" && str3[1].Trim() == "JJ") //Luật 3
                                        {
                                            str4 = str2[0].Trim() + " " + str3[0].Trim();
                                            //str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                        }
                                        else if ((str2[1].Trim() == "NN" || str2[1].Trim() == "NNS") && str3[1].Trim() == "JJ") //Luật 4
                                        {
                                            str4 = str2[0].Trim() + " " + str3[0].Trim();
                                            //str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                        }
                                        else if ((str2[1].Trim() == "RB" || str2[1].Trim() == "RBR" || str2[1].Trim() == "RBS")
                                            && (str3[1].Trim() == "VB" || str3[1].Trim() == "VBD" || str3[1].Trim() == "VBN" || str3[1].Trim() == "VBG")) //Luật 5
                                        {
                                            str4 = str2[0].Trim() + " " + str3[0].Trim();
                                            //str += str2[0].Trim() + "|" + str2[1].Trim() + " ;" + str3[0].Trim() + "|" + str3[1].Trim() + ";";
                                        }
                                        else if (str2[1].Trim() == "JJ" || str2[1].Trim() == "NN"
                                                || str2[1].Trim() == "RB" || str2[1].Trim() == "RBR" || str2[1].Trim() == "RBS"
                                                || str2[1].Trim() == "VBZ" || str2[1].Trim() == "VBD" || str2[1].Trim() == "VBN"
                                                || str2[1].Trim() == "VBG" || str2[1].Trim() == "VB"
                                                || str2[1].Trim() == "NNP" || str2[1].Trim() == "NNS")
                                        {
                                            str4 = str2[0].Trim();
                                            type = str2[1].Trim();
                                            //str += str2[0].Trim() + "|" + str2[1].Trim() + " ;";
                                        }

                                        //cập nhật từ điển
                                        if (str4.Trim().Count() > 1)
                                        {
                                            var data2 = db.Vocabulary_1.Where(e => e.Word.Trim().ToLower().Equals(str4) && e.GroupCommentId == idgc).ToList();
                                            if (data2.Count <= 0)
                                            {
                                                Vocabulary_1 data3 = new Vocabulary_1();
                                                string idv = Public.GetID();
                                                while (db.Vocabulary_1.Where(e => e.Id == idv).Count() > 0)
                                                {
                                                    idv = Public.GetID();
                                                }
                                                data3.Id = idv;
                                                data3.Word = str4.Trim();
                                                data3.TypeWord = type.Trim();
                                                data3.GroupCommentId = idgc;
                                                data3.Type = 2;
                                                db.Vocabulary_1.Add(data3);
                                                db.SaveChanges();
                                            }
                                            else
                                            {
                                                var data6 = data2.FirstOrDefault();
                                                data6.Counts++;
                                                db.Entry(data6).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }
                                        }

                                    }
                                    catch { }
                                }
                                #endregion
                            }
                            else
                            {
                                idgc = "170319111826335";
                                #region Tách không tạo từ ghép
                                string se = "";
                                foreach (var item2 in str1)
                                {
                                    try
                                    {
                                        string linei = "";
                                        if (item2.Trim() != "")
                                        {
                                            string[] str2 = item2.ToString().Split('/');
                                            if (str2[1].Trim() == "JJ" || str2[1].Trim() == "NN"
                                                || str2[1].Trim() == "RB" || str2[1].Trim() == "RBS"
                                                || str2[1].Trim() == "RBR" || str2[1].Trim() == "VBN"
                                                || str2[1].Trim() == "VBZ" || str2[1].Trim() == "VBD"
                                                || str2[1].Trim() == "VBG" || str2[1].Trim() == "VB"
                                                || str2[1].Trim() == "NNP" || str2[1].Trim() == "NNS")
                                            {
                                                string str3 = str2[0].Trim().ToLower();
                                              if (stopword.TryGetValue(str3, out linei)) continue;
                                              if (se == "") se = str3;
                                              else
                                                  se = String.Concat(se, String.Concat(" ", str3));
                                                //cập nhật từ điển
                                                if (str2[0].Trim().Count() > 1)
                                                {
                                                    string typeword = str2[1].Trim();
                                                    var data2 = db.Vocabulary_1.Where(e => e.Word.Trim().ToLower().Equals(str3) && e.TypeWord.Trim().ToLower().Equals(typeword) && e.GroupCommentId == idgc).ToList();
                                                    if (data2.Count <= 0)
                                                    {
                                                        Vocabulary_1 data4 = new Vocabulary_1();
                                                        string idv = Public.GetID();
                                                        while (db.Vocabulary_1.Where(e => e.Id == idv).Count() > 0)
                                                        {
                                                            idv = Public.GetID();
                                                        }
                                                        data4.Id = idv;
                                                        data4.Word = str3;
                                                        data4.TypeWord = str2[1].Trim();
                                                        data4.GroupCommentId = idgc;
                                                        data4.Counts = 1;
                                                        data4.Type = 1;
                                                        db.Vocabulary_1.Add(data4);
                                                        db.SaveChanges();
                                                    }
                                                    else
                                                    {
                                                        var data6 = data2.FirstOrDefault();
                                                        data6.Counts++;
                                                        db.Entry(data6).State = EntityState.Modified;
                                                        db.SaveChanges();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                }
                                //tao cau loai bo tu dung
                                Sentensesnotword senotopword = new Sentensesnotword();
                                senotopword.Id = Public.GetID(); ;
                                senotopword.ContentReview = se.Trim();
                                senotopword.CommentId = item.Id;
                                db.Sentensesnotwords.Add(senotopword);
                                db.SaveChanges();
                                #endregion
                            }
                        }
                        catch { }
                    }
                }
                //sen.Logs = str;
                //db.Entry(sen).State = EntityState.Modified;
                //db.SaveChanges();
            }

            //CountVocabulary(idgc);

            //IEnumerable<GroupComent> data6 = (IEnumerable<GroupComent>)db.GroupComents.ToList();
            //ViewBag.GroupComent = new SelectList(data, "Id", "Name", idgc);
            ViewBag.Time = sw.ElapsedMilliseconds;
            sw.Stop();
            var data5 = db.Vocabulary_1.ToList();
            return PartialView("_ListVocabulary", data5);
        }

        public void CountVocabulary(string idgc)
        {
            var vc = db.Vocabularies.Where(e => e.GroupCommentId == idgc).ToList();
            var cm = db.Comments.Where(e => e.GroupCommentId == idgc).ToList();
            foreach (var itemV in vc)
            {
                int count = 0;
                foreach (var itemCM in cm)
                {
                    string sN = "\\b" + Convert.ToString(itemV.Word.Trim().ToLower()) + "\\b";
                    Regex thegex = new Regex(sN.ToLower());
                    MatchCollection theMatches = thegex.Matches(itemCM.Comment1.Trim().ToLower());
                    count += theMatches.Count;
                }

                var uv = db.Vocabularies.Find(itemV.Id);
                uv.Counts = count;
                db.Entry(uv).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public ActionResult List(string id = "")
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);

            IEnumerable<GroupComent> data = (IEnumerable<GroupComent>)db.GroupComents.ToList();
            ViewBag.GroupComent = new SelectList(data, "Id", "Name", id);
            var data2 = db.Vocabularies.Where(e => e.GroupCommentId == id).ToList();
            ViewBag.Id = id;
            return View(data2);
        }

        public ActionResult LoadListVocabulary(string idgc)
        {
            var data = db.Vocabulary_1.ToList();
            Stopwatch sw = Stopwatch.StartNew();
            ViewBag.Time = sw.ElapsedMilliseconds;
            sw.Stop();
            return PartialView("_ListVocabulary", data);
        }
        public ActionResult ListVocabularyPro()
        {
            var data = db.Vocabularies.ToList();
            return PartialView("_ListVocabularyPro", data);
        }

        public ActionResult Delete(string id)
        {
            Session["current_url"] = Request.Url.AbsoluteUri;

            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);

            var data = db.Vocabularies.Find(id);
            ViewBag.Meg = data.Word;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var data = db.Vocabularies.Find(id);
                string idgc = data.GroupCommentId;
                db.Vocabularies.Remove(data);

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

    }
}
