namespace MvcMovie.Controllers;
public class PersonController : Controller {
    public IActionResult Index(){
        return View();
        }
   
    public string Welcome(){
        return " Welcome đến với PersonController"

            }
            }