using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using url_compress_demo.Commands;

namespace url_compress_demo.CommandHandlers
{
    interface ICompressorCommandHandler
    {
        void Handle(CompressUrlCommand command);

        void Handle(IncrementClickCountCommand command);
    }
}
