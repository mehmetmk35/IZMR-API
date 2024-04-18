using IzmirGunesAPI.Application.DTOs.S4inUser;
using IzmirGunesAPI.Application.Repositorys.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Abstractions.Services
{
    public interface IUserService:IBaseManager
    {
        Task UpdateRefreshTokenAsync(string refreshToken,string userName, DateTime accessTokenDate, int addOnAccessTokenDate, string company);
        Task<bool> CreateTableS4inTokenTable(string company, string tableName);
        Task<bool> CreateUserS4inTokenTable(string company, string userName, string tableName);
        Task<bool> CheckUserExistsS4inTable(string company, string userName, string tableName);
        Task<UserRefreshToken> GetUserRefrehToken(string company, string refreshToken, string tableName);

        

    }
}
