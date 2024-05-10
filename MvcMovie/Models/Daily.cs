using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    [Table("DaiLy")]
    public class DaiLy : Hethongphanphoi
    {

        public string? MaDaiLy { get; set; }
        public string? TenDaiLy { get; set; }
        public string? DiaChi { get; set; }
        public string? NguoiDaiDien { get; set; }
        public string? DienThoai { get; set; }
        [ForeignKey("MaHTPP")]
         public  Hethongphanphoi? HTPP { get; set; }
       
      
    }
}
        

    




