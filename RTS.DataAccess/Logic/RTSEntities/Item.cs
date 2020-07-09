using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RTS.DataAccess.Logic.RTSEntities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public int DeviceTypeId { get; set; }

        [ForeignKey("DeviceTypeId")]
        public ItemCategorie DeviceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int SerialNumber { get; set; }
        public Boolean IsActive { get; set; }
        public string CurentUserId { get; set; }

        [ForeignKey("CurentUserId")]
        public Employee CurentUser { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
