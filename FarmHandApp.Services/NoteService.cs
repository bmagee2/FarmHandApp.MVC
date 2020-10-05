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
                    NoteId = model.NoteId,
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

        // Add note to specific chore without having to manually enter choreid
        //public bool CreateChoreNote(NoteCreate model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {


        //        var entity =
        //            new Note()
        //            {
        //                UserId = _userId.ToString(),
        //                ChoreId = model.ChoreId,
        //                NoteId = model.NoteId,
        //                NoteTitle = model.NoteTitle,
        //                NoteText = model.NoteText,
        //                IsPublished = model.IsPublished,
        //                CreatedUtc = DateTime.Now,
        //                ModifiedUtc = DateTime.Now
        //            };

        //        ctx.Notes.Add(entity);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}

        // Test

        // Get all notes by choreid??
        //public IEnumerable<NoteListItem> GetNotesByChoreId(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //          ctx
        //          .Notes
        //          .FirstOrDefault(e => e.ChoreId == id);

        //        List<NoteListItem> noteListItems = new List<NoteListItem>();

        //        foreach (Note item in noteListItems)
        //        {
        //                NoteListItem note = new NoteListItem
        //                {
        //                    NoteId = item.NoteId,
        //                    ChoreId = item.ChoreId,
        //                    NoteTitle = item.NoteTitle,
        //                    NoteText = item.NoteText,
        //                    CreatedUtc = item.CreatedUtc,
        //                    ModifiedUtc = item.ModifiedUtc
        //                };
        //                noteListItems.Add(note);
                   
        //        }
        //        return noteListItems;
        //    }
        //}

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
