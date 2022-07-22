using CorporateCourseManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCourseManagement.Controllers
{
    public class AdminController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly CorporateCourseManagementDbContext _context;

        public AdminController(IConfiguration configuration, CorporateCourseManagementDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpGet]
        [Route("getUsers"), Authorize(Roles = "Admin")]
        public IEnumerable<User> GetUsers()
        {
            var staffs = _context.Users;

            return (staffs);
        }

        [HttpGet]
        [Route("getTrainers"), Authorize(Roles = "Admin")]
        public IEnumerable<User> GetTrainer()
        {
            

            var staffs =  _context.Users.Where(x => x.Role == "Trainer");

            return (staffs);
        }


        [HttpGet]
        [Route("getTrainees"), Authorize(Roles = "Admin")]
        public IEnumerable<User> GetTrainee()
        {


            var staffs = _context.Users.Where(x => x.Role == "Trainee");

            return (staffs);
        }

    }
}
