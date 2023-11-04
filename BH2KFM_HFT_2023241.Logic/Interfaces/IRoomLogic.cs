﻿using BH2KFM_HFT_2023241.Models;
using System.Collections.Generic;

namespace BH2KFM_HFT_2023241.Logic
{
    public interface IRoomLogic
    {
        void Create(Room item);
        void Delete(int id);
        Room Read(int id);
        IEnumerable<Room> ReadAll();
        void Update(Room item);
    }
}