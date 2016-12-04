using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using url_compress_demo.Queries;
using Newtonsoft.Json;
using url_compress_demo.CommandHandlers;
using url_compress_demo.Commands;
using url_compress_demo.Services;
using System.Net;
using url_compress_demo.ErrorHandling;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace url_compress_demo.Controllers.QueryControllers
{
    public class UrlController : Controller
    {
        private ICompressorQueryFacade _compressorQuery;
        private ICompressorCommandHandler _compressorCommandHandler;
        private IUserService _userService;

        public UrlController(ICompressorQueryFacade compressorQuery, ICompressorCommandHandler compressorCommandHandler, IUserService userService)
        {
            _compressorQuery = compressorQuery;
            _compressorCommandHandler = compressorCommandHandler;
            _userService = userService;
        }

        [HttpGet("urlapi/all")]
        public string GetUserUrls()
        {
            string userId = _userService.GetCurrentUserId();

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

        [HttpGet("urlapi/last")]
        public string GetLastUserUrl()
        {
            string userId = _userService.GetCurrentUserId();

            try
            {
                var query = _compressorQuery.Urls.Where(url => url.UserId == userId).OrderByDescending(url => url.CreationDate).FirstOrDefault();
                return JsonConvert.SerializeObject(query);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost("urlapi/compress")]
        public string CompressUrl(string url)
        {
            try
            {
                string userId = _userService.GetCurrentUserId();

                url = WebUtility.UrlDecode(url);

                Uri uriResult;
                bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == "http" || uriResult.Scheme == "https");

                if (!result)
                    throw new InvalidUrlException($"Invalid url: {url}");

                CompressUrlCommand command = new CompressUrlCommand()
                {
                    SourceUrl = uriResult.ToString(),
                    UserId = userId
                };
                _compressorCommandHandler.Handle(command);

                var query = _compressorQuery.Urls.Where(u => u.UserId == userId).OrderByDescending(u => u.CreationDate).FirstOrDefault();
                return JsonConvert.SerializeObject(query);
            }
            catch(InvalidUrlException ex)
            {
                Response.StatusCode = 500;
                return JsonConvert.SerializeObject(new ErrorDto() { Code = ex.GetType().Name, Message = ex.Message });
            }
        }

        [HttpGet("go/{urlId}")]
        public ActionResult RedirectToUrl(string urlId)
        {
            var url = _compressorQuery.Urls.FirstOrDefault(u => u.Id == urlId);

            if (url != null)
            {
                var command = new IncrementClickCountCommand()
                {
                    Id = urlId
                };

                _compressorCommandHandler.Handle(command);

                return Redirect(url.SourceUrl);
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}
