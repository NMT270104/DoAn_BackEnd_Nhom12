using Microsoft.EntityFrameworkCore; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Models {
    [Table("Users")]
    public class User
    {
        //[Key]
        [Required]
        public int UserID { get; set; }
        [Required]
        [MinLength(4)]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }



        // Quan hệ 1-n với Orders
        public ICollection<Order>? Orders { get; set; }
    }
}