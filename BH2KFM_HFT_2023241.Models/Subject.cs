using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }

        public Subject() { }

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

        public override bool Equals(object obj)
        {
            Subject b = obj as Subject;
            if (b is null)
            {
                return false;
            }

            return
                this.SubjectID == b.SubjectID &&
                this.Name.Equals(b.Name) &&
                this.Credits == b.Credits &&
                this.Semester == b.Semester;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.SubjectID, this.Name, this.Credits, this.Semester);
        }
    }
}
