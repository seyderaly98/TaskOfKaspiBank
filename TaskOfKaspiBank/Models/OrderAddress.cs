using System;
using System.ComponentModel.DataAnnotations;

namespace TaskOfKaspiBank.Models
{
    public class OrderAddress
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Адрес  
        /// </summary>
        [Required(ErrorMessage = "Это поле необходимо заполнить.")]
        public string Address { get; set; }
        /// <summary>
        /// Номер карты
        /// </summary>
        [Required(ErrorMessage = "Это поле необходимо заполнить.")]
        public string CarNumber { get; set; }
        /// <summary>
        /// Дата cоздания
        /// </summary>
        public DateTime DateCreation { get; } = DateTime.Now;
    }
}