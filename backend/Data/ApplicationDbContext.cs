using Microsoft.EntityFrameworkCore;
namespace project_name.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
     {
     }
    public DbSet <Newsletters> Newsletters{ get; set; }
  }
}