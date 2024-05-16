
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcMovie.Models;
 
public class Hethongphanphoi
    {
       
        [Key]
        public string MaHTPP { get; set; }
        public string? TenHTPP { get; set; }
         public virtual ICollection<DaiLy> DaiLy { get; set; } //điều hướng từ lớp mô hình của bạn đến các thực thể DaiLy liên quan 
    }