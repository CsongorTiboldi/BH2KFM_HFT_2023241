using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BH2KFM_HFT_2023241.Models
{
    public class Room
    {
        [Key]
        public int DoorID { get; set; }

        public int Capacity { get; set; }

        public bool HasProjector { get; set; }

        [NotMapped]
        public virtual ICollection<Course> Courses { get; set; }

        public Room() { }

        public Room(string data)
        {
            string[] d = data.Split(';');
            DoorID = int.Parse(d[0]);
            Capacity = int.Parse(d[1]);
            HasProjector = bool.Parse(d[2]);
        }

        public override string ToString()
        {
            string projector = HasProjector ? "has a projector" : "no projector";
            return $"#{DoorID}, capacity: {Capacity}, ({projector})"; 
        }
    }
}
