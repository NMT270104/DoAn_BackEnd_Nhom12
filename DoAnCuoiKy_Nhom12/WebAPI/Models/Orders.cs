using Microsoft.EntityFrameworkCore; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Models {
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Required]
        public int OrderID { get; set; }

        // public int UserID { get; set; }
        // [ForeignKey("UserID")]

        public int BookID { get; set; }
        [ForeignKey("BookID")]

        [Required]
        public DateTime OrderDate { get; set; }



        // Quan hệ n-1 với User
        public User User { get; set; }

        // Quan hệ n-1 với Book
        public Book Book { get; set; }
        public string OrderItems { get;  set; }
        public string CustomerName { get; internal set; }
        public string CustomerEmail { get; internal set; }
        public string ShippingAddress { get; internal set; }
    }
}