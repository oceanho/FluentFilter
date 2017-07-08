using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Shouldly;
using FluentFilter;
using FluentFilter.Mappings;
using OhPrimitives;
using System.Linq;

namespace FluentFilter.Test.Mappings
{
    public class DisableFieldExprAttributeTest : FluentFilterTestBase
    {
        public DisableFieldExprAttributeTest()
        {
            FluentFilterManager.Reset();
            FluentFilterManager.AddMapping(new MyOrderOfMappingTestFilterFieldMapping());
        }

        #region Verify_DisableFieldExprAttributeShouldBeWork

        [Fact]
        public void Verify_DisableFieldExprAttributeShouldBeWork()
        {
            var myfilter = new MyOrderOfMappingTestFilter();

            #region [DisableFieldExpr(DisableFieldExprMode.Filter)]
            myfilter.Id.Value = 0;
            myfilter.Id.CompareMode = CompareMode.Equal;

            var _orderQuery = OrderDataSources.AsQueryable();
            var _neworderQuery = _orderQuery.ApplyFluentFilter(myfilter);
            var _neworderQueryedList = _neworderQuery.ToList();

            // 过滤条件应该永远不会起作用，但是可以排序（因为在属性Id上应用了[DisableFieldExpr(DisableFieldExprMode.Filter)]标记）
            Assert.Equal(10, _neworderQueryedList.Count);

            // 降序排列，第一个最大，最后一个最小
            var last = _neworderQueryedList.Last();
            var first = _neworderQueryedList.First();

            Assert.True(last.Id < first.Id);
            #endregion

            #region [DisableFieldExpr(DisableFieldExprMode.Sort)]

            // 清空 Id 字段
            myfilter.Id = null;

            // 筛选条件能生效
            myfilter.UserId = new CompareField<int>
            {
                Value = 1001,
                CompareMode = CompareMode.GreaterThanOrEqual
            };

            // 排序无论怎么设置，都应该无效
            myfilter.UserId.SortMode = SortMode.Asc;

            var _orderQuery2 = OrderDataSources.AsQueryable();
            var _neworderQuery2 = _orderQuery2.ApplyFluentFilter(myfilter);
            var _neworderQueryedList2 = _neworderQuery2.ToList();

            // 验证条件过滤生效
            Assert.Equal(8, _neworderQueryedList2.Count);

            // 验证：不应包含任何排序字眼
            Assert.DoesNotContain("OrderBy", _neworderQuery2.Expression.ToString());
            Assert.DoesNotContain("OrderByDescending", _neworderQuery2.Expression.ToString());
            Assert.DoesNotContain("ThenBy", _neworderQuery2.Expression.ToString());
            Assert.DoesNotContain("ThenByDescending", _neworderQuery2.Expression.ToString());

            #endregion

            #region [DisableFieldExpr(DisableFieldExprMode.SortAndFilter)]

            // 清空 UserId 字段
            myfilter.UserId = null;
            myfilter.OrderState = new ContainsField<OrderState>
            {
                CompareMode = CompareMode.Contains,
                SortMode = SortMode.Asc,
                Values = new OrderState[] { OrderState.Cancel }
            };

            var _orderQuery3 = OrderDataSources.AsQueryable();
            var _neworderQuery3 = _orderQuery3.ApplyFluentFilter(myfilter);
            var _neworderQueryedList3 = _neworderQuery3.ToList();

            // 验证条件过滤生效
            Assert.Equal(10, _neworderQueryedList3.Count);

            // 验证：不应包含任何排序字眼
            Assert.DoesNotContain("OrderBy", _neworderQuery3.Expression.ToString());
            Assert.DoesNotContain("OrderByDescending", _neworderQuery3.Expression.ToString());
            Assert.DoesNotContain("ThenBy", _neworderQuery2.Expression.ToString());
            Assert.DoesNotContain("ThenByDescending", _neworderQuery3.Expression.ToString());
            #endregion

            // 清空条件
            myfilter.OrderState = null;

            myfilter.OrderFee = new CompareField<decimal>
            {
                Value = 104.00M,
                CompareMode = CompareMode.GreaterThanOrEqual,
                SortPriority = 1,
                SortMode = SortMode.Desc,
            };

            var _orderQuery4 = OrderDataSources.AsQueryable();
            var _neworderQuery4 = _orderQuery4.ApplyFluentFilter(myfilter);
            var _neworderQueryedList4 = _neworderQuery4.ToList();

            // 验证条件过滤生效
            Assert.Equal(6, _neworderQueryedList4.Count);

            // 验证：应包含排序、筛选字眼
            Assert.Contains("Where", _neworderQuery4.Expression.ToString());
            Assert.Contains("OrderBy", _neworderQuery4.Expression.ToString());

            // 验证：最后一个订单的金额必须大于或等于第一个订单的金额
            Assert.True(_neworderQueryedList4.First().OrderFee >= _neworderQueryedList4.Last().OrderFee);
        }
        #endregion
    }

    internal class MyOrderOfMappingTestFilter : DefaultDataFilter<MyOrder, MyOrderOfMappingTestFilter>
    {
        public MyOrderOfMappingTestFilter()
        {
            Id = new CompareField<int>()
            {
                SortMode = SortMode.Desc,
                SortPriority = 1
            };
        }

        [DisableFieldExpr(DisableFieldExprMode.Filter)]
        public CompareField<int> Id { get; set; }

        [DisableFieldExpr(DisableFieldExprMode.Sort)]
        public CompareField<int> UserId { get; set; }

        public CompareField<decimal> OrderFee { get; set; }

        [DisableFieldExpr(DisableFieldExprMode.SortAndFilter)]
        public ContainsField<OrderState> OrderState { get; set; }

    }

    internal class MyOrderOfMappingTestFilterFieldMapping : DefaultExprNameMapping<MyOrderOfMappingTestFilter>
    {
        public override MappingInfo[] Mapping()
        {
            var maps = base.Mapping();

            return maps;
        }
    }
}
