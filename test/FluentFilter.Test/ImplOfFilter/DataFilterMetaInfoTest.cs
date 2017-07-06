using Xunit;
using FluentFilter;
using System.Linq;
using OhPrimitives;
using OhDotNetLib.Extension;
using FluentFilter.Inetnal.ImplOfFilter;
using System.Linq.Expressions;
using FluentFilter.Inetnal.ImplOfFilter.Utils;

namespace FluentFilter.Test.ImplOfFilter
{
    public class DataFilterMetaInfoTest : FluentFilterTestBase
    {
        #region Verify_BasicOperationShouldBeWork

        [Fact]
        public void Verify_BasicOperationShouldBeWork()
        {
            var myfilter = new MyOrderFilter();
            var myfilterInfo = DataFilterMetaInfoHelper.GetFilterMeatInfo(myfilter);
            Assert.Equal(0, myfilterInfo.FilterFields.Count);

            myfilter.OrderId = new CompareField<int>
            {
                Value = 1000,
                CompareMode = CompareMode.GreaterThanOrEqual
            };
            myfilterInfo = DataFilterMetaInfoHelper.GetFilterMeatInfo(myfilter);
            Assert.Equal(1, myfilterInfo.FilterFields.Count);


            myfilter.State = new ContainsField<OrderState>
            {
                CompareMode = CompareMode.Contains,
                Values = new OrderState[] { OrderState.Completed }
            };
            myfilterInfo = DataFilterMetaInfoHelper.GetFilterMeatInfo(myfilter);
            Assert.Equal(2, myfilterInfo.FilterFields.Count);
        }
        #endregion
    }
}
