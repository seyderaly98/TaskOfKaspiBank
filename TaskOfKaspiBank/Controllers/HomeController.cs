using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskOfKaspiBank.Models;
using TaskOfKaspiBank.Models.Data;

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
        
        public IActionResult Index()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        #endregion

    }
}