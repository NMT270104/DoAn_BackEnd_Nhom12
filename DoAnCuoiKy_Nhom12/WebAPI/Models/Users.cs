using Microsoft.EntityFrameworkCore; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Models {
    [Table("Users")]
    public class User
    {
        [Key]
        [Required]
        public int UserID { get; set; }
        [Required]
        [MinLength(4)]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Password { get; set;}

        // Quan hệ 1-n với Orders
        public ICollection<Order>? Orders { get; set; }
        // Thêm thuộc tính để theo dõi tổng số sách đã đặt hàng
        [NotMapped]
        public int TotalBooksOrdered
        {
            get { return Orders?.Count ?? 0; }
        }

        // Thêm thuộc tính để theo dõi tổng giá sách đã mua
        [NotMapped]
        public double TotalAmountSpent
        {
            get { return Orders?.Sum(o => o.Book?.Price ?? 0) ?? 0; }
        }
    }
}