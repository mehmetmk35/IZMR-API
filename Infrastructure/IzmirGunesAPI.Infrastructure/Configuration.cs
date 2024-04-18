using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Infrastructure
{
    static class Configuration
    {
        static public string Rest_RestUrl
        {
            get
            {   var path = Environment.CurrentDirectory;
                ConfigurationManager configurationManager = new();         
                configurationManager.SetBasePath(path); ;
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager?.GetSection("Rest")["RestUrl"];
            }
        }
        static public DateTime CurrentTimeTr
        {
            get
            {
                return DateTime.UtcNow.AddHours(3);
            }
        }
    }
}
