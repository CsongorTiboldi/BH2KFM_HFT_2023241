using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BH2KFM_HFT_2023241.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int Credits { get; set; }

        public int Semester { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public Subject()
        {

        }
        public Subject(string data)
        {
            string[] d = data.Split(';');
            SubjectID = int.Parse(d[0]);
            Name = d[1];
            Credits = int.Parse(d[2]);
            Semester = int.Parse(d[3]);
        }

        public override string ToString()
        {
            return $"#{SubjectID}, {Name} ({Credits} credit), semester: {Semester}";
        }
    }
}
