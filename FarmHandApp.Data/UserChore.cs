using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Data
{
    public class UserChore
    {
        [Key]
        public int UserChoreId { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(Chore))]
        public int ChoreId { get; set; }
        public virtual Chore Chore { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}
