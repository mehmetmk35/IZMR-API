using IzmirGunesAPI.Application.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Features.Command.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommadResponse
    {
        public Token Token { get; set; }
    }
}
