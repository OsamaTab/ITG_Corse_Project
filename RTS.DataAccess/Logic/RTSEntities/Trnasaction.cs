using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RTS.DataAccess.Logic.RTSEntities
{
    public class Trnasaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ItemRequestId { get; set; }

        [ForeignKey("ItemRequestId")]
        public ItemRequest Item { get; set; }
        [Required]
        public int DeviceTypeId { get; set; }

        [ForeignKey("DeviceTypeId")]
        public ItemCategorie DeviceType { get; set; }
        public DateTime TransectionDate { get; set; }
    }
}
