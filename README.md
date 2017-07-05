# FluentFilter
FluentFilter 是一个 .net linq query 查询扩展。它将查询条件（Where）和排序功能（Order）进行抽象，并且将它们结构化为一系列称为字段的对象（class），然后转换为Expression Tree应用到IQueryable<T>上，从而完成查询过滤和排序功能。

## FluentFilter 词汇表
1. DataFilter（称为：数据过滤器）它实现了IDataFilter接口，作用于某个Queryable<T>，从而实现Where条件及OrderBy排序功能，是FluentFilter的基本组成单元。
2. FilterField（称为：过滤字段）DateFilter是由一个或多个FilterField组成的，FilterField分为排序和筛选（筛选字段包括排序功能）两种。筛选类的字段，又分为

a. 单值比较（EqualField<TPrimitive>,CompareField<TPrimitive>）等于，大于，大于等于，小于等于

b. 多值比较（Contains<TPrimitive>）包含，不包含

c. 区间比较（FreeDomRange<TPrimitive>,BetweenRange<TPrimitive>,Range<TPrimitive>）大于[等于] && 小于[等于]，大于等于 && 小于等于，大于 && 小于 。

## Nuget Package
`PM> Install-Package FluentFilter -Pre`

## Simple demo
```csharp
///<summary>
/// Order
///</summary>
public class Order{
  public int OrderId{get;set;}
  public DateTime CreationTime {get;set;}
}

///<summary>
/// OrderFilter
///</summary>
public class OrderFilter:DefaultFilter<Order,OrderFilter>{
  public EqualField<int> OrderId{get;set;}
  public FreeDomRangeField<DateTime> CreationTime {get;set;}
}

// 订单数据源（实际项目中通常由DbContext或者仓储层[Repository]提供）
var orderSources = new List<Order>{
  new Order{OrderId=1,CreationTime=DateTime.Now},
  new Order{OrderId=2,CreationTime=DateTime.Now},
}.AsQueryable();

// 创建一个OrderSources的数据过滤器，在实际项目中，这些参数通常由前端传到后台，后台通过
// 参数反序列化或者由框架提供的参数绑定，比如 ASP.NET MVC 的参数绑定功能
var orderFilter = new OrderFilter{
  OrderId = new EqualField<int>{
    Value = 1,
  }
}

// 应用过滤器，完成数据过滤功能，该结果最终会筛选出订单号等于1的订单记录
var result = orderSources.ApplyFluentFilter(orderFilter);
```
注：以上代码仅简单示例，更详细使用手册请查阅：https://github.com/oceanho/FluentFilterSample 或 相关文档、

## 我要贡献
1. 您在使用fluentfilter,发现有任何bug或想法，请一定提issue给我，谢谢
2. 您可以为本项目翻译不同语言版本的文档（本项目暂时未提供完整的文档，文档正在准备中。。。）
3. 为本项目完善已实现的feature，实现更多的feature，让它变得更完善，更好用。
