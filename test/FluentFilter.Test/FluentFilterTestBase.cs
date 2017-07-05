using System;
using System.Collections.Generic;

using Xunit;
using FluentFilter;
using System.Linq;
using OhPrimitives;
using OhDotNetLib.Extension;
namespace FluentFilter.Test
{
#if !DBTEST
    public class DefaultDataFilterTest : FluentFilterTestDataBase
#else
    public abstract class FluentFilterTestBase : FluentFilterTestDataBaseFromDb
#endif
    {
    }
}
