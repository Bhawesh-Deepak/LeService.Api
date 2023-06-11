using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace LeService.Api.Helpers
{
    public record ResponseHelper
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public record ResponseEntitiesHelper<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
    }
}
