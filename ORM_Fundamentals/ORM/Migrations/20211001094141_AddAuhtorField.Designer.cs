﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ORM.Domain;

namespace ORM.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211001094141_AddAuhtorField")]
    partial class AddAuhtorField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ORM.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Citations")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Citations = 10,
                            Content = "Content1",
                            CreatedDateTime = new DateTime(2011, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Citations = 10,
                            Content = "Content2",
                            CreatedDateTime = new DateTime(2018, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ORM.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Author1",
                            CreatedDateTime = new DateTime(2001, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Book1"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Author2",
                            CreatedDateTime = new DateTime(1999, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Book2"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Author2",
                            CreatedDateTime = new DateTime(2006, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Book3"
                        });
                });

            modelBuilder.Entity("ORM.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReviewerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            Mark = 4,
                            ReviewDateTime = new DateTime(2005, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReviewerName = "Name1"
                        },
                        new
                        {
                            Id = 2,
                            BookId = 1,
                            Mark = 2,
                            ReviewDateTime = new DateTime(2012, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReviewerName = "Name2"
                        },
                        new
                        {
                            Id = 3,
                            BookId = 3,
                            Mark = 5,
                            ReviewDateTime = new DateTime(2012, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReviewerName = "Name3"
                        });
                });

            modelBuilder.Entity("ORM.Models.Review", b =>
                {
                    b.HasOne("ORM.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("ORM.Models.Book", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
