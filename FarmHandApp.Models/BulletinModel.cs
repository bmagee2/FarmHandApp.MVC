using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Models
{
    public class BulletinListItem
    {
        public int BulletinId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Headline")]
        public string BulletinTitle { get; set; }

        [Display(Name = "Bulletin")]
        [DataType(DataType.MultilineText)]
        public string BulletinText { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }

        [Display(Name = "View Comments")]
        public virtual List<CommentListItem> Comments { get; set; }
        //public List<CommentDetail> Comments { get; set; }
    }

    public class BulletinCreate
    {
        //public int BulletinId { get; set; }

        //public string UserId { get; set; }

        //[Display(Name = "Entered By")]
        //public string UserName { get; set; }
        [Required]
        [Display(Name = "Headline")]
        public string BulletinTitle { get; set; }

        [Required]
        [Display(Name = "Bulletin")]
        [DataType(DataType.MultilineText)]
        public string BulletinText { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedUtc { get; set; }

    }

    public class BulletinDetail
    {
        public int BulletinId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Headline")]
        public string BulletinTitle { get; set; }

        [Display(Name = "Bulletin")]
        public string BulletinText { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }

        public virtual List<CommentListItem> Comments { get; set; }
    }

    public class BulletinEdit
    {
        public int BulletinId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Headline")]
        public string BulletinTitle { get; set; }

        [Display(Name = "Bulletin")]
        public string BulletinText { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }

    }
}
