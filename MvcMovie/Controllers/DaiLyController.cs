
using Microsoft.AspNetCore.Mvc;


namespace MvcMovie.Controllers
{
    public class DaiLyController : Controller
    {
        [HttpPost]
        public ActionResult Index(string MaDaiLy,string TenDaiLy,string DiaChi,string NguoiDaiDien,string DienThoai,string MaHTPP)
        {
            ViewBag.info = "đại lý " + MaDaiLy + "-" + TenDaiLy + "-" + DiaChi + "-" + NguoiDaiDien + "-" + DienThoai + "-" + MaHTPP;
            return View();
        }
    }
}
