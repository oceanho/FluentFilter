using FluentFilter;
using FluentFilter.Mappings;
using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentFilter.Test
{
    /// <summary>
    /// FluentFilter 测试基类，用于提供测试所需的数据源
    /// </summary>
    public abstract class FluentFilterTestBase
    {
        public FluentFilterTestBase()
        {
            DataSoures = new List<MyOrder>() {
                new MyOrder(){ UserId=1000,OrderId=1000, OrderState= OrderState.Cancel, OrderFee=100.00M,CreationTime=DateTime.Now },
                new MyOrder(){ UserId=1000,OrderId=1001, OrderState= OrderState.Completed, OrderFee=101.00M,CreationTime=DateTime.Now },
                new MyOrder(){ UserId=1001,OrderId=1002, OrderState= OrderState.Completed, OrderFee=102.00M,CreationTime=DateTime.Now },
                new MyOrder(){ UserId=1002,OrderId=1003, OrderState= OrderState.Completed, OrderFee=103.03M,CreationTime=DateTime.Now },
                new MyOrder(){ UserId=1003,OrderId=1004, OrderState= OrderState.Completed, OrderFee=104.00M,CreationTime=DateTime.Now },
                new MyOrder(){ UserId=1003,OrderId=1005, OrderState= OrderState.Completed, OrderFee=108.00M,CreationTime=DateTime.Now },
                new MyOrder(){ UserId=2009,OrderId=1009, OrderState= OrderState.Completed, OrderFee=199.99M,CreationTime=DateTime.Now,OrderRemarks="Left查找" },
                new MyOrder(){ UserId=2000,OrderId=1008, OrderState= OrderState.Completed, OrderFee=199.99M,CreationTime=DateTime.Now,OrderRemarks="查找Right" },
                new MyOrder(){ UserId=1010,OrderId=1007, OrderState= OrderState.Completed, OrderFee=199.99M,CreationTime=DateTime.Now,OrderRemarks="Full查找1"},
                new MyOrder(){ UserId=1004,OrderId=1006, OrderState= OrderState.Completed, OrderFee=199.99M,CreationTime=DateTime.Now,OrderRemarks="Full查找2"},
            }.AsQueryable();
        }
        protected IQueryable<MyOrder> DataSoures { get; }
    }

    public class MyOrder
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public decimal OrderFee { get; set; }
        public OrderState OrderState { get; set; }
        public DateTime CreationTime { get; set; }
        public String OrderRemarks { get; set; }
    }

    public enum OrderState
    {
        Completed = 1,
        Cancel = 2
    }

    public class MyOrderFilter : DefaultDataFilter<MyOrder, MyOrderFilter>
    {
        public CompareField<int> OrderId { get; set; }
        public ContainsField<int> UserId { get; set; }

        [FilterExprName("OrderState")]
        public ContainsField<OrderState> State { get; set; }

        [FilterExprName("OrderFee")]
        public CompareField<decimal> TotalFee { get; set; }

        public FreeDomRangeField<DateTime> CreationTime { get; set; }

        [FilterExprName("OrderRemarks")]
        public LikeField Remarks { get; set; }
    }
}
