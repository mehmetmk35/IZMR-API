using IzmirGunesAPI.Application.Repositorys.Contexts;

namespace IzmirGunesAPI.Application.Repositorys.Repositorys.TBLSTHAR
{
    public interface IDetailTblSthar:IBaseManager
    {
        Task<List<DTOs.TBLSTHAR>> GetDetailInvoice(string Fisno,string company);
    }
}
