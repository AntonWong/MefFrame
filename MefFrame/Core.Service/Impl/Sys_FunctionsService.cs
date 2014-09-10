//------------------------------------------------------------------------------
//Copyright ©车易拍-公共服务组团队. All Rights Reserved.
//------------------------------------------------------------------------------
using Core.Data.Repositories;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using Core.Models;


namespace Core.Service.Impl
{
    /// <summary>
    /// ——Sys_Functions
    /// </summary>    
    public class Sys_FunctionsService : CoreServiceBase, ISys_FunctionsContract
    {
        #region 受保护属性 获取或设置数据访问对象

        [Import]
        protected ISys_FunctionsRepository Sys_FunctionsRepository { get; set; }  
 
        #endregion

        #region 公共属性
        /// <summary>
        /// 数据对象
        /// </summary>
        public IQueryable<Sys_Functions> Sys_Functionss
        {
            get { return Sys_FunctionsRepository.Entities; }
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 添   加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Insert(Sys_Functions entity)
        {
            return Sys_FunctionsRepository.Insert(entity);
        }
        
		/// <summary>
        /// 删  除
        /// </summary>
        /// <param name="id"> ID主键 </param>
		/// <param name="isSave">是否执行保存</param>
        /// <returns> 操作影响的行数 </returns>
        public int Delete(int id, bool isSave = false)
        {
            //Sys_FunctionsRepository.DeleteEntity(new Sys_Functions{Id=id});
			return 0;
        }

        /// <summary>
        /// 按需修改
        /// </summary>
        /// <param name="propertyExpression">需要修改的字段：.UpdateEntity<Member/>(m => new  {m.Password,m.ModifyDate}, member);</param>
        /// <param name="entity">实体</param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public int Update(Expression<Func<Sys_Functions, object>> propertyExpression,Sys_Functions entity,bool isSave=false)
        {
            return Sys_FunctionsRepository.UpdateEntity(propertyExpression, isSave, entity);
        }

        #endregion
       
    }
}
