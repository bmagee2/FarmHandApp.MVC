﻿using FarmHandApp.Data;
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

        //private ApplicationDbContext _db = new ApplicationDbContext();

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        //public NoteService(int choreId)
        //{
        //    _dbContext = new ApplicationDbContext();
        //}

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
                    //IsPublished = model.IsPublished,
                    CreatedUtc = DateTimeOffset.Now,
                    ModifiedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Add note to specific chore without having to manually enter choreid -- combine create & edit or delete??

        public bool CreateChoreNote(NoteCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                // GET CHORE BY CHOREID -- like in Edit 
                //.Single(e => e.ChoreId == model.ChoreId)
                //var chore = ctx.Chores.Single(e => e.ChoreId == model.ChoreId);
                var chore = GetChoreById(model.ChoreId);    // need GetChoreById for controller

                //CREATE NEW NOTE
                var entity =
                    new Note()
                    {
                        UserId = _userId.ToString(),
                        ChoreId = model.ChoreId,
                        NoteId = model.NoteId,
                        NoteTitle = model.NoteTitle,
                        NoteText = model.NoteText,
                        //IsPublished = model.IsPublished,
                        CreatedUtc = DateTimeOffset.Now,
                        ModifiedUtc = DateTimeOffset.Now
                    };

                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public Chore GetChoreById(int choreId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //var entity =
                //    ctx
                //        .Chores
                //        .Single(e => e.ChoreId == choreId);

                //ctx.Chores.Remove(entity);

                return ctx.Chores.Single(e => e.ChoreId == choreId); // can't be a bool...
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
                        //.Where(e => e.IsPublished)
                        .Where(e => e.UserId == _userId.ToString())
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

        // GET ALL NOTES BY CHOREID -- not the best way to do this
        public IEnumerable<NoteListItem> GetNotesByChoreId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                  ctx
                  .Notes
                  .ToList();
                
                List<NoteListItem> noteListItems = new List<NoteListItem>();

                foreach (Note item in query)
                {
                    if (item.ChoreId == id)
                    {
                        NoteListItem note = new NoteListItem
                        {
                            NoteId = item.NoteId,
                            ChoreId = item.ChoreId,
                            UserName = item.UserName,
                            NoteTitle = item.NoteTitle,
                            NoteText = item.NoteText,
                            CreatedUtc = item.CreatedUtc,
                            ModifiedUtc = item.ModifiedUtc
                        };
                        noteListItems.Add(note);
                    }
                }
                return noteListItems;
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
                        UserName = entity.UserName,
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
                        .Single(e => e.NoteId == model.NoteId && e.UserId == _userId.ToString());

                entity.NoteTitle = model.NoteTitle;
                entity.NoteText = model.NoteText;
                entity.ModifiedUtc = DateTimeOffset.Now;

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
                        .Single(e => e.NoteId == noteId && e.UserId == _userId.ToString());

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
