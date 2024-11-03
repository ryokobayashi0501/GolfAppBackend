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
        public DbSet<GolfAppBackend.Models.Course> Course { get; set; } = default!;
        public DbSet<GolfAppBackend.Models.Round> Round { get; set; } = default!;
        public DbSet<GolfAppBackend.Models.RoundDetail> RoundDetail { get; set; } = default!;
        public DbSet<GolfAppBackend.Models.Shot> Shot { get; set; } = default!;
    }
}
