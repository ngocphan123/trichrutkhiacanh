using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

using System.Diagnostics;
using System.Threading.Tasks;

namespace StudyDoIT.Areas.AdminIT.Controllers
{
    public class VectorWordController : Controller
    {
        // GET: AdminIT/VectorWord
        public ActionResult Index()
        {
           /* ScriptEngine engine = Python.CreateEngine();
            engine.ExecuteFile(@"D:\python\test.py");*/
            string fileName = @"D:\python\study.py";

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Python27\python.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            ViewBag.Time = output;
            p.WaitForExit();
            return View();
        }
        private static void doPython()
        {
            /*var py = Python.CreateEngine();
            py.ExecuteFile(@"D:\python\study.py");*/
            var ipy = Python.CreateRuntime();
            dynamic test = ipy.UseFile(@"D:\python\study.py");
            test.Simple();
        }
    }
}