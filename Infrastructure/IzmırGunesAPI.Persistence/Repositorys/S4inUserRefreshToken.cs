using IzmirGunesAPI.Application.DTOs.TBLFATUIRS;
using IzmirGunesAPI.Application.Repositorys.Repositorys.S4inUser;
using IzmirGunesAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace IzmirGunesAPI.Persistence.Repositorys
{
    public class S4inUserRefreshToken : BaseManager, IS4inUserRefreshToken
    {
        public async Task<bool> UpdateUserRefreshToken(string company)
        {
           
            String query = String.Format(@" update a set a=1");
            IDbCommand cmd = base.PrepareCommand(query, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;

            try
            {
                await conn.OpenAsync();
                //base.AddParameterWithValue(cmd, "@OFFSET", Page * Size);

               
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0 ? true : false);
                
                
            }           
            finally
            {
                base.HandleConnection(conn);
            }

             
        }
    }
}
