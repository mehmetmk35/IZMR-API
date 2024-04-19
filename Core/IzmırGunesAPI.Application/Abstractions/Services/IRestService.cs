using IzmirGunesAPI.Application.DTOs.Rest;
using IzmirGunesAPI.Application.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Abstractions.Services
{
    public interface IRestService
    {
        Task<RestToken> GetToken(RestContent company);
        Task RemoveRestToken(string Token);

    }
}
