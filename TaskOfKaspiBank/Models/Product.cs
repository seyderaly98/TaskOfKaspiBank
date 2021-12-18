using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TaskOfKaspiBank.Models
{
    public class Product
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        public int Id { get;   set; }
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
        public int RemainingProductQuantity { get; set; } //TODO: если останется время добавить ограничение заказа по количеству товара
        /// <summary>
        /// Путь к изображению продукта
        /// </summary>
        public string PathImage { get; set; }

      

    }
}