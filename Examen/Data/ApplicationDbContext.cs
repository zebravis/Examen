﻿using System;
using System.Collections.Generic;
using System.Text;
using Examen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Examen.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Subcategory> Subcategory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Reservering> Reservering { get; set; }
        public DbSet<Bestelling> Bestelling { get; set; }
        public DbSet<Examen.Models.Bestelregel> Bestelregel { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminrole = new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var userrole = new IdentityRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER"
            };


            var admin = new IdentityUser
            { 
                Id = "1",
                UserName = "admin@test.com",
                Email = "admin@test.com",
                NormalizedEmail = "admin@test.com".ToUpper(),
                NormalizedUserName = "admin@test.com".ToUpper(),
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = false,
                SecurityStamp = "dab"
            };

            PasswordHasher<IdentityUser> ph1 = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = ph1.HashPassword(admin, "Test123$");

            var user = new IdentityUser
            {
                Id = "2",
                UserName = "user@test.com",
                Email = "user@test.com",
                NormalizedEmail = "user@test.com".ToUpper(),
                NormalizedUserName = "user@test.com".ToUpper(),
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = false,
                SecurityStamp = "dab"
            };

            PasswordHasher<IdentityUser> ph2 = new PasswordHasher<IdentityUser>();
            user.PasswordHash = ph2.HashPassword(user, "Test123$");

            var adminuserrole = new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1"
            };

            var useruserrole = new IdentityUserRole<string>
            {
                RoleId = "2",
                UserId = "2"
            };

            modelBuilder.Entity<IdentityRole>().HasData(
                adminrole,
                userrole
                );
        
            modelBuilder.Entity<IdentityUser>().HasData(
                admin,
                user
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                adminuserrole,
                useruserrole
                );

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Eten"
                },
                new Category
                {
                    Id = 2,
                    Name = "Drinken"
                }
            );
            modelBuilder.Entity<Subcategory>().HasData(
                new Subcategory
                {
                    Id = 1,
                    Name = "Voorgerechten",
                    CategoryId = 1
                },
                new Subcategory
                {
                    Id = 2,
                    Name = "Hoofdgerechten",
                    CategoryId = 1
                },
                new Subcategory
                {
                    Id = 3,
                    Name = "Nagerechten",
                    CategoryId = 1
                },
                new Subcategory
                {
                    Id = 4,
                    Name = "Koude dranken",
                    CategoryId = 2
                },
                new Subcategory
                {
                    Id = 5,
                    Name = "Warme dranken",
                    CategoryId = 2
                }

            );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Soep van de dag",
                    Price = 7,
                    SubcategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Rib-eye Steak",
                    Price = 14,
                    SubcategoryId = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "Creme Brulee",
                    Price = 9,
                    SubcategoryId = 3
                },
                new Product
                {
                    Id = 4,
                    Name = "Cola",
                    Price = 3,
                    SubcategoryId = 4
                },
                new Product
                {
                    Id = 5,
                    Name = "Cappucino",
                    Price = 3,
                    SubcategoryId = 5
                }
                );
        }
    }
}

