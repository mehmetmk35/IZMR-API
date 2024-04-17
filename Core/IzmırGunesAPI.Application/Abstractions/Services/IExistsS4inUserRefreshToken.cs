using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.Abstractions.Services
{
    public interface IExistsTable
    {
        Task<bool> IfExists(string TableName, string company);
    }
}
