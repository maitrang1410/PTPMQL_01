using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace MvcMovie.Models
{
    [Table("Persons")]
    public class Person
    {
        [Key]
       [Required]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     // [Index(IsUnique = true)] // Đảm bảo rằng PersonId là duy nhất
        public  string? PersonId { get; set; }

        public string? FullName { get; set; }
        public string? Address { get; set; }
        
    }
}