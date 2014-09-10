//------------------------------------------------------------------------------
//Copyright ©车易拍-公共服务组团队. All Rights Reserved.
//------------------------------------------------------------------------------
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Service;
using Site.Models;

namespace Site.Service
{
    /// <summary>
    /// ——TestData核心业务契约
    /// </summary>    
    public interface ITestDataSiteContract:ITestDataContract
    {
        List<MenuView> Menus();

        MenuView Menu(int id);

        int DeleteMenu(int id);

        int SaveMenu(MenuView model);
    }
}
