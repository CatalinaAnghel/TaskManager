using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.DataAccess.DataModels;

namespace TaskManager.DataAccess.Data
{
    public class TaskManagerDbContext : IdentityDbContext<Users, Roles, string>
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
            : base(options)
        {
        }

        public override DbSet<Users> Users { get; set; }
        public override DbSet<Roles> Roles { get; set; }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public DbSet<UserRoles> UserRoles { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectTasks> ProjectTasks { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<UserTeams> UserTeams { get; set; }
        public DbSet<Badges> Badges { get; set; }
        public DbSet<UserBadges> UserBadges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ID_1 = "a18be9c0-aa65-4af8-bd17-00bd9344e571";
            const string ROLE_ID_2 = "a18be9c0-aa65-4af8-bd17-00bd9344e572";

            // Add roles:
            builder.Entity<Roles>().HasData(
                new Roles
                {
                    Id = ROLE_ID_1,
                    Name = "Administrator",
                    NormalizedName = "Admin"
                },
                new Roles
                {
                    Id = ROLE_ID_2,
                    Name = "User",
                    NormalizedName = "User"
                }
           );

            // Add an administrator:
            var hasher = new PasswordHasher<Users>();
            builder.Entity<Users>().HasData(
                new Users
                {
                    Id = ADMIN_ID,
                    UserName = "tm_dev_team@tm.com",
                    NormalizedUserName = "TM_DEV_TEAM@TM.COM",
                    Email = "tm_dev_team@tm.com",
                    NormalizedEmail = "TM_DEV_TEAM@TM.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Taskmanager1234@"),
                    SecurityStamp = string.Empty
                }
           );

            // Assign a role to the administrator
            builder.Entity<UserRoles>().HasData(
                new UserRoles
                {
                    RoleId = ROLE_ID_1,
                    UserId = ADMIN_ID
                }
            );

            // Add badges:
            builder.Entity<Badges>().HasData(
                new Badges
                {
                    BadgesId = 1,
                    Name = "Beginner",
                    NecessaryScore = 500,
                    UserBadges = null
                },
                new Badges
                {
                    BadgesId = 2,
                    Name = "Advanced",
                    NecessaryScore = 25000,
                    UserBadges = null
                },
                new Badges
                {
                    BadgesId = 3,
                    Name = "Expert",
                    NecessaryScore = 50000,
                    UserBadges = null
                }
            );

        }
    }
}
