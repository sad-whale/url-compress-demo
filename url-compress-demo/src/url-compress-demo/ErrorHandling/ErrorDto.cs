using Newtonsoft.Json;

namespace url_compress_demo.ErrorHandling
{
    /// <summary>
    /// dto для передачи данных об ошибках
    /// </summary>
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
