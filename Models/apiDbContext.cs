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

        public DbSet<Users> users { get; set; }
        public DbSet<Users> courses { get; set; }
        public DbSet<GolfAppBackend.Models.Courses> Courses { get; set; } = default!;
        public DbSet<Rounds> rounds { get; set; }
        public DbSet<GolfAppBackend.Models.Holes> Holes { get; set; } = default!;

    }
}
