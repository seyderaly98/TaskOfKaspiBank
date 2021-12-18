using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskOfKaspiBank.Models.Enums;

namespace TaskOfKaspiBank.Models
{
    public class Product
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Наименование продукта 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Оставшееся количество продукта  
        /// </summary>
        public int RemainingProductQuantity { get; set; } // TODO: если останется время добавить ограничение заказа по количеству товара
        /// <summary>
        /// Путь к изображению продукта
        /// </summary>
        public string PathImage { get; set; }
        
        public virtual List<InformationOrderedProduct> InformationOrderedProduct { get; set; }


        #region public методы


        /// <summary>
        /// Получить количество товара в корзину  
        /// </summary>
        /// <returns></returns>
        public int GetQuantityProductCart()
        {
            if(InformationOrderedProduct?.Count > 0)
                return InformationOrderedProduct.FirstOrDefault(p => p.Order.Status == OrderStatus.Forming)?.Quantity ?? 0;
            return 0;
        }

        #endregion
    }
}