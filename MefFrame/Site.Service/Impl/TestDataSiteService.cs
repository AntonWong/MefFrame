//------------------------------------------------------------------------------
// <copyright file="TestDataConfiguration.generated.cs">
//        生成时间：2014-09-02 13:50
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using Core.Service.Impl;
using Core.Models;
using Site.Models;

namespace Site.Service.Impl
{
    /// <summary>
    /// ——TestData
    /// </summary>  
    [Export(typeof(ITestDataSiteContract))]
    internal class TestDataSiteService : TestDataService, ITestDataSiteContract
    {
        /// <summary>
        /// 列表集合
        /// </summary>
        /// <returns></returns>
        public List<MenuView> Menus()
        {
            return TestDatas.Select(s => new MenuView {Id = s.Id, Name = s.Name}).ToList();
        }
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuView Menu(int id)
        {
            return TestDatas.Where(m=>m.Id==id).Select(s => new MenuView {Id = s.Id, Name = s.Name}).FirstOrDefault();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteMenu(int id)

        {
            Delete(id);
            return UnitOfWork.Commit();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveMenu(MenuView model)
        {
            try
            {
                if (model.Id > 0)
                {
                    Update(m => new {m.Name}, new TestData {Id = model.Id, Name = model.Name});
                    return UnitOfWork.Commit();
                }
                return Insert(new TestData { Name = model.Name });
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
            
        }
    }
}
