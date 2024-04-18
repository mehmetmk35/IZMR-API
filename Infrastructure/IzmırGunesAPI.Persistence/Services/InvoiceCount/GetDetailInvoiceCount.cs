using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLSTHAR;
using IzmirGunesAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IzmirGunesAPI.Persistence.Services.InvoiceCount
{
    public class GetDetailInvoiceCount : BaseManager, IGetDetailInvoiceCount
    {
        public async Task<int> Count(string Fisno, string company)
        {
            int result = 0;
            String sorgu = String.Format(@" select COUNT(*) FROM TBLSTHAR  WHERE FISNO=@FISNO");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;

            try
            {
                await conn.OpenAsync();
                base.AddParameterWithValue(cmd, "@FISNO", Fisno);
                SqlDataReader datareader = (SqlDataReader)cmd.ExecuteReader();

                while (await datareader.ReadAsync())
                {
                    result = base.GetSafeInteger(datareader, 0, 0);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                base.HandleConnection(conn);
            }

            return result;
        }
    }
}
