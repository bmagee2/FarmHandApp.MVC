using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Models
{
    public class CommentListItem
    {
        public int CommentId { get; set; }
        public int BulletinId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Comment")]
        public string CommentText { get; set; }

        [Display(Name = "Created at")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified at")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }

    public class CommentCreate
    {
        public int CommentId { get; set; }
        public int BulletinId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Comment")]
        public string CommentText { get; set; }

        [Display(Name = "Created at")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified at")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }

    public class CommentDetail
    {
        public int CommentId { get; set; }
        public int BulletinId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Comment")]
        public string CommentText { get; set; }

        [Display(Name = "Created at")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified at")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }

    public class CommentEdit
    {
        public int CommentId { get; set; }
        public int BulletinId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Comment")]
        public string CommentText { get; set; }

        [Display(Name = "Created at")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified at")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
