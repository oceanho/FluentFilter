using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Fluent.DataFilter;

using Xunit;
using Fluent.DataFilter.Extensions;

namespace Fluent.DataFilterTest.DataFilter
{
    public class GroupFilterTest
    {
        public GroupFilterTest() { }

        [Fact]
        public void DataEntityGroupFilterSerialize_ShouldBeOK_Test()
        {
            var obj = new MyGroupFilterEntity()
            {
                Left = new MyGroupFilterEntity()
                {
                    EntityOfFilter = new MyGroupEntity
                    {
                        Id = 100000,
                        Price = 1000001,
                        CreationTime = new RangeField<DateTime>
                        {
                            Min = DateTime.Now.ToDayOfStart(),
                            Max = DateTime.Now.ToDayOfFinish()
                        }
                    }
                },

                Kind = GroupFilterKind.Default,

                Right = new MyGroupFilterEntity()
                {
                    EntityOfFilter = new MyGroupEntity
                    {
                        Id = 200000,
                        Price = 2000001
                    }
                }
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }

    public class MyGroupEntity : DefaultDataFilter<MyGroupEntity>
    {
        public CompareField<Int32> Id { get; set; }
        public CompareField<Decimal> Price { get; set; }
        public RangeField<DateTime> CreationTime { get; set; }
    }
    public class MyGroupFilterEntity : DefaultDataFilter<MyGroupEntity>, IGroupFilter<MyGroupEntity, MyGroupFilterEntity>
    {
        public GroupFilterKind Kind { get; set; }
        public MyGroupFilterEntity Left { get; set; }
        public MyGroupFilterEntity Right { get; set; }
    }
}