using Newtonsoft.Json;

namespace url_compress_demo.ErrorHandling
{
    public class ErrorDto
    {
        public string Code { get; set; }

        public string Message { get; set; }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
