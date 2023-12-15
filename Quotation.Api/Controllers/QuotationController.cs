using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quotation.Application.Interfaces;

namespace Quotation.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly IQuotationService _quotationService;

        public QuotationController(IQuotationService quotationService) => _quotationService = quotationService;

        [HttpGet("{date}")]
        public async Task<ActionResult> GetQuotations(DateOnly date) => Ok(await _quotationService.GetQuotationsAsync(date));
    }
}
