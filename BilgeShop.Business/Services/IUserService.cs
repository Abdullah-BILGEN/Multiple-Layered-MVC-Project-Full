using BilgeShop.Business.Dtos;
using BilgeShop.Business.Managers;
using BilgeShop.Business.Types;
using BilgeShop.Data.Entities;
using BilgeShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Business.Services
{
    public interface IUserService
    {

        ServiceMessage AddUser(UserAddDto userAddDto);

        UserInfoDto LoginUser(UserLogindto userLogindto);
        


    }
}
