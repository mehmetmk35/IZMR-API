using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.DTOs.Token;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Command.AppUser.LogimUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;
        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
             Token token =  await _authService.LoginAsync(new() { DbName = request.DbName, BranchCode = request.BranchCode, Password = request.Password, UserName = request.UserName }, 60);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
        }
    }
}
