using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RTS.DataAccess.Logic.RTSEntities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DeviceTypeId { get; set; }

        [ForeignKey("DeviceTypeId")]
        public ItemCategorie DeviceType { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int SerialNumber { get; set; }
        [Required]
        public Boolean IsActive { get; set; }
        [DefaultValue("False")]
        public Boolean IsDeleted { get; set; }
        public string CurentUserId { get; set; }

        [ForeignKey("CurentUserId")]
        public Employee CurentUser { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
