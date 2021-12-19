using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskOfKaspiBank.Models.Data;
using TaskOfKaspiBank.Models.Enums;
using TaskOfKaspiBank.Services;

namespace TaskOfKaspiBank.Controllers
{
    public class ManagerController : Controller
    {
        
        #region Поле и свойства
        
        private TaskOfKaspiBankContext _db;
        private LogService _log;
        
        #endregion
        
        #region Конструкторы

        public ManagerController(TaskOfKaspiBankContext db, LogService log)
        {
            _db = db;
            _log = log;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var orders = await _db.Orders.ToListAsync();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(string orderId)
        {
            try
            {
                if (!await _db.Orders.AnyAsync(o => o.Id == orderId)) return Json(false);
                ViewBag.LogData =  _log.GetLogData(orderId);
                return PartialView("Partial/OrderDetailsPartial",await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId));
            }
            catch (Exception e)
            {
#if(DEBUG)
                //TODO: Логирование 
                Console.WriteLine(e);
#endif
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Отправить заказ 
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        [HttpPost]
        public async Task<IActionResult> SendOrder(string orderId)
        {
            try
            {
                if (!await _db.Orders.AnyAsync(o => o.Id == orderId)) return Json(false);
                var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
                order.Status = OrderStatus.Completed;
                await _db.SaveChangesAsync();
                _log.Logger(order.Id,$"Номер заказа #{order.Number.Substring(0, 13)}: cтатус = {order.StatusName}");
                return Json(true);
            }
            catch (Exception e)
            {
#if(DEBUG)
                //TODO: Логирование 
                Console.WriteLine(e);
#endif
                return StatusCode(500);
            }
        }
        #endregion
        
    }
}