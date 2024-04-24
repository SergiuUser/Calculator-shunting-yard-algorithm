using Calculator.mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Calculator.utils;

namespace Calculator.mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Evaluate([FromBody] string expression)
        {
            try
            {
                InfixCalculator calculator = new InfixCalculator();
                expression = calculator.calculate(expression).ToString();
                return Json(new { StatusCode = 200, expression });
            } catch (Exception ex)
            {
                expression = ex.Message;
                return Json(new { StatusCode = 500, expression });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
