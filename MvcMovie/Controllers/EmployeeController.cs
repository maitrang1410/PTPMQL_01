namespace MvcMovie.Controllers;
public class EmployeeController : Controller {
    public IActionResult Index(){
        return View();
        }
   
    public string Welcome(){
        return " Welcome đến với EmployeeController"

            }
            }