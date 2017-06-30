using System;

using Xunit;
using Shouldly;

using FluentFilter;
using FluentFilter.Extensions;
using OhPrimitives;

namespace FluentFilter.Test.Fields
{
    using OhDotNetLib.Extension;

    public class AndAlsoFieldTest
    {
        [Fact]
        public void DateTime_ShouldBeOK_Test()
        {
            var andAlsoField = new RangeField<DateTime>()
            {
                Min = DateTime.Now.GetMinOfDay(),
                Max = DateTime.Now.GetMaxOfDay()
            };
            //andAlsoField.IsSatisfy().ShouldBe(true);

            //andAlsoField.Min = andAlsoField.Max;
            //andAlsoField.IsSatisfy().ShouldBe(false);

            //andAlsoField.Min = null;
            //andAlsoField.IsSatisfy().ShouldBe(true);

            //andAlsoField.Min = andAlsoField.Max = null;
            //andAlsoField.IsSatisfy().ShouldBe(false);
        }

        [Fact]
        public void Int_ShouldBeOK_Test()
        {
            var compareField = new CompareField<int>();
        }
    }
}
