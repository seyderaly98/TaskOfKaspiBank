using System;

namespace TaskOfKaspiBank.Models.Data
{
    public abstract class OrderAddress
    {
        public int Id { get; set; }
        /// <summary>
        /// Адрес  
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Номер карты
        /// </summary>
        public short CarNumber { get; set; }
    }
}