using FluentFilter;
using FluentFilter.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OhPrimitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentFilter.Test
{
    /// <summary>
    /// FluentFilter 测试基类，用于提供测试所需的数据源
    /// </summary>
    public class FluentFilterTestDataBaseFromDb : FluentFilterTestDataBase
    {
        public FluentFilterTestDataBaseFromDb()
        {
            var options = new DbContextOptionsBuilder<MyTestDb>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var db = new MyTestDb(options);
            MyOrders = db.Orders;
            OrderDetails = db.OrderDetails;
            Products = db.Products;

            // 初始化数据
            MyOrders.AddRange(TempnoaryList);

            var details = TempnoaryList.SelectMany(p => p.Details).Distinct();
            OrderDetails.AddRange(details);

            var products = details.Select(p => p.ProductInfo).Distinct(new ProductEqualityComparer());
            Products.AddRange(products);

            db.SaveChanges();
        }

        protected DbSet<MyOrder> MyOrders { get; }
        protected DbSet<MyOrderDetail> OrderDetails { get; }
        protected DbSet<MyProduct> Products { get; }
        protected override IQueryable<MyOrder> OrderDataSoures => MyOrders;
        protected override IQueryable<MyOrderDetail> OrderDetailDataSoures => OrderDetails;
        protected override IQueryable<MyProduct> ProductDataSoures => Products;

        /// <summary>
        /// more info
        /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
        /// </summary>
        private sealed class MyTestDb : DbContext
        {
            public MyTestDb()
            {
            }

            public MyTestDb(DbContextOptions options)
                : base(options)
            {
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;");
                }
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<MyOrder>((action) =>
                {
                    action
                    .Ignore(p => p.Details)                    
                    .HasMany(nav=>nav.Details);
                });

                modelBuilder.Entity<MyOrderDetail>((action) =>
                {
                    action
                    .Ignore(p => p.ProductInfo)
                    .HasOne(nav => nav.ProductInfo);
                });
                modelBuilder.Entity<MyProduct>();
            }

            public DbSet<MyOrder> Orders { get; set; }
            public DbSet<MyOrderDetail> OrderDetails { get; set; }
            public DbSet<MyProduct> Products { get; set; }
        }
    }
}
