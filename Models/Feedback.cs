using System.Collections.Generic;

namespace ClassroomReservation.Models
{

public class Feedback
{
    public int Id { get; set; }
    public int InstructorId { get; set; }
    public string? Message { get; set; } 
    public DateTime CreatedDate { get; set; }
    public int Rating { get; set; } 
    public string? ClassroomId { get; set; } 
    public virtual Instructor? Instructor { get; set; } 
}

}