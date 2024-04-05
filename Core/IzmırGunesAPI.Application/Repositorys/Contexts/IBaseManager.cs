using System.Data;
using System.Data.SqlClient;

namespace IzmirGunesAPI.Application.Repositorys.Contexts
{
    public interface  IBaseManager
    {
        IDbCommand  PrepareCommand(string commandText, CommandType commandType, Domain.Entity.Enumerations.ConnectionType connType, string Sirket);
        Task HandleConnection(SqlConnection conn);
        IDataParameter AddParameterWithValue(IDbCommand cmd, String paramName, Object value);
        String ConvertCorruptedChars(String s);
        DateTime GetSafeDateTime(IDataReader dataReader, int index, DateTime defaultValue);
        byte GetSafeByte(IDataReader dataReader, int index, byte defaultValue);
        bool GetSafeBoolean(IDataReader dataReader, int index, bool defaultValue);
        int GetSafeInteger(IDataReader dataReader, int index, int defaultValue);
        int GetSafeInt16(IDataReader dataReader, int index, Int16 defaultValue);
        float GetSafeFloat(IDataReader dataReader, int index, float defaultValue);
        double GetSafeDouble(IDataReader dataReader, int index, double defaultValue);
        decimal GetSafeDecimal(IDataReader dataReader, int index, decimal defaultValue);
        String GetSafeString(IDataReader dataReader, int index, String defaultValue);
        Char GetSafeChar(IDataReader dataReader, int index, Char defaultValue);





    }
}
