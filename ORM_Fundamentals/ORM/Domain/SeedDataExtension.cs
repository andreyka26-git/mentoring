using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ORM.Models;

namespace ORM.Domain
{
    public static class SeedDataExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var books = new List<Book>
            {
                new() {Id = 1, CreatedDateTime = DateTime.Now, Name = "Book1", Author = "Author1"},
                new() {Id = 2, CreatedDateTime = DateTime.Now, Name = "Book2", Author = "Author2"},
                new() {Id = 3, CreatedDateTime = DateTime.MaxValue, Name = "Book3", Author = "Author2"},
            };

            var reviews = new List<Review>
            {
                new() {Id = 1, BookId = 1, Mark = 4, ReviewDateTime = DateTime.Now, ReviewerName = "Name1"},
                new() {Id = 2, BookId = 1, Mark = 2, ReviewDateTime = DateTime.MinValue, ReviewerName = "Name2"},
                new() {Id = 3, BookId = 3, Mark = 5, ReviewDateTime = DateTime.MaxValue, ReviewerName = "Name3"}
            };

            var articles = new List<Article>
            {
                new() {Id = 1, Citations = 10, CreatedDateTime = DateTime.Now, Content = "Content1"},
                new() {Id = 2, Citations = 10, CreatedDateTime = DateTime.Now, Content = "Content2"}
            };

            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Review>().HasData(reviews);
            modelBuilder.Entity<Article>().HasData(articles);
        }
    }
}
