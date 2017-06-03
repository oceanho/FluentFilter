using System;
using System.Linq.Expressions;
using Abp.DataFilter.DataFilter;
using Abp.DataFilter.DataFilter.Fields;
using Abp.Domain.Entities;
using Xunit;

namespace Abp.DataFilterTest.DataFilter
{
    public class DataFilterTest
    {
        public DataFilterTest(){}

        [Fact]
        public void DataEntityFilterSerialize_ShouldBeOK_Test()
        {
            var obj = new MyFilterEntity()
            {
                Id = new EqualField<int>() { Field = 100 },
                Price = new RangeField<decimal>() { Min = 50, Max = 1000 }
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
    public class MyEntity:Entity<int>
    {
        public decimal Price { get; set; }
    }
    public class MyFilterEntity :AbstactDataFilter<MyEntity>
    {
        public EqualField<int> Id { get; set; }
        public RangeField<decimal> Price { get; set; }
    }
}
