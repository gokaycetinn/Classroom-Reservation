using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ClassroomReservation.Data;
using ClassroomReservation.Models;
using System.Net.Http;
using Newtonsoft.Json;
using ClassroomReservation.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ClassroomReservation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class PanelModel : PageModel
    {
        private readonly ClassroomDbContext _context;
        private readonly IdentityDataInitializer _identityInitializer;
        private readonly ILogger<PanelModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EmailService _emailService;

        public PanelModel(
            ClassroomDbContext context,
            IdentityDataInitializer identityInitializer,
            ILogger<PanelModel> logger,
            UserManager<IdentityUser> userManager,
            EmailService emailService)
        {
            _context = context;
            _identityInitializer = identityInitializer;
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
        }

        public class ReservationRequest
        {
            public int Id { get; set; }
            public string? ClassroomName { get; set; } = string.Empty;
            public DateTime Date { get; set; }
            public string? StartTime { get; set; } = string.Empty;
            public string? EndTime { get; set; } = string.Empty;
            public string Status { get; set; } = "Pending";
            public string SemesterName { get; set; } = string.Empty;
            public string InstructorName { get; set; } = string.Empty;
            public int InstructorId { get; set; }
            public DateTime RequestDate { get; set; }
            public DateTime ReservationDate { get; set; }
            public bool IsConflict { get; set; }
            public bool IsHoliday { get; set; }
        }

        public class AcademicTerm
        {
            public required string Name { get; set; }
        }

        public class Class
        {
            public required string Name { get; set; }
            public double AverageRating { get; set; }
            public List<string> WeeklySchedule { get; set; } = new();
            public List<Feedback> Feedback { get; set; } = new();
        }

        public class Feedback
        {
            public required string Comment { get; set; }
            public int Rating { get; set; }
        }

        public class ReservationConflict
        {
            public string ClassroomName { get; set; } = string.Empty;
            public DateTime Date { get; set; }
            public string StartTime { get; set; } = string.Empty;
            public string EndTime { get; set; } = string.Empty;
        }

        public List<ReservationRequest> ReservationRequests { get; set; } = new();
        public List<AcademicTerm> AcademicTerms { get; set; } = new();
        public List<ClassroomReservation.Models.Instructor> Instructors { get; set; } = new();
        public List<Class> Classes { get; set; } = new();
        public required Class SelectedClass { get; set; }
        public List<ReservationConflict> ReservationConflicts { get; set; } = new();

        

        public List<Semester> Semesters { get; set; } = new();

        [BindProperty]
        public required string TermName { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public string? InstructorName { get; set; }
        [BindProperty]
        public string? InstructorEmail { get; set; }
        [BindProperty]
        public string? InstructorPassword { get; set; }

        [BindProperty]
        public string? EditInstructorName { get; set; }
        
        [BindProperty]
        public string? EditInstructorEmail { get; set; }

        [BindProperty]
        public int EditSemesterYear { get; set; }
        
        [BindProperty]
        public string? EditSemesterSeason { get; set; }
        
        public async Task<IActionResult> OnPostEditInstructor(int instructorId)
        {
            
            if (string.IsNullOrEmpty(EditInstructorName) || string.IsNullOrEmpty(EditInstructorEmail))
            {
                TempData["Error"] = "All fields are required.";
                return RedirectToPage();
            }
            
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(EditInstructorEmail, 
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                TempData["Error"] = "Please enter a valid email address.";
                return RedirectToPage();
            }
            
            
            if (EditInstructorName.Length < 2 || EditInstructorName.Length > 50)
            {
                TempData["Error"] = "Name must be between 2 and 50 characters.";
                return RedirectToPage();
            }
           
            var instructor = await _context.Instructors.FindAsync(instructorId);
            if (instructor == null)
            {
                TempData["Error"] = "Instructor not found.";
                return RedirectToPage();
            }
            
            
            if (instructor.Email != EditInstructorEmail)
            {
                var existingUser = await _userManager.FindByEmailAsync(EditInstructorEmail);
                if (existingUser != null && existingUser.Email != instructor.Email)
                {
                    TempData["Error"] = "Email address is already in use.";
                    return RedirectToPage();
                }
                
                
                var identityUser = await _userManager.FindByEmailAsync(instructor.Email);
                if (identityUser != null)
                {
                    identityUser.Email = EditInstructorEmail;
                    identityUser.UserName = EditInstructorEmail;
                    var result = await _userManager.UpdateAsync(identityUser);
                    
                    if (!result.Succeeded)
                    {
                        TempData["Error"] = "Failed to update identity user email: " + 
                            string.Join(", ", result.Errors.Select(e => e.Description));
                        return RedirectToPage();
                    }
                }
            }
            
            
            instructor.Name = EditInstructorName;
            instructor.Email = EditInstructorEmail;
            
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated instructor: {instructor.Email}");
                TempData["SuccessMessage"] = "Instructor updated successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update instructor: {instructor.Email}");
                TempData["Error"] = "Failed to update instructor. Please try again.";
            }
            
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditSemester(int semesterId)
        {
            
            if (EditSemesterYear <= 0 || string.IsNullOrEmpty(EditSemesterSeason))
            {
                TempData["Error"] = "Year and season are required.";
                return RedirectToPage();
            }
            
           
            if (EditSemesterSeason != "Spring" && EditSemesterSeason != "Fall")
            {
                TempData["Error"] = "Season must be either Spring or Fall.";
                return RedirectToPage();
            }
            
            
            var semester = await _context.Semesters.FindAsync(semesterId);
            if (semester == null)
            {
                TempData["Error"] = "Semester not found.";
                return RedirectToPage();
            }
            
            
            var existingSemester = _context.Semesters.FirstOrDefault(s => 
                s.Id != semesterId && 
                s.Year == EditSemesterYear && 
                s.Season == EditSemesterSeason);
                
            if (existingSemester != null)
            {
                TempData["Error"] = $"A semester with year {EditSemesterYear} and season {EditSemesterSeason} already exists.";
                return RedirectToPage();
            }
            
           
            semester.Year = EditSemesterYear;
            semester.Season = EditSemesterSeason;
            
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated semester: {semester.Year} {semester.Season}");
                TempData["SuccessMessage"] = "Academic term updated successfully.";
                
               
                var oldSemesterName = $"{semester.Year} - {semester.Season}";
                var newSemesterName = $"{EditSemesterYear} - {EditSemesterSeason}";
                
                var reservationsToUpdate = _context.Reservations.Where(r => r.SemesterName == oldSemesterName).ToList();
                foreach (var reservation in reservationsToUpdate)
                {
                    reservation.SemesterName = newSemesterName;
                }
                
                if (reservationsToUpdate.Any())
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Updated {reservationsToUpdate.Count} reservations with new semester name: {newSemesterName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update semester: {semester.Year} {semester.Season}");
                TempData["Error"] = "Failed to update academic term. Please try again.";
            }
            
            return RedirectToPage();
        }

        public async Task OnGetAsync()
        {
            var reservations = _context.Reservations.ToList();
            var holidayDates = reservations.Select(r => r.ReservationDate).Distinct().ToList();

            var holidays = new HashSet<DateTime>();
            foreach (var date in holidayDates)
            {
                if (await CheckIfHoliday(date))
                {
                    holidays.Add(date);
                }
            }

            ReservationRequests = reservations.Select(r => new PanelModel.ReservationRequest
            {
                Id = r.Id,
                ClassroomName = r.ClassroomId,
                Date = r.ReservationDate,
                Status = r.Status,
                SemesterName = r.SemesterName,
                InstructorName = r.InstructorName,
                InstructorId = r.InstructorId,
                RequestDate = r.RequestDate,
                ReservationDate = r.ReservationDate,
                StartTime = r.StartTime?.ToString("hh\\:mm"), 
                EndTime = r.EndTime?.ToString("hh\\:mm"),   
                IsConflict = reservations.Any(other => 
                    other.Id != r.Id && 
                    other.ClassroomId == r.ClassroomId && 
                    other.ReservationDate.Date == r.ReservationDate.Date &&
                    other.SemesterName == r.SemesterName &&
                    ((other.StartTime <= r.StartTime && r.StartTime < other.EndTime) || 
                     (other.StartTime < r.EndTime && r.EndTime <= other.EndTime) ||
                     (r.StartTime <= other.StartTime && other.EndTime <= r.EndTime))),
                IsHoliday = holidays.Contains(r.ReservationDate)
            }).ToList();

            Semesters = _context.Semesters.ToList();

            
            Instructors = _context.Instructors.ToList();
        }        private async Task<bool> CheckIfHoliday(DateTime date)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    string apiUrl = $"https://date.nager.at/api/v3/PublicHolidays/{date.Year}/TR";

                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var holidays = JsonConvert.DeserializeObject<List<Holiday>>(await response.Content.ReadAsStringAsync());
                        if (holidays != null)
                        {
                            if (holidays.Any(h => h.Date.Date == date.Date))
                            {
                               
                                var reservationsOnHoliday = _context.Reservations.Where(r => r.ReservationDate.Date == date.Date).ToList();
                                foreach (var reservation in reservationsOnHoliday)
                                {
                                    var instructor = _context.Instructors.FirstOrDefault(i => i.Id == reservation.InstructorId);
                                    if (instructor != null)
                                    {
                                        string subject = "Reservation on Holiday";
                                        string body = $"Your reservation for classroom {reservation.ClassroomId} on {reservation.ReservationDate:d} falls on an official holiday.";
                                        await _emailService.SendEmailAsync(toEmail: instructor.Email, subject: subject, body: body, fromEmail: "gokaycetin10@gmail.com");
                                    }
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Holiday API Error: {ex.Message}");
            }
            return false;
        }

        private class Holiday
        {
            public DateTime Date { get; set; }
            public string? LocalName { get; set; }
            public string? Name { get; set; }
            public string? CountryCode { get; set; }
            public bool Global { get; set; }
            public bool Fixed { get; set; }
            public string[]? Counties { get; set; }
            public string? Type { get; set; }
        }        public async Task<IActionResult> OnPostAddInstructor()
        {
            if (string.IsNullOrEmpty(InstructorName) || string.IsNullOrEmpty(InstructorEmail) || string.IsNullOrEmpty(InstructorPassword))
            {
                ModelState.AddModelError(string.Empty, "All fields are required.");
                return Page();
            }

           
            var existingUser = await _userManager.FindByEmailAsync(InstructorEmail);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Email address is already in use.");
                return Page();
            }

            try 
            {
              
                await _identityInitializer.CreateInstructorUserAsync(InstructorEmail, InstructorPassword);
                
               
                var identityUser = await _userManager.FindByEmailAsync(InstructorEmail);
                if (identityUser == null)
                {
                    throw new Exception("Failed to create identity user.");
                }

                
                var instructor = new ClassroomReservation.Models.Instructor
                {
                    Name = InstructorName,
                    Email = InstructorEmail,
                    Password = identityUser.PasswordHash ?? throw new Exception("Password hash is null")
                };

                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Created new instructor: {InstructorEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to create instructor: {InstructorEmail}");
                
                
                var identityUser = await _userManager.FindByEmailAsync(InstructorEmail);
                if (identityUser != null)
                {
                    await _userManager.DeleteAsync(identityUser);
                }

                ModelState.AddModelError(string.Empty, "Failed to create instructor account. Please try again.");
                return Page();
            }

            return RedirectToPage("/Admin/Panel");
        }

        public IActionResult OnPostAddTerm()
        {
            if (!int.TryParse(Request.Form["TermYear"], out int year) || string.IsNullOrEmpty(Request.Form["TermSeason"].ToString()))
            {
                ModelState.AddModelError(string.Empty, "Year and Season are required.");
                return Page();
            }

            var season = Request.Form["TermSeason"].ToString();

            var semester = new Semester
            {
                Year = year,
                Season = season
            };

            _context.Semesters.Add(semester);
            _context.SaveChanges();

            return RedirectToPage("/Admin/Panel");
        }        public async Task<IActionResult> OnPostApproveRequest(int requestId)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == requestId);
            if (reservation != null)
            {
                
            var semesterParts = reservation.SemesterName.Split(" - ");
            if (semesterParts.Length != 2 || !int.TryParse(semesterParts[0], out var year))
            {
                TempData["Error"] = "Invalid semester format in reservation.";
                return RedirectToPage();
            }
            var season = semesterParts[1];

            
            var semester = new Semester { Year = year, Season = season };
            
           
            if (!semester.IsDateInSemester(reservation.ReservationDate))
            {
                TempData["Error"] = $"Cannot approve reservation: date {reservation.ReservationDate:d} is not within the {semester.GetDateRangeDescription()} semester period.";
                return RedirectToPage();
            }

           
            var hasConflict = _context.Reservations.Any(other => 
                    other.Id != reservation.Id && 
                    other.ClassroomId == reservation.ClassroomId && 
                    other.ReservationDate.Date == reservation.ReservationDate.Date &&
                    other.SemesterName == reservation.SemesterName &&
                    ((other.StartTime <= reservation.StartTime && reservation.StartTime < other.EndTime) || 
                     (other.StartTime < reservation.EndTime && reservation.EndTime <= other.EndTime) ||
                     (reservation.StartTime <= other.StartTime && other.EndTime <= reservation.EndTime)));

            if (hasConflict)
            {
                TempData["Error"] = "Cannot approve reservation due to scheduling conflict.";
                return RedirectToPage();
            }

                reservation.Status = "Approved";
                _context.SaveChanges();

                
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (loggedInUser == null || string.IsNullOrEmpty(loggedInUser.Email))
                {
                    _logger.LogError("Failed to retrieve the logged-in user's email.");
                    TempData["Error"] = "Unable to send email. Please try again later.";
                    return RedirectToPage();
                }

                var instructor = _context.Instructors.FirstOrDefault(i => i.Id == reservation.InstructorId);                if (instructor != null)
                {
                    string subject = "Reservation Approved";
                    string body = $"Your reservation for classroom {reservation.ClassroomId} on {reservation.ReservationDate:d} from {reservation.StartTime} to {reservation.EndTime} has been approved.";
                    await _emailService.SendEmailAsync(toEmail: instructor.Email, subject: subject, body: body, fromEmail: "gokaycetin10@gmail.com");
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectRequest(int requestId)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == requestId);
            if (reservation != null)
            {
                reservation.Status = "Rejected";
                _context.SaveChanges();

                
                var instructor = _context.Instructors.FirstOrDefault(i => i.Id == reservation.InstructorId);                if (instructor != null)
                {
                    string subject = "Reservation Rejected";
                    string body = $"Your reservation for classroom {reservation.ClassroomId} on {reservation.ReservationDate:d} from {reservation.StartTime} to {reservation.EndTime} has been rejected.";
                    await _emailService.SendEmailAsync(toEmail: instructor.Email, subject: subject, body: body, fromEmail: "gokaycetin10@gmail.com");
                }
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDeleteRequest(int requestId)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == requestId);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }        public async Task<IActionResult> OnPostDeleteInstructor(int instructorId)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == instructorId);
            if (instructor != null)
            {
               
                var identityUser = await _userManager.FindByEmailAsync(instructor.Email);
                if (identityUser != null)
                {
                    var result = await _userManager.DeleteAsync(identityUser);
                    if (!result.Succeeded)
                    {
                        _logger.LogError($"Failed to delete identity user for instructor {instructor.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        TempData["Error"] = "Failed to delete instructor account. Please try again.";
                        return RedirectToPage();
                    }
                }

                
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully deleted instructor {instructor.Email}");
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDeleteSemester(int semesterId)
        {
            var semester = _context.Semesters.FirstOrDefault(s => s.Id == semesterId);
            if (semester != null)
            {
                _context.Semesters.Remove(semester);
                _context.SaveChanges();
            }

            return RedirectToPage();        }
    }
}