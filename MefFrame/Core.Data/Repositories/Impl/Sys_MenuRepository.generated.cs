﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//	   如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="Sys_MenuRepository.generated.cs">
//		Copyright(c)2013 HOBOR.All rights reserved.
//		CLR版本：4.0.30319.239
//      开发组织：HOBOR开发团队@中国
//      公司网站：http://www.xieshouwang.com.cn
//      所属工程：XieShouWang.School.Sharon.Core.Data
//      生成时间：2014-09-02 13:50
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.Linq;

using Component.Data;
using Core.Models;


namespace Core.Data.Repositories.Impl
{
	/// <summary>
    ///   仓储操作层实现——Sys_Menu
    /// </summary>
    [Export(typeof(ISys_MenuRepository))]
    public partial class Sys_MenuRepository : EFRepositoryBase<Sys_Menu>, ISys_MenuRepository
    { }
}