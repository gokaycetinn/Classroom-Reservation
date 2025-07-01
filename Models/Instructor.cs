using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomReservation.Pages.Admin
{
}

namespace ClassroomReservation.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(512)]  
        public required string Password { get; set; }  
    }
}