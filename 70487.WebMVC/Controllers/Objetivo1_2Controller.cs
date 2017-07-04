using System.Web.Mvc;
using System.Web.Caching;
using System.IO;
using System.Runtime.Caching;
using System;

namespace _70487.WebMVC.Controllers
{
    public class Objetivo1_2Controller : Controller
    {
        // GET: Objetivo1_2
        public ActionResult Index()
        {
            return View();
        }

        #region System.Web.Caching.Cache

        private const string keyCacheFile = "sampleFile";
        private const string keyCacheVariavel = "sampleVariavel";
        private const string filePath = "~/Content/Objetivo1_2/sampleFile.txt";

        /// <summary>
        /// System.Web.Caching.Cache
        /// </summary>
        /// <returns></returns>
        public ActionResult Cache()
        {
            ViewBag.fileContent = CacheFile();
            ViewBag.variavelContent = CacheVariable(1);
            return View();
        }

        private dynamic CacheVariable(int value)
        {
            Cache cache = new Cache();

            var variavel = cache[keyCacheVariavel];
            if(variavel == null)
            {
                variavel = value;
                cache.Insert(keyCacheVariavel, variavel);
            }

            return variavel.ToString();
        }

        private string CacheFile()
        {
            var fileInfo = new FileInfo(Server.MapPath(filePath));
            Cache cache = new Cache();
            var fileContent = cache[keyCacheFile];

            if (fileContent == null || string.IsNullOrEmpty(fileContent.ToString()))
            {
                using (StreamReader sr = new StreamReader(fileInfo.FullName))
                {
                    fileContent = sr.ReadToEnd();
                    CacheDependency cacheDependency = new CacheDependency(fileInfo.FullName);
                    cache.Insert(keyCacheFile, fileContent, cacheDependency);
                }
            }

            return fileContent.ToString();
        }

        #endregion

        #region ObjectCache

        private const string objectCacheKey = "Cache1";

        /// <summary>
        /// System.Runtime.Caching.ObjectCache
        /// </summary>
        /// <returns></returns>
        public ActionResult ObjectCache()
        {
            ObjectCache cache = MemoryCache.Default;

            this.RunObjectCache(objectCacheKey, 1, true);
            ViewBag.cache1 = cache[objectCacheKey];

            this.RunObjectCache(objectCacheKey, 2, true);
            ViewBag.cache2 = cache[objectCacheKey];

            this.RunObjectCache(objectCacheKey, 3, true);
            ViewBag.cache3 = cache[objectCacheKey];

            return View();
        }

        private void RunObjectCache(string key, int value, bool absolute)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();

            if (absolute)
            {
                policy = new CacheItemPolicy()
                {
                    AbsoluteExpiration = new System.DateTimeOffset(DateTime.Now.AddSeconds(15))
                };
            }
            else
            {
                policy = new CacheItemPolicy()
                {
                    SlidingExpiration = new TimeSpan(0, 0, 15)
                };
            }


            var testandoValor = cache[key];
            cache.Remove(key);
            CacheItem cacheItem = new CacheItem(key);
            cacheItem.Value = value;

            cache.Add(cacheItem, policy);
        }

        #endregion

       

    }
}