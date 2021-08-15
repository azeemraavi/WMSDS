using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Entities;
using Wmsds.Entities.Common;

namespace Wmsds.Bll.Account
{
    public interface IUserAccount
    {
        Task<WmsdsResponse<AppUser>> DoLogin(string username, string password);
        Task<WmsdsResponse<AppUser>> AddUser(AppUser appUser);
    }
}
