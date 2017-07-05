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
    public  class FluentFilterTestDataBaseFromDb: FluentFilterTestDataBase
    {
        public FluentFilterTestDataBaseFromDb()
        {
            var options = new DbContextOptionsBuilder<MyTestDb>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var db = new MyTestDb(options);
            _myorder = db.MyOrders;

            // 初始化数据
            _myorder.AddRange(TempnoaryList);
            db.SaveChanges();
        }

        private DbSet<MyOrder> _myorder;

        protected override IQueryable<MyOrder> DataSoures => _myorder;

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
                modelBuilder.Entity<MyOrder>();
            }

            public DbSet<MyOrder> MyOrders { get; set; }
        }
    }
}
