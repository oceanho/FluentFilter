using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abp.DataFilter.DataFilter;
using Abp.DataFilter.DataFilter.Fields;
using Abp.Domain.Entities;
using Xunit;
using Abp.Timing;
using Abp.DataFilter.DataFilter.Extensions;

namespace Abp.DataFilterTest.DataFilter
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
                    Filter = new MyGroupEntity
                    {
                        Id = 100000,
                        Price = 1000001,
                        CreationTime = new BetweenAndField<DateTime>
                        {
                            Start = Clock.Now.ToDayOfStart(),
                            Finish = Clock.Now.ToDayOfFinish()
                        }
                    }
                },

                Kind = GroupFilterKind.Default,

                Right = new MyGroupFilterEntity()
                {
                    Filter = new MyGroupEntity
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
        public EqualField<Int32> Id { get; set; }
        public EqualField<Decimal> Price { get; set; }
        public BetweenAndField<DateTime> CreationTime { get; set; }
    }
    public class MyGroupFilterEntity : DefaultDataFilter<MyGroupEntity>, IGroupFilter<MyGroupEntity, MyGroupFilterEntity>
    {
        public GroupFilterKind Kind { get; set; }
        public MyGroupFilterEntity Left { get; set; }
        public MyGroupFilterEntity Right { get; set; }
    }
}
