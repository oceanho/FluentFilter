using FluentFilter;
using FluentFilter.Mappings;
using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace FluentFilter.Test
{
    /// <summary>
    /// FluentFilter 测试数据 基类
    /// </summary>
    public abstract class FluentFilterTestDataBase
    {
        protected virtual IQueryable<MyOrder> OrderDataSoures => TempnoaryList.ToImmutableList().AsQueryable();
        protected virtual IQueryable<MyOrderDetail> OrderDetailDataSoures => TempnoaryList.ToImmutableList().SelectMany(p => p.Details).AsQueryable();
        protected virtual IQueryable<MyProduct> ProductDataSoures => TempnoaryList.ToImmutableList().SelectMany(p => p.Details).Select(l => l.ProductInfo).Distinct(new ProductEqualityComparer()).AsQueryable();

        protected List<MyOrder> TempnoaryList = new List<MyOrder>() {
                new MyOrder(){
                    Id=1000,UserId =1000, OrderState= OrderState.Cancel, OrderFee=100.00M,CreationTime=DateTime.Now,
                    Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1000,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=1,Name="测试产品1" } },
                        new MyOrderDetail(){OrderId=1000,ProductId=2, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=2,Name="测试产品2" } },
                        new MyOrderDetail(){OrderId=1000,ProductId=3, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=3,Name="测试产品3" } }
                    }
                },
                new MyOrder(){
                    Id=1001,UserId =1000, OrderState= OrderState.Completed, OrderFee=101.00M,CreationTime=DateTime.Now,
                    Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1001,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=1,Name="测试产品1" } }
                    }
                },
                new MyOrder(){
                    Id=1002,UserId =1001,OrderState= OrderState.Completed, OrderFee=102.00M,CreationTime=DateTime.Now,
                     Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1002,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=2,Name="测试产品2" } }
                    }
                },
                new MyOrder(){
                    Id=1003,UserId =1002, OrderState= OrderState.Completed, OrderFee=103.03M,CreationTime=DateTime.Now,
                    Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1003,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=3,Name="测试产品3" } }
                    }
                },
                new MyOrder(){
                    Id=1004,UserId =1003, OrderState= OrderState.Completed, OrderFee=104.00M,CreationTime=DateTime.Now,
                    Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1004,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=1,Name="测试产品1" } },
                        new MyOrderDetail(){OrderId=1004,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=2,Name="测试产品2" } }
                    }
                },
                new MyOrder(){
                    Id=1005,UserId =1003, OrderState= OrderState.Completed, OrderFee=108.00M,CreationTime=DateTime.Now,
                    Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1005,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=1,Name="测试产品1" } },
                        new MyOrderDetail(){OrderId=1005,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=2,Name="测试产品2" } }
                    }
                },
                new MyOrder(){
                    Id=1009,UserId =2009, OrderState= OrderState.Completed, OrderFee=199.99M,CreationTime=DateTime.Now,OrderRemarks="Left查找",
                    Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1009,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=1,Name="测试产品1" } }
                    }
                },
                new MyOrder(){
                    Id=1008,UserId =2000, OrderState= OrderState.Completed, OrderFee=199.99M,CreationTime=DateTime.Now,OrderRemarks="查找Right",
                    Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1008,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=1,Name="测试产品1" } },
                        new MyOrderDetail(){OrderId=1008,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=3,Name="测试产品3" } }
                    }
                },
                new MyOrder(){
                    Id=1007,UserId =1010, OrderState= OrderState.Completed, OrderFee=199.99M,CreationTime=DateTime.Now,OrderRemarks="Full查找1",
                    Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1007,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=3,Name="测试产品3" } }
                    }
                },
                new MyOrder(){
                    Id=1006,UserId =1004, OrderState= OrderState.Completed, OrderFee=199.99M,CreationTime=DateTime.Now,OrderRemarks="Full查找2",
                    Details = new List<MyOrderDetail>
                    {
                        new MyOrderDetail(){OrderId=1006,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=1,Name="测试产品1" } },
                        new MyOrderDetail(){OrderId=1006,ProductId=1, BuyCount=1,Price=100,Amount=100,ProductInfo=new MyProduct{ Id=2,Name="测试产品2" } }
                    }
                },
        };
    }

    public class MyOrder
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal OrderFee { get; set; }
        public OrderState OrderState { get; set; }
        public DateTime CreationTime { get; set; }
        public String OrderRemarks { get; set; }
        public List<MyOrderDetail> Details { get; set; }
    }

    public class MyOrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int BuyCount { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("ProductId")]
        public MyProduct ProductInfo { get; set; }
    }

    public class MyProduct
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
    }

    public enum OrderState
    {
        Completed = 1,
        Cancel = 2
    }

    public class MyOrderFilter : DefaultDataFilter<MyOrder, MyOrderFilter>
    {
        public MyOrderFilter()
        {
            Id = new CompareField<int>()
            {
                SortMode = SortMode.Desc,
                CompareMode = CompareMode.Equal,
                SortPriority = 10,
                FilterSwitch = FilterSwitch.Close
            };
        }

        [FilterFieldExprNameMap("Id", SortMode = SortMode.Desc, SortPriority = 100)]
        public CompareField<int> Id { get; set; }

        public ContainsField<int> UserId { get; set; }

        [FilterFieldExprNameMap("OrderState")]
        public ContainsField<OrderState> State { get; set; }

        [FilterFieldExprNameMap("OrderFee")]
        public CompareField<decimal> TotalFee { get; set; }

        public FreeDomRangeField<DateTime> CreationTime { get; set; }

        [FilterFieldExprNameMap("OrderRemarks")]
        public LikeField Remarks { get; set; }

        public ContainsField<int> ProductId { get; set; }
    }

    public class MyOrderDetailFilter : DefaultDataFilter<MyOrderDetail, MyOrderDetailFilter>
    {
        public MyOrderDetailFilter()
        {
            Id = new SortField<int>(SortMode.Desc);
        }

        public SortField<int> Id { get; set; }
        public EqualsField<int> OrderId { get; set; }

        [FilterFieldExprNameMap("ProductInfo.Id")]
        public CompareField<int> ProductInfoId { get; set; }
    }

    public class ProductEqualityComparer : IEqualityComparer<MyProduct>
    {
        public bool Equals(MyProduct x, MyProduct y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(MyProduct obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
