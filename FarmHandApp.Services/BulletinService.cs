using FarmHandApp.Data;
using FarmHandApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Services
{
    public class BulletinService
    {
        private readonly Guid _userId;

        public BulletinService(Guid userId)
        {
            _userId = userId;
        }

        // CREATE
        public bool CreateBulletin(BulletinCreate model)
        {
            var entity =
                new Bulletin()
                {
                    UserId = _userId.ToString(),
                    BulletinTitle = model.BulletinTitle,
                    BulletinText = model.BulletinText,
                    CreatedUtc = DateTimeOffset.Now,
                    ModifiedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bulletins.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // GET ALL BULLETINS (INDEX)
        public IEnumerable<BulletinListItem> GetAllBulletins()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bulletins
                        .Where(e => e.BulletinId >= 0) 
                        .Select(
                            e =>
                                new BulletinListItem
                                {
                                    BulletinId = e.BulletinId,
                                    BulletinTitle = e.BulletinTitle,
                                    BulletinText = e.BulletinText,
                                    UserId = e.UserId,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                }
                        );
                return query.ToArray();
            }
        }

        // DETAIL
        //public BulletinDetail GetBulletinById(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //                .Bulletins
        //                .Single(e => e.BulletinId == id);
        //        return
        //            new BulletinDetail
        //            {
        //                UserId = entity.UserId,
        //                UserName = entity.UserName,
        //                BulletinId = entity.BulletinId,
        //                BulletinTitle = entity.BulletinTitle,
        //                BulletinText = entity.BulletinText,
        //                CreatedUtc = entity.CreatedUtc,
        //                ModifiedUtc = entity.ModifiedUtc,
        //            };
        //    }
        //}

        public BulletinDetail GetBulletinById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bulletins
                        .SingleOrDefault(e => e.BulletinId == id);     // CHANGED
                var detail =    // CHANGED
                    new BulletinDetail
                    {
                        UserId = entity.UserId,
                        UserName = entity.UserName,
                        BulletinId = entity.BulletinId,
                        BulletinTitle = entity.BulletinTitle,
                        BulletinText = entity.BulletinText,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        //Get All Comments for this Bulletin
                        Comments = ConvertDataEntitiesToViewModel(entity.Comments.ToList())
                    };
                return detail;  // CHANGED
            }
        }

        // ADDED METHOD
        public List<CommentListItem> ConvertDataEntitiesToViewModel(List<Comment> comments)
        {
            // instantiate a new List<CommentListItem>**

            List<CommentListItem> returnList = new List<CommentListItem>();

            // foreach through my entity.AllComments
            foreach (var comment in comments)
            {
                //create a new CommentListItem
                var commentListItem = new CommentListItem();

                // assign it the values from the entity.AllComments[i],
                commentListItem.CommentId = comment.CommentId;
                commentListItem.CommentText = comment.CommentText;
                commentListItem.CreatedUtc = comment.CreatedUtc;

                // add CommentListItem to my List<CommentListItem>**
                returnList.Add(commentListItem);
            }
            //  return that List<CommentListItem>**
            return returnList;
        }


        public bool UpdateBulletin(BulletinEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bulletins
                        .Single(e => e.BulletinId == model.BulletinId);

                entity.BulletinTitle = model.BulletinTitle;
                entity.BulletinText = model.BulletinText;
                entity.ModifiedUtc = DateTime.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBulletin(int bulletinId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bulletins
                        .Single(e => e.BulletinId == bulletinId);

                ctx.Bulletins.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
