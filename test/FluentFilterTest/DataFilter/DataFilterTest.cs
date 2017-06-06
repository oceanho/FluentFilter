using FluentFilter;
using Xunit;

namespace FluentFilterTest.DataFilter
{
    public class DataFilterTest
    {
        public DataFilterTest() { }

        [Fact]
        public void DataEntityFilterSerialize_ShouldBeOK_Test()
        {
            var obj = new MyFilterEntity()
            {
                Id = new CompareField<int>() { Value = 100 },
                Price = new RangeField<decimal>() { Min = 50, Max = 1000 }
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
    public class MyEntity
    {
        public decimal Price { get; set; }
    }
    public class MyFilterEntity : DefaultDataFilter<MyEntity, MyFilterEntity>
    {
        public CompareField<int> Id { get; set; }
        public RangeField<decimal> Price { get; set; }
    }
}
