using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers;

public class EmployeeController : Controller {
    public IActionResult Index(){
        return View();
        }

        public string Welcome()
        {
            return "Welcome đến với EmployeeController";
        }

        [HttpPost]
        public IActionResult Create(string PersonId, string FullName,string Address,int Age)
        {
            ViewBag.infoEmployee = "Nhân viên ID: " + PersonId + "-" + FullName + "-" + Address + " tuổi =" + Age;
            return View();
        }
    }

            
            