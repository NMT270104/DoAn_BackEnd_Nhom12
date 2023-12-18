using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Models {
    [Table("Books")]
    public class Book
    {
        [Key]
        [Required]  
        public int BookID { get; set; }
        [Required]
        public string? NameBook { get; set; }
       // [Key]
        [Required]
        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")]

        //[Key]
        [Required]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]

        [Required]
        public string? Image {get;set;}
        [Required]
        public string? Description {get;set;}
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        
// Quan hệ n-1 với Author
public Author? Author { get; set; }
// Quan hệ n-1 với Category
public Category? Category { get; set; }
// Quan hệ 1-n với Orders
public ICollection<Order>? Orders { get; set; }

    }
}