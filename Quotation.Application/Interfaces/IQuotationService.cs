using Quotation.Domain.Entities;

namespace Quotation.Application.Interfaces
{
    public interface IQuotationService
    {
        Task<IBaseStatus<List<Valute>>> GetQuotationsAsync(DateOnly date);
    }
}
