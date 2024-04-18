using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.DTOs.Token;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Command.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommadHandler : IRequestHandler<RefreshTokenLoginCommadRequest, RefreshTokenLoginCommadResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenLoginCommadHandler(IAuthService authService)
        {
            _authService = authService;
        }

        async Task<RefreshTokenLoginCommadResponse> IRequestHandler<RefreshTokenLoginCommadRequest, RefreshTokenLoginCommadResponse>.Handle(RefreshTokenLoginCommadRequest request, CancellationToken cancellationToken)
        {
            Token result = await _authService.RefreshTokenLoginAsync(request.RefreshToken, request.Company, request.UserName);
            return new() { Token = result };
        }
    }
}
