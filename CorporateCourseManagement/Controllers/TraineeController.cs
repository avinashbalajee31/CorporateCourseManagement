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
        [Route("FeedbackByTrainee"), Authorize(Roles = "Trainee")]
        public async Task<ActionResult<Feedback>> feedbackByTrainee([FromBody] Feedback feedback)
        {

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return Ok("Your Feedback is Saved");
        }
    }
}
