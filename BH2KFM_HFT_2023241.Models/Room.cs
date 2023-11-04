using System;
using System.ComponentModel.DataAnnotations;

namespace BH2KFM_HFT_2023241.Models
{
    internal class Room
    {
        [Key]
        public int DoorID { get; set; }

        public int Capacity { get; set; }

        public bool HasProjector { get; set; }

        public Room(string data)
        {
            string[] d = data.Split(';');
            DoorID = int.Parse(d[0]);
            Capacity = int.Parse(d[1]);
            HasProjector = bool.Parse(d[2]);
        }

    }
}
