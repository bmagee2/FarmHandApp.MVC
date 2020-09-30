using FarmHandApp.Data;
using FarmHandApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Services
{
    public class ChoreUserService
    {
        private readonly Guid _userId;

        public ChoreUserService(Guid userId)
        {
            _userId = userId;
        }

        // CREATE NOTE
        public bool CreateChoreUser(ChoreUserCreate model)
        {
            var entity =
                new ChoreUser()
                {
                    UserId = _userId.ToString(),
                    ChoreId = model.ChoreId,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ChoreUsers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // GET ALL NOTES (INDEX)
        public IEnumerable<ChoreUserListItem> GetAllChoreUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ChoreUsers
                        .Where(e => e.UserId == _userId.ToString()) 
                        .Select(
                            e =>
                                new ChoreUserListItem
                                {
                                    ChoreUserId = e.ChoreUserId,
                                    UserId = _userId.ToString(),
                                    ChoreId = (int)e.ChoreId,   // fix
                                    ChoreIsComplete = e.ChoreIsComplete
                                }
                        );

                return query.ToArray();
            }
        }

        // DETAIL
        public ChoreUserDetail GetChoreUserById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ChoreUsers
                        .Single(e => e.ChoreUserId == id && e.UserId == _userId.ToString());
                return
                    new ChoreUserDetail
                    {
                        ChoreUserId = entity.ChoreUserId,
                        ChoreId = (int)entity.ChoreId,
                        UserId = entity.UserId,
                        ChoreIsComplete = entity.ChoreIsComplete
                    };
            }
        }

        // UPDATE
        public bool UpdateChoreUser(ChoreUserEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ChoreUsers
                        .Single(e => e.ChoreUserId == model.ChoreUserId && e.UserId == _userId.ToString());

                entity.ChoreUserId = model.ChoreUserId;
                entity.UserId = model.UserId;
                entity.ChoreId = model.ChoreId;
                entity.ChoreIsComplete = model.ChoreIsComplete;

                return ctx.SaveChanges() == 1;
            }
        }

        // DELETE
        public bool DeleteChoreUser(int choreUserId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ChoreUsers
                        .Single(e => e.ChoreUserId == choreUserId);

                ctx.ChoreUsers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
