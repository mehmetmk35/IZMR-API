using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.DTOs.Rest;
using IzmirGunesAPI.Application.DTOs.Token;
using IzmirGunesAPI.Application.Exceptions;
using IzmirGunesAPI.Persistence.Services.UserService;

namespace IzmirGunesAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRestService _restService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;

        public AuthService(IRestService restService, ITokenHandler tokenHandler, IUserService userService)
        {
            _restService = restService;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<Token> LoginAsync(RestContent company, int accessTokenLifeTime)
        {
            // await _restService.GetToken(company);
            RestToken result = await _restService.GetToken(new() { GrantType = "password", BranchCode = "0", Password = "SISBIM%", UserName = "SISBIM", DbName = "GULYAPANAS2023", DbUser = "TEMELSET", DbPassword = "", Dbtype = "0" });
            if (result.status==false)
                 throw new NotFoundUserException();
            if (result.status==true) {
                Token token =   _tokenHandler.CreateAccessToken(accessTokenLifeTime, company.UserName);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, company.UserName, token.Expiration, 60, company.DbName);
                return token;
            }
            throw new AuthenticationErrorException();
        }

    }
}
