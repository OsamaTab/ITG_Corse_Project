﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RTS.BusinessLogic.ViewModel
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public string RoleName { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
