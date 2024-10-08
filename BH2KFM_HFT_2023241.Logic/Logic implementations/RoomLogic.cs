﻿using BH2KFM_HFT_2023241.Repository;
using BH2KFM_HFT_2023241.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace BH2KFM_HFT_2023241.Logic
{
    public class RoomLogic : IRoomLogic
    {
        IRepository<Room> repository;

        public RoomLogic(IRepository<Room> repository)
        {
            this.repository = repository;
        }

        public void Create(Room item)
        {
            if (item.Capacity < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(item), "Room capacity cannot be smaller than 10");
            }

            repository.Create(item);
        }

        public void Delete(int id)
        {
            Read(id);

            repository.Delete(id);
        }

        public Room Read(int id)
        {
            var item = repository.Read(id);
            if (item is null)
            {
                throw new ArgumentException("Room does not exist");
            }

            return item;
        }

        public IEnumerable<Room> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Room item)
        {
            this.Read(item.DoorID);

            if (item.Capacity < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(item), "Room capacity cannot be smaller than 10");
            }

            repository.Update(item);
        }

        //non-CRUD methods:

        public IEnumerable<Room> ProjectorRooms()
        {
            return this.ReadAll().Where(t => t.HasProjector);
        }

        public int MaxCapacity()
        {
            return this.ReadAll().Max(t => t.Capacity);
        }

        public double AverageSubjectCreditInRoom(int roomID)
        {
            return this.Read(roomID).Courses.Average(t => t.Subject.Credits);
        }

        public int MaxSubjectSemesterInRoom(int roomID)
        {
            return this.Read(roomID).Courses.Max(t => t.Subject.Semester);
        }

        public IEnumerable<Subject> Subjects(int roomId)
        {
            return this.Read(roomId).Courses.Select(t => t.Subject).Distinct();
        }
    }
}
