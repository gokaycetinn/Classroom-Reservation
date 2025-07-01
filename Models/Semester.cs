using System;
using System.Collections.Generic;

namespace ClassroomReservation.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public required string Season { get; set; }

        public DateTime GetStartDate()
        {
            return Season.ToLower() == "fall" 
                ? new DateTime(Year, 9, 1)   
                : new DateTime(Year, 2, 1);  
        }

        public DateTime GetEndDate()
        {
            return Season.ToLower() == "fall" 
                ? new DateTime(Year + 1, 1, 31)  
                : new DateTime(Year, 6, 30);     
        }

        public bool IsDateInSemester(DateTime date)
        {
            var startDate = GetStartDate();
            var endDate = GetEndDate();
            return date >= startDate && date <= endDate;
        }

        public string GetDateRangeDescription()
        {
            return Season.ToLower() == "fall"
                ? $"Eylül {Year} - Ocak {Year + 1}"
                : $"Şubat {Year} - Haziran {Year}";
        }
    }
}