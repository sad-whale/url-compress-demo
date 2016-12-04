namespace url_compress_demo.Commands
{
    /// <summary>
    /// команда создания новой сжатой ссылки
    /// </summary>
    public class CompressUrlCommand
    {
        public string SourceUrl { get; set; }

        public string UserId { get; set; }
    }
}
