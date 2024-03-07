namespace MvcMovie.Controllers;
public class PersonController : Controller {
    public IActionResult Index(){
        return View();
        }
   
    [httpPost]
    public IActionResult Index(Peson ps){
        string StrOut= "xin chao"+ps.PersonId+ "-"+ps.FullName+"-"+ps.Address;
        ViewBag.infoPerson= strOutput;
        return View();
            }