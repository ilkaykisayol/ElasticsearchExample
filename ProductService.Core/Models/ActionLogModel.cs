using System;

namespace ProductService.Core.Models
{
    public class ActionLogModel
    {
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Method { get; set; }
        public string Payload { get; set; }
        public string Response { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseTime { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime RespondedAt { get; set; }
        public int HttpStatusCode { get; set; }
    }
}
