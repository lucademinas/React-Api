﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Admin : User
    {
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
