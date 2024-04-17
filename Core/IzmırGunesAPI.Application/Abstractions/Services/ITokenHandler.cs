using IzmirGunesAPI.Application.DTOs.Token;
using IzmirGunesAPI.Domain.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int second, string UserName);
        string CreateRefreshToken();
    }
}
