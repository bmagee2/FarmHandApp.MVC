﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmHandApp.Models
{
    public class ChoreUserListItem
    {
        public int ChoreUserId { get; set; }
        public string UserId { get; set; }
        public int ChoreId { get; set; }
        public bool ChoreIsComplete { get; set; }
    }

    public class ChoreUserCreate
    {
        //public int ChoreUserId { get; set; }
        //public string UserId { get; set; }
        public int ChoreId { get; set; }
        public bool ChoreIsComplete { get; set; }
    }

    public class ChoreUserDetail
    {
        public int ChoreUserId { get; set; }
        public string UserId { get; set; }
        public int ChoreId { get; set; }
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
