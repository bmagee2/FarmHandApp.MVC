using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Models
{
    public class UserChoreListItem
    {
        public int UserChoreId { get; set; }

        public string UserId { get; set; }

        public int ChoreId { get; set; }

        [Display(Name = "Is completed?")]
        public bool IsComplete { get; set; }
    }

    public class UserChoreCreate
    {
        [Required]
        public int UserChoreId { get; set; }

        public string UserId { get; set; }

        public int ChoreId { get; set; }

        public bool IsComplete { get; set; }
    }

    public class UserChoreDetail
    {
        public int UserChoreId { get; set; }

        public string UserId { get; set; }

        public int ChoreId { get; set; }

        [Display(Name = "Is completed?")]
        public bool IsComplete { get; set; }
    }

    public class UserChoreEdit
    {
        public int UserChoreId { get; set; }
        public string UserId { get; set; }  // needed?

        public int ChoreId { get; set; }    // needed?

        [Display(Name = "Is completed?")]
        public bool IsComplete { get; set; }
    }
}
