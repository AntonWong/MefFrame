//------------------------------------------------------------------------------
//Copyright ©车易拍-公共服务组团队. All Rights Reserved.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Service.Impl;
using Core.Models;

namespace Site.Service.Impl
{
    /// <summary>
    /// ——Sys_Menu
    /// </summary>  
    [Export(typeof(ISys_MenuSiteContract))]
    internal class Sys_MenuSiteService : Sys_MenuService, ISys_MenuSiteContract
    {
       
    }
}
