using Microsoft.AspNetCore.Mvc;

namespace demo_MCV.Controllers {

 public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Index(string Id, int Age )
        {
             string strOutput= "Thông tin nhân viên: " + EmployeeId + " tuoi " + Age;
            ViewBag.Message = strOutput;
            return View();
        }
    }
}