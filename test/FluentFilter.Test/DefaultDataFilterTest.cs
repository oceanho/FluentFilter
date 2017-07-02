using System;
using System.Collections.Generic;

using Xunit;
using FluentFilter;
using System.Linq;
using OhPrimitives;
using OhDotNetLib.Extension;
namespace FluentFilter.Test
{
    public class DefaultDataFilterTest : FluentFilterTestBase
    {
        [Fact]
        public void DefaultDataFilter_ShouldBeOK_Test()
        {
            var filter = new MyOrderFilter();
            filter.OrderId = new CompareField<int>(1000);
            filter.CreationTime = new FreeDomRangeField<DateTime>(DateTime.Now.GetMinOfDay(), DateTime.Now.GetMaxOfDay());
        }

        [Fact]
        public void DefaultDataFilter_ApplyFilter_ShouldBeOK_Test()
        {
            var filter = new MyOrderFilter();
            filter.OrderId = new CompareField<int>(1006)
            {
                CompareMode = CompareMode.GreaterThanOrEqual
            };

            filter.TotalFee = new CompareField<decimal>
            {
                Value = 0.0M,
                CompareMode = CompareMode.GreaterThanOrEqual,
            };

            filter.CreationTime = new FreeDomRangeField<DateTime>(DateTime.Now.GetMinOfDay(), DateTime.Now.GetMaxOfDay());

            var _orderList = DataSoures;

            var _query1 = from a in _orderList
                          select a;
            var _newQuery1 = _query1.ApplyFluentFilter(filter);
            // _newQuery1 的 Where 条件应该为 OrderId >= 1006 && OrderFee >= 0 ，根据数据源 _orderList 所筛选出来的结果，应该是 4 条记录
            Assert.Equal(4, _newQuery1.ToList().Count());


            // ----------------测试ContainsField<int>--------------------//
            // 清空 OrderId / TotalFee / CreationTime 查询条件 -
            filter.OrderId = null;
            filter.TotalFee = null;
            filter.CreationTime = null;
            filter.UserId = new ContainsField<int>
            {
                Values = new int[] { 2009 }
            };
            var _query2 = from a in _orderList
                          select a;
            var _newQuery2 = _query2.ApplyFluentFilter(filter);
            Assert.Equal(1, _newQuery2.ToList().Count());
            Assert.Equal(2009, _newQuery2.ToList().FirstOrDefault().UserId);


            // ----------------测试ContainsField<Enum>--------------------//
            // 清空 OrderId / TotalFee / CreationTime 查询条件 -
            filter.UserId = null;
            filter.State = new ContainsField<OrderState>
            {
                Values = new OrderState[] { OrderState.Cancel }
            };
            var _query3 = from a in _orderList
                          select a;
            var _newQuery3 = _query3.ApplyFluentFilter(filter);
            Assert.Equal(1, _newQuery3.ToList().Count());
            Assert.Equal(OrderState.Cancel, _newQuery3.ToList().FirstOrDefault().OrderState);

            // 改变 State 的查询条件为不包含（查询出的结果应该是9条）
            filter.State.CompareMode = CompareMode.NotContains;
            var _query4 = from a in _orderList
                          select a;
            var _newQuery4 = _query4.ApplyFluentFilter(filter);
            Assert.Equal(9, _newQuery4.ToList().Count());
            Assert.Equal(OrderState.Completed, _newQuery4.ToList().FirstOrDefault().OrderState);


            //var _query2 = from a in _orderList
            //              orderby a.CreationTime descending, a.OrderId ascending, a.OrderFee ascending
            //              select a;

            //var _newQuery2 = _query2.ApplyFluentFilter(filter);
            //// _newQuery1 的 Where 条件应该为 OrderId >= 1006 && OrderFee >= 0 ，根据数据源 _orderList 所筛选出来的结果，应该是 4 条记录
            //Assert.Equal(4, _newQuery2.ToList().Count());


            //  当 Query 中存在 Where 筛选条件，目前（2017-07-02）的实现问题，需要完善实现后再进行单元测试。以下测试先屏蔽了
            //var _query = from a in _orderList
            //             where a.OrderId > 0
            //             orderby a.CreationTime descending, a.OrderId ascending, a.OrderFee ascending
            //             select a;

            //var _newQuery = _query.ApplyFluentFilter(filter);
            //Assert.Equal(_newQuery.ToList().Count(), 4);
        }
    }
}
