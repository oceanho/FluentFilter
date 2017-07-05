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
        [Fact]
        public void Verity_ParseShouldBeWork()
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
            var exprTree = DataFilterMetaInfoParser.Parse<MyOrder>(filter, "_myParam");
            Assert.Equal(exprTree.NodeType, ExpressionType.Lambda);
            Assert.Equal(exprTree.ToString(), "_myParam => (True AndAlso value(System.Int32[]).Contains(_myParam.UserId))");
        }
    }
}
