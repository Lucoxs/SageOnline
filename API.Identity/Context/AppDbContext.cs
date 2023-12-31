﻿using API.Identity.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Identity.Context
{
    public class AppDbContext : IdentityDbContext<User>, IDataProtectionKeyContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*Database.EnsureCreated();
            Database.Migrate();*/

            modelBuilder.Entity<Company>()
                .HasMany(x => x.Users)
                .WithOne(y => y.Company)
                .HasForeignKey("co_id")
                .IsRequired();
        }
    }
}
