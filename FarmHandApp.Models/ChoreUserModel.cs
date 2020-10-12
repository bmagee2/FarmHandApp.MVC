using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Models
{
    public class ChoreUserListItem
    {
        public int ChoreUserId { get; set; }
        public string UserId { get; set; }
        public int? ChoreId { get; set; }
        [Display(Name = "Name")]
        public string ChoreName { get; set; }
        [Display(Name = "Description")]
        public string ChoreDescription { get; set; }
        public bool ChoreIsComplete { get; set; }
    }

    public class ChoreUserCreate
    {
        //public int ChoreUserId { get; set; }
        //public string UserId { get; set; }
        public int ChoreId { get; set; }
        [Display(Name = "Name")]
        public string ChoreName { get; set; }
        [Display(Name = "Description")]
        public string ChoreDescription { get; set; }
        public bool ChoreIsComplete { get; set; }
    }

    public class ChoreUserDetail
    {
        public int ChoreUserId { get; set; }
        public string UserId { get; set; }
        public int ChoreId { get; set; }
        [Display(Name = "Name")]
        public string ChoreName { get; set; }
        [Display(Name = "Description")]
        public string ChoreDescription { get; set; }
        public bool ChoreIsComplete { get; set; }
    }

    public class ChoreUserEdit
    {
        public int ChoreUserId { get; set; }
        public string UserId { get; set; }
        public int ChoreId { get; set; }
        public bool ChoreIsComplete { get; set; }
    }
}
