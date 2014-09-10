using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Component.Data
{
    /// <summary>
    ///     定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public interface IRepository<TEntity>
    {
        #region 属性

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        #endregion

        #region 公共方法

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(TEntity entity, bool isSave = true);

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(IEnumerable<TEntity> entities, bool isSave = true);

     
        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(TEntity entity, bool isSave = true);

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(IEnumerable<TEntity> entities, bool isSave = false);

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     修改操作
        /// </summary>
        /// <param name="filterExpression">查询条件-谓语表达式</param>
        /// <param name="updateExpression">实体-谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        int Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression);

        /// <summary>
        /// 按需修改实体 调用方法 例如：dbContext.UpdateEntity<Member/>(m => new  {m.Password,m.AddDate}, member);
        /// CreateDate:2014年9月5日 17:17:32
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="propertyExpression">需要修改的属性字段</param>
        /// <param name="isSave"></param>
        /// <param name="entities">实体-可变长度数组</param>
        int UpdateEntity(Expression<Func<TEntity, object>> propertyExpression,bool isSave =false,
            params TEntity[] entities);


        /// <summary>
        /// 根据主键ID删除实体
        /// 调用方法 例如：dbContext.DeleteEntity<Member/>(new Member { Id = 1 });
        /// CreateDate:2014年9月5日 17:17:51
        /// </summary>
        /// <typeparam name="TEntity">可变长度数组</typeparam>
        /// <param name="isSave">是否执行保存</param>
        /// <param name="entities"></param>
        int DeleteEntity(bool isSave = false, params TEntity[] entities);

        #endregion
    }
}