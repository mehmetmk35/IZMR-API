﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzmirGunesAPI.Application.DTOs.S4inUser
{
    public class UserRefreshToken
    {
        public string RefreshToken { get; set; }
        public DateTime Expiration{get; set;}
    }
}
