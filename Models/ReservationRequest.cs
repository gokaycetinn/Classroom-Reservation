using System;
using System.ComponentModel.DataAnnotations;

namespace ClassroomReservation.Models
{
    public class ReservationRequest
    {
        [Required]
        public string ClassroomId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan? StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan? EndTime { get; set; }
    }
}
