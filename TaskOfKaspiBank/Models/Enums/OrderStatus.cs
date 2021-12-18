namespace TaskOfKaspiBank.Models.Enums
{
    /// <summary>
    /// Состояние  заказа 
    /// </summary>
    public enum OrderStatus
    {
        None = 0,
        /// <summary>
        /// Формируется (во время добавления товаров)
        /// </summary>
        Forming = 1,
        /// <summary>
        /// Оплачен (после заполнения формы оформления)
        /// </summary>
        Paid = 2,
        /// <summary>
        /// Выполнен (после нажатия кнопки "Выполнен")
        /// </summary>
        Completed = 3
    }
}