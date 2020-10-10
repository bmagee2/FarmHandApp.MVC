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
                    //IsPublished = model.IsPublished,
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
                        .Where(e => e.ChoreId >= 0) // view all bool (view by manager only or all staff?) -- better option?
                        //.ToList()   // CHANGED
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
                                    UserId = e.UserId,
                                    //UserName = e.UserName,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                    //Notes
                                    //Notes = ConvertDataEntitiesToViewModel(e.Notes.ToList())
                                }
                        );
                return query.ToArray();
            }
        }


        // DETAIL
        //public ChoreDetail GetChoreById(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //                .Chores
        //                .Single(e => e.ChoreId == id);
        //        return
        //            new ChoreDetail
        //            {
        //                UserId = entity.UserId,
        //                UserName = entity.UserName,
        //                ChoreId = entity.ChoreId,   // this fixed "Id Mismatch" message
        //                ChoreName = entity.ChoreName,
        //                ChoreDescription = entity.ChoreDescription,
        //                Location = entity.Location,
        //                Animal = entity.Animal,
        //                TimeOfDay = entity.TimeOfDay,
        //                IsDaily = entity.IsDaily,
        //                CreatedUtc = entity.CreatedUtc,
        //                ModifiedUtc = entity.ModifiedUtc,
        //                //Get All Notes for this chore
        //            };
        //    }
        //}
        public ChoreDetail GetChoreById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Chores
                        .SingleOrDefault(e => e.ChoreId == id);     // CHANGED
                var detail =    // CHANGED
                    new ChoreDetail
                    {
                        UserId = entity.UserId,
                        UserName = entity.UserName,
                        ChoreId = entity.ChoreId,   
                        ChoreName = entity.ChoreName,
                        ChoreDescription = entity.ChoreDescription,
                        Location = entity.Location,
                        Animal = entity.Animal,
                        TimeOfDay = entity.TimeOfDay,
                        IsDaily = entity.IsDaily,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        //Get All Notes for this chore
                        Notes = ConvertDataEntitiesToViewModel(entity.Notes.ToList())
                    };
                return detail;  // CHANGED
            }
        }

        // ADDED METHOD
        public List<NoteListItem> ConvertDataEntitiesToViewModel(List<Note> notes)
        {
            // instantiate a new List<NoteListItem>**

            List<NoteListItem> returnList = new List<NoteListItem>();

            // foreach through my entity.AllNotes
            foreach (var note in notes)
            {
                //create a new NoteListItem
                var noteListItem = new NoteListItem();

                // assign it the values from the entity.AllNotes[i],
                noteListItem.NoteId = note.NoteId;
                noteListItem.NoteTitle = note.NoteTitle;
                noteListItem.NoteText = note.NoteText;
                noteListItem.CreatedUtc = note.CreatedUtc;

                // add NoteListItem to my List<NoteListItem>**
                returnList.Add(noteListItem);
            }
            //  return that List<NoteListItem>**
            return returnList;
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
                entity.ModifiedUtc = DateTime.Now;

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
