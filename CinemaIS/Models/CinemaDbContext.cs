﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaIS.Models
{
    public class CinemaDbContext : IdentityDbContext<Visitor>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Hall> Halls { get; set; }

        public CinemaDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "6b7bf0ac-b815-455a-8908-8133983c9200",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<Visitor>().HasData(new Visitor
            {
                Id = "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                UserName = "Администратор",
                NormalizedUserName = "АДМИНИСТРАТОР",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "root"),
                SecurityStamp = string.Empty,
                ConcurrencyStamp = string.Empty,
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "6b7bf0ac-b815-455a-8908-8133983c9200",
                UserId = "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7"
            });


            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "66666666-b815-455a-8908-8133983c9200",
                Name = "manager",
                NormalizedName = "MANAGER"
            });

            builder.Entity<Visitor>().HasData(new Visitor
            {
                Id = "aaaaaaaa-3d13-433f-bc24-fcf769b6e6e7",
                UserName = "Менеджер",
                NormalizedUserName = "МЕНЕДЖЕР",
                Email = "manager@email.com",
                NormalizedEmail = "MANAGER@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "root"),
                SecurityStamp = string.Empty,
                ConcurrencyStamp = string.Empty,
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "66666666-b815-455a-8908-8133983c9200",
                UserId = "aaaaaaaa-3d13-433f-bc24-fcf769b6e6e7"
            });
        }

    }
}
