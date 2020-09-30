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

        // CREATE
        public bool CreateChore(ChoreCreate model)
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
                    IsPublished = model.IsPublished,
                    CreatedUtc = DateTimeOffset.Now,
                    ModifiedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Chores.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // GET ALL CHORES (INDEX)
        public IEnumerable<ChoreListItem> GetAllChores()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Chores
                        .Where(e => e.IsPublished) // view all bool (view by manager only or all staff?) -- better option?
                        .Select(
                            e =>
                                new ChoreListItem
                                {
                                    ChoreId = e.ChoreId,
                                    ChoreName = e.ChoreName,
                                    ChoreDescription = e.ChoreDescription,
                                    Location = e.Location,
                                    Animal = e.Animal,
                                    TimeOfDay = e.TimeOfDay,
                                    IsDaily = e.IsDaily,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );
                return query.ToArray();
            }
        }

        // DETAIL
        public ChoreDetail GetChoreById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Chores
                        .Single(e => e.ChoreId == id); 
                return
                    new ChoreDetail
                    {
                        UserId = entity.UserId,
                        ChoreId = entity.ChoreId,   // this fixed "Id Mismatch" message
                        ChoreName = entity.ChoreName,
                        ChoreDescription = entity.ChoreDescription,
                        Location = entity.Location,
                        Animal = entity.Animal,
                        TimeOfDay = entity.TimeOfDay,
                        IsDaily = entity.IsDaily,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        // GET NOTES FOR EACH CHORE
        public IEnumerable<NoteListItem> GetNotesByChoreId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                  ctx
                      .Chores
                      .FirstOrDefault(e => e.ChoreId == id);

                IEnumerable<Note> notes = entity.Notes;

                return CreateListOfNotes(notes);
            }
        }

        private IEnumerable<NoteListItem> CreateListOfNotes(IEnumerable<Note> notes)
        {

            List<NoteListItem> noteListItems = new List<NoteListItem>();

            foreach (Note note in notes)
            {
                NoteListItem noteListItem = new NoteListItem
                {
                    ChoreId = note.ChoreId,
                    NoteId = note.NoteId,
                    NoteText = note.NoteText

                };
                noteListItems.Add(noteListItem);
            }
            return noteListItems;
        }

        public bool UpdateChore(ChoreEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Chores
                        .Single(e => e.ChoreId == model.ChoreId);

                entity.ChoreName = model.ChoreName;
                entity.ChoreDescription = model.ChoreDescription;
                entity.Location = model.Location;
                entity.Animal = model.Animal;
                entity.TimeOfDay = model.TimeOfDay;
                entity.IsDaily = model.IsDaily;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteChore(int choreId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Chores
                        .Single(e => e.ChoreId == choreId);

                ctx.Chores.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
