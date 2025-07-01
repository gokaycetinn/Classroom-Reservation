using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ClassroomReservation.Data;
using ClassroomReservation.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClassroomReservation.Pages.Admin
{
    public class FeedbacksModel : PageModel
    {
        private readonly ClassroomDbContext _context;
        private static DateTime currentWeekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);

        public FeedbacksModel(ClassroomDbContext context)
        {
            _context = context;
            Feedbacks = new List<FeedbackWithInstructorName>();
            ClassroomList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Room A", Value = "A" },
                new SelectListItem { Text = "Room B", Value = "B" },
                new SelectListItem { Text = "Room C", Value = "C" },
                new SelectListItem { Text = "Room D", Value = "D" },
                new SelectListItem { Text = "Room E", Value = "E" }
            };
        }

        public List<FeedbackWithInstructorName> Feedbacks { get; set; }
        public Dictionary<string, double> ClassroomAverageRatings { get; set; } = new Dictionary<string, double>();
        public List<SelectListItem> ClassroomList { get; set; }
        public DateTime WeekStartDate => currentWeekStart;

        public void OnGet()
        {
            var feedbacks = _context.Feedbacks.ToList();            Feedbacks = feedbacks
                .Select(f => {
                    var instructor = _context.Instructors.FirstOrDefault(i => i.Id == f.InstructorId);                    return new FeedbackWithInstructorName
                    {
                        Id = f.Id,
                        InstructorName = instructor != null ? instructor.Name : "Unknown",
                        ClassroomId = f.ClassroomId,
                        Message = f.Message,
                        CreatedDate = f.CreatedDate,
                        Rating = f.Rating
                    };
                }).ToList();

            ClassroomAverageRatings = feedbacks
                .GroupBy(f => f.ClassroomId)
                .ToDictionary(
                    g => g.Key?.ToString() ?? "Unknown Classroom",
                    g => g.Average(f => f.Rating)
                );
        }

        public IActionResult OnGetChangeWeek(int offset)
        {
            currentWeekStart = currentWeekStart.AddDays(offset * 7);
            return RedirectToPage();
        }

        public List<List<Reservation>> GetWeeklySchedule(string classroomId)
        {
            var weeklySchedule = new List<List<Reservation>>();

            for (int i = 0; i < 7; i++) // Monday to Sunday
            {
                var currentDay = currentWeekStart.AddDays(i);
                var reservations = _context.Reservations
                    .Where(r => r.ClassroomId == classroomId &&
                           r.ReservationDate.Date == currentDay.Date)
                    .OrderBy(r => r.StartTime)
                    .ToList();

                weeklySchedule.Add(reservations);
            }

            return weeklySchedule;
        }        public class FeedbackWithInstructorName
        {
            public int Id { get; set; }
            public string? InstructorName { get; set; }
            public string? ClassroomId { get; set; }
            public string? Message { get; set; }
            public DateTime CreatedDate { get; set; }
            public int Rating { get; set; }
        }

        public IActionResult OnPostDeleteFeedback(int feedbackId)
        {
            var feedback = _context.Feedbacks.Find(feedbackId);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Feedback has been deleted successfully.";
            }
            return RedirectToPage();
        }
    }
}