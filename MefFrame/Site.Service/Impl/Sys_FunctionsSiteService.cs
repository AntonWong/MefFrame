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
    /// ——Sys_Functions
    /// </summary>  
    [Export(typeof(ISys_FunctionsSiteContract))]
    internal class Sys_FunctionsSiteService : Sys_FunctionsService, ISys_FunctionsSiteContract
    {
       
    }
}
