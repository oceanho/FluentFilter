﻿using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Shouldly;
using Fluent.DataFilter;
using System.Linq;

namespace Fluent.DataFilterTest
{
    public class DefaultDataFilterTest
    {
        [Fact]
        public void DefaultDataFilter_ShouldBeOK_Test()
        {
            var filter = new MyOrderDataFilter();
            filter.OrderId = 10000;
            filter.CreationTime = DateTime.Now;

            var expression = filter.ToExpression();
        }

        [Fact]
        public void DefaultDataFilter_ApplyFilter_ShouldBeOK_Test()
        {
            var filter = new MyOrderDataFilter();
            filter.OrderId = 10000;
            filter.CreationTime = DateTime.Now;

            var expression = filter.ToExpression();

            var _orderList = new List<MyOrder>() {
                new MyOrder(){ OrderId=1000,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1001,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1002,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1003,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1004,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1005,CreationTime=DateTime.Now },
                new MyOrder(){ OrderId=1006,CreationTime=DateTime.Now },
            }.AsQueryable();

            var _query = from a in _orderList
                         where a.OrderId > 0 && a.CreationTime > DateTime.Now.AddDays(-1)
                         select a;
            var _newQuery = _query.ApplyDataFilter(filter);
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