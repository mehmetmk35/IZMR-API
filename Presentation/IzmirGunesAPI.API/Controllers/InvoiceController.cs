using IzmirGunesAPI.Application.Features.Queries.GetInvoice;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IzmirGunesAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
