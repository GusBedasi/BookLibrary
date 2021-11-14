using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookLibrary.Data;
using BookLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary
{
    class Program
    {
        public static DbContextOptions<LibraryDbContext> _options = 
            new DbContextOptionsBuilder<LibraryDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=library_db;User Id=sa;Password=Password123;")
            .Options;
        
        static void Main(string[] args)
        {
            // Descomente essa parte para criar o banco de dados
            // using (var db = new LibraryDbContext(_options))
            // {
            //     db.Database.EnsureCreated();
            // }

            // Descomente essa parte para criar registros no banco os dados
            // var authors = CreateFakeData();
            // using (var db = new LibraryDbContext(_options))
            // {
            //     db.Authors.AddRange(authors);
            //     db.SaveChanges();
            // }

            //Descomente essa parte para printar o que foi buscado do banco de dados
            var author = FindOne<Author>(x => x.Name == "J. R. R. Tolkien",
                x => x.Books);

            if (author == null)
            {
                Console.WriteLine("Author don't exist!");
                return;
            }
            
            Console.WriteLine(author.ToString());
            
            foreach (var book in author.Books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        // Code to find a specific entity
        public static T FindOne<T>(Expression<Func<T, bool>> filter, 
            params Expression<Func<T, object>>[] include) where T: class
        {
            using (var db = new LibraryDbContext(_options))
            {
                IQueryable<T> set = db.Set<T>();
                
                foreach (var item in include)
                {
                    set = set.Include(item);
                }
                
                return set.FirstOrDefault(filter);
            }
        }
        
        // Code to find a collection of entities of the same type
        public static ICollection<T> FindAll<T>(Expression<Func<T, bool>> filter) where T: class
        {
            using (var db = new LibraryDbContext(_options))
            {
                return db.Set<T>().Where(filter).ToList();
            }
        }
        
        public static List<Author> CreateFakeData()
        {
            return new List<Author>()
            {
                new Author()
                {
                    Name = "J. R. R. Tolkien",
                    Books = new List<Book>()
                    {
                        new Book() {Title = "The Hobbit", PublicationYear = 1937},
                        new Book() {Title = "Lord of the rings", PublicationYear = 1954}
                    }
                },
                new Author()
                {
                    Name = "C. S. Lewis",
                    Books = new List<Book>()
                    {
                        new Book() {Title = "The Chronicles of Narnia", PublicationYear = 1950},
                        new Book() {Title = "The great divorce", PublicationYear = 1945}
                    }
                }
            };
        }
    }
}
