using IzmirGunesAPI.Application.Repositorys.Contexts;
using IzmirGunesAPI.Domain.Entity.Enumerations;
using System.Data;
using System.Data.SqlClient;

namespace IzmirGunesAPI.Persistence.Contexts
{
    public class BaseManager : IBaseManager
    {
      

        public IDbCommand PrepareCommand(string commandText, CommandType commandType, ConnectionType connType, string company)
        {
            IDbCommand  cmd = null;
            switch (connType)
            {
                 
                case Domain.Entity.Enumerations.ConnectionType.NetsisSirket:
                    {
                        String connString = Configuration.ConnectionStringSql;
                        connString = connString.Replace("DEFAULT", company);
                        SqlConnection  conn = new SqlConnection(connString);
                        cmd =  new  SqlCommand(commandText, conn);
                        cmd.CommandType = commandType;
                    }
                    break;
                case Domain.Entity.Enumerations.ConnectionType.Netsis:
                    {
                        String connString = Configuration.ConnectionStringNetsisSql;
                        SqlConnection conn = new SqlConnection(connString);
                        cmd = new SqlCommand(commandText, conn);
                        cmd.CommandType = commandType;
                    }
                    break;
            }
            return cmd;
        }
        public async Task HandleConnection(SqlConnection conn)
        {
            if (conn != null)
            {
               await conn.CloseAsync();
               await conn.DisposeAsync();
            }
        }

        public  IDataParameter AddParameterWithValue(IDbCommand cmd, string paramName, object value)
        {
           IDataParameter param = null;
            if (cmd != null) 
            {
                if (cmd is SqlCommand)
                {
                    param = ((SqlCommand)cmd).Parameters.AddWithValue(paramName, value);
                }
                else
                {
                    throw new NotSupportedException("Only SQL Server Connector is supported. In future releases we may support other connectors.");
                }
            }
            return param;
        }
        public String ConvertCorruptedChars(String s)
        {
            String r = s;
            if (!String.IsNullOrEmpty(r))
            {
                r = r.Replace('Þ', 'Ş').Replace('þ', 'ş').Replace('Ý', 'İ').Replace('ý', 'i').Replace('Ð', 'Ğ').Replace('ð', 'ğ');
            }

            return r;
        }

        public DateTime GetSafeDateTime(IDataReader dataReader, int index, DateTime defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? dataReader.GetDateTime(index) : defaultValue;
        }

        public byte GetSafeByte(IDataReader dataReader, int index, byte defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? dataReader.GetByte(index) : defaultValue;
        }

        public bool GetSafeBoolean(IDataReader dataReader, int index, bool defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? dataReader.GetBoolean(index) : defaultValue;
        }

        public int GetSafeInteger(IDataReader dataReader, int index, int defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? dataReader.GetInt32(index) : defaultValue;
        }

        public int GetSafeInt16(IDataReader dataReader, int index, short defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? dataReader.GetInt16(index) : defaultValue;
        }
        public float GetSafeFloat(IDataReader dataReader, int index, float defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? dataReader.GetFloat(index) : defaultValue;
        }
        public double GetSafeDouble(IDataReader dataReader, int index, double defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? dataReader.GetDouble(index) : defaultValue;
        }
        public decimal GetSafeDecimal(IDataReader dataReader, int index, decimal defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? dataReader.GetDecimal(index) : defaultValue;
        }
        public String GetSafeString(IDataReader dataReader, int index, String defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? ConvertCorruptedChars(dataReader.GetString(index)) : defaultValue;
        }
        public Char GetSafeChar(IDataReader dataReader, int index, Char defaultValue)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            if (index < 0 || index >= dataReader.FieldCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return !dataReader.IsDBNull(index) ? dataReader.GetChar(index) : defaultValue;
        }

         
    }
}
