using IzmirGunesAPI.Application.Repositorys.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Repositorys.Repositorys.S4inUser
{
    public interface IS4inUserRefreshToken:IBaseManager    
    {
        Task<Boolean> UpdateUserRefreshToken(string company);
    }
}
