using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskOfKaspiBank.Models;
using TaskOfKaspiBank.Models.Data;
using TaskOfKaspiBank.Models.Enums;
using TaskOfKaspiBank.Services;

namespace TaskOfKaspiBank.Controllers
{
    public class HomeController : Controller
    {
        
        #region Поле и свойства
        
        private TaskOfKaspiBankContext _db;
        private LogService _log;
        
        #endregion

        #region Конструкторы

        public HomeController(TaskOfKaspiBankContext db, LogService log)
        {
            _db = db;
            _log = log;
        }

        #endregion
        
        #region Actions
        
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _db.Products.Include(p => p.InformationOrderedProduct).ToListAsync();
                if (products != null) ViewBag.Order = await _db.Orders.FirstOrDefaultAsync(o => o.Status == OrderStatus.Forming);
                ViewBag.Products = products;
                return View();
            }
            catch (Exception e)
            {
#if(DEBUG)
                //TODO: Логирование 
                Console.WriteLine(e);
#endif
                throw;
            }
        }

        /// <summary>
        /// Оформить заказ
        /// </summary>
        /// <param name="model">OrderAddress</param>
        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> PlaceOrder(OrderAddress model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                var order = await _db.Orders.FirstOrDefaultAsync(p => p.Status == OrderStatus.Forming);
                order.Address = model;
                order.Status = OrderStatus.Paid;
                await _db.SaveChangesAsync();
                _log.Logger(order.Id,$"Номер заказа #{order.Number.Substring(0, 13)}: cтатус = {order.StatusName}");
                return RedirectToAction("Index");
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
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="productId">Id продукта</param>
        [HttpPost]
        public async Task<IActionResult> AddProductCart(string productId) 
        {
            try
            {
                // TODO; Если время останется добавить проверку на товар в наличии
                if (!_db.Products.Any(p => p.Id == productId)) return Json(false);
                var order = await _db.Orders.FirstOrDefaultAsync(o => o.Status == OrderStatus.Forming);
                if (order is null)
                {
                    order = new Order(productId);
                    await _db.Orders.AddAsync(order);
                    var log = $"Создан новый заказ. Номер заказа #{order.Number.Substring(0, 13)}, cтатус = {order.StatusName}";
                    _log.Logger(order.Id,log);
                }
                else
                {
                    var productInfo = order.ProductsInformation.FirstOrDefault(p => p.ProductId == productId);
                    if (productInfo is null)
                        order.ProductsInformation.Add(new InformationOrderedProduct(productId, order.Id));
                    else productInfo.Quantity++;
                    var log = $"Номер заказа #{order.Number.Substring(0, 13)}: Добавлен продукт, cтатус = {order.StatusName}";
                    _log.Logger(order.Id,log);
                }
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

        /// <summary>
        /// Удалить продукт из корзины
        /// </summary>
        /// <param name="productId">Id продукта</param>
        [HttpPost]
        public async Task<IActionResult> RemoveProductCart(string productId)
        {
            try
            {
                // TODO; Если время останется добавить проверку на товар в наличии
                if (!_db.Products.Any(p => p.Id == productId)) return Json(false);
                var order = await _db.Orders.FirstOrDefaultAsync(o => o.Status == OrderStatus.Forming);
                var productInfo = order?.ProductsInformation.FirstOrDefault(p => p.ProductId == productId);
                if (productInfo == null) return Json(false);
                if (productInfo.Quantity > 0) productInfo.Quantity--;
                if (productInfo.Quantity == 0) _db.InformationOrderedProducts.Remove(productInfo);
                await _db.SaveChangesAsync();
                var log = $"Номер заказа #{order.Number.Substring(0, 13)}: Продукт '{productInfo.Product.Name}' удалено из корзины, cтатус = {order.StatusName}";
                _log.Logger(order.Id,log);
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
        
        /// <summary>
        /// Обновить корзину заказов 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> UpdateOrderBasket()
        {
            try
            {
                var order = await _db.Orders.FirstOrDefaultAsync(o => o.Status == OrderStatus.Forming);
                return PartialView("Partial/OrderTablePartial", order);
            }
            catch (Exception ex)
            {
#if(DEBUG)
                //TODO: Логирование 
                Console.WriteLine(ex);
#endif
                return Json(new {status = 500, message = ex.Message});
            }
            
        }
        
        
        #endregion
        
    }
}