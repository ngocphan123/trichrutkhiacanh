using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using StudyDoIT.Models.NLP;
using StudyDoIT.Models.Common;
using System.IO;
using System.Text;
using System.Data;

namespace StudyDoIT.Areas.AdminIT.Controllers
{
    public class AspectController : Controller
    {
        lCMSData db = new lCMSData();
        // GET: AdminIT/Aspect
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                FileStream fs = new FileStream("D:\\hoctap\\DoAnTotNghiep\\soucecode\\stopWord.txt", FileMode.Open);
                StreamReader rd = new StreamReader(fs, Encoding.UTF8);
                string line = "";
                Dictionary<string, string> stopword = new Dictionary<string, string>();
                while ((line = rd.ReadLine()) != null)
                {
                    stopword.Add(line, line);
                } 
                string comment = collection["comment"];
                ViewBag.Time = comment;
                Dictionary<int, string> diccomment = new Dictionary<int, string>();
                ViewData["Message"] = "Welcome to ASP.NET MVC!";

                DataTable dt = new DataTable("MyTable");
                dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                dt.Columns.Add(new DataColumn("Col3", typeof(string)));
                dt.Columns.Add(new DataColumn("core", typeof(string)));
                /*List các khía cạnh của câu
                 */
                
                string[] str = comment.Split('.', '!', '?');
                int d = 1;
                Dictionary<int, ListSupport> dicSupport = new Dictionary<int, ListSupport>();
                var datalist = db.VectorWords.Where(e => e.idaspect != "0").ToList();  
                int dem = 0;
                 for (int i = 0; i < str.Length; i++)
                 {
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
                         try
                         {
                             if (j < str.Length)
                             {
                                 if (!Char.IsUpper(str[j].Trim().ToCharArray()[0]))
                                 {
                                     str2 += " " + str[j];
                                     i = j;
                                 }
                             }
                         }
                         catch
                         {

                         }
   
                         //loại bỏ từ dừng
                         str2 = str2.Replace("n't", " n't ");
                         str2 = str2.ToLower();
                         string[] result = str2.Split(' ');
                         line = "";
                         foreach (KeyValuePair<string, string> kvp in stopword)
                         {

                             if (result[0] == kvp.Value)
                             {
                                 line = String.Concat(kvp.Value, " ");
                                 str2 = str2.Replace(line, " ");
                             }
                             if (result[result.Length - 1] == kvp.Value)
                             {
                                 line = String.Concat(" ", kvp.Value);
                                 str2 = str2.Replace(line, " ");
                             }
                             line = String.Concat(" ", kvp.Value);
                             line = String.Concat(line, " ");

                             str2 = str2.Replace(line, " ");
                         }
                             string[] str1 = str2.Split(' ');                                                      
                             foreach (var list in datalist)
                             {
                             double support = 0;
                             for (int w = 0; w < str1.Length; w++)
                             {
                                 string tmp = str1[w].ToString();
                                 var vectorword = db.VectorWords.Where(e => e.word == tmp).ToList();
                                
                                //var arrvecw = vectorword.First();
                                 if (vectorword.Count() > 0)
                                 {
                                     var arrvecw = vectorword.First();
                                     string[] arrvecword = arrvecw.vector.Split(',');
                                     double kc = 0;
                                    
                                         string[] arrvec = list.vector.Split(',');
                                         for (int k = 0; k < arrvec.Length; k++)
                                         {
                                             kc += (Convert.ToDouble(arrvec[k]) - Convert.ToDouble(arrvecword[k])) * (Convert.ToDouble(arrvec[k]) - Convert.ToDouble(arrvecword[k]));
                                         }
                                         support += 1/(Math.Sqrt(kc)+ 0.1);
                                      
                                 }
                               }
                             dem++;
                             support = Math.Round (support / (str1.Length), 1);
                             ListSupport sp = new ListSupport(str2,list.idaspect,support);

                             dicSupport.Add(dem, sp);

                             DataRow row = dt.NewRow();
                             row["Col1"] = str2;
                             row["Col2"] = list.idaspect;
                             row["Col3"] = support;
                             row["core"] = list.word;
                             dt.Rows.Add(row);
                             }                           
                        
                     }
                 }
                 
                 ViewBag.MyDictionary = dicSupport;
                 return PartialView("Result", dt);
            }
            catch
            {
                TempData["error"] = "Thêm lỗi.";
                return PartialView("Result");
            }
        }

        public class ListSupport
        {
            string senten;
            string idaspect;
            double sup;

            public ListSupport(string s, string i, double su)
            {
                senten = s;
                idaspect = i;
                sup = su;
            }

        }
    }
}