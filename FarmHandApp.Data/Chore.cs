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

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ChoreLocation Location { get; set; }

        [Required]
        public TypeOfAnimal Animal { get; set; }

        [Required]
        public TimeOfDay TimeOfDay { get; set; }

        [Required]
        public bool IsDaily { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset ModifiedUtc { get; set; }

        public virtual List<Note> Notes { get; set; }
    }

    public enum ChoreLocation
    {
        Animal_Encounters_Barn,
        Prairie_Town_Barn,
        Bank_Barn,
        Ag_Office,
        Facilities_Building,
        Other
    }

    public enum TypeOfAnimal
    {
        Sheep,
        Goats,
        Cows,
        Donkeys,
        Chickens,
        Pigs,
        Other
    }

    public enum TimeOfDay
    {
        Morning,
        Evening
    }
}
