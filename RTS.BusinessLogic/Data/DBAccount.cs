using Microsoft.AspNetCore.Identity;
using RTS.BusinessLogic.IServices;
using RTS.BusinessLogic.ViewModel;
using RTS.DataAccess.Data;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS.BusinessLogic.Data
{
    public class DBAccount :IAccountService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Employee> _userManager;
        private readonly RTSDBContext _context;


        public DBAccount(RoleManager<IdentityRole> roleManager, UserManager<Employee> userManager, RTSDBContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }



        public async Task<List<UserViewModel>> GetEmployee()
        {
            var model = new List<UserViewModel>();

            foreach (var user in _userManager.Users)
            {
                if (user.IsDeleted == false)
                {
                    var userRole = new UserViewModel
                    {
                        UserId = user.Id,
                        UserName = user.Email
                    };
                    foreach (var role in _roleManager.Roles)
                    {
                        if (await _userManager.IsInRoleAsync(user, role.Name))
                        {
                            userRole.RoleName = role.Name;
                            userRole.RoleId = role.Id;
                        }
                    }
                    model.Add(userRole);
                }
            }
            return model.ToList();
        }
        public async Task<List<UserViewModel>> GetDeletedEmployee()
        {
            var model = new List<UserViewModel>();
            foreach (var user in _userManager.Users)
            {
                if (user.IsDeleted == true)
                {
                    var userRole = new UserViewModel
                    {
                        UserId = user.Id,
                        UserName = user.Email
                    };
                    foreach (var role in _roleManager.Roles)
                    {
                        if (await _userManager.IsInRoleAsync(user, role.Name))
                        {
                            userRole.RoleName = role.Name;
                            userRole.RoleId = role.Id;
                        }
                    }
                    model.Add(userRole);
                }
            }
            return model.ToList();
        }

        public List<IdentityRole> GetRole()
        {
            var role = _roleManager.Roles;
            return role.ToList();
        }
        public async Task Edit(string userId,UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            user.Email = model.UserName;
            user.IsDeleted = model.IsDeleted;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }
        public async Task Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
