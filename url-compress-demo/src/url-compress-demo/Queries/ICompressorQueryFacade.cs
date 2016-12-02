using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using url_compress_demo.Models;

namespace url_compress_demo.Queries
{
    public interface ICompressorQueryFacade
    {
        IQueryable<CompressedUrl> Urls { get; }
    }
}
