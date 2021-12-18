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
        public ISet<Product> Products { get; set; }
        /// <summary>
        /// Заказы
        /// </summary>
        public ISet<Order> Orders { get; set; }
        /// <summary>
        /// Адреса заказов
        /// </summary>
        public ISet<OrderAddress> OrderAddresses { get; set; }
        
        public TaskOfKaspiBankContext(DbContextOptions<TaskOfKaspiBankContext> options) : base(options) {}
    }
}