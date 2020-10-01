using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId {get; set;}
        public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(Chore))]
        public int ChoreId { get; set; }       
        public virtual Chore Chore { get; set; }

        [Required]
        public string NoteTitle { get; set; }

        [Required]
        public string NoteText { get; set; }

        [Required]
        public bool IsPublished { get; set; }   // get all notes work around -- better option?

        public DateTime CreatedUtc { get; set; }

        public DateTime ModifiedUtc { get; set; }
    }
}
