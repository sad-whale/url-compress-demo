using url_compress_demo.Commands;

namespace url_compress_demo.CommandHandlers
{
    public interface ICompressorCommandHandler
    {
        void Handle(CompressUrlCommand command);

        void Handle(IncrementClickCountCommand command);
    }
}
