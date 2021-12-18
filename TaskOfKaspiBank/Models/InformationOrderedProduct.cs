using System;

namespace TaskOfKaspiBank.Models
{
    /// <summary>
    /// Информация о заказанном продукте 
    /// </summary>
    public class InformationOrderedProduct
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        /// <summary>
        /// Количество заказа
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Id продукта 
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// Продукт
        /// </summary>
        public virtual Product Product { get; set; }
        /// <summary>
        /// Id заказа 
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// Заказ
        /// </summary>
        public virtual Order Order { get; set; }

        #region Конструкторы

        public InformationOrderedProduct(){}
        
        public InformationOrderedProduct(string productId, string orderId)
        {
            ProductId = productId;
            OrderId = orderId;
            Quantity = 1;
        }
        
        #endregion

    }
}