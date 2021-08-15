using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Dal;
using Wmsds.Entities;
using Wmsds.Entities.Common;

namespace Wmsds.Bll.Account
{
    public class UserAccountService : IUserAccount
    {
        public async Task<WmsdsResponse<AppUser>> AddUser(AppUser appUser)
        {
            var response = new WmsdsResponse<AppUser>();

            appUser.CreateAt = DateTime.Now;
            appUser.IsActive = true;
            if (appUser == null)
            {
                response.ResponseCode = EnumStatus.RequiredField;
                response.ResponseMessage = "All required fields are missing.";
                return response;
            }

            var isAlreadyExist = await IsUserAlreadyExist(appUser.UserName);
            if (isAlreadyExist)
            {
                response.ResponseCode = EnumStatus.AlreadyExist;
                response.ResponseMessage = "Username already exist.";
                return response;
            }
            if (string.IsNullOrEmpty(appUser.UserName))
            {
                response.ResponseCode = EnumStatus.RequiredField;
                response.ResponseMessage = "User name is required filed";
                return response;
            }

            if (string.IsNullOrEmpty(appUser.Password))
            {
                response.ResponseCode = EnumStatus.RequiredField;
                response.ResponseMessage = "Password is required filed";
                return response;
            }

            using (var _dbContext = new EntityContext())
            {
                _dbContext.AppUsers.Add(appUser);
                int res = await _dbContext.SaveChangesAsync();
                if (res > 0)
                {
                    response.ResponseCode = EnumStatus.Success;
                    response.ResponseMessage = "Successs";
                }
                else
                {
                    response.ResponseCode = EnumStatus.Failed;
                    response.ResponseMessage = "Failed to add user. Please try again.";
                    return response;
                }

            }
            return null;
        }
        private async Task<bool> IsUserAlreadyExist(string userName)
        {
            using (var _dbContext = new EntityContext())
            {
                var res = await _dbContext
                    .AppUsers.Where(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
                ).SingleOrDefaultAsync();
                if (res != null)
                    return true;
                else
                    return false;
            }
        }
        public async Task<WmsdsResponse<AppUser>> DoLogin(string username, string password)
        {
            var response = new WmsdsResponse<AppUser>();
            if (string.IsNullOrEmpty(username))
            {
                response.ResponseCode = EnumStatus.RequiredField;
                response.ResponseMessage = "User name is required filed";
                return response;
            }

            if (string.IsNullOrEmpty(password))
            {
                response.ResponseCode = EnumStatus.RequiredField;
                response.ResponseMessage = "Password is required filed";
                return response;
            }

            using (var _dbContext = new EntityContext())
            {
                var res = await _dbContext
                    .AppUsers.Where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && x.Password.Equals(password)).SingleOrDefaultAsync();
                if (res == null)
                {
                    response.ResponseCode = EnumStatus.NotFound;
                    response.ResponseMessage = "Invalid username/password.";
                    return response;
                }
                else
                {
                    response.ResponseCode = EnumStatus.Success;
                    response.ResponseMessage = "Successs";                    
                    response.DataObject = res;
                    response.DataObject.Password = "";
                    return response;
                }
            }

        }
    }
}
