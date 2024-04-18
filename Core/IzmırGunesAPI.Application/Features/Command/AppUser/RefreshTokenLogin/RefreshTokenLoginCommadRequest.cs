using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Command.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommadRequest:IRequest<RefreshTokenLoginCommadResponse>
    {
        public string RefreshToken { get; set; }
        public string? Company { get; set; }
        public string? UserName { get; set; }
    }
}
