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
        public DbSet<RoundHole> RoundHoles { get; set; } // 追加
        public DbSet<Shot> Shots { get; set; } // 追加


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Round と RoundHole の関係を設定
            modelBuilder.Entity<Round>()
                .HasMany(r => r.RoundHoles)
                .WithOne(rh => rh.Round)
                .HasForeignKey(rh => rh.roundId)
                .OnDelete(DeleteBehavior.Cascade);

            // Hole と RoundHole の関係を設定
            modelBuilder.Entity<Hole>()
                .HasMany(h => h.RoundHoles)
                .WithOne(rh => rh.Hole)
                .HasForeignKey(rh => rh.holeId)
                .OnDelete(DeleteBehavior.Restrict);

            // RoundHole と Shot の関係を設定
            modelBuilder.Entity<RoundHole>()
                .HasMany(rh => rh.Shots)
                .WithOne(s => s.RoundHole)
                .HasForeignKey(s => s.roundHoleId)
                .OnDelete(DeleteBehavior.Cascade);

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

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shot>()
                .Property(s => s.ballDirection)
                .HasConversion<string>();

            modelBuilder.Entity<Shot>()
                .Property(s => s.clubUsed)
                .HasConversion<string>();

            modelBuilder.Entity<Shot>()
                .Property(s => s.shotType)
                .HasConversion<string>();

            modelBuilder.Entity<Shot>()
                .Property(s => s.ballHeight)
                .HasConversion<string>();

            modelBuilder.Entity<Shot>()
                .Property(s => s.lie)
                .HasConversion<string>();

            modelBuilder.Entity<Shot>()
                .Property(s => s.shotResult)
                .HasConversion<string>();
        }
    }
}
