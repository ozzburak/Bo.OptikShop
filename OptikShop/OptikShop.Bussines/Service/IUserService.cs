using OptikShop.Business.Dtos;

using OptikShop.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Business.Service
{
    public interface IUserService
    {
        ServiceMessage AddUser(AddUserDto addUserDto);

        UserInfoDto LoginUser(LoginUserDto loginUserDto);
        
    }
}
