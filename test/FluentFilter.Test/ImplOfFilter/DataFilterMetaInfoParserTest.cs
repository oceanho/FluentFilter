using Xunit;
using FluentFilter;
using System.Linq;
using OhPrimitives;
using OhDotNetLib.Extension;
using FluentFilter.Inetnal.ImplOfFilter;
using System.Linq.Expressions;

namespace FluentFilter.Test.ImplOfFilter
{
    public class DataFilterMetaInfoParserTest : FluentFilterTestBase
    {
        #region Verity_ParseContainsField<T>ShouldBeWork
        [Fact]
        public void Verity_ParseContainsFieldShouldBeWork()
        {
            var filter = new MyOrderFilter()
            {
                UserId = new ContainsField<int>
                {
                    CompareMode = CompareMode.Contains,
                    SortMode = SortMode.Asc,
                    Values = new int[] { 10000 }
                }
            };

            var exprTree2 = DataFilterMetaInfoParser.Parse<MyOrder>(filter);
            Assert.Equal(exprTree2.NodeType, ExpressionType.Lambda);
            Assert.Equal(exprTree2.ToString(), "OhLq_P1 => value(System.Int32[]).Contains(OhLq_P1.UserId)");

            var exprTree3 = DataFilterMetaInfoParser.Parse<MyOrder>(filter, "_myParam");
            Assert.Equal(exprTree3.NodeType, ExpressionType.Lambda);
            Assert.Equal(exprTree3.ToString(), "_myParam => value(System.Int32[]).Contains(_myParam.UserId)");
        }
        #endregion
    }
}
