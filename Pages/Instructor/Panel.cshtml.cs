using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using ClassroomReservation.Data;
using ClassroomReservation.Models;
using ClassroomReservation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace ClassroomReservation.Pages.Instructor
{
    [Authorize(Roles = "Instructor")]
    public class PanelModel : PageModel
    {
        private readonly ClassroomDbContext _context;
        private readonly EmailService _emailService;
        private ReservationRequest newRequest = new ReservationRequest
        {
            ClassroomId = string.Empty,
            Date = DateTime.Today,
            StartTime = null,
            EndTime = null
        };

        private FeedbackModel newFeedback = new FeedbackModel
        {
            ClassroomId = null,
            Message = null
        };

        
        private const string AdminEmail = "gokaycetin10@gmail.com";        public PanelModel(ClassroomDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public class Reservation
        {
            public int Id { get; set; }
            public string ClassroomId { get; set; } = string.Empty;
            public DateTime ReservationDate { get; set; }
            public DateTime RequestDate { get; set; }
            public string Status { get; set; } = string.Empty;
            public int InstructorId { get; set; }
            public string SemesterName { get; set; } = string.Empty;
            public string InstructorName { get; set; } = string.Empty;
            public string ClassroomName { get; set; } = string.Empty;
            public TimeSpan? StartTime { get; set; }
            public TimeSpan? EndTime { get; set; }
            public DateTime Date { get; set; }
        }

        public class ReservationRequest
        {
            public required string ClassroomId { get; set; }
            public DateTime? Date { get; set; }
            public TimeSpan? StartTime { get; set; }
            public TimeSpan? EndTime { get; set; }
        }

        public class FeedbackModel
        {
            public string? ClassroomId { get; set; }
            public string? Message { get; set; }
        }

        public List<SelectListItem> ClassroomList { get; set; } = new()
        {
            new SelectListItem { Text = "Room A", Value = "A" },
            new SelectListItem { Text = "Room B", Value = "B" },
            new SelectListItem { Text = "Room C", Value = "C" },
            new SelectListItem { Text = "Room D", Value = "D" },
            new SelectListItem { Text = "Room E", Value = "E" }
        };

        public static List<Reservation> AllReservations { get; set; } = new();
        public required List<Reservation> MyReservations { get; set; }
        public List<Semester> SemesterList { get; set; } = new();

        [BindProperty]
        public ReservationRequest NewRequest { get => newRequest; set => newRequest = value; }

        [BindProperty]
        public FeedbackModel NewFeedback { get => newFeedback; set => newFeedback = value; }

        public void OnGet()
        {
            var email = HttpContext.Session.GetString("InstructorEmail");
            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidOperationException("Instructor email not found in session.");
            }

            var instructor = _context.Instructors.FirstOrDefault(i => i.Email == email);
            if (instructor == null)
            {
                MyReservations = new List<Reservation>();
                throw new InvalidOperationException("Instructor not found.");
            }

            
            var dbReservations = _context.Reservations
                .Where(r => r.InstructorId == instructor.Id)
                .OrderByDescending(r => r.RequestDate)
                .ToList();

            MyReservations = dbReservations.Select(r => new Reservation
            {
                Id = r.Id,
                ClassroomId = r.ClassroomId,
                ReservationDate = r.ReservationDate,
                RequestDate = r.RequestDate,
                Status = r.Status,
                InstructorId = r.InstructorId,
                SemesterName = r.SemesterName,
                InstructorName = r.InstructorName,
                StartTime = r.StartTime,
                EndTime = r.EndTime
            }).ToList();

            
            var today = DateTime.Today;
            SemesterList = _context.Semesters
                .AsEnumerable() 
                .Where(s => s.GetEndDate() >= today)
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Season)
                .ToList();
        }

        public IActionResult OnPostSubmitRequest()
        {
            
            MyReservations = new List<Reservation>();
            
            
            SemesterList = _context.Semesters.ToList();

            var email = HttpContext.Session.GetString("InstructorEmail");
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError(string.Empty, "Session expired. Please log in again.");
                return RedirectToPage("/Index");
            }

            var instructor = _context.Instructors.FirstOrDefault(i => i.Email == email);
            if (instructor == null)
            {
                ModelState.AddModelError(string.Empty, "Instructor not found.");
                return RedirectToPage("/Index");
            }

            
            var dbReservations = _context.Reservations
                .Where(r => r.InstructorId == instructor.Id)
                .OrderByDescending(r => r.RequestDate)
                .ToList();

            MyReservations = dbReservations.Select(r => new Reservation
            {
                Id = r.Id,
                ClassroomId = r.ClassroomId,
                ReservationDate = r.ReservationDate,
                RequestDate = r.RequestDate,
                Status = r.Status,
                InstructorId = r.InstructorId,
                SemesterName = r.SemesterName,
                InstructorName = r.InstructorName,
                StartTime = r.StartTime,
                EndTime = r.EndTime
            }).ToList();

            if (NewRequest == null || 
                string.IsNullOrEmpty(NewRequest.ClassroomId) || 
                NewRequest.Date == null || 
                NewRequest.StartTime == null || 
                NewRequest.EndTime == null)
            {
                ModelState.AddModelError(string.Empty, "All fields are required.");
                return Page();
            }

            if (NewRequest.StartTime >= NewRequest.EndTime)
            {
                ModelState.AddModelError(string.Empty, "End time must be after start time.");
                return Page();
            }

            var reservationDateTime = NewRequest.Date.Value.Date.Add(NewRequest.StartTime.Value);

            
            var selectedSemesterId = Request.Form["SelectedSemester"].ToString();
            var selectedSemester = _context.Semesters.FirstOrDefault(s => s.Id.ToString() == selectedSemesterId);
            
            if (selectedSemester == null)
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Selected semester is invalid.");
                return Page();
            }

            
            if (!selectedSemester.IsDateInSemester(NewRequest.Date.Value))
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, $"Selected date must be within the {selectedSemester.GetDateRangeDescription()} semester period.");
                
                
                ViewData["SelectedSemester"] = selectedSemesterId;
                ViewData["ClassroomId"] = NewRequest.ClassroomId;
                ViewData["Date"] = NewRequest.Date?.ToString("yyyy-MM-dd");
                ViewData["StartTime"] = NewRequest.StartTime?.ToString(@"hh\:mm");
                ViewData["EndTime"] = NewRequest.EndTime?.ToString(@"hh\:mm");
                
                return Page();
            }

            var newReservation = new ClassroomReservation.Models.Reservation
            {
                ClassroomId = NewRequest.ClassroomId,
                ReservationDate = reservationDateTime,
                RequestDate = DateTime.Now,
                Status = "Pending",
                InstructorId = instructor.Id,
                InstructorName = instructor.Name, 
                SemesterName = $"{selectedSemester.Year} - {selectedSemester.Season}",
                StartTime = NewRequest.StartTime, 
                EndTime = NewRequest.EndTime      
            };

            try
            {
                if (_context == null)
                    throw new InvalidOperationException("Database context is not initialized.");

                _context.Reservations.Add(newReservation);
                _context.SaveChanges();

                
                var subject = "New Reservation Request";
                var body = $"A new reservation request has been submitted by {instructor.Name} for Room {newReservation.ClassroomId} on {newReservation.ReservationDate.ToShortDateString()} from {newReservation.StartTime} to {newReservation.EndTime}.";
                _emailService.SendEmailAsync(AdminEmail, subject, body, instructor.Email).Wait();

                TempData["ReservationSuccess"] = "Reservation has been successfully submitted!";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the reservation.");
                Console.WriteLine($"Error: {ex.Message}");
                return Page();
            }
        }

        
        //----------------------------------------------------------------//

        public async Task<IActionResult> OnPostSubmitFeedback()
        {
            Console.WriteLine($"Received ClassroomId: {NewFeedback.ClassroomId}");

            
            MyReservations = new List<Reservation>();

            var email = HttpContext.Session.GetString("InstructorEmail");
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError(string.Empty, "Session expired. Please log in again.");
                return RedirectToPage("/Index");
            }

            var instructor = _context.Instructors.FirstOrDefault(i => i.Email == email);
            if (instructor == null)
            {
                ModelState.AddModelError(string.Empty, "Instructor not found.");
                return RedirectToPage("/Index");
            }

            
            var dbReservations = _context.Reservations
                .Where(r => r.InstructorId == instructor.Id)
                .OrderByDescending(r => r.RequestDate)
                .ToList();

            MyReservations = dbReservations.Select(r => new Reservation
            {
                Id = r.Id,
                ClassroomId = r.ClassroomId,
                ReservationDate = r.ReservationDate,
                RequestDate = r.RequestDate,
                Status = r.Status,
                InstructorId = r.InstructorId,
                SemesterName = r.SemesterName,
                InstructorName = r.InstructorName,
                StartTime = r.StartTime,
                EndTime = r.EndTime
            }).ToList();

            if (!int.TryParse(Request.Form["NewFeedback.Rating"], out var rating) || rating < 1 || rating > 5)
            {
                ModelState.AddModelError(string.Empty, "Invalid rating value.");
                return Page();
            }

            if (string.IsNullOrEmpty(NewFeedback.ClassroomId) || string.IsNullOrEmpty(NewFeedback.Message))
            {
                ModelState.AddModelError(string.Empty, "All fields are required.");
                return Page();
            }

            var feedback = new ClassroomReservation.Models.Feedback
            {
                InstructorId = instructor.Id,
                Message = NewFeedback.Message,
                CreatedDate = DateTime.Now,
                Rating = rating,
                ClassroomId = NewFeedback.ClassroomId
            };

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

            var subject = "New Feedback Submitted";
            var body = $"A new feedback has been submitted by {instructor.Name} for Room {NewFeedback.ClassroomId}. Message: {NewFeedback.Message}.";
            await _emailService.SendEmailAsync(toEmail: AdminEmail, subject: subject, body: body, fromEmail: instructor.Email);

            TempData["FeedbackSuccess"] = "Feedback has been successfully submitted!";
            return RedirectToPage();
        }


        public JsonResult OnPostAddReservationAjax([FromBody] ReservationRequest request)
        {
            
            MyReservations = new List<Reservation>();
            
            if (string.IsNullOrEmpty(request.ClassroomId) || request.Date == null ||
                request.StartTime == null || request.EndTime == null)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { error = "All fields are required." });
            }

            if (request.StartTime >= request.EndTime)
            {
                Response.StatusCode = 400;
                return new JsonResult(new { error = "End time must be after start time." });
            }

            var classroomName = ClassroomList.FirstOrDefault(c => c.Value == request.ClassroomId)?.Text ?? "Unknown";

            var newReservation = new Reservation
            {
                ClassroomName = classroomName,
                Date = request.Date.Value,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };

            AllReservations.Add(newReservation);
            return new JsonResult(newReservation);
        }

        public IActionResult OnGetGetReservations()
        {
            
            MyReservations = new List<Reservation>();
              var reservations = _context.Reservations.Select(r => new
            {
                id = r.Id,
                instructorId = r.InstructorId,
                classroomId = r.ClassroomId,
                requestDate = r.RequestDate,
                reservationDate = r.ReservationDate,
                status = r.Status,
                semesterName = r.SemesterName,
                instructorName = r.InstructorName,
                title = " "  + " - " +
                    (r.EndTime.HasValue ? r.EndTime.Value.ToString(@"hh\:mm") : "") + "" + " Room: " + r.ClassroomId  ,
                start = r.ReservationDate,
                end = r.ReservationDate.AddHours(1) 
            }).ToList();

            return new JsonResult(reservations);
        }


        public async Task<IActionResult> OnPostLogout()
        {
            
            MyReservations = new List<Reservation>();
            
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }        public async Task<IActionResult> OnPostEditReservation(int reservationId)
        {
            
            var username = User.Identity?.Name;
            var instructor = _context.Instructors.FirstOrDefault(i => i.Email == username);
            
            if (instructor == null)
            {
                return RedirectToPage();
            }
            
            
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservation == null)
            {
                return RedirectToPage();
            }
            
            try
            {
                
                string subject = "Reservation Edit Request";
                string body = $"Instructor named {instructor.Name} wants to make changes to his reservation dated {reservation.ReservationDate.ToString("dd.MM.yyyy")}";
                
                await _emailService.SendEmailAsync(
                    toEmail: AdminEmail, 
                    subject: subject,
                    body: body,
                    fromEmail: instructor.Email 
                );
                
                TempData["ReservationSuccess"] = "Your edit request has been sent to the administrator.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to send notification: {ex.Message}";
            }
            
            return RedirectToPage();
        }
          public async Task<IActionResult> OnPostCancelReservation(int reservationId)
        {
            
            var username = User.Identity?.Name;
            var instructor = _context.Instructors.FirstOrDefault(i => i.Email == username);
            
            if (instructor == null)
            {
                return RedirectToPage();
            }
            
          
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservation == null)
            {
                return RedirectToPage();
            }
            
            try
            {
               
                string subject = "Reservation Cancellation Request";
                string body = $"Instructor named {instructor.Name} wants to cancel his reservation dated {reservation.ReservationDate.ToString("dd.MM.yyyy")}";
                
                await _emailService.SendEmailAsync(
                    toEmail: AdminEmail, 
                    subject: subject,
                    body: body,
                    fromEmail: instructor.Email 
                );
                
                TempData["ReservationSuccess"] = "Your cancellation request has been sent to the administrator.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to send notification: {ex.Message}";
            }
            
            return RedirectToPage();
        }
    }
}