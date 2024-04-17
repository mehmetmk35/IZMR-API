using IzmirGunesAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IzmirGunesAPI.Persistence.Contexts;

namespace IzmirGunesAPI.Persistence.Services.Company
{
    public class GetCompany : BaseManager, IGetCompany
    {
        public async Task<List<Application.DTOs.Company>> GetCompanyList()
        {
            List<Application.DTOs.Company> List = new();
            String sorgu = String.Format(@"SELECT SIRKET FROM SIRKETLER30");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.Netsis, String.Empty);
            SqlConnection conn = (SqlConnection)cmd.Connection;

            try
            {
                await conn.OpenAsync();              
                SqlDataReader datareader = (SqlDataReader)cmd.ExecuteReader();

                while (await datareader.ReadAsync())
                {
                    Application.DTOs.Company company = new();
                    company.CompanyName = base.GetSafeString(datareader, 0, "");     
                    List.Add(company);
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
