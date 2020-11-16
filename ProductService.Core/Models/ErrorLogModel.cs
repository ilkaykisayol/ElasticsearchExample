using System;

namespace ProductService.Core.Models
{
    public class ErrorLogModel
    {
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Method { get; set; }
        public string Payload { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStack { get; set; }
        public DateTime ErrorAt { get; set; }
    }
}
