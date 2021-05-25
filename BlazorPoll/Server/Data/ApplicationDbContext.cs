using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorPoll.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>().HasOne(p => p.Author).WithMany();
            modelBuilder.Entity<Poll>().HasMany(p => p.Answers).WithOne(a => a.Poll);
            modelBuilder.Entity<Poll>().HasMany(p => p.Comments).WithOne(c => c.Poll);
            modelBuilder.Entity<IdentityUser>().HasMany<Comment>().WithOne(c => c.Author);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
