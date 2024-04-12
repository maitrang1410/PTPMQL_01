using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
namespace MvcMovie.Controllers
{
    public class HethongphanphoiController : Controller
    {
        [HttpPost]
        public  IActionResult Index(string MaHTPP, string TenHTPP){
            ViewBag.strOut= " Dữ liệu in ra là  "+ MaHTPP+TenHTPP;
            return View();
        }
    }
    }
    