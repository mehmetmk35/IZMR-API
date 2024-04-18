using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Domain.Entity.Identity;
using IzmirGunesAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IzmirGunesAPI.Persistence.Services.ExistsTable;
using IzmirGunesAPI.Application.DTOs.TBLFATUIRS;
using System.Drawing;
using IzmirGunesAPI.Application.DTOs.S4inUser;
using IzmirGunesAPI.Application.DTOs.Rest;

namespace IzmirGunesAPI.Persistence.Services.UserService
{
    public class UserService : BaseManager, IUserService
    {
        private readonly IExistsTable  _existsTable;

        public UserService(IExistsTable existsTable)
        {
            _existsTable = existsTable;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken,string userName, DateTime accessTokenDate, int addOnAccessTokenDate,string company)
        {
            if(!await _existsTable.IfExists(Configuration.RefreshTokenDbTableName,company))
            {
               await  CreateTableS4inTokenTable(company, Configuration.RefreshTokenDbTableName);
            }
            if (!await CheckUserExistsS4inTable(company,userName, Configuration.RefreshTokenDbTableName))
            {
                await CreateUserS4inTokenTable(company, userName, Configuration.RefreshTokenDbTableName);
            }
            #region UpdateRefToken

          
            string sqlQuery = @$"UPDATE {Configuration.RefreshTokenDbTableName} SET
							RefreshToken = @NewRefreshToken ,
							RefreshTokenEndDate=@RefreshTokenEndDate
							WHERE UserName = @UserName";


            IDbCommand cmd = base.PrepareCommand(sqlQuery, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;
            try
            {
                await conn.OpenAsync();
               
                base.AddParameterWithValue(cmd, "@UserName", userName);
                base.AddParameterWithValue(cmd, "@NewRefreshToken", refreshToken);
                base.AddParameterWithValue(cmd, "@RefreshTokenEndDate", accessTokenDate.AddSeconds(addOnAccessTokenDate));               

                int resut = cmd.ExecuteNonQuery();            
            }
            catch (Exception e)
            {
                throw new Exception();
            }
            finally
            {
                base.HandleConnection(conn);

            }
            #endregion


        }



        public async Task<bool> CreateTableS4inTokenTable(string company,string tableName)
        {
            int result = 0;
            String sorgu = String.Format(@$" IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}')
                                                BEGIN
                                                    CREATE TABLE {tableName} (
                                                        UserName NVARCHAR(100) NOT NULL,
                                                        RefreshToken NVARCHAR(MAX),
                                                        RefreshTokenEndDate DATETIME,
		                                                CrateDate DATETIME,
		                                                CONSTRAINT PK_S4INUSERREFRESHTOKEN PRIMARY KEY (UserName)
                                                    )
                                                END
                                                 ");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;
            try
            {
                await conn.OpenAsync();
               
               
                 int resut =  cmd.ExecuteNonQuery();
                return (resut > 0 ? true : false);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                base.HandleConnection(conn);

            }
             
           
           
           
        }

        public async Task<bool> CreateUserS4inTokenTable(string company,string userName, string tableName)
        {
            int result = 0;
            string insertSingleRowQuery = $"INSERT INTO {tableName} (UserName, CrateDate) VALUES (@UserName, @CrateDate)";

            
            IDbCommand cmd = base.PrepareCommand(insertSingleRowQuery, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;
            try
            {
                await conn.OpenAsync();
                base.AddParameterWithValue(cmd, "@UserName", userName);               
                base.AddParameterWithValue(cmd, "@CrateDate", Configuration.CurrentTimeTr);
               
               
                int resut = cmd.ExecuteNonQuery();
                return (resut > 0 ? true : false);
            }
            finally
            {
                base.HandleConnection(conn);

            }
        }

        public async Task<bool> CheckUserExistsS4inTable(string company, string userName, string tableName)
        {
            bool result = false;

            String sorgu = String.Format(@$"SELECT CAST(
                                                       CASE 
                                                           WHEN EXISTS (select 1 from {tableName} where userName=@userName) 
                                                           THEN 1 
                                                           ELSE 0 
                                                       END 
                                                   AS BIT) AS ExistsUser");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;

            try
            {
                await conn.OpenAsync();
                base.AddParameterWithValue(cmd, "@userName", userName);
               
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

        public async Task<UserRefreshToken> GetUserRefrehToken(string company, string refreshToken, string tableName)
        {         
            String sorgu = String.Format(@$"SELECT  RefreshToken,RefreshTokenEndDate FROM {tableName}  WHERE RefreshToken=@RefreshToken");
            IDbCommand cmd = base.PrepareCommand(sorgu, CommandType.Text, Domain.Entity.Enumerations.ConnectionType.NetsisSirket, company);
            SqlConnection conn = (SqlConnection)cmd.Connection;
            UserRefreshToken refToken = new();
            try
            {
                await conn.OpenAsync();
                base.AddParameterWithValue(cmd, "@RefreshToken", refreshToken);
                
                SqlDataReader datareader = (SqlDataReader)cmd.ExecuteReader();

                while (await datareader.ReadAsync())
                {
                   
                    refToken.RefreshToken = base.GetSafeString(datareader, 0, "");
                    refToken.Expiration = base.GetSafeDateTime(datareader, 1, new DateTime(9999, 1, 1));                    
                }
            }
            
            finally
            {
                base.HandleConnection(conn);
            }
 
            return refToken;
        }
    }
}
