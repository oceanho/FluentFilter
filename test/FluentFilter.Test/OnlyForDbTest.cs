#if DBTEST
using Xunit;
using System.Linq;
using OhPrimitives;
namespace FluentFilter.Test
{

    public class OnlyForDbTest : FluentFilterTestBase
    {

        #region Verify_HasOneShouldBeWork

        [Fact]
        public void Verify_HasOneShouldBeWork()
        {
            var orderQuery = OrderDataSoures.AsQueryable();
            var orderFilter = new MyOrderFilter()
            {
                Id = new CompareField<int>
                {
                    CompareMode = CompareMode.Equal,
                    SortMode = SortMode.Asc,
                    Value = 1000
                }
            };

            var _newQuery = orderQuery.ApplyFluentFilter(orderFilter);
            Assert.Equal(1, _newQuery.ToList().Count);
            #endregion
        }
    }
}
#endif
