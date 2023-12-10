using CatViP_API.Models;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace CatViP_API.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<ActionType>().HasData(
                new ActionType() { Id = 1, Name = "Like"},
                new ActionType() { Id = 2, Name = "DisLike" }
            );

            modelBuilder.Entity<CatCaseReportType>().HasData(
                new CatCaseReportType() { Id = 1, Name = "Missing" },
                new CatCaseReportType() { Id = 2, Name = "Dead" }
            );

            modelBuilder.Entity<ExpertApplicationStatus>().HasData(
                new ExpertApplicationStatus() { Id = 1, Name = "Success" },
                new ExpertApplicationStatus() { Id = 2, Name = "Pending" },
                new ExpertApplicationStatus() { Id = 3, Name = "Rejected" },
                new ExpertApplicationStatus() { Id = 4, Name = "Revoked" }
            );

            modelBuilder.Entity<PostType>().HasData(
                new PostType() { Id = 1, Name = "Daily sharing" },
                new PostType() { Id = 2, Name = "Expert tip" }
            );

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType() { Id = 1, Name = "Food" },
                new ProductType() { Id = 2, Name = "Collar" },
                new ProductType() { Id = 3, Name = "Health care" },
                new ProductType() { Id = 4, Name = "Toy" },
                new ProductType() { Id = 5, Name = "Litter and tray" },
                new ProductType() { Id = 6, Name = "Bowl" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "System Admin" },
                new Role() { Id = 2, Name = "Cat Owner" },
                new Role() { Id = 3, Name = "Cat Expert" },
                new Role() { Id = 4, Name = "Cat Product Seller" }
            );

            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Username = "admin", FullName = "CatViP Admin", Email = "admin@catvip.my", Password = BCrypt.Net.BCrypt.HashPassword("abc12345"), Gender = true, DateOfBirth = new DateTime(2000, 1, 1), RoleId = 1 },
                new User() { Id = 2, Username = "stephen", FullName = "stephen sim", Email = "simshansiong2002@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("abc12345"), Gender = true, DateOfBirth = new DateTime(2000, 1, 1), RoleId = 2 },
                new User() { Id = 3, Username = "tong", FullName = "yung huey", Email = "tong@catvip.my", Password = BCrypt.Net.BCrypt.HashPassword("abc12345"), Gender = false, DateOfBirth = new DateTime(2000, 1, 1), RoleId = 3 },
                new User() { Id = 4, Username = "wafir", FullName = "wafir the best", Email = "wafir@catvip.my", Password = BCrypt.Net.BCrypt.HashPassword("abc12345"), Gender = true, DateOfBirth = new DateTime(2000, 1, 1), RoleId = 4 }
            );
        }
    }
}
