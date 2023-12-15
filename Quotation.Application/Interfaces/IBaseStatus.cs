namespace Quotation.Application.Interfaces
{
    public interface IBaseStatus
    {
        int StatusCode { get; set; }
        string Message { get; set; }
    }

    public interface IBaseStatus<T>
    {
        int StatusCode { get; set; }
    }
}
