using FarmHandApp.Data;
using FarmHandApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity =
                new Note()
                {
                    UserId = _userId.ToString(),
                    ChoreId = model.ChoreId,
                    NoteId = model.NoteId,
                    NoteText = model.NoteText,
                    CreatedUtc = DateTimeOffset.Now,
                    ModifiedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //public IEnumerable<NoteListItem> GetAllNotes()   // i think this is wrong
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //                .Notes
        //                .Where(e => e.UserChoreId == id)       // GET ALL NOTES FOR A CHORE BY CHOREID OR USERCHOREID?
        //                .Select(
        //                    e =>
        //                        new NoteListItem
        //                        {
        //                            NoteId = e.NoteId,
        //                            ChoreId = e.ChoreId,    // needed?
        //                            NoteText = e.NoteText,
        //                            CreatedUtc = e.CreatedUtc,
        //                            ModifiedUtc = e.ModifiedUtc
        //                        }
        //                );

        //        return query.ToArray();
        //    }
        //}


        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == id);   // ???
                return
                    new NoteDetail
                    {
                        UserChoreId = entity.UserChoreId,
                        NoteId = entity.NoteId,
                        ChoreId = entity.ChoreId,    // needed?
                        NoteText = entity.NoteText,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
    }
}
