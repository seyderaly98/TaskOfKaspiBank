using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskOfKaspiBank.Models.Data;
using TaskOfKaspiBank.Models.Enums;

namespace TaskOfKaspiBank.Controllers
{
    public class ManagerController : Controller
    {
        #region Поле и свойства
        
        private TaskOfKaspiBankContext _db;
        
        #endregion
        
        #region Конструкторы

        public ManagerController(TaskOfKaspiBankContext db)
        {
            _db = db;
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