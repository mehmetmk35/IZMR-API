
using IzmirGunesAPI.Application.DTOs;
using IzmirGunesAPI.Application.Repositorys.Contexts;

namespace IzmirGunesAPI.Application.Abstractions.Services
{
    public interface IGetBranchService:IBaseManager
    {
        Task<List<Branch>> GetBranch(string company, string businessCode);
    }
}
