using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace url_compress_demo.Models
{
    public class CompressedUrl
    {
        public string Id { get; set; }

        public string SourceUrl { get; set; }

        public string UserId { get; set; }

        public DateTime CreationDate { get; set; }

        public int ClickCount { get; set; }
    }
}
