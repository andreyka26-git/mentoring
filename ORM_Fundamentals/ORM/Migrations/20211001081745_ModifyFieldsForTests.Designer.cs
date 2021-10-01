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
    [Migration("20211001081745_ModifyFieldsForTests")]
    partial class ModifyFieldsForTests
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
                            CreatedDateTime = new DateTime(2021, 10, 1, 11, 17, 44, 523, DateTimeKind.Local).AddTicks(9918)
                        },
                        new
                        {
                            Id = 2,
                            Citations = 10,
                            Content = "Content2",
                            CreatedDateTime = new DateTime(2021, 10, 1, 11, 17, 44, 524, DateTimeKind.Local).AddTicks(287)
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
                            CreatedDateTime = new DateTime(2021, 10, 1, 11, 17, 44, 519, DateTimeKind.Local).AddTicks(5037),
                            Name = "Book1"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Author2",
                            CreatedDateTime = new DateTime(2021, 10, 1, 11, 17, 44, 523, DateTimeKind.Local).AddTicks(6917),
                            Name = "Book1"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Author2",
                            CreatedDateTime = new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999),
                            Name = "Book1"
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
                            ReviewDateTime = new DateTime(2021, 10, 1, 11, 17, 44, 523, DateTimeKind.Local).AddTicks(8341),
                            ReviewerName = "Name1"
                        },
                        new
                        {
                            Id = 2,
                            BookId = 1,
                            Mark = 2,
                            ReviewDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReviewerName = "Name2"
                        },
                        new
                        {
                            Id = 3,
                            BookId = 3,
                            Mark = 5,
                            ReviewDateTime = new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999),
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
