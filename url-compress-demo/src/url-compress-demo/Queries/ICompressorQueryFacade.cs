using System.Linq;
using url_compress_demo.Models;

namespace url_compress_demo.Queries
{
    /// <summary>
    /// интерфейс запросов к данным - моя попытка придерживаться принципов CQRS
    /// </summary>
    public interface ICompressorQueryFacade
    {
        IQueryable<CompressedUrl> Urls { get; }
    }
}
