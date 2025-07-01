using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ClassroomReservation.Models;

namespace ClassroomReservation.Data
{
    public class ClassroomDbContext : IdentityDbContext<IdentityUser>
    {
        public ClassroomDbContext(DbContextOptions<ClassroomDbContext> options)
            : base(options)
        {
        }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Semester> Semesters { get; set; }
    }
}