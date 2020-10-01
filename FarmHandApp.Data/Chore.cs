using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Data
{
    public class Chore
    {
        [Key]
        public int ChoreId { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string UserName
        {
            get
            {
                return User.UserName;
            }
        }

        [Required]
        public string ChoreName { get; set; }

        [Required]
        public string ChoreDescription { get; set; }

        [Required]
        public ChoreLocation Location { get; set; }

        public TypeOfAnimal Animal { get; set; }

        [Required]
        public TimeOfDay TimeOfDay { get; set; }

        [Required]
        public bool IsDaily { get; set; }

        [Required]
        public bool IsPublished { get; set; }   // get all chores work around -- better option?

        public DateTime CreatedUtc { get; set; }

        public DateTime ModifiedUtc { get; set; }

        public virtual List<Note> Notes { get; set; }
    }

    public enum ChoreLocation
    {
        Animal_Encounters_Barn,
        Golden_Eagle_Barn,
        Campbell_Barn,
        Bank_Barn,
        Ag_Office,
        Facilities_Building,
        Other
    }

    public enum TypeOfAnimal
    {
        Sheep,
        Arapawa_Goats,
        Saanen_Goats,
        Cows,
        Donkeys,
        Chickens,
        Pigs,
        Other
    }

    public enum TimeOfDay
    {
        Morning,
        Evening,
        Morning_and_Evening
    }
}
