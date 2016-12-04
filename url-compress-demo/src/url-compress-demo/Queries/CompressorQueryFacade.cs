using System.Linq;
using url_compress_demo.Models;
using url_compress_demo.Persistance;

namespace url_compress_demo.Queries
{
    /// <summary>
    /// ендпоинт для запросов к данным - моя попытка придерживаться принципов CQRS
    /// получает dbcontext из ioc контейнера и предоставляет его данные для чтения как IQueryable
    /// </summary>
    public class CompressorQueryFacade : ICompressorQueryFacade
    {
        private CompressorContext _context;

        public CompressorQueryFacade(CompressorContext context)
        {
            _context = context;
        }

        public IQueryable<CompressedUrl> Urls
        {
            get
            {
                return _context.Urls as IQueryable<CompressedUrl>;
            }
        }
    }
}
