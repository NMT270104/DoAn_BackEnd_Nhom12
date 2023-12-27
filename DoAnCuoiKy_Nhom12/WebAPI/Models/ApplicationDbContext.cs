using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models {
    public class ApplicationDbContext :IdentityDbContext<User>{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
              //Sử Dụng Composite Primary Key
            // modelBuilder.Entity<Order>()
            //     .HasKey(o => new { o.OrderID, o.UserID, o.BookID });

            // modelBuilder.Entity<Book>()
            //     .HasKey(b => new { b.BookID, b.AuthorID, b.CategoryID });

            // Xác định quan hệ giữa các bảng và khóa chính
            
            // modelBuilder.Entity<Order>()
            //     .HasOne(o => o.UserID)
            //     .WithMany(u => u.Orders)
            //     .HasForeignKey(o => o.UserID)
            //     .IsRequired()
            //     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Book)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BookID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //  modelBuilder.Entity<Order>()
            // .HasMany(o => o.OrderItems)
            // .WithOne()
            // .HasForeignKey(oi => oi.OrderId)
            // .Ignore(o => o.OrderItems);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

            // Khai báo DbSet cho mỗi bảng
            public DbSet<User> Users { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Book> Books { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Author> Authors { get; set; }
    }
}