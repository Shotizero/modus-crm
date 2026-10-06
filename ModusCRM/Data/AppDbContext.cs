using Microsoft.EntityFrameworkCore;
using ModusCRM.Models;

namespace ModusCRM.Data;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Port=2222;Database=service_center;Username=admin;Password=saGiaxMor21aa");
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Employee>().ToTable("employees");
        mb.Entity<Employee>().Property(e => e.Id).HasColumnName("id");
        mb.Entity<Employee>().Property(e => e.FirstName).HasColumnName("first_name");
        mb.Entity<Employee>().Property(e => e.LastName).HasColumnName("last_name");
        mb.Entity<Employee>().Property(e => e.Position).HasColumnName("position");
        mb.Entity<Employee>().Property(e => e.PhoneNumber).HasColumnName("phone_number");
        mb.Entity<Employee>().Property(e => e.Login).HasColumnName("login");
        mb.Entity<Employee>().Property(e => e.Password).HasColumnName("password");        
    }
}
