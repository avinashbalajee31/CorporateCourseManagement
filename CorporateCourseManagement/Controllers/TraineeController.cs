using CorporateCourseManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCourseManagement.Controllers
{
    public class TraineeController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly CorporateCourseManagementDbContext _context;

        public TraineeController(IConfiguration configuration, CorporateCourseManagementDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }



        [HttpPost]
        [Route("enrollInCourse"), Authorize(Roles = "Trainee")]
        public async Task<ActionResult<Course>> EnrollInCourse([FromBody] Course courseId)
        {
            var flag = 0;
            var TokenVariables = HttpContext.User;
            var Name = "";
            if (TokenVariables?.Claims != null)
            {
                foreach (var claim in TokenVariables.Claims)
                {
                    Name = claim.Value;
                    break;
                }
            }



            try
            {
                var newChanges = _context.Courses.Where(e => e.Id == courseId.Id).SingleOrDefault();

                var enrolledNames = newChanges.NameOfTheStudentsEnrolled;
                if(enrolledNames != null)
                {
                    char separator = ',' ;
                    string[] namesAlreadyEnrolled = newChanges.NameOfTheStudentsEnrolled.Split(separator);
                    //new string[] { newChanges.NameOfTheStudentsEnrolled.Split(",") };
                    foreach (String NamesInArray in namesAlreadyEnrolled)
                    {
                        if (NamesInArray.Equals(Name))
                        {
                            flag = 1;
                        }
                    }
                }

                

                if (newChanges != null && flag==0)
                {
                    var NoOfStudentsEnrolled = newChanges.NoOfStudentsEnrolled;
                    newChanges.NoOfStudentsEnrolled = NoOfStudentsEnrolled + 1;
                    if (enrolledNames != null)
                    {
                        newChanges.NameOfTheStudentsEnrolled = enrolledNames  + Name + ",";
                    }
                    else
                    {
                        newChanges.NameOfTheStudentsEnrolled = Name + ",";
                    }
                    _context.SaveChanges();
                    Send.Producer(Name + " you have successfully registered for " + newChanges.CourseName + " course") ;

                    return Ok(Name + " you have successfully registered for " + newChanges.CourseName + " course");
                }
                else if (flag == 1)
                {
                    Send.Producer("You have already registered in the course, please select a different course to enroll");

                    return BadRequest("You have already registered in the course, please select a different course to enroll");
                }
                else
                {
                    Send.Producer("You couldn't register");

                    return BadRequest("You couldn't register");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured " + ex);
            }
        }



        [HttpDelete]
        [Route("withdrawCourse/{id}"), Authorize(Roles = "Trainee")]
        public string withdrawCourse( int? id)
        {
            try
            {
                var TokenVariables = HttpContext.User;
                var count = 0;
                var name = "";
                if (TokenVariables?.Claims != null)
                {
                    foreach (var claim in TokenVariables.Claims)
                    {
                        name = claim.Value;
                        break;
                    }
                }

                var newChanges = _context.Courses.Where(e => e.Id ==id).SingleOrDefault();

                string[] list = newChanges.NameOfTheStudentsEnrolled.Split(',');
                foreach (var w in list)
                {
                    if (name.Equals(w))
                    {
                        break;
                    }
                    else
                    {
                        count += 1;
                    }
                }
                var newString = "";
                foreach (var w in list)
                {
                    if (w == list[count])
                    {
                        continue;
                    }
                    else
                    {
                        newString += w + ",";
                    }
                }
                newChanges.NoOfStudentsEnrolled = newChanges.NoOfStudentsEnrolled - 1;
                newChanges.NameOfTheStudentsEnrolled = "";
                newChanges.NameOfTheStudentsEnrolled = newString;
                _context.SaveChanges();
                return "withdrawn from the course";
            }
            catch (Exception ex)
            {
                return "Exception occurred: " + ex;
            }
        }








        [HttpPost]
        [Route("feedbackByTrainee"), Authorize(Roles = "Trainee")]
        public async Task<ActionResult<Feedback>> FeedbackByTrainee([FromBody] Feedback feedback)
        {

            var checkUserRole = _context.Users.Where(x=> x.Name==feedback.AboutWhom).SingleOrDefault();  
            if (checkUserRole.Role != "Trainee") { 
                var TokenVariables = HttpContext.User;
                var Name = "";
                if (TokenVariables?.Claims != null)
                {
                    foreach (var claim in TokenVariables.Claims)
                    {
                        Name = claim.Value;
                        break;
                    }
                }
                var userDetails = _context.Users.Where(x => x.Name == Name).SingleOrDefault();
                var insert = new Feedback
                {
                    Name = userDetails.Name,
                    EmailId = userDetails.EmailId,
                    Role = userDetails.Role,
                    AboutWhom = feedback.AboutWhom,
                    FeedbackInWords = feedback.FeedbackInWords,
                    Rating = feedback.Rating
                };


                _context.Feedbacks.Add(insert);
                await _context.SaveChangesAsync();
                Send.Producer("Your Feedback about Trainer is Saved");

                return Ok("Your Feedback about Trainer is Saved");
            }

            else
            {
                return BadRequest("You cannot give feedback about another trainee");
            }

        }
    }
}
