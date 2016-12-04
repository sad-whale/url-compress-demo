using System.Linq;
using url_compress_demo.Models;
using url_compress_demo.Persistance;

namespace url_compress_demo.Queries
{
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
