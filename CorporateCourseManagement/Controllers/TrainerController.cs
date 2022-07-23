using CorporateCourseManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateCourseManagement.Controllers
{
    public class TrainerController : Controller
    {


        private readonly IConfiguration _configuration;
        private readonly CorporateCourseManagementDbContext _context;

        public TrainerController(IConfiguration configuration, CorporateCourseManagementDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPut]
        [Route("updateCourseDuration"), Authorize(Roles = "Trainer")]
        public string UpdateCourseDuration([FromBody] Course course)
        {
            try
            {
                var newChanges = _context.Courses.Where(e => e.Id == course.Id).SingleOrDefault();
                newChanges.CourseDurationInWeeks = course.CourseDurationInWeeks;
                _context.SaveChanges();
                return "course " + newChanges.CourseName+ " is being Updated";
            }
            catch (Exception ex)
            {
                return "Error Occured " + ex;
            }

        }


        [HttpPost]
        [Route("feedbackByTrainer"), Authorize(Roles = "Trainer")]
        public async Task<ActionResult<Feedback>> feedbackByTrainer([FromBody] Feedback feedback)
        {

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
                Name=userDetails.Name,
                EmailId=userDetails.EmailId,
                Role=userDetails.Role,
                AboutWhom=feedback.AboutWhom,
                FeedbackInWords=feedback.FeedbackInWords,
                Rating=feedback.Rating
            };


            _context.Feedbacks.Add(insert);
            await _context.SaveChangesAsync();
            return Ok("Your Feedback is Saved");
        }


    }
}
