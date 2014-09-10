using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using Component.Tools;
using EntityFramework.Extensions;

namespace Component.Data
{
    /// <summary>
    ///     EntityFramework仓储操作基类
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public abstract class EFRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    // : IRepository<TEntity> where TEntity : Entity
    {
        #region 属性

        /// <summary>
        ///     获取 仓储上下文的实例
        /// </summary>
        [Import]
        public IUnitOfWork UnitOfWork { get; set; }


        /// <summary>
        ///     获取或设置 EntityFramework的数据仓储上下文
        /// </summary>
        protected IUnitOfWorkContext EFContext
        {
            get
            {
                if (UnitOfWork is IUnitOfWorkContext)
                {
                    return UnitOfWork as IUnitOfWorkContext;
                }
                throw new DataAccessException(string.Format("数据仓储上下文对象类型不正确，应为IRepositoryContext，实际为 {0}",
                    UnitOfWork.GetType().Name));
            }
        }

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        public virtual IQueryable<TEntity> Entities
        {
            get { return EFContext.Set<TEntity>(); }
        }

        #endregion

        #region 公共方法

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(TEntity entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterNew(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            PublicHelper.CheckArgument(entities, "entities");
            EFContext.RegisterNew(entities);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(object id, bool isSave = true)
        {
            PublicHelper.CheckArgument(id, "id");
            TEntity entity = EFContext.Set<TEntity>().Find(id);
            return entity != null ? Delete(entity, isSave) : 0;
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TEntity entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterDeleted(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            PublicHelper.CheckArgument(entities, "entities");
            EFContext.RegisterDeleted(entities);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).Delete();
        }

        /// <summary>
        ///     修改操作
        /// </summary>
        /// <param name="filterExpression">查询条件-谓语表达式</param>
        /// <param name="updateExpression">实体-谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public virtual int Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
           return Entities.Update(filterExpression, updateExpression);
        }
        #endregion

        //Add At 2014年9月5日

        /// <summary>
        /// 按需修改实体 调用方法 例如：dbContext.UpdateEntity<Member/>(m => new  {m.Password,m.AddDate}, member);
        /// CreateDate:2014年9月5日 17:17:32
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="propertyExpression">需要修改的属性字段</param>
        /// <param name="isSave"></param>
        /// <param name="entities">实体-可变长度数组</param>
        public int UpdateEntity(Expression<Func<TEntity, object>> propertyExpression,bool isSave =false,
            params TEntity[] entities)
        {
            EFContext.UpdateEntity(propertyExpression, entities);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        /// 根据主键ID删除实体
        /// 调用方法 例如：dbContext.DeleteEntity<Member/>(new Member { Id = 1 });
        /// CreateDate:2014年9月5日 17:17:51
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="isSave"></param>
        /// <param name="entities"></param>
        public int DeleteEntity(bool isSave =false,params TEntity[] entities)
        {
            EFContext.DeleteEntity(entities);
            return isSave ? EFContext.Commit() : 0;
        }

    }
}