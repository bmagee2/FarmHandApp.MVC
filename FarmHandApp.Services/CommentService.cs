using FarmHandApp.Data;
using FarmHandApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Services
{
    public class CommentService
    {
        private readonly Guid _userId;

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        // CREATE COMMENT
        public bool CreateComment(CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    UserId = _userId.ToString(),
                    BulletinId = model.BulletinId,
                    CommentId = model.CommentId,
                    CommentText = model.CommentText,
                    CreatedUtc = DateTimeOffset.Now,
                    ModifiedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Create comment with BulletinId
        public bool CreateBulletinComment(CommentCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var bulletin = GetBulletinById(model.BulletinId);

                var entity =
                    new Comment()
                    {
                        UserId = _userId.ToString(),
                        BulletinId = model.BulletinId,
                        CommentId = model.CommentId,
                        CommentText = model.CommentText,
                        CreatedUtc = DateTimeOffset.Now,
                        ModifiedUtc = DateTimeOffset.Now
                    };

                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public Bulletin GetBulletinById(int bulletinId)     // method already in BulletinService 
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Bulletins.Single(e => e.BulletinId == bulletinId);
            }
        }


        // GET ALL COMMENTS (INDEX)
        public IEnumerable<CommentListItem> GetAllComments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Comments
                        .Where(e => e.UserId == _userId.ToString())
                        .Select(
                            e =>
                                new CommentListItem
                                {
                                    CommentId = e.CommentId,
                                    BulletinId = e.BulletinId,
                                    CommentText = e.CommentText,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        // GET ALL COMMENTS BY BULLETINID -- not the best way to do this
        public IEnumerable<CommentListItem> GetCommentsByBulletinId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                  ctx
                  .Comments
                  .ToList();

                List<CommentListItem> commentListItems = new List<CommentListItem>();

                foreach (Comment item in query)
                {
                    if (item.BulletinId == id)
                    {
                        CommentListItem comment = new CommentListItem
                        {
                            CommentId = item.CommentId,
                            BulletinId = item.BulletinId,
                            UserName = item.UserName,
                            CommentText = item.CommentText,
                            CreatedUtc = item.CreatedUtc,
                            ModifiedUtc = item.ModifiedUtc
                        };

                        commentListItems.Add(comment);
                    }
                }
                return commentListItems;
            }
        }

        // COMMENT DETAIL
        public CommentDetail GetCommentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.CommentId == id);
                return
                    new CommentDetail
                    {
                        CommentId = entity.CommentId,
                        BulletinId = entity.BulletinId,
                        UserName = entity.UserName,
                        CommentText = entity.CommentText,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        // UPDATE
        public bool UpdateComment(CommentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.CommentId == model.CommentId && e.UserId == _userId.ToString());

                entity.CommentText = model.CommentText;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        // DELETE
        public bool DeleteComment(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.CommentId == commentId && e.UserId == _userId.ToString());

                ctx.Comments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
