using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnagramSolver.EF.DatabaseFirst.Entities
{
    public partial class DictionaryContextDBF : DbContext
    {
        public DictionaryContextDBF()
        {
        }

        public DictionaryContextDBF(DbContextOptions<DictionaryContextDBF> options)
            : base(options)
        { }

        public virtual DbSet<CachedWords> CachedWords { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
        public virtual DbSet<Words> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("EfDfConnection"));
                throw new Exception("No connection string.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWords>(entity =>
            {
                entity.HasKey(e => e.CacheId);

                entity.Property(e => e.CacheId).HasColumnName("Cache_ID");

                entity.Property(e => e.AnagramWordId).HasColumnName("Anagram_Word_ID");

                entity.Property(e => e.SearchedWord)
                    .HasColumnName("Searched_Word")
                    .HasMaxLength(255);

                entity.HasOne(d => d.AnagramWord)
                    .WithMany(p => p.CachedWords)
                    .HasForeignKey(d => d.AnagramWordId)
                    .HasConstraintName("FK__CachedWor__Anagr__60A75C0F");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SearchTime)
                    .HasColumnName("Search_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserIp)
                    .HasColumnName("User_IP")
                    .HasMaxLength(255);

                entity.Property(e => e.UserSearchedWord)
                    .IsRequired()
                    .HasColumnName("User_Searched_Word")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Words>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}