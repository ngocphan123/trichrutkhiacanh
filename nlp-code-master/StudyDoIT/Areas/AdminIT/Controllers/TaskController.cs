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
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json.Linq;
namespace StudyDoIT.Areas.AdminIT.Controllers
{
    public class TaskController : Controller
    {
        lCMSData db = new lCMSData();
        // GET: AdminIT/Task
        public ActionResult Index()
        {
            var data = db.Vocabularies.ToList();
            Stopwatch sw = Stopwatch.StartNew();
            ViewBag.Time = sw.ElapsedMilliseconds;
            sw.Stop();
            return View(data);
        }

        public class TuDien
        {
            public string Id { get; set; }
            public string Word { get; set; }
            public string TypeWord { get; set; }
            public string GroupCommentId { get; set; }
            public int Count { get; set; }

            public int Type { get; set; }
            public TuDien(string id, string n1, string typeword, int s1, string groupommentid, int type)
            {
                Id = id;
                Word = n1;
                TypeWord = typeword;
                GroupCommentId = groupommentid;
                Count = s1;
                Type = type;
            }

        }

        public class ListTuDien
        {
            Dictionary<string, TuDien> Vocabulary;
            public ListTuDien(Dictionary<string, TuDien> V)
            {
                Vocabulary = V;
            }

        }
        public int Testdb(Object data)
        {
            var str = "";
            return 1;
        }
        public Dictionary<string, TuDien> TaskVocabulary(Object data, MaxentTagger tagger, string gr, Dictionary<string, string> stopword)
        {
            Dictionary<string, TuDien> OjVocabulary = new Dictionary<string, TuDien>();
            foreach (var item in (dynamic)data)
            {
                var text = item.ContentReview;
                //string text = "But a lot. ";
                var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(text)).toArray();
                foreach (ArrayList sentence in sentences)
                {
                    try
                    {
                        var taggedSentence = tagger.tagSentence(sentence);
                        string[] str1 = taggedSentence.ToString().Split(',', '[', ']');
                        #region Tách không tạo từ ghép
                        foreach (var item2 in str1)
                        {
                            try
                            {
                                string line = "";
                                if (item2.Trim() != "")
                                {
                                    string[] str2 = item2.ToString().Split('/');
                                    if (str2[1].Trim() == "JJ" || str2[1].Trim() == "JJR" || str2[1].Trim() == "JJS"
                                        || str2[1].Trim() == "RB" || str2[1].Trim() == "RBR" || str2[1].Trim() == "RBS"
                                        || str2[1].Trim() == "VBZ" || str2[1].Trim() == "VBD" || str2[1].Trim() == "VBN"
                                        || str2[1].Trim() == "VBG" || str2[1].Trim() == "VB" || str2[1].Trim() == "VBP"
                                        || str2[1].Trim() == "NNP" || str2[1].Trim() == "NNS" || str2[1].Trim() == "NN" || str2[1].Trim() == "NNPS")
                                    {
                                        string str3 = str2[0].Trim().ToLower();
                                        if (stopword.TryGetValue(str3, out line)) continue;
                                        //cập nhật từ điển
                                        if (str2[0].Trim().Count() > 1)
                                        {
                                            //string id, string n1, string typeword, int s1, string groupommentid, int type
                                            TuDien Ojvalue = new TuDien("", "", "", 0, "", 0);
                                            string idv = Public.GetID();
                                            if (ListVocabulary.TryGetValue(str2[1].Trim() + "_" + str3, out Ojvalue))
                                            {
                                                Ojvalue.Count += 1;
                                                ListVocabulary[str2[1].Trim() + "_" + str3] = Ojvalue;
                                            }
                                            else
                                            {
                                                ListVocabulary.Add(str2[1].Trim() + "_" + str3, new TuDien(idv, str3, str2[1].Trim(), 1, gr, 1));
                                            }
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                        #endregion
                    }
                    catch { }
                }
            }
            //ListTuDien V = new ListTuDien(OjVocabulary);
            return OjVocabulary;
        }
        Dictionary<string, TuDien> ListVocabulary = new Dictionary<string, TuDien>();
        public ActionResult LoadListVocabulary()
        {
            Stopwatch sw = Stopwatch.StartNew();
            /* Stopwatch sw = Stopwatch.StartNew();
             ViewBag.Time = sw.ElapsedMilliseconds;
             sw.Stop();
             */
            Stopwatch sw1 = Stopwatch.StartNew();
            string urlRoot = System.IO.Path.Combine(Server.MapPath("~/Uploads/english-left3words"), "english-left3words-distsim.tagger");
            MaxentTagger tagger = new MaxentTagger(urlRoot);

            FileStream fs = new FileStream("D:\\hoctap\\DoAnTotNghiep\\soucecode\\stopWord.txt", FileMode.Open);
            StreamReader rd = new StreamReader(fs, Encoding.UTF8);
            string line = "";
            Dictionary<string, string> stopword = new Dictionary<string, string>();
            while ((line = rd.ReadLine()) != null)
            {
                stopword.Add(line, line);
            }

            var tasks = new List<Task<Dictionary<string, TuDien>>>();            
            var groupdata = db.GroupComents.Select(y => y.Id).ToList();
            int RecordsPerPage = 1;
            int recordStart = (groupdata.Count()) / RecordsPerPage;
            int Limitrecord = recordStart;
            var tasksave = new List<Task>();
            for (int g = 1; g <= RecordsPerPage; g++)
            {
                var list = groupdata.Skip((g - 1) * recordStart).Take(Limitrecord).ToList();
                Object data = db.Sentenses.Where(e => list.Contains(e.Comment.GroupCommentId)).ToList();
                Task<Dictionary<string, TuDien>> t = Task<Dictionary<string, TuDien>>.Factory.StartNew(() => TaskVocabulary(data, tagger, "170319111826335", stopword));
                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (KeyValuePair<string, TuDien> v in ListVocabulary)
            {
                if (v.Value == null) continue;
                Vocabulary data4 = new Vocabulary();
                string idv = v.Value.Id;
                while (db.Vocabularies.Where(e => e.Id == idv).Count() > 0)
                {
                    idv = Public.GetID();
                }
                v.Value.Word = v.Value.Word.ToLower();
                data4.Id = idv;
                data4.Word = v.Value.Word;
                data4.TypeWord = v.Value.TypeWord;
                data4.GroupCommentId = "170319111826335";
                data4.Counts = v.Value.Count;
                data4.Type = v.Value.Type;
                db.Vocabularies.Add(data4);
                db.SaveChanges();
            }
            var datalist = db.Vocabularies.ToList();
            ViewBag.Time = sw.ElapsedMilliseconds;
            sw.Stop();
            return PartialView("_LoadVocabulary", datalist);
        }
    }
}