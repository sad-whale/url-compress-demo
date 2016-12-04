using System;

namespace url_compress_demo.ErrorHandling
{
    public class InvalidUrlException: Exception
    {
        public InvalidUrlException(string message)
            : base(message)
        {

        }
    }
}
