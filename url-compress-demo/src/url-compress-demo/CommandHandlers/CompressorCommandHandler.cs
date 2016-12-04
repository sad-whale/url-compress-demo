using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using url_compress_demo.Commands;
using url_compress_demo.Models;
using url_compress_demo.Persistance;

namespace url_compress_demo.CommandHandlers
{
    public class CompressorCommandHandler : ICompressorCommandHandler
    {
        private CompressorContext _context;

        public CompressorCommandHandler(CompressorContext context)
        {
            _context = context;
        }

        public void Handle(IncrementClickCountCommand command)
        {
            if (command == null)
                return;

            var url = _context.Urls.FirstOrDefault(u => u.Id == command.Id);
            if (url != null)
            {
                url.ClickCount++;
                _context.SaveChanges();
            }
        }

        public void Handle(CompressUrlCommand command)
        {
            CompressedUrl newUrl = new CompressedUrl()
            {
                ClickCount = 0,
                CreationDate = DateTime.Now,
                Id = Guid.NewGuid().ToString("D"),
                SourceUrl = command.SourceUrl,
                UserId = command.UserId
            };

            _context.Urls.Add(newUrl);
            _context.SaveChanges();
        }
    }
}
