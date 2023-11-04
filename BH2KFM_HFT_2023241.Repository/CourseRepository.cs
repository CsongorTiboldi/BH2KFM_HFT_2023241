using BH2KFM_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH2KFM_HFT_2023241.Repository
{
    public class CourseRepository : Repository<Course>, IRepository<Course>
    {
        public CourseRepository(LectureDbContext ctx)
            : base(ctx)
        { }

        public override Course Read(int id)
        {
            return this.ctx.Courses.FirstOrDefault(t => t.CourseID == id);
        }

        public override void Update(Course item)
        {
            var old = Read(item.CourseID);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
