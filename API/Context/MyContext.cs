using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context 
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Education> Educations { get; set; }

        public DbSet<AccountRole> AccountRoles{ get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Emp)
                .WithOne(b => b.Acc)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Accoun)
                .WithOne(b => b.Profil)
                .HasForeignKey<Profiling>(b => b.NIK);

            modelBuilder.Entity<Education>()
                .HasMany(c => c.Profiling)
                .WithOne(e => e.Education);

            modelBuilder.Entity<University>()
                .HasMany(c => c.Education)
                .WithOne(e => e.University);

            modelBuilder.Entity<Account>()
                .HasMany(c => c.AccountRole)
                .WithOne(e => e.Account);

            modelBuilder.Entity<Role>()
                .HasMany(c => c.AccountRole)
                .WithOne(e => e.Role);

        }
    }
}
