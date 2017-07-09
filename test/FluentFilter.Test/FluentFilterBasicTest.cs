using System;
using System.Collections.Generic;

using Xunit;
using FluentFilter;
using System.Linq;
using OhPrimitives;
using OhDotNetLib.Extension;
namespace FluentFilter.Test
{
    public class FluentFilterBasicTest : FluentFilterTestBase
    {
        #region Verify_OnlySortShouldBeWork

        [Fact]
        public void Verify_OnlySortShouldBeWork()
        {
            var _orderList = OrderDataSources;

            //  OrderId主键倒序排列,详细见 MyOrderFilter 构造函数
            var filter = new MyOrderFilter();

            var _query = _orderList;
            var _newQuery = _query.ApplyFluentFilter(filter);
            var _newQueryList = _newQuery.ToList();
            for (int i = 0; i < 10; i++)
            {
                // 排序出来的结果，订单号应该是(从1009递减到1)
                Assert.Equal((1009 - i), _newQueryList[i].Id);
            }
        }
        #endregion

        #region Verify_HasWhereExprShouldBeWork

        [Fact]
        public void Verify_HasWhereExprShouldBeWork()
        {
            var _orderList = OrderDataSources;
            var filter = new MyOrderFilter
            {
                Id = new EqualsField<int>
                {
                    Value = 1000
                }
            };

            var _query = from a in _orderList
                         where a.Id > 0
                         select a;

            var _newQuery = _query.ApplyFluentFilter(filter);
            Assert.Equal(1, _newQuery.ToList().Count());
        }
        #endregion

        #region Verify_HasWhereAndSortExprShouldBeWork

        [Fact]
        public void Verify_HasWhereAndSortExprShouldBeWork()
        {
            var _orderList = OrderDataSources;

            /* 
             * 查询订单号 大于等于 1000 且 订单金额大于等于 100 ，按金额升序，然后订单号降序 后的结果列表
             */

            var filter = new MyOrderFilter
            {
                // 订单号大于等于 1002
                Id = new CompareField<int>
                {
                    Value = 1002,
                    CompareMode = CompareMode.GreaterThanOrEqual,
                    SortMode = SortMode.Desc,
                    SortPriority = 100
                },
                // 订单金额大于等于 100
                TotalFee = new CompareField<decimal>
                {
                    Value = 100,
                    CompareMode = CompareMode.GreaterThanOrEqual,
                    SortMode = SortMode.Asc,
                    SortPriority = 100
                }
            };

            var _query = _orderList.OrderBy(p => p.Id);

            var _newQuery = _query.ApplyFluentFilter(filter);

            Assert.Equal(8, _newQuery.ToList().Count());

            var str = _newQuery.Expression.ToString();
            Assert.Contains(".ThenBy(p => p.OrderFee).ThenByDescending(p => p.Id)", str);
        }
        #endregion

        #region Verify_HasWhereButNoSortExprShouldBeWork

        [Fact]
        public void Verify_HasWhereButNoSortExprShouldBeWork()
        {
            var _orderList = OrderDataSources;

            /* 
             * 查询订单号 大于等于 1000 且 订单金额大于等于 100 ，按金额升序，然后订单号降序 后的结果列表
             */

            var filter = new MyOrderFilter
            {
                // 订单号大于等于 1002
                Id = new CompareField<int>
                {
                    Value = 1002,
                    CompareMode = CompareMode.GreaterThanOrEqual,
                    SortMode = SortMode.Desc,
                    SortPriority = 1
                },
                // 订单金额大于等于 100
                TotalFee = new CompareField<decimal>
                {
                    Value = 100.0M,
                    CompareMode = CompareMode.GreaterThanOrEqual,
                    SortMode = SortMode.Asc,
                    SortPriority = 1
                }
            };

            var _query = _orderList.AsQueryable();

            var _newQuery = _query.ApplyFluentFilter(filter);

            Assert.Equal(8, _newQuery.ToList().Count());

            var lastOrderFee = _newQuery.ToList().Last().OrderFee;
            var firsrtOrderFee = _newQuery.ToList().First().OrderFee;
            Assert.True(lastOrderFee >= firsrtOrderFee);

            var exprStr = _newQuery.Expression.ToString();
            Assert.EndsWith(".Where(OhLq_P1 => ((OhLq_P1.Id >= 1002) AndAlso (OhLq_P1.OrderFee >= 100.0))).OrderBy(OhLq_P1 => OhLq_P1.OrderFee).ThenByDescending(OhLq_P1 => OhLq_P1.Id)", exprStr);

            // 只按订单号筛选，倒叙排列（禁用SortMode 和 关闭FilterSwitch）
            filter.TotalFee.SortMode = SortMode.Disable;
            filter.TotalFee.FilterSwitch = FilterSwitch.Close;

            filter.Id.CompareMode = CompareMode.Equal;

            var _newQuery2 = _query.ApplyFluentFilter(filter);
            exprStr = _newQuery2.Expression.ToString();

            Assert.Equal(1, _newQuery2.ToList().Count);
            Assert.EndsWith(".Where(OhLq_P1 => (OhLq_P1.Id == 1002)).OrderByDescending(OhLq_P1 => OhLq_P1.Id)", exprStr);
        }
        #endregion

        #region Verify_DataFilterApplyFilterShouldBeWork

        [Fact]
        public void Verify_DataFilterApplyFilterShouldBeWork()
        {
            var orders = OrderDataSources;
            var orderFilter = new MyOrderFilter();
            orderFilter.Id = new CompareField<int>(1006)
            {
                CompareMode = CompareMode.GreaterThanOrEqual
            };

            orderFilter.TotalFee = new CompareField<decimal>
            {
                Value = 0.0M,
                CompareMode = CompareMode.GreaterThanOrEqual,
            };

            orderFilter.CreationTime = new FreeDomRangeField<DateTime>(DateTime.Now.GetMinOfDay(), DateTime.Now.GetMaxOfDay());

            var _query1 = from a in orders
                          select a;
            var _newQuery1 = _query1.ApplyFluentFilter(orderFilter);
            // _newQuery1 的 Where 条件应该为 OrderId >= 1006 && OrderFee >= 0 ，根据数据源 _orderList 所筛选出来的结果，应该是 4 条记录
            Assert.Equal(4, _newQuery1.ToList().Count());

            #region 测试ContainsField

            // ----------------测试ContainsField<int>--------------------//
            // 清空 OrderId / TotalFee / CreationTime 查询条件 -
            orderFilter.Id = null;
            orderFilter.TotalFee = null;
            orderFilter.CreationTime = null;
            orderFilter.UserId = new ContainsField<int>
            {
                Values = new int[] { 2009 }
            };
            var _query2 = from a in orders
                          select a;
            var _newQuery2 = _query2.ApplyFluentFilter(orderFilter);
            Assert.Equal(1, _newQuery2.ToList().Count());
            Assert.Equal(2009, _newQuery2.ToList().FirstOrDefault().UserId);


            // ----------------测试ContainsField<Enum>--------------------//
            // 清空 OrderId / TotalFee / CreationTime 查询条件 -
            orderFilter.UserId = null;
            orderFilter.State = new ContainsField<OrderState>
            {
                Values = new OrderState[] { OrderState.Cancel }
            };
            var _query3 = from a in orders
                          select a;
            var _newQuery3 = _query3.ApplyFluentFilter(orderFilter);
            Assert.Equal(1, _newQuery3.ToList().Count());
            Assert.Equal(OrderState.Cancel, _newQuery3.ToList().FirstOrDefault().OrderState);

            // 改变 State 的查询条件为不包含（查询出的结果应该是9条）
            orderFilter.State.CompareMode = CompareMode.NotContains;
            var _query4 = from a in orders
                          select a;
            var _newQuery4 = _query4.ApplyFluentFilter(orderFilter);
            Assert.Equal(9, _newQuery4.ToList().Count());
            Assert.Equal(OrderState.Completed, _newQuery4.ToList().FirstOrDefault().OrderState);
            #endregion

            #region 测试LikeField

            // ----------------测试LikeField--------------------//            
            orderFilter.State = null;
            orderFilter.Remarks = new LikeField
            {
                CompareMode = CompareMode.LeftLike,
                Value = "Left"
            };
            var _query5 = from a in orders
                          select a;
            var _newQuery5 = _query5.ApplyFluentFilter(orderFilter);
            Assert.Equal(1, _newQuery5.ToList().Count());
            Assert.Equal("Left查找", _newQuery5.ToList().FirstOrDefault().OrderRemarks);


            //  Right Search
            orderFilter.Remarks = new LikeField
            {
                CompareMode = CompareMode.RightLike,
                Value = "Right"
            };
            var _query6 = from a in orders
                          select a;
            var _newQuery6 = _query6.ApplyFluentFilter(orderFilter);
            Assert.Equal(1, _newQuery6.ToList().Count());
            Assert.Equal("查找Right", _newQuery6.ToList().FirstOrDefault().OrderRemarks);

            // Full Search
            orderFilter.Remarks = new LikeField
            {
                CompareMode = CompareMode.FullSearchLike,
                Value = "ll查"
            };
            var _query7 = from a in orders
                          select a;
            var _newQuery7 = _query7.ApplyFluentFilter(orderFilter);
            Assert.Equal(2, _newQuery7.ToList().Count());
            Assert.Contains("Full查找", _newQuery7.ToList().LastOrDefault().OrderRemarks);
            Assert.Contains("Full查找", _newQuery7.ToList().FirstOrDefault().OrderRemarks);

            #endregion

            #region EqualField test

            // EqualField
            orderFilter.Remarks = null;
            orderFilter.Id = new EqualsField<int>
            {
                Value = 1000,
            };
            var _query8 = from a in orders
                          select a;
            var _newQuery8 = _query8.ApplyFluentFilter(orderFilter);
            Assert.Equal(1, _newQuery8.ToList().Count());
            Assert.Equal(1000, _newQuery8.ToList().LastOrDefault().Id);

            #endregion

            //var _query2 = from a in _orderList
            //              orderby a.CreationTime descending, a.OrderId ascending, a.OrderFee ascending
            //              select a;

            //var _newQuery2 = _query2.ApplyFluentFilter(filter);
            //// _newQuery1 的 Where 条件应该为 OrderId >= 1006 && OrderFee >= 0 ，根据数据源 _orderList 所筛选出来的结果，应该是 4 条记录
            //Assert.Equal(4, _newQuery2.ToList().Count());


            //  当 Query 中存在 Where 筛选条件，目前（2017-07-02）的实现有问题，需要重新实现后再进行单元测试。以下测试先屏蔽了
            //var _query = from a in _orderList
            //             where a.OrderId > 0
            //             orderby a.CreationTime descending, a.OrderId ascending, a.OrderFee ascending
            //             select a;

            //var _newQuery = _query.ApplyFluentFilter(filter);
            //Assert.Equal(_newQuery.ToList().Count(), 4);
        }
        #endregion
    }
}
