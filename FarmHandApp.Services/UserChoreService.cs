using FarmHandApp.Data;
using FarmHandApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Services
{
    public class UserChoreService
    {
        private readonly Guid _userId;

        public UserChoreService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateUserChore(UserChoreCreate model)
        {
            var entity =
                new UserChore()
                {
                    UserId = _userId.ToString(),
                    ChoreId = model.ChoreId,
                    IsComplete = model.IsComplete
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserChores.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //public IEnumerable<UserChoreListItem> GetAllUserChores()
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //                .UserChores
        //                .Where(e => e.UserChoreId == _userId)
        //                .Select(
        //                    e =>
        //                        new UserChoreListItem
        //                        {

        //                        }
        //                );

        //        return query.ToArray();
        //    }
        //}

        public UserChoreDetail GetUserChoreById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserChores
                        // .FirstOrDefault ?
                        .Single(e => e.UserChoreId == id); // && e => e.IsPublished);?
                return
                    new UserChoreDetail
                    {
                        UserId = entity.UserId,
                        //ChoreId = entity.ChoreId,
                        IsComplete = entity.IsComplete
                    };
            }
        }

        public bool UpdateUserChore(UserChoreEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserChores
                        // .FirstOrDefault ?
                        .Single(e => e.ChoreId == model.ChoreId && e.UserId.ToString() == _userId.ToString());

                entity.ChoreId = model.ChoreId; // needed?
                entity.UserId = model.UserId;   // needed?
                entity.IsComplete = model.IsComplete;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteUserChore(int userChoreId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserChores
                        // .FirstOrDefault ?
                        .Single(e => e.UserChoreId == userChoreId && e.UserId.ToString() == _userId.ToString());

                ctx.UserChores.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
