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
                
                string line = "";
                string comment = collection["comment"];
                ViewBag.Time = comment;
                Dictionary<int, string> diccomment = new Dictionary<int, string>();
                ViewData["Message"] = "Welcome to ASP.NET MVC!";
                DataTable dt = new DataTable("MyTable");
                dt.Columns.Add(new DataColumn("sentent", typeof(string)));
                dt.Columns.Add(new DataColumn("idaspect", typeof(string)));
                dt.Columns.Add(new DataColumn("support", typeof(string)));
                dt.Columns.Add(new DataColumn("core", typeof(string)));

                DataTable datasentent = new DataTable();
                datasentent.Columns.Add(new DataColumn("sentent", typeof(string)));
                datasentent.Columns.Add(new DataColumn("idaspect", typeof(string)));
                datasentent.Columns.Add(new DataColumn("support", typeof(string)));
                datasentent.Columns.Add(new DataColumn("core", typeof(string)));
                DataTable datatmp = new DataTable("Kết quả tạm");
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
                         string strdata = str2;
                         //loại bỏ từ dừng
                         str2 = str2.Replace("n't", " not ");
                         str2 = str2.ToLower();
                         string[] result = str2.Split(' ');
                         line = "";
                         var stopword = db.StopWords.ToList(); 
                         foreach (var kvp in stopword)
                         {

                             if (result[0] == kvp.StopWord1)
                             {
                                 line = String.Concat(kvp.StopWord1, " ");
                                 str2 = str2.Replace(line, " ");
                             }
                             if (result[result.Length - 1] == kvp.StopWord1)
                             {
                                 line = String.Concat(" ", kvp.StopWord1);
                                 str2 = str2.Replace(line, " ");
                             }
                             line = String.Concat(" ", kvp.StopWord1);
                             line = String.Concat(line, " ");

                             str2 = str2.Replace(line, " ");
                         }
                             string[] str1 = str2.Split(' ');                                                      
                             foreach (var list in datalist)
                             {
                             double support = 0;
                             for (int w = 0; w < str1.Length; w++)
                             {
                                 string tmp = str1[w].ToString().Replace(",", "");
                                 tmp = tmp.Replace(";", "");
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
                             row["sentent"] = strdata.Trim();
                             row["idaspect"] = list.idaspect.Trim();
                             row["support"] = support;
                             row["core"] = list.word;
                             dt.Rows.Add(row);
                             
                             }                           
                        
                     }
                 }
                //hiển thị kết quả.              
                 var listcore = db.CoreWords.ToList();
                 for (int i = 0; i < str.Length; i++)
                 {
                     if (str[i].Trim() != "" && str[i].Length > 2)
                     {
                         string strtmp = str[i].Trim();
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
                                     strtmp += " " + str[j];
                                     i = j;
                                 }
                             }
                             foreach (var core in listcore)
                             {
                                 string id = core.id.Trim();
                                 double sp = 0;
                                 datatmp = dt.AsEnumerable()
                                .Where(r => r.Field<string>("idaspect") == id && r.Field<string>("sentent") == strtmp.Trim())
                                .CopyToDataTable();
                                 int dtmp = 0;
                                 foreach (DataRow row in datatmp.Rows)
                                 {
                                     sp += Convert.ToDouble(row.ItemArray[2]);
                                     dtmp++;

                                 }
                                 DataRow rowsentent = datasentent.NewRow();
                                 rowsentent["sentent"] = strtmp;
                                 rowsentent["idaspect"] = core.aspect;
                                 rowsentent["support"] = sp / dtmp;
                                 rowsentent["core"] = core.core_word;
                                 datasentent.Rows.Add(rowsentent);
                             }
                            
                         }
                         catch
                         {

                         }

                     }
                 }
                 return PartialView("Result", datasentent);
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