using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Repositorys.Repositorys.TBLFATUIRS
{
    public interface IInvoiceCount
    {
        public  Task<int>Count(string company);
    }
}
