using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.DTOs;
using IzmirGunesAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Persistence.Services.Company
{
    public class GetBranchService : BaseManager, IGetBranchService
    {
        public async Task<List<Branch>> GetBranch(string company, string businessCode)
        {
            List<Branch> List = new();
            String sorgu = String.Format(@"select SUBE_KODU,SUBSTRING(UNVAN,0,CHARINDEX(' ',UNVAN)) from TBLSUBELER where ISLETME_KODU=@ISLETME_KODU");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;
            try
            {
                await conn.OpenAsync();
                base.AddParameterWithValue(cmd, "@ISLETME_KODU", businessCode);
                SqlDataReader datareader = (SqlDataReader)cmd.ExecuteReader();

                while (await datareader.ReadAsync())
                {
                    Branch branch = new();
                    branch.BranchCode = base.GetSafeInt16(datareader, 0, 0);
                    branch.BranchName = base.GetSafeString(datareader, 1, "");
                    List.Add(branch);
                }
            }
            finally
            {
                base.HandleConnection(conn);
            }

            return List;
        }


    }
}
