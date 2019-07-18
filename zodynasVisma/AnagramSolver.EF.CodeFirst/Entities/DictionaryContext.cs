using AnagramSolver.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace AnagramSolver.EF.CodeFirst
{
    public class DictionaryContext : DbContext
    {
        public DictionaryContext()
        {

        }

        public DictionaryContext(DbContextOptions<DictionaryContext> options)
            : base(options)
        { }

        public virtual DbSet<CachedWords> CachedWords { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
        public virtual DbSet<Words> Words { get; set; }
        public virtual DbSet<SearchedWords> SearchedWords { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LT-LIT-SC-0116\\ANAGRAMSOLVER;Database = AnagramSolverDB; Integrated Security = true");
                //throw new Exception("No connection string.");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWords>(entity =>
            {
                entity.HasOne<Words>(s => s.Words)
                .WithMany(g => g.CachedWords)
                .HasForeignKey(s => s.WordID);

                entity.HasOne<SearchedWords>(s => s.SearchedWords)
                .WithMany(g => g.CachedWords)
                .HasForeignKey(s => s.SearchedWordID);

                entity.HasKey(e => e.CacheID);
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.HasOne<SearchedWords>(s => s.SearchedWords)
                .WithMany(g => g.UserLog)
                .HasForeignKey(s => s.SearchedWordID);

                entity.HasOne<UserInfo>(s => s.UserInfo)
                .WithMany(g => g.UserLog)
                .HasForeignKey(s => s.UserIP);

                entity.HasKey(e => e.LogID);
            });

            modelBuilder.Entity<Words>(entity =>
            {
                entity.HasKey(e => e.WordID);
            });

            modelBuilder.Entity<SearchedWords>(entity =>
            {
                entity.HasKey(e => e.SearchedWordId);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserIP);
            });
        }

    }
}
