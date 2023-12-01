using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models {
    public class ApplicationDbContext :DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}