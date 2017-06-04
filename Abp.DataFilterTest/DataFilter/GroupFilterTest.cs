using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abp.DataFilter.DataFilter;
using Abp.DataFilter.DataFilter.Fields;
using Abp.Domain.Entities;
using Xunit;

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
                    Entity = new MyGroupEntity
                    {
                        Id=100000,
                        Price=1000001
                    }
                },

                Flag = GroupFilterKind.Default,

                Right = new MyGroupFilterEntity()
                {
                    Entity = new MyGroupEntity
                    {
                        Id = 200000,
                        Price = 2000001
                    }
                }
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }

    public class MyGroupEntity : MyEntity
    {
    }
    public class MyGroupFilterEntity : DefaultDataFilter<MyGroupEntity>, IGroupFilter<MyGroupEntity, MyGroupFilterEntity>
    {
        public MyGroupFilterEntity Left { get; set; }
        public MyGroupFilterEntity Right { get; set; }
        public GroupFilterKind Flag { get; set; }

        Expression<Func<MyGroupEntity, bool>> IDataFilter<MyGroupEntity>.ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
