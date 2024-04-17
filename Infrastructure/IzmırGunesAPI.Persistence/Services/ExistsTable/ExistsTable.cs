using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IzmirGunesAPI.Persistence.Services.ExistsTable
{
    public class ExistsTable : BaseManager, IExistsTable
    {

        async Task<bool> IExistsTable.IfExists(string TableName, string company)
        {
            bool result=false;
           
            String sorgu = String.Format(@"SELECT CAST(
                                                   CASE 
                                                       WHEN EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @dbName) 
                                                       THEN 1 
                                                       ELSE 0 
                                                   END 
                                               AS BIT) AS TableExists");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;

            try
            {
                await conn.OpenAsync();
                base.AddParameterWithValue(cmd, "@dbName", TableName);
                SqlDataReader datareader = (SqlDataReader)cmd.ExecuteReader();

                while (await datareader.ReadAsync())
                {
                    result = base.GetSafeBoolean(datareader, 0, false);
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
