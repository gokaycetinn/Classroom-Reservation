using System.Collections.Generic;

namespace ClassroomReservation.Models
{
public class Reservation
{
    public int Id { get; set; }
  
    public string ClassroomId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; }
    public int InstructorId { get; set; }
    public string SemesterName { get; set; }
    public string InstructorName { get; set; } 
    public TimeSpan? StartTime { get; set; } 
    public TimeSpan? EndTime { get; set; } 
   
}
}