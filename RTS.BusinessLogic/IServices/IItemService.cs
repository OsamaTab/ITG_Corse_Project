﻿using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.IServices
{
    public interface IItemService
    {
        public List<Item> GetItems();

        public Task Create(Item item);
        public Task Edit(Item item);
        public Task Delete(int id);
    }
}