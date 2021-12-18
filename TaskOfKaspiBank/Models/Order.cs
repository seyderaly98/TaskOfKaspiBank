using System;
using System.Collections.Generic;
using TaskOfKaspiBank.Models.Enums;

namespace TaskOfKaspiBank.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Номер заказа
        /// </summary>
        public Guid Number { get; } = new Guid();
        /// <summary>
        /// Информация о продуктах
        /// </summary>
        public virtual List<InformationOrderedProduct> ProductsInformation { get; set; }
        public string AddressId { get; set; }
        /// <summary>
        /// Адрес заказа
        /// </summary>
        public virtual OrderAddress Address { get; set; }
        /// <summary>
        /// Состояние  заказа 
        /// </summary>
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Дата cоздания
        /// </summary>
        public DateTime DateCreation { get; } = DateTime.Now;

        #region Конструкторы

        public Order(){}

        public Order(string productId)
        {
            Status = OrderStatus.Forming;
            ProductsInformation = new List<InformationOrderedProduct>()
            {
                new InformationOrderedProduct(orderId:this.Id,productId:productId)
            };
        }

        #endregion
        
    }
}