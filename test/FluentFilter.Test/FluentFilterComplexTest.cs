using System;
using System.Collections.Generic;

using Xunit;
using FluentFilter;
using System.Linq;
using OhPrimitives;
using OhDotNetLib.Extension;
namespace FluentFilter.Test
{
    public class FluentFilterComplexTest : FluentFilterTestBase
    {
        #region Verify_DeepLvPropertyShouldBeWork

        [Fact]
        public void Verify_DeepLvPropertyShouldBeWork()
        {
            var _order = OrderDataSoures.OrderBy(p => p.Id).First();
            var _orderDetailList = OrderDetailDataSoures.Where(p => p.OrderId == _order.Id);
        }
        #endregion
    }
}
