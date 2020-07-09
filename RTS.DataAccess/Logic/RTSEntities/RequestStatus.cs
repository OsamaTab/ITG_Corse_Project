using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RTS.DataAccess.Logic.RTSEntities
{
    public class RequestStatus
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
