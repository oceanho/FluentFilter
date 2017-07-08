# FluentFilter
FluentFilter 是一个 .net linq query 查询扩展。它将查询条件（Where）和排序功能（Order）进行抽象，并且将它们结构化为一系列称为字段的对象（class），然后转换为Expression Tree应用到IQueryable<T>上，从而完成查询过滤和排序功能。

## FluentFilter 词汇表
### 1、DataFilter（过滤器）
它实现了`IDataFilter`接口，作用于某个Queryable<T>，从而实现Where条件及OrderBy排序功能，是FluentFilter的基本组成单元。
### 2、FilterField（过滤字段）
`DateFilter` 由一个或多个FilterField组成的，是FluentFilter的最小组成单元。`FilterField` 分为排序和筛选（筛选字段通常拥有排序功能）两种。筛选类的字段，分为
#### a、单值比较（EqualField<TPrimitive>,CompareField<TPrimitive>）等于，大于，大于等于，小于等于
#### b、多值比较（Contains<TPrimitive>）包含，不包含
#### c、区间比较（FreeDomRange<TPrimitive>,BetweenRange<TPrimitive>,Range<TPrimitive>）大于[等于] && 小于[等于]，大于等于 && 小于等于，大于 && 小于 。
#### d、模糊查询（LikeField）COLUMN like '%XXX', COLUMN like 'XXX%', COLUMN like 'XX%YY'
#### e、仅排序功能（SortField）仅仅适用于排序，无数据过滤功能
### 3、FilterFieldHandler（过滤字段转换为Expression的处理Handler）
在FluentFilter中，所有筛选类型字段转换为Expression Tree，都是通过 FilterFieldHandler 实现的。

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

// 订单数据源（实际项目中通常由DbContext或者基础设施层的仓储[Repository]提供）
var orderSources = new List<Order>{
  new Order{OrderId=1,CreationTime=DateTime.Now},
  new Order{OrderId=2,CreationTime=DateTime.Now},
}.AsQueryable();

// 创建一个OrderSources的数据过滤器，在实际项目中，这些参数通常由前端通过表单的方式传给后台，后台通过
// 参数反序列化或者由框架提供的参数绑定（比如 ASP.NET MVC 的参数绑定功能）
var orderFilter = new OrderFilter{
  OrderId = new EqualField<int>{
    Value = 1,
  }
}

// 应用过滤器，完成数据过滤功能，该结果最终会筛选出订单号等于1的订单记录
var result = orderSources.ApplyFluentFilter(orderFilter);
```
注：以上代码仅简单示例，更详细使用方法，请查阅：https://github.com/oceanho/FluentFilterSample 或 相关文档、

## 怎么贡献？
1. 您在使用fluentfilter,发现有任何bug或想法，请一定提issue给我，谢谢
2. 您可以为本项目翻译不同语言版本的文档（本项目暂时未提供完整的文档，文档正在准备中。。。）
3. 为本项目完善已实现的feature，实现更多的feature，让它变得更完善，更好用。
