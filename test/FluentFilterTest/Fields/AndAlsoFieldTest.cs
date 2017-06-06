using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Shouldly;

using FluentFilter;
using FluentFilter.Extensions;

namespace FluentFilterTest.Fields
{
    public class AndAlsoFieldTest
    {
        [Fact]
        public void DateTime_ShouldBeOK_Test()
        {
            var andAlsoField = new RangeField<DateTime>()
            {
                Min = DateTime.Now.ToDayOfStart(),
                Max = DateTime.Now.ToDayOfFinish()
            };
            andAlsoField.IsSatisfy().ShouldBe(true);

            andAlsoField.Min = andAlsoField.Max;
            andAlsoField.IsSatisfy().ShouldBe(false);

            andAlsoField.Min = null;
            andAlsoField.IsSatisfy().ShouldBe(true);

            andAlsoField.Min = andAlsoField.Max = null;
            andAlsoField.IsSatisfy().ShouldBe(false);
        }

        [Fact]
        public void Int_ShouldBeOK_Test()
        {
            var compareField = new CompareField<int>();
        }
    }
}
