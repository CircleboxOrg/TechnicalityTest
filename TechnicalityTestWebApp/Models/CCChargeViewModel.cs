﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalityTestWebApp.Models
{
    public class CCChargeViewModel
    {
        public int ChargeId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }
}
