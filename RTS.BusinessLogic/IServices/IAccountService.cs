using Microsoft.AspNetCore.Identity;
using RTS.BusinessLogic.ViewModel;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.IServices
{
    public interface IAccountService
    {
        public List<IdentityRole> GetRole();
        public Task<List<UserViewModel>> GetEmployee();
        public Task<List<UserViewModel>> GetDeletedEmployee();

        public Task Edit(string userId, UserViewModel model);
        public Task Delete(string id);
    }
}
