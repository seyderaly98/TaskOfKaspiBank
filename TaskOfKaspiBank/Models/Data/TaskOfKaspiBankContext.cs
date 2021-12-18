using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskOfKaspiBank.Models.Data
{
    public class TaskOfKaspiBankContext: IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Продукты
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// Заказы
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        /// <summary>
        /// Адреса заказов
        /// </summary>
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        
        public TaskOfKaspiBankContext(DbContextOptions<TaskOfKaspiBankContext> options) : base(options) {}
        
         protected override void OnModelCreating(ModelBuilder builder)
         {
             var products = new List<Product>()
             {
                 new Product(){Id = 1,Name = "Продукт 1",Price = 100,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
                 new Product(){Id = 2,Name = "Продукт 2",Price = 200,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
                 new Product(){Id = 3,Name = "Продукт 3",Price = 300,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
                 new Product(){Id = 4,Name = "Продукт 4",Price = 400,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
                 new Product(){Id = 5,Name = "Продукт 5",Price = 500,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
                 new Product(){Id = 6,Name = "Продукт 6",Price = 600,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
                 new Product(){Id = 7,Name = "Продукт 7",Price = 700,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
                 new Product(){Id = 8,Name = "Продукт 8",Price = 800,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
                 new Product(){Id = 9,Name = "Продукт 9",Price = 900,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
                 new Product(){Id = 10,Name = "Продукт 10",Price = 110,RemainingProductQuantity = 10, PathImage = "files/images/scale_1200.png"},
             };
            base.OnModelCreating(builder);
            builder.Entity<Product>().HasData(products);
        }
    }
}