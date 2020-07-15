using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTS.BusinessLogic.ViewModel
{
    public class ItemViewModel
    {
        public IEnumerable<Item> items { get; set; }
        public IEnumerable<Item> Devices { get; set; }
        public IEnumerable<ItemRequest> Request { get; set; }


    }
}
