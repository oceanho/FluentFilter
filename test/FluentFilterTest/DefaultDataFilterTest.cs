using System;
using System.Collections.Generic;

using Xunit;
using FluentFilter;
using System.Linq;
using OhPrimitives;

namespace FluentFilterTest
{
    public class DefaultDataFilterTest
    {
        [Fact]
        public void DefaultDataFilter_ShouldBeOK_Test()
        {
            var filter = new MyOrderDataFilter();
            filter.OrderId = new CompareField<int>(1000);
            filter.CreationTime = new CompareField<DateTime>(DateTime.Now);
        }

        [Fact]
        public void DefaultDataFilter_ApplyFilter_ShouldBeOK_Test()
        {
            var filter = new MyOrderDataFilter();
            filter.OrderId = new CompareField<int>(1000);
            filter.CreationTime = new CompareField<DateTime>(DateTime.Now);

            var _orderList = new List<MyOrder>() {
                new MyOrder(){ OrderId=1000,OrderFee=100.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1001,OrderFee=101.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1002,OrderFee=102.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1003,OrderFee=103.03M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1004,OrderFee=104.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1005,OrderFee=108.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1006,OrderFee=199.99M,CreationTime=DateTime.Now },
            }.AsQueryable();

            var _query = from a in _orderList
                         where (a.OrderId >= 1000 && a.OrderId <= 1002) || (a.OrderFee == 199.99M)
                         orderby a.CreationTime descending, a.OrderId ascending, a.OrderFee ascending
                         select a;

            var _newQuery = _query.ApplyFluentFilter(filter);
        }
    }

    public class MyOrder
    {
        public int OrderId { get; set; }
        public decimal OrderFee { get; set; }
        public DateTime CreationTime { get; set; }
    }
    public class MyOrderDataFilter : DefaultDataFilter<MyOrder, MyOrderDataFilter>
    {
        public CompareField<int> OrderId { get; set; }
        public CompareField<DateTime> CreationTime { get; set; }
    }
}
