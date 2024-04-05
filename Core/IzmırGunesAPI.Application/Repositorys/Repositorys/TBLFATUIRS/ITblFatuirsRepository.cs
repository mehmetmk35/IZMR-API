using IzmirGunesAPI.Application.Repositorys.Contexts;

namespace IzmirGunesAPI.Application.Repositorys.Repositorys.TBLFATUIRS
{
    public interface  ITblFatuirsRepository: IBaseManager
    {
        Task<List<Application.DTOs.TBLFATUIRS.TBLFATUIRS>> GetFatuirs(int Page, int Size);

    }
}
