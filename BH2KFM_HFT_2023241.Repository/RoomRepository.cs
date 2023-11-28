using BH2KFM_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH2KFM_HFT_2023241.Repository
{
    public class RoomRepository : Repository<Room>, IRepository<Room>
    {
        public RoomRepository(LectureDbContext ctx)
            : base(ctx)
        { }

        public override Room Read(int id)
        {
            return this.ctx.Rooms.FirstOrDefault(t => t.DoorID == id);
        }

        public override void Update(Room item)
        {
            var old = Read(item.DoorID);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
