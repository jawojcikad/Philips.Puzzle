﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Puzzle.Core.CSV
{
    public class CSVColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public CSVColumnAttribute(string name) => Name = name;
    }
}
