using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RTS.DataAccess.Logic.RTSEntities
{
    public class Employee : IdentityUser
    {
        [DefaultValue("False")]
        public Boolean IsDeleted { get; set; }
    }
}
