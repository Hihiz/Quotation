using Quotation.Application.Interfaces;

namespace Quotation.Application.Models
{
    public class StatusResponse : IBaseStatus
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class StatusResponse<T> : IBaseStatus<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }

    public class StatusResponseError<T> : IBaseStatus<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
