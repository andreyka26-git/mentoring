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
                new() {Id = 1, CreatedDateTime = new DateTime(2001, 3, 2), Name = "Book1", Author = "Author1"},
                new() {Id = 2, CreatedDateTime = new DateTime(1999, 2, 23), Name = "Book2", Author = "Author2"},
                new() {Id = 3, CreatedDateTime = new DateTime(2006, 7, 12), Name = "Book3", Author = "Author2"},
            };

            var reviews = new List<Review>
            {
                new() {Id = 1, BookId = 1, Mark = 4, ReviewDateTime = new DateTime(2005, 6, 21), ReviewerName = "Name1"},
                new() {Id = 2, BookId = 1, Mark = 2, ReviewDateTime = new DateTime(2012, 10, 13), ReviewerName = "Name2"},
                new() {Id = 3, BookId = 3, Mark = 5, ReviewDateTime = new DateTime(2012, 3, 2), ReviewerName = "Name3"}
            };

            var articles = new List<Article>
            {
                new() {Id = 1, Citations = 10, CreatedDateTime = new DateTime(2011, 12, 11), Content = "Content1"},
                new() {Id = 2, Citations = 10, CreatedDateTime = new DateTime(2018, 3, 5), Content = "Content2"}
            };

            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Review>().HasData(reviews);
            modelBuilder.Entity<Article>().HasData(articles);
        }
    }
}
