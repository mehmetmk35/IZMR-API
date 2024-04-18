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
    public class GetBusinessService : BaseManager, IGetBusinessService
    {
        

        public async Task<List<Business>> GetBusinessList(string company)
        {
            List<Business> List = new();
            String sorgu = String.Format(@"SELECT ISLETME_KODU,ADI FROM TBLISLETMELER WHERE AKTIF='E' AND ISLETME_KODU NOT IN ('-2','-1')");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;

            try
            {
                await conn.OpenAsync();
                SqlDataReader datareader = (SqlDataReader)cmd.ExecuteReader();

                while (await datareader.ReadAsync())
                {
                    Business business = new();
                    business.BusinessCode = base.GetSafeInt16(datareader, 0, 0);
                    business.BusinessName = base.GetSafeString(datareader, 1, "");
                    List.Add(business);
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
