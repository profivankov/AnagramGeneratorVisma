using AnagramSolver.EF.CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LT-LIT-SC-0116\\ANAGRAMSOLVER; Database=AnagramSolverDB; Integrated Security=true");
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
                //entity.Property(e => e.CacheID).HasColumnType("CacheID");
                //entity.Property(e => e.SearchedWordID).HasColumnType("SearchedWordID");
                //entity.Property(e => e.WordID).HasColumnType("WordID");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.HasOne<SearchedWords>(s => s.SearchedWords)
                .WithMany(g => g.UserLog)
                .HasForeignKey(s => s.SearchedWordID);

                entity.HasKey(e => e.LogID);
                //entity.Property(e => e.LogID).HasColumnType("LogID");
                //entity.Property(e => e.UserIP).HasColumnType("UserIP");
                //entity.Property(e => e.SearchedWordID).HasColumnType("SearchedWordID");
                //entity.Property(e => e.SearchTime).HasColumnType("SearchTime");
            });

            modelBuilder.Entity<Words>(entity =>
            {
                entity.HasKey(e => e.WordID);
                //entity.Property(e => e.WordID).HasColumnType("WordID");
                //entity.Property(e => e.Word).HasColumnType("Word").HasMaxLength(255);
            });

            modelBuilder.Entity<SearchedWords>(entity =>
            {
                entity.HasKey(e => e.SearchedWordId);
                //entity.Property(e => e.SearchedWordID).HasColumnType("SearchedWordID");
                //entity.Property(e => e.SearchedWord).HasColumnType("SearchedWord").HasMaxLength(255);
            });
        }

    }
}
