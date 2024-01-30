using CurdApplication.Migrations;
using CurdApplication.Models;
using CurdApplication.Models.Account;
using CurdApplication.Models.ImageImplementation;
using Microsoft.EntityFrameworkCore;

namespace CurdApplication.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        //jo model bana hai usko table k form represent karne k liye DbSet use karte hai Employee model hai aur Employees database ka naam hai
        public DbSet<Employee>Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
    }
}
