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
        public ActionResult GetUsers()
        {
            var staffs = _context.Users;
            if(staffs != null)
            {
                var users =staffs.Select(x => new
                {
                    Id=x.Id,
                    Name=x.Name,
                    EmailID=x.EmailId,
                    Role=x.Role,    
                });
                return Ok(users);
            }

            return BadRequest("no users available");
        }



        [HttpGet]
        [Route("getTrainers"), Authorize(Roles = "Admin")]
        public ActionResult GetTrainer()
        {
            

            var staffs =  _context.Users.Where(x => x.Role == "Trainer");
            if (staffs != null)
            {
                var users = staffs.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    EmailID = x.EmailId,
                    Role = x.Role,
                });
                return Ok(users);
            }

            return BadRequest(staffs);
        }


        [HttpGet]
        [Route("getTrainees"), Authorize(Roles = "Admin")]
        public ActionResult GetTrainee()
        {


            var staffs = _context.Users.Where(x => x.Role == "Trainee"); 
            if (staffs != null)
            {
                var users = staffs.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    EmailID = x.EmailId,
                    Role = x.Role,
                });
                return Ok(users);
            }

            return BadRequest(staffs);
        }



        [HttpGet]
        [Route("getCourse"), Authorize(Roles = "Admin")]
        public IEnumerable<Course> GetCourses()
        {
            var Course = _context.Courses;

            return (Course);
        }


        [HttpPost]
        [Route("addCourse"), Authorize(Roles = "Admin")]
        public string AddCourse([FromBody]Course course)
        {
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return "Course " + course.CourseName+"Added Successfully" ;
            }
            catch (Exception ex)    
            {
                return "error Occurred " + ex;
            }
        }


        [HttpPut]
        [Route("updateCourse"), Authorize(Roles = "Admin")]
        public string UpdateCourse([FromBody] Course course)
        {
            try
            {
                var newChanges= _context.Courses.Where(e=> e.Id == course.Id).SingleOrDefault();
                newChanges.Id=course.Id;
                newChanges.CourseName=course.CourseName;
                newChanges.CourseDurationInWeeks = course.CourseDurationInWeeks;
                newChanges.CourseTrainer = course.CourseTrainer;
                _context.SaveChanges();
                return "course " + course.CourseName + " is being Updated";
            }
            catch(Exception ex)
            {
                return "Error Occured " + ex;
            }
            
        }

        [HttpDelete]
        [Route("deleteCourse/{id}"), Authorize(Roles = "Admin")]
        public string DeleteEmployee(int? id)
        {
            try
            {               
                var course = _context.Courses.Where(e => e.Id == id).SingleOrDefault();
                    _context.Courses.Remove(course);
                    _context.SaveChanges();
                
                return "Course with Id=" + id + " is deleted successfully";
            }
            catch (Exception ex)
            {
                return "Exception occurred: " + ex;
            }
        }


        [HttpGet]
        [Route("getFeedback"), Authorize(Roles = "Admin")]
        public IEnumerable<Feedback> GetFeedback()
        {
            var Feedback = _context.Feedbacks;

            return (Feedback);
        }


        [HttpDelete]
        [Route("deleteFeedback/{id}"), Authorize(Roles = "Admin")]
        public string DeleteFeedback(int? id)
        {
            try
            {
                var feedback = _context.Feedbacks.Where(e => e.Id == id).SingleOrDefault();
                _context.Feedbacks.Remove(feedback);
                _context.SaveChanges();

                return "Feedback with Id=" + id + " is deleted successfully";
            }
            catch (Exception ex)
            {
                return "Exception occurred: " + ex;
            }
        }




    }
}


