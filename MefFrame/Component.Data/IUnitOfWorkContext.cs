using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Component.Data
{
    /// <summary>
    ///     数据单元操作接口
    /// </summary>
    public interface IUnitOfWorkContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        ///     为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class; 

        /// <summary>
        ///     注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterNew<TEntity>(TEntity entity) where TEntity : class; 

        /// <summary>
        ///     批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : class; 

        /// <summary>
        ///     注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterModified<TEntity>(TEntity entity) where TEntity : class; 

        /// <summary>
        ///     注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class; 

        /// <summary>
        ///     批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : class; 

        /// <summary>
        /// 按需修改实体 调用方法 例如：dbContext.UpdateEntity<Member/>(m => new  {m.Password,m.AddDate}, member);
        /// CreateDate:2014年9月5日 17:17:32
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="propertyExpression">需要修改的属性字段</param>
        /// <param name="entities">实体-可变长度数组</param>
        void UpdateEntity<TEntity>(Expression<Func<TEntity, object>> propertyExpression,
            params TEntity[] entities) where TEntity : class;

        /// <summary>
        /// 根据主键ID删除实体
        /// 调用方法 例如：dbContext.DeleteEntity<Member/>(new Member { Id = 1 });
        /// CreateDate:2014年9月5日 17:17:51
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        void DeleteEntity<TEntity>(params TEntity[] entities) where TEntity : class;

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        void ExecuteSqlCommand(string sql);
    }
}