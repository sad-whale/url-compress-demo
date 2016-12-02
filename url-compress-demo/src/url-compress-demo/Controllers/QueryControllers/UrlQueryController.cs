using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using url_compress_demo.Queries;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace url_compress_demo.Controllers.QueryControllers
{
    [Route("api/[controller]")]
    public class UrlQueryController : Controller
    {
        ICompressorQueryFacade _compressorQuery;

        public UrlQueryController(ICompressorQueryFacade compressorQuery)
        {
            _compressorQuery = compressorQuery;
        }

        [HttpGet]
        public string GetUserUrls()
        {
            string userId = HttpContext.User.Identity.Name;

            try
            {
                var query = _compressorQuery.Urls.Where(url => url.UserId == userId);
                return JsonConvert.SerializeObject(query.ToList());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public string GetLastUserUrl()
        {
            string userId = HttpContext.User.Identity.Name;

            try
            {
                var query = _compressorQuery.Urls.Where(url => url.UserId == userId).OrderByDescending(url => url.CreationDate).First();
                return JsonConvert.SerializeObject(query);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
