using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.DTOs.Rest;
using IzmirGunesAPI.Application.DTOs.S4inUser;
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

        public async Task<Token> LoginAsync(RestContent company)
        {
            RestToken result = await _restService.GetToken(company);
             
            //RestToken result = await _restService.GetToken(new() { GrantType = "password", BranchCode = "0", Password = "SISBIM%", UserName = "SISBIM", DbName = "GULYAPANAS2023", DbUser = "TEMELSET", DbPassword = "", Dbtype = "0" });
            if (result.status==false)
                 throw new NotFoundUserException(result.messaj);
            if (result.status==true) {
                await _restService.RemoveRestToken(result.token); // netsis tarafından  token silme
                Token token =   _tokenHandler.CreateAccessToken(Configuration.TokenTimePeriot, company.UserName);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, company.UserName, token.Expiration, Configuration.RefreshTokenTimePeriot, company.DbName); //1 SAATİIK  REF TOKEN 
                return token;
            }
            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken, string company,string userName)
        {
           UserRefreshToken resutl = await _userService.GetUserRefrehToken(company, refreshToken,Configuration.RefreshTokenDbTableName);
            if (resutl != null && resutl.Expiration>Configuration.CurrentTimeTr)
            {
                Token token = _tokenHandler.CreateAccessToken(Configuration.TokenTimePeriot, userName);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, userName, token.Expiration, Configuration.RefreshTokenTimePeriot, company); //1 SAATİIK  REF TOKEN 
                return token;
            }
            else
                throw new NotFoundUserException();
        }
    }
}
