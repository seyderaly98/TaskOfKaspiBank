using System;
using System.Collections.Generic;
using TaskOfKaspiBank.Models.Enums;

namespace TaskOfKaspiBank.Models
{
    public class Order
    {
        public int Id { get;  set; }
        /// <summary>
        /// Номер заказа
        /// </summary>
        public Guid Number { get; } = new Guid();
        /// <summary>
        /// Список продуктов 
        /// </summary>
        public List<Product> Products { get; set; }
        /// <summary>
        /// Адрес заказа
        /// </summary>
        public OrderAddress Address { get; set; }

        /// <summary>
        /// Состояние  заказа 
        /// </summary>
        public OrderStatus Status { get; set; }
        
    }
}