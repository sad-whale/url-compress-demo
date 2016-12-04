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
        //внедрение интерфейсов запросов, команд и прочих зависимостей
        private ICompressorQueryFacade _compressorQuery;
        private ICompressorCommandHandler _compressorCommandHandler;
        private IUserService _userService;

        public UrlController(ICompressorQueryFacade compressorQuery, ICompressorCommandHandler compressorCommandHandler, IUserService userService)
        {
            _compressorQuery = compressorQuery;
            _compressorCommandHandler = compressorCommandHandler;
            _userService = userService;
        }

        /// <summary>
        /// возвратить все созданные ссылки текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("urlapi/all")]
        public string GetUserUrls()
        {
            //получаем id пользователя
            string userId = _userService.GetCurrentUserId();

            try
            {
                //делаем запрос на все ссылки, созданные пользователем 
                //и возвращаем в виде json
                var query = _compressorQuery.Urls.Where(url => url.UserId == userId);
                return JsonConvert.SerializeObject(query.ToList());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// получить последнюю ссылку, созданную пользователем
        /// </summary>
        /// <returns></returns>
        [HttpGet("urlapi/last")]
        public string GetLastUserUrl()
        {
            //все по аналогии с предыдущим методом,
            //только делаем запрос на последнюю запись
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

        /// <summary>
        /// "сжать" ссылку
        /// </summary>
        /// <param name="url">исходная ссылка</param>
        /// <returns>объект сжатой ссылки</returns>
        [HttpPost("urlapi/compress")]
        public string CompressUrl(string url)
        {
            try
            {
                string userId = _userService.GetCurrentUserId();

                url = WebUtility.UrlDecode(url);

                //если переданная ссылка невалидная и/или имеет схему, отличную от https или http,
                //выбрасываем исключение
                Uri uriResult;
                bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == "http" || uriResult.Scheme == "https");

                if (!result)
                    throw new InvalidUrlException($"Invalid url {url}");

                //создаем команду на "создание" новой сжатой ссылки
                CompressUrlCommand command = new CompressUrlCommand()
                {
                    SourceUrl = uriResult.ToString(),
                    UserId = userId
                };
                //выполняем команду
                _compressorCommandHandler.Handle(command);

                //делаем запрос на последнюю созданную ссылку и возвращаем её
                var query = _compressorQuery.Urls.Where(u => u.UserId == userId).OrderByDescending(u => u.CreationDate).FirstOrDefault();
                return JsonConvert.SerializeObject(query);
            }
            //обработка исключения при неверной ссылке
            catch(InvalidUrlException ex)
            {
                Response.StatusCode = 500;
                return JsonConvert.SerializeObject(new ErrorDto() { Code = ex.GetType().Name, Message = ex.Message });
            }
        }

        /// <summary>
        /// редирект на исходную ссылку
        /// </summary>
        /// <param name="urlId">id сжатой ссылки</param>
        /// <returns></returns>
        [HttpGet("go/{urlId}")]
        public ActionResult RedirectToUrl(string urlId)
        {
            //из базы получаем ссылку с переданным id
            var url = _compressorQuery.Urls.FirstOrDefault(u => u.Id == urlId);

            if (url != null)
            {
                var command = new IncrementClickCountCommand()
                {
                    Id = urlId
                };

                _compressorCommandHandler.Handle(command);

                //если ссылка нашлась - редиректим на исходную ссылку
                return Redirect(url.SourceUrl);
            }
            else
            {
                //иначе - на дефолтную страницу
                return Redirect("/");
            }
        }
    }
}
