﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Usuario : IdentityUser
    {
        public string? ImagemPerfilUrl { get; set; }
        public int teste { get; set; }
    }
}
