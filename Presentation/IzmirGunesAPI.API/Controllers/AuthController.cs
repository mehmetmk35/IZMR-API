using IzmirGunesAPI.Application.Features.Command.AppUser.LogimUser;
using IzmirGunesAPI.Application.Features.Queries.GetBranch;
using IzmirGunesAPI.Application.Features.Queries.GetCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IzmirGunesAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpGet("getcompany")]
        public async Task<IActionResult> GetCompany([FromQuery] GetCompanyQueryRequest getCompanyQueryRequest)
        {
            GetCompanyQueryResponse response = await _mediator.Send(getCompanyQueryRequest);
            return Ok(response);
        }
        [HttpGet("getbranch")]
        public async Task<IActionResult> GetBranch([FromQuery] GetBranchQueryRequest getBranchQueryRequest)
        {
            GetBranchQueryResponse response = await _mediator.Send(getBranchQueryRequest);
            return Ok(response);
        }
    }
}
