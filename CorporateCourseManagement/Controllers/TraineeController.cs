﻿using CorporateCourseManagement.Models;
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
                    return Ok(Name + " you have successfully registered for " + newChanges.CourseName + " course");
                }
                else if (flag == 1)
                {
                    return BadRequest("You have already registered in the course, please select a different course to enroll");
                }
                else
                {
                    return BadRequest("You couldn't register");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured " + ex);
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
                return Ok("Your Feedback about Trainer is Saved");
            }

            else
            {
                return BadRequest("You cannot give feedback about another trainee");
            }

        }
    }
}
