using IzmirGunesAPI.Application.DTOs.Rest;
using IzmirGunesAPI.Application.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(RestContent company);
        Task<Token> RefreshTokenLoginAsync(string refreshToken, string company,  string userName);
    }
}
