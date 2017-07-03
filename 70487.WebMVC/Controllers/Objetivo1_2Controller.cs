using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using System.IO;

namespace _70487.WebMVC.Controllers
{
    public class Objetivo1_2Controller : Controller
    {
        // GET: Objetivo1_2
        public ActionResult Index()
        {
            
            var fileInfo = new FileInfo(Server.MapPath("~/Content/Objetivo1_2/sampleFile.txt"));
            Cache cache = new Cache();
            var fileContent = cache["sampleFile"];
            
            if(fileContent == null || string.IsNullOrEmpty(fileContent.ToString()))
            {
                using (StreamReader sr = new StreamReader(fileInfo.FullName))
                {
                    fileContent = sr.ReadToEnd();
                    CacheDependency cacheDependency = new CacheDependency(fileInfo.FullName);
                    cache.Insert("sampleFile", fileContent, cacheDependency);
                }
            }
            ViewBag.fileContent = fileContent.ToString();
            return View();
        }
    }
}