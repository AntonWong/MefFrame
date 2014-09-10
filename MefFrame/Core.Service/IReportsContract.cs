//------------------------------------------------------------------------------
//Copyright ©车易拍-公共服务组团队. All Rights Reserved.
//------------------------------------------------------------------------------
using Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Service
{
    /// <summary>
    /// ——Reports
    /// </summary>    
    public interface IReportsContract
    {
       
        //查询数据集
        IQueryable<Reports> Reportss { get; }
        //添加
        int Insert(Reports entity);

        /// <summary>
        /// 删  除
        /// </summary>
        /// <param name="id"> ID主键 </param>
        /// <param name="isSave">是否执行保存</param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(int id, bool isSave = false);
        
		/// <summary>
        /// 按需修改
        /// </summary>
        /// <param name="propertyExpression">需要修改的字段：.UpdateEntity<Member/>(m => new  {m.Password,m.ModifyDate}, member);</param>
        /// <param name="entity">实体</param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        int Update(Expression<Func<Reports, object>> propertyExpression,Reports entity,bool isSave=false);
   
    }
}
