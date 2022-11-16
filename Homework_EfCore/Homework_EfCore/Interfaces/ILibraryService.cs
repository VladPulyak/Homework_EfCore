using Homework_EfCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_EfCore.Interfaces
{
    public interface ILibraryService
    {
        public Task<Users> AddUser(Users user);
        public Task<List<Users>> GetUsers();
        public Task<Books> AddBook(Books book, string authorFirstName, string authorLastName);
        public Task<List<Books>> GetBooks();
        public Task<List<Authors>> GetAuthors();
        public Task<Authors> AddAuthor(Authors author);
        public Task<UserBooks> TakeBook(Books book, string userEmail, string authorFirstName, string authorLastName);
        public Task<List<UserBooksInfo>> GetUserBooks();
        public Task<List<string>> DeleteUsersWithoutBooks();
        public Task<Books> ReturnBook(string userEmail, string bookName);
    }   
}
