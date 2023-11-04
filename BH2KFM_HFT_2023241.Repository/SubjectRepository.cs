using BH2KFM_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH2KFM_HFT_2023241.Repository
{
    public class SubjectRepository : Repository<Subject>, IRepository<Subject>
    {
        public SubjectRepository(LectureDbContext ctx)
            : base(ctx)
        { }

        public override Subject Read(int id)
        {
            return this.ctx.Subjects.FirstOrDefault(t => t.SubjectID == id);
        }

        public override void Update(Subject item)
        {
            var old = Read(item.SubjectID);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
