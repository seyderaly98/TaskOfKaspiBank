using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TaskOfKaspiBank.Models.Enums;

namespace TaskOfKaspiBank.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Номер заказа
        /// </summary>
        public string Number { get; set; } =  Guid.NewGuid().ToString();
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
        [NotMapped] public string StatusName => GetStatusName();
        /// <summary>
        /// Дата cоздания
        /// </summary>
        public DateTime DateCreation => DateTime.Now;

        /// <summary>
        /// Общая стоимость заказа
        /// </summary>
        [NotMapped]
        public decimal TotalCost => ProductsInformation.Sum(productInfo => productInfo.Product.Price * productInfo.Quantity);
        

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

        private string GetStatusName()
        {
            return Status switch
            {
                OrderStatus.Completed => "Выполнен",
                OrderStatus.Forming => "Формируется",
                OrderStatus.Paid => "Оплачен",
                _ => "Склад"
            };
        }
    }
}