using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPoll.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
