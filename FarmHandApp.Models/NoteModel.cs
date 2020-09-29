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
        public int UserChoreId { get; set; }
        public int ChoreId { get; set; }

        [Display(Name = "Note")]
        public string NoteText { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }

    public class NoteCreate
    {
        public int NoteId { get; set; }
        public int UserChoreId { get; set; }
        public int ChoreId { get; set; }

        [Required]
        [MaxLength(2000)]
        public string NoteText { get; set; }
    }

    public class NoteDetail
    {
        public int NoteId { get; set; }
        public int UserChoreId { get; set; }
        public int ChoreId { get; set; }

        [Display(Name = "Note")]
        public string NoteText { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }

    public class NoteEdit
    {

    }
}
