using System;
using System.ComponentModel.DataAnnotations;

namespace BH2KFM_HFT_2023241.Models
{
    internal class Course
    {
        //TODO: setup
        [Key]
        public int CourseID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int CourseSubject { get; set; }

        public int Location { get; set; }
    }
}
