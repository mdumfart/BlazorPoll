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
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>().HasOne<User>(p => p.Author).WithMany();
            modelBuilder.Entity<Poll>().HasMany<Answer>(p => p.Answers).WithOne(a => a.Poll);
            modelBuilder.Entity<Poll>().HasMany<Comment>(p => p.Comments).WithOne(c => c.Poll);
            modelBuilder.Entity<User>().HasMany<Comment>().WithOne(c => c.Author);
        }
    }
}
