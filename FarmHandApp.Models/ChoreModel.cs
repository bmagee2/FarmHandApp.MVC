﻿using FarmHandApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Models
{
    // the properties that will show up in the view
    public class ChoreListItem
    {
        public int? ChoreId { get; set; }

        //[Display(Name = "Enter By")]
        public string UserId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

       [Display(Name = "Name")]
        public string ChoreName { get; set; }

        [Display(Name = "Description")]
        public string ChoreDescription { get; set; }

        public ChoreLocation Location { get; set; }

        public TypeOfAnimal Animal { get; set; }

        [Display(Name = "Time of Day")]
        public TimeOfDay TimeOfDay { get; set; }

        [Display(Name = "Daily Chore?")]
        public bool IsDaily { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }

        [Display(Name = "View Notes")]
        public virtual List<NoteListItem> Notes { get; set; }

        public ChoreUser ChoreUser { get; set; }
    }

    public class ChoreCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Name")]
        public string ChoreName { get; set; }

        [Required]
        [MaxLength(4000)]
        [Display(Name = "Description")]
        public string ChoreDescription { get; set; }

        [Required]
        public ChoreLocation Location { get; set; }

        public TypeOfAnimal Animal { get; set; }

        [Required]
        [Display(Name = "Time of Day")]
        public TimeOfDay TimeOfDay { get; set; }

        [Required]
        [Display(Name = "Daily Chore?")]
        public bool IsDaily { get; set; }

        //[Required]
        [Display(Name = "Publish Chore?")]
        public bool IsPublished { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedUtc { get; set; }
    }

    public class ChoreDetail
    {
        public int ChoreId { get; set; }

        //[Display(Name = "Enter By")]
        public string UserId { get; set; }

        [Display(Name = "Entered By")]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        public string ChoreName { get; set; }

        [Display(Name = "Description")]
        public string ChoreDescription { get; set; }

        public ChoreLocation Location { get; set; }

        public TypeOfAnimal Animal { get; set; }

        [Display(Name = "Time of Day")]
        public TimeOfDay TimeOfDay { get; set; }

        [Display(Name = "Daily Chore?")]
        public bool IsDaily { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }
        public virtual List<NoteListItem> Notes { get; set; }
    }

    public class ChoreEdit
    {
        public int ChoreId { get; set; }

        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Name")]
        public string ChoreName { get; set; }

        [MaxLength(4000)]
        [Display(Name = "Description")]
        public string ChoreDescription { get; set; }

        public ChoreLocation Location { get; set; }

        public TypeOfAnimal Animal { get; set; }

        [Display(Name = "Time of Day")]
        public TimeOfDay TimeOfDay { get; set; }

        [Display(Name = "Daily Chore?")]
        public bool IsDaily { get; set; }

    }
}
