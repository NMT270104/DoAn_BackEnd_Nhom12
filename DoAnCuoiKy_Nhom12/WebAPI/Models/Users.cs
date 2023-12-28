using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPI.Models {
    //[Table("Users")]
    public class User : IdentityUser
    {

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