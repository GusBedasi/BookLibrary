using System;
using System.Collections.Generic;
using BookLibrary.Data;
using BookLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer("Server=localhost,1433;Database=library_db;User Id=sa;Password=Password123;")
                .Options;

            using (var db = new LibraryDbContext(options))
            {
                var authors = CreateFakeData();

                db.Authors.AddRange(authors);

                db.SaveChanges();
            }
        }

        public static List<Author> CreateFakeData()
        {
            return new List<Author>()
            {
                new Author()
                {
                    Name = "Nicolle Laurelli",
                    Books = new List<Book>()
                    {
                        new Book() {Title = "Abraço da escuridão", PublicationYear = 1986},
                        new Book() {Title = "Uma gota de esperança", PublicationYear = 1987}
                    }
                },
                new Author()
                {
                    Name = "Gustavo Bedasi",
                    Books = new List<Book>()
                    {
                        new Book() {Title = "Amor a primeira vista", PublicationYear = 1986},
                        new Book() {Title = "Beba água depois do sexo", PublicationYear = 1987}
                    }
                }
            };
        }
    }
}