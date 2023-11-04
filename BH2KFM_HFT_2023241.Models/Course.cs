using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH2KFM_HFT_2023241.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [ForeignKey(nameof(Subject))]
        public int CourseSubject { get; set; }

        [ForeignKey(nameof(Room))]
        public int Location { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Room Room { get; set; }

        public Course(string data)
        {
            string[] d = data.Split(';');
            CourseID = int.Parse(d[0]);
            StartTime = DateTime.ParseExact(d[1], "ddd HH mm",null);
            EndTime = DateTime.ParseExact(d[2], "ddd HH mm",null);
            CourseSubject = int.Parse(d[3]);
            Location = int.Parse(d[4]);
        }
    }
}
