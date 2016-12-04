using System.Linq;
using url_compress_demo.Models;

namespace url_compress_demo.Queries
{
    public interface ICompressorQueryFacade
    {
        IQueryable<CompressedUrl> Urls { get; }
    }
}
