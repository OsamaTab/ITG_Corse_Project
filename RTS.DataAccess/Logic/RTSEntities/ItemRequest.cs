using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RTS.DataAccess.Logic.RTSEntities
{
    public class ItemRequest
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public string RequestedUserId { get; set; }

        [ForeignKey("RequestedUserId")]
        public Employee RequestedUser { get; set; }

        public string ItemOwner { get; set; }

        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public RequestStatus Status { get; set; }

        
    }
}
