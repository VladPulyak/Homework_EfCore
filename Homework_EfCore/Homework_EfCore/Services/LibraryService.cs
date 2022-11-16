using Homework_EfCore.Interfaces;
using Homework_EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Homework_EfCore.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly MyDBContext _context;
        public LibraryService(MyDBContext context)
        {
            _context = context;
        }
        public async Task<Users> AddUser(Users user)
        {
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            return user;
        }
        public async Task<List<Users>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task<Books> AddBook(Books book, string authorFirstName, string authorLastName)
        {
            var author = await _context.Authors.Where(q => q.FirstName == authorFirstName && q.LastName == authorLastName).SingleOrDefaultAsync();
            book.AuthorId = author.AuthorId;
            await _context.Books.AddAsync(book);
            _context.SaveChanges();
            return book;
        }
        public async Task<List<Books>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }
        public async Task<Authors> AddAuthor(Authors author)
        {
            await _context.Authors.AddAsync(author);
            _context.SaveChanges();
            return author;
        }
        public async Task<List<Authors>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors;
        }
        public async Task<UserBooks> TakeBook(Books book, string userEmail, string authorFirstName, string authorLastName)
        {
            var userBooks = new UserBooks();
            userBooks.UserId = _context.Users.Single(q => q.Email == userEmail).UserId;
            var authorIdOfCurrentBook = _context.Authors.Single(q => (q.FirstName == authorFirstName) && (q.LastName == authorLastName)).AuthorId;
            userBooks.BookId = _context.Books.Where(q => (q.Name == book.Name) && (q.AuthorId == authorIdOfCurrentBook) && (q.Year == book.Year)).Single().BookId;
            await _context.UserBooks.AddAsync(userBooks);
            _context.SaveChanges();
            return userBooks;
        }
        public async Task<List<UserBooksInfo>> GetUserBooks()
        {
            var listWithUserBooksInfo = await _context.UserBooks.Select(q => new UserBooksInfo
            {
                UserFullName = q.User.FirstName + " " + q.User.LastName,
                BookName = q.Book.Name,
                UserBirthDate = q.User.BirthDate,
                AuthorFullName = q.Book.Author.FirstName + " " + q.Book.Author.LastName,
                BookYear = q.Book.Year,
            }).ToListAsync();
            return listWithUserBooksInfo;
        }
        public async Task<List<string>> DeleteUsersWithoutBooks()
        {
            var fullNameOFDeletedUser = new List<string>();
            var listWithUserBooks = await _context.UserBooks.ToListAsync();
            var listWithUsers = await _context.Users.ToListAsync();
            foreach (var user in listWithUsers)
            {
                var listWithDeletedUsers = listWithUserBooks.Where(q => q.UserId == user.UserId);
                if (listWithDeletedUsers.IsNullOrEmpty())
                {
                    fullNameOFDeletedUser.Add(user.FirstName + " " + user.LastName);
                    _context.Users.Remove(user);
                }
            }
            _context.SaveChanges();
            return fullNameOFDeletedUser;
        }
        public async Task<Books> ReturnBook(string userEmail, string bookName)
        {
            var listWithUserBooks = await _context.UserBooks.Include(q => q.User).Include(q => q.Book).ToListAsync();
            var deletedUser = listWithUserBooks.Where(q => q.User.Email == userEmail && q.Book.Name == bookName).Single();
            _context.UserBooks.Remove(deletedUser);
            _context.SaveChanges();
            return deletedUser.Book;
        }
    }
}
