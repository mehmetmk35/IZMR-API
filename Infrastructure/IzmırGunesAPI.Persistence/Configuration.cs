using Microsoft.Extensions.Configuration;

namespace IzmirGunesAPI.Persistence
{
    static  class Configuration
    {
        static public string ConnectionStringSql
        {
            get
            {
                var path = Environment.CurrentDirectory;

                ConfigurationManager configurationManager = new();                
                configurationManager.SetBasePath(path);
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("SQLConnection");
            }
        }
        static public string ConnectionStringNetsisSql
        {
            get
            {
                var path = Environment.CurrentDirectory;
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(path);
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("NetsisSQLConnection");
            }
        }
        static public string RefreshTokenDbTableName
        {
            get
            {
                var path = Environment.CurrentDirectory;
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(path); ;
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager?.GetSection("RefreshTokenDbTableName")["TableName"];
            }
        }
    }
}
