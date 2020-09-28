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

        [ForeignKey(nameof(UserChore))]
        public int UserChoreId { get; set; }
        public virtual UserChore UserChore { get; set; }

        [Required]
        public string NoteText { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
