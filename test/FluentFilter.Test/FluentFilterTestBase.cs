using System;
using System.Collections.Generic;

using Xunit;
using FluentFilter;
using System.Linq;
using OhPrimitives;
using OhDotNetLib.Extension;
namespace FluentFilter.Test
{
    public abstract class FluentFilterTestBase :
#if !DBTEST
         FluentFilterTestDataBase
#else
     FluentFilterTestDataBaseFromDb
#endif
    {
        public FluentFilterTestBase()
        {
            FluentFilterManager.Reset();
        }
    }
}
