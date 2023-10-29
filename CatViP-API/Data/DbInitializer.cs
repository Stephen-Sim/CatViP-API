﻿using CatViP_API.Models;
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
                new ExpertApplicationStatus() { Id = 3, Name = "Rejected" }
            );

            modelBuilder.Entity<PostReportStatus>().HasData(
                new PostReportStatus() { Id = 1, Name = "False information" },
                new PostReportStatus() { Id = 2, Name = "Inappropriate content" },
                new PostReportStatus() { Id = 3, Name = "Others" }
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

            modelBuilder.Entity<Models.TransactionStatus>().HasData(
                new Models.TransactionStatus() { Id = 1, Name = "Success" },
                new Models.TransactionStatus() { Id = 2, Name = "Pending" },
                new Models.TransactionStatus() { Id = 3, Name = "Failed" }
            );
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Username = "admin", FullName = "CatViP Admin", Email = "admin@catvip.my", Password = BCrypt.Net.BCrypt.HashPassword("abc12345"), Gender = true, DateOfBirth = new DateTime(2000, 1, 1) }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole() { Id = 1, UserId = 1, RoleId = 1 }
            );
        }
    }
}