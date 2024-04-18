using IzmirGunesAPI.Application.Repositorys.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Repositorys.Repositorys.TBLSTHAR
{
    public interface  IGetDetailInvoiceCount:IBaseManager
    {
        public Task<int> Count(string Fisno, string company);
    }
}
