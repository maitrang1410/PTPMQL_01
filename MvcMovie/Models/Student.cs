using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcMovie.Models
{ 
    [Table("Student")]
    public class Student
    {
        [Key]
        public string? StudentId { get ; set ;}
        public string? FullName  { get; set; }
     }
     }
     