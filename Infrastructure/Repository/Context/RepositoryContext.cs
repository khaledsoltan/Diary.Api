using Api.Host.Domain.Entites;
using Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Diary.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repository.Context
{

    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration()); modelBuilder.Entity<User>().ToTable("DiaryUsers", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("DiaryRoles", "dbo");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("DiaryUserRoles", "dbo");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("DiaryUserLogins", "dbo");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("DiaryRoleClaims", "dbo");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("DiaryUserTokens", "dbo");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("DiaryUserClaims", "dbo");
        }
        public DbSet<Contact>? Contact { get; set; }
        public DbSet<DiarY>? Diary { get; set; }
        public DbSet<DiaryEvent>? DiaryEvent { get; set; }
        public DbSet<DiaryEntry>? DiaryEntry { get; set; }

    }
}
