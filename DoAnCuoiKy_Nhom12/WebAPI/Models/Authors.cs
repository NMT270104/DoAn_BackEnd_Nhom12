using Microsoft.EntityFrameworkCore; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Models {
    [Table("Authors")]
    public class Author
    {
        [Key]
        [Required]
        public int AuthorID { get; set; }
        [Required]
        public string? AuthorName { get; set; }


        // Quan hệ 1-n với Book
        public ICollection<Book>? Books { get; set; }
    }
}