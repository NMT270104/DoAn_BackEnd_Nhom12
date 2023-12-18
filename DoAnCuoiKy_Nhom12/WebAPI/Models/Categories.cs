using Microsoft.EntityFrameworkCore; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Models {
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Required]        
        public int CategoryID { get; set; }
        [Required]
        public string? CategoryName { get; set; }



        // Quan hệ 1-n với Book
        public ICollection<Book>? Books { get; set; }
    }
}