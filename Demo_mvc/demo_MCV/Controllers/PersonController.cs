using System.Reflection.Metadata.Ecma335;
using demo_MCV.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEMO_MCV.Controllers{
public class PersonController : Controller{
     
public IActionResult Index(){

return View();
}

[HttpPost]// PT nhận DL từ view lên 

 public IActionResult Create( Person ps){ //strOutput
    
  string strOutput="xin chào"+ ps.Masinhvien +"-"+ ps.Fullname;// Mainhvien, Fullname sd lưu dl từ view lên , 2 thuộc tính chính là name của thẻ Input muốn gửi DL lên 

    
  ViewBag.Message=strOutput; // gửi dl từ controller lên view
   return View();
}

}
}

