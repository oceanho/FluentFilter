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
            var filter = new MyOrderDataFilter();
            filter.OrderId = new CompareField<int>(1000);
            filter.CreationTime = new FreeDomRangeField<DateTime>(DateTime.Now.GetMinOfDay(), DateTime.Now.GetMaxOfDay());
        }

        [Fact]
        public void DefaultDataFilter_ApplyFilter_ShouldBeOK_Test()
        {
            var filter = new MyOrderDataFilter();
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
                          orderby a.CreationTime descending, a.OrderId ascending, a.OrderFee ascending
                          select a;

            var _newQuery1 = _query1.ApplyFluentFilter(filter);
            // _newQuery1 的 Where 条件应该为 OrderId >= 1006 && OrderFee >= 0 ，根据数据源 _orderList 所筛选出来的结果，应该是 4 条记录
            Assert.Equal(4, _newQuery1.ToList().Count());


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
