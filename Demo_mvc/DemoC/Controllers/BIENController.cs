namespace DEMO_MCV : Controllers;
public class BIENController : Controller {

    public IActionResult Index(){
             string strOutput="xin chào"+ Ten +"-"+ Age ;
            ViewBag.Message=strOutput; // gửi dl từ controller lên view
 return View();
        }
    }