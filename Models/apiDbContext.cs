using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi_test.models;
using GolfAppBackend.Models;

namespace WebApi_test.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions option) : base(option)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Hole> Holes { get; set; }
        public DbSet<Round> Rounds { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Rounds)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Holes)
                .WithOne(h => h.Course)
                .HasForeignKey(h => h.courseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Rounds)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.courseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
