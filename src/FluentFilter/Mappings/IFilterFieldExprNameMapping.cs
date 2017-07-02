﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Mappings
{
    public interface IFilterFieldExprNameMapping
    {
        MappingInfo[] Mapping();
        Type FilterType { get; }
    }
}