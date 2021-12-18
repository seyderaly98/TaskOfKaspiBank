using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskOfKaspiBank.Models;
using TaskOfKaspiBank.Models.Data;
using TaskOfKaspiBank.Models.Enums;

namespace TaskOfKaspiBank.Controllers
{
    public class HomeController : Controller
    {
        #region Поле и свойства
        
        private readonly ILogger<HomeController> _logger;
        private TaskOfKaspiBankContext _db;
        
        #endregion

        #region Конструкторы

        public HomeController(ILogger<HomeController> logger, TaskOfKaspiBankContext db)
        {
            _logger = logger;
            _db = db;
        }
        
        #endregion
        
        #region Actions
        
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _db.Products.Include(p => p.InformationOrderedProduct).ToListAsync();
                return View(products);
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

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        #endregion

        #region API

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
                }
                else
                {
                    var productInfo = order.ProductsInformation.FirstOrDefault(p => p.ProductId == productId);
                    if (productInfo is null)
                        order.ProductsInformation.Add(new InformationOrderedProduct(productId, order.Id));
                    else productInfo.Quantity++;
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
                productInfo.Quantity--;
                if (productInfo.Quantity == 0)
                    _db.InformationOrderedProducts.Remove(productInfo);
                return Json(false);
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