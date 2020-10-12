using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Models
{
    public class NoteListItem
    {
        public int NoteId { get; set; }
        public int ChoreId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Note Title")]
        public string NoteTitle { get; set; }

        [Display(Name = "Note")]
        public string NoteText { get; set; }

        [Display(Name = "Created at")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified at")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }

    public class NoteCreate
    {
        public int NoteId { get; set; }
        public int ChoreId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Note Title")]
        public string NoteTitle { get; set; }

        [Required]
        [MaxLength(2000)]
        [Display(Name = "Note")]
        public string NoteText { get; set; }

        //[Required]
        //[Display(Name = "Add Note to Chore?")]
        //public bool IsPublished { get; set; }
    }

    public class NoteDetail
    {
        public int NoteId { get; set; }
        public int ChoreId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Note Title")]
        public string NoteTitle { get; set; }

        [Display(Name = "Note")]
        public string NoteText { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }

    public class NoteEdit
    {
        public int NoteId { get; set; }

        [Display(Name = "Note Title")]
        public string NoteTitle { get; set; }

        [Display(Name = "Note")]
        public string NoteText { get; set; }
    }
}
