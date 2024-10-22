using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi_test.models;

namespace WebApi_test.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions option) : base(option)
        {
        }

        public DbSet<Users> users { get; set; }
    }
}
