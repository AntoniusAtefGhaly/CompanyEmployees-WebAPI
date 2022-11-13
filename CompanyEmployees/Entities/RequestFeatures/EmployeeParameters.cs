﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class EmployeeParameters: RequestParameters
    {
        public EmployeeParameters()
        {
            OrderBy = "name";
        }
        public int MinAge { get; set; } 
        public int MaxAge { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxAge > MinAge;
    }
}