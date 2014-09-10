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
    /// ——Reports
    /// </summary>  
    [Export(typeof(IReportsSiteContract))]
    internal class ReportsSiteService : ReportsService, IReportsSiteContract
    {
       
    }
}
