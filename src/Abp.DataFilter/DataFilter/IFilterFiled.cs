﻿using System;
using System.Linq.Expressions;

namespace Abp.DataFilter.DataFilter
{
    public interface IFiledFilter
    {
        bool IsSatisfy();
    }
}
