using IzmirGunesAPI.Application.DTOs;
using IzmirGunesAPI.Application.Repositorys.Contexts;

namespace IzmirGunesAPI.Application.Abstractions.Services
{
    public interface IGetBusinessService: IBaseManager
    {
        Task<List<Business>> GetBusinessList(string company);
    }
}
