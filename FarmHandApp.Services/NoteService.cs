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

        // CREATE NOTE
        public bool CreateNote(NoteCreate model)
        {
            var entity =
                new Note()
                {
                    UserId = _userId.ToString(),
                    ChoreId = model.ChoreId,
                    //NoteId = model.NoteId,
                    NoteTitle = model.NoteTitle,
                    NoteText = model.NoteText,
                    IsPublished = model.IsPublished,
                    CreatedUtc = DateTime.Now,
                    ModifiedUtc = DateTime.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }


        // GET ALL NOTES (INDEX)
        public IEnumerable<NoteListItem> GetAllNotes()   
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Notes
                        .Where(e => e.IsPublished)       // GET ALL NOTES FOR A CHORE BY CHOREID OR USERCHOREID?
                        .Select(
                            e =>
                                new NoteListItem
                                {
                                    NoteId = e.NoteId,
                                    ChoreId = e.ChoreId,    
                                    NoteTitle = e.NoteTitle,
                                    NoteText = e.NoteText,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        // NOTE DETAIL
        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == id);   
                return
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        ChoreId = entity.ChoreId,    
                        NoteTitle = entity.NoteTitle,
                        NoteText = entity.NoteText,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        // UPDATE
        public bool UpdateNote(NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == model.NoteId);

                entity.NoteTitle = model.NoteTitle;
                entity.NoteText = model.NoteText;
                entity.ModifiedUtc = DateTime.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        // DELETE
        public bool DeleteNote(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == noteId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
