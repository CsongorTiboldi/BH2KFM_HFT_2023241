using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

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

        [NotMapped]
        public virtual Subject Subject { get; set; }

        [NotMapped]
        public virtual Room Room { get; set; }

        public Course() { }

        public Course(string data)
        {
            string[] d = data.Split(';');
            CourseID = int.Parse(d[0]);
            StartTime = DateTime.ParseExact(d[1], "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
            EndTime = DateTime.ParseExact(d[2], "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
            CourseSubject = int.Parse(d[3]);
            Location = int.Parse(d[4]);
        }

        public override string ToString()
        {
            return $"#{CourseID}, {StartTime.ToString("HH:mm")}-{EndTime.ToString("HH:mm")} ({StartTime.DayOfWeek}), subject ID: #{CourseSubject}, room ID: #{Location}";
        }
    }
}
