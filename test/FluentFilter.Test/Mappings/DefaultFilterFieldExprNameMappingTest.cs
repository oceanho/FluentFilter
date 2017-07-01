using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Shouldly;
using FluentFilter;
using FluentFilter.Mappings;

namespace FluentFilter.Test.Mappings
{
    public class DefaultFilterFieldExprNameMappingTest : FluentFilterTestBase
    {
        public DefaultFilterFieldExprNameMappingTest()
        {
            FluentFilterManager.Reset();
            FluentFilterManager.AddMapping<MyFilterFieldMapping>();
        }

        [Fact]
        public void Verify_MappingShouldBeWork()
        {
        }
    }

    public class MyFilterFieldMapping : DefaultExprNameMapping<MyOrderDataFilter>
    {
    }
}
