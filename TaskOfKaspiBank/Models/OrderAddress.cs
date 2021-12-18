using System;

namespace TaskOfKaspiBank.Models
{
    public class OrderAddress
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Адрес  
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Номер карты
        /// </summary>
        public short CarNumber { get; set; }
        /// <summary>
        /// Дата cоздания
        /// </summary>
        public DateTime DateCreation { get; } = DateTime.Now;
    }
}