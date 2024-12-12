using Microsoft.AspNetCore.Mvc;
using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public UserController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

        //create user
        [HttpPost("CreateUser")]
        public IActionResult Create(User user)
        {
            if (_fullStackDbContext.Users.Where(u => u.Email == user.Email).FirstOrDefault() != null) {
                return Ok("Already Exists");
            }
            user.MemberSince = DateTime.Now;
            _fullStackDbContext.Users.Add(user);
            _fullStackDbContext.SaveChanges();
            return Ok("Success");
        }

        //login user
        [HttpPost("LoginUser")]
        public IActionResult Login(Login user)
        {
            var userAvaiable = _fullStackDbContext.Users.Where(u => u.Email == user.Email && u.Pwd == user.Pwd).FirstOrDefault(); 
            if(userAvaiable != null)
            {
                return Ok("Success");
            }
            return Ok("Failure");
        }
    }
}
