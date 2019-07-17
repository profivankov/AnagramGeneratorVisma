﻿// <auto-generated />
using System;
using AnagramSolver.EF.CodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    [DbContext(typeof(DictionaryContext))]
    [Migration("20190717140551_update")]
    partial class update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.Entities.CachedWords", b =>
                {
                    b.Property<int>("CacheID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SearchedWordID");

                    b.Property<int?>("WordID");

                    b.HasKey("CacheID");

                    b.HasIndex("SearchedWordID");

                    b.HasIndex("WordID");

                    b.ToTable("CachedWords");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.Entities.SearchedWords", b =>
                {
                    b.Property<int>("SearchedWordId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SearchedWord");

                    b.HasKey("SearchedWordId");

                    b.ToTable("SearchedWords");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.Entities.UserLog", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("SearchTime");

                    b.Property<int>("SearchedWordID");

                    b.Property<string>("UserIP");

                    b.HasKey("LogID");

                    b.HasIndex("SearchedWordID");

                    b.ToTable("UserLog");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.Entities.Words", b =>
                {
                    b.Property<int>("WordID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word");

                    b.HasKey("WordID");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.Entities.CachedWords", b =>
                {
                    b.HasOne("AnagramSolver.EF.CodeFirst.Entities.SearchedWords", "SearchedWords")
                        .WithMany("CachedWords")
                        .HasForeignKey("SearchedWordID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnagramSolver.EF.CodeFirst.Entities.Words", "Words")
                        .WithMany("CachedWords")
                        .HasForeignKey("WordID");
                });

            modelBuilder.Entity("AnagramSolver.EF.CodeFirst.Entities.UserLog", b =>
                {
                    b.HasOne("AnagramSolver.EF.CodeFirst.Entities.SearchedWords", "SearchedWords")
                        .WithMany("UserLog")
                        .HasForeignKey("SearchedWordID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
