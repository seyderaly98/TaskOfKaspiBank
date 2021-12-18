using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TaskOfKaspiBank.Models
{
    public abstract class Product
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        public int Id { get;  set; }
        /// <summary>
        /// Наименование продукта 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// Оставшееся количество продукта  
        /// </summary>
        public string RemainingProductQuantity { get; set; } //TODO: если останется время добавить ограничение заказа по количеству товара
        /// <summary>
        /// Путь к изображению продукта
        /// </summary>
        public string PathImage { get; set; }
        
    }
}