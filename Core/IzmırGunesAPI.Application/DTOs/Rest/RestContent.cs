using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.DTOs.Rest
{
    public class RestContent
    {
        public string GrantType { get; set; } = "password";
        public string BranchCode { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string DbName { get; set; }
        public string DbUser { get; set; } = "TEMELSET";
        public string DbPassword { get; set; } = "";
        public string Dbtype { get; set; } = "0";

    }
}
