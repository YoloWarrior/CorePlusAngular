﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class UserForLoginDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public bool IsConfirm { get; set; }
    }
}
