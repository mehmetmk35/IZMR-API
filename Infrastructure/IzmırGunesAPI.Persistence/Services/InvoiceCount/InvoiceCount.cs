using IzmirGunesAPI.Application.DTOs.TBLFATUIRS;
using IzmirGunesAPI.Application.Repositorys.Repositorys.TBLFATUIRS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IzmirGunesAPI.Persistence.Contexts;

namespace IzmirGunesAPI.Persistence.Services.InvoiceCount
{
    public class InvoiceCount : BaseManager, IInvoiceCount
    {
        public async Task<int> Count(string company)
        {
            int result = 0;
            String sorgu = String.Format(@"SELECT count(1) FROM TBLFATUIRS FT");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;

            try
            {
                await conn.OpenAsync();                
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
