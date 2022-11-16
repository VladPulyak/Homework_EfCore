using Homework_EfCore.Interfaces;
using Homework_EfCore.Models;
using Homework_EfCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Homework_EfCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpPost("/AddUser")]
        public async Task<Users> AddUser([FromForm] Users user)
        {
            return await _libraryService.AddUser(user);
        }
        [HttpGet("/GetUsers")]
        public async Task<List<Users>> GetUsers()
        {
            return await _libraryService.GetUsers();
        }
        [HttpPost("/AddBook")]
        public async Task<Books> AddBook([FromBody] Books book, [FromForm] string authorFirstName, [FromForm] string authorLastName)
        {
            return await _libraryService.AddBook(book, authorFirstName, authorLastName);
        }
        [HttpGet("/GetBooks")]
        public async Task<List<Books>> GetBooks()
        {
            return await _libraryService.GetBooks();
        }
        [HttpGet("/GetAuthors")]
        public async Task<List<Authors>> GetAuthors()
        {
            return await _libraryService.GetAuthors();
        }
        [HttpPost("/AddAuthor")]
        public async Task<Authors> AddAuthor([FromBody] Authors author)
        {
            return await _libraryService.AddAuthor(author);
        }
        [HttpPost("/TakeBook/{userEmail}/{authorFirstName}/{authorLastName}")]
        public async Task<UserBooks> TakeBook([FromBody] Books book, [FromRoute] string userEmail, [FromRoute] string authorFirstName, [FromRoute] string authorLastName)
        {
            return await _libraryService.TakeBook(book, userEmail, authorFirstName, authorLastName);
        }
        [HttpGet("/GetUserBooks")]
        public async Task<List<UserBooksInfo>> GetUserBooks()
        {
            return await _libraryService.GetUserBooks();
        }
        [HttpDelete("/DeleteUsersWithoutBooks")]
        public async Task<List<string>> DeleteUsersWithoutBooks()
        {
            return await _libraryService.DeleteUsersWithoutBooks();
        }
        [HttpPut("/ReturnBook/{userEmail}/{bookName}")]
        public async Task<Books> ReturnBook([FromRoute] string userEmail, [FromRoute] string bookName)
        {
            return await _libraryService.ReturnBook(userEmail, bookName);
        }
    }
}