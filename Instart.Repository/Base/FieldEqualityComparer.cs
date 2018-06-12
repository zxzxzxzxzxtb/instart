﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public class FieldEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x?.ToLower() == y?.ToLower();
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}
