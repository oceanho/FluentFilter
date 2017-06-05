using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Shouldly;
using Fluent.DataFilter;

namespace Fluent.DataFilterTest
{
    public class DefaultDataFilterTest
    {
        [Fact]
        public void DefaultDataFilter_ShouldBeOK_Test()
        {
            var filter = new MyOrderDataFilter();
            filter.CreationTime = DateTime.Now;

            var expression = filter.ToExpression();
        }
    }

    public class MyOrder
    {
        public int OrderId { get; set; }
        public DateTime CreationTime { get; set; }
    }
    public class MyOrderDataFilter : DefaultDataFilter<MyOrder>
    {
        public CompareField<int> OrderId { get; set; }
        public CompareField<DateTime> CreationTime { get; set; }
    }
}
