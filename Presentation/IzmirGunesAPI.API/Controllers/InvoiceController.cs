using IzmirGunesAPI.Application.Features.Queries.GetDetailInvoice;
using IzmirGunesAPI.Application.Features.Queries.GetInvoice;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IzmirGunesAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetInvoces([FromQuery] GetInvoiceQueryRequest getInvoiceQueryRequest)
        {
            GetInvoiceQueryResponse result = await _mediator.Send(getInvoiceQueryRequest);
            return Ok(result);

        }
        [HttpGet("detail")]
        public async Task<IActionResult> GetDetailInvoice([FromQuery] GetDetailInvoiceQueryRequest getDetailInvoiceQueryRequest)
        {
            GetDetailInvoiceQueryResponse result = await _mediator.Send(getDetailInvoiceQueryRequest);
            return Ok(result);
        }
    }
}
