using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RTS.DataAccess.Logic.RTSEntities
{
    public class ItemCategorie
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
