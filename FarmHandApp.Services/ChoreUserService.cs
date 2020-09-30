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

        //// NOTE DETAIL
        //public NoteDetail GetNoteById(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //                .Notes
        //                .Single(e => e.NoteId == id);
        //        return
        //            new NoteDetail
        //            {
        //                NoteId = entity.NoteId,
        //                ChoreId = entity.ChoreId,
        //                NoteTitle = entity.NoteTitle,
        //                NoteText = entity.NoteText,
        //                CreatedUtc = entity.CreatedUtc,
        //                ModifiedUtc = entity.ModifiedUtc
        //            };
        //    }
        //}

        //// UPDATE
        //public bool UpdateNote(NoteEdit model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //                .Notes
        //                .Single(e => e.NoteId == model.NoteId);

        //        entity.NoteText = model.NoteText;
        //        entity.ModifiedUtc = DateTimeOffset.UtcNow;

        //        return ctx.SaveChanges() == 1;
        //    }
        //}

        //// DELETE
        //public bool DeleteNote(int noteId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //                .Notes
        //                .Single(e => e.NoteId == noteId);

        //        ctx.Notes.Remove(entity);

        //        return ctx.SaveChanges() == 1;
        //    }
        //}
    }
}
