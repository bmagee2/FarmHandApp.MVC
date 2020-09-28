using FarmHandApp.Data;
using FarmHandApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Services
{
    public class ChoreService
    {
        private readonly Guid _userId;

        public ChoreService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(ChoreCreate model)
        {
            var entity =
                new Chore()
                {
                    UserId = _userId.ToString(),
                    ChoreName = model.ChoreName,
                    ChoreDescription = model.ChoreDescription,
                    Location = model.Location,
                    Animal = model.Animal,
                    TimeOfDay = model.TimeOfDay,
                    IsDaily = model.IsDaily,
                    CreatedUtc = DateTimeOffset.Now,
                    ModifiedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Chores.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //public IEnumerable<ChoreListItem> GetAllChores()
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //                .Chores
        //                .Where(e => e.IsPublished) // view all bool (view by manager only or all staff)
        //                .Select(
        //                    e =>
        //                        new ChoreListItem
        //                        {
        //                            ChoreId = e.ChoreId,
        //                            ChoreName = e.ChoreName,
        //                            ChoreDescription = e.ChoreDescription,


        //                        }
        //                );
        //        return query.ToArray();
        //    }
        //}
    }
}
