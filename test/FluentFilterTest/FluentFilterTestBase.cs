using FluentFilter;
using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentFilterTest
{
    public abstract class FluentFilterTestBase
    {
        public FluentFilterTestBase()
        {
            DataSoures = new List<MyOrder>() {
                new MyOrder(){ OrderId=1000,OrderFee=100.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1001,OrderFee=101.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1002,OrderFee=102.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1003,OrderFee=103.03M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1004,OrderFee=104.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1005,OrderFee=108.00M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1006,OrderFee=199.99M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1007,OrderFee=199.99M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1008,OrderFee=199.99M,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1009,OrderFee=199.99M,CreationTime=DateTime.Now },
            }.AsQueryable();
        }
        protected IQueryable<MyOrder> DataSoures { get; }
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
        public CompareField<decimal> OrderFee { get; set; }
        public FreeDomRangeField<DateTime> CreationTime { get; set; }
    }
}
