using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;

namespace Component.Data
{
    /// <summary>
    ///     单元操作实现基类
    /// </summary>
    public abstract class UnitOfWorkContextBase : IUnitOfWorkContext
    {
        /// <summary>
        ///     获取 当前使用的数据访问上下文对象
        /// </summary>
        protected abstract DbContext Context { get; }

        /// <summary>
        ///     获取 当前单元操作是否已被提交
        /// </summary>
        public bool IsCommitted { get; private set; }

        /// <summary>
        ///     提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            if (IsCommitted)
            {
                return 0;
            }
            try
            {

                int result = Context.SaveChanges();
                IsCommitted = true;
                return result;
            }
            catch (Exception e)
            {
                //if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                //{
                //var sqlEx = e.InnerException.InnerException as SqlException;
                //string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                throw new Exception(e.Message);
                //}
                //throw;
            }
        }

        /// <summary>
        ///     把当前单元操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            IsCommitted = false;
        }

        public void Dispose()
        {
            if (!IsCommitted)
            {
                Commit();
            }
            Context.Dispose();
        }

        /// <summary>
        ///     为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : class // where TEntity : Entity
        {
            return Context.Set<TEntity>();
        }

        /// <summary>
        ///     注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class //where TEntity : Entity
        {
            EntityState state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            IsCommitted = false;
        }

        /// <summary>
        ///     批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : class //where TEntity : Entity
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterNew(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        ///     注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterModified<TEntity>(TEntity entity) where TEntity : class
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Set<TEntity>().Attach(entity);
            }
            Context.Entry(entity).State = EntityState.Modified;
            IsCommitted = false;
        }

        /// <summary>
        ///     注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Entry(entity).State = EntityState.Deleted;
            IsCommitted = false;
        }

        /// <summary>
        ///     批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        //where TEntity : Entity
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterDeleted(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        #region 按需修改实体
        /// <summary>
        /// 按需修改实体 调用方法 例如：dbContext.UpdateEntity<Member/>(m => new  {m.Password,m.AddDate}, member);
        /// CreateDate:2014年9月5日 17:17:32
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="propertyExpression">需要修改的属性字段</param>
        /// <param name="entities">实体-可变长度数组</param>
        public void UpdateEntity<TEntity>(Expression<Func<TEntity, object>> propertyExpression,
            params TEntity[] entities) where TEntity : class
        {
             if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            if (entities == null) throw new ArgumentNullException("entities");
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
            foreach (TEntity entity in entities)
            {
                DbEntityEntry<TEntity> entry = Context.Entry(entity);
                entry.State = EntityState.Unchanged;
                foreach (var memberInfo in memberInfos)
                {
                    entry.Property(memberInfo.Name).IsModified = true;
                }
                Context.Configuration.ValidateOnSaveEnabled = false;
            }
        }
        #endregion

        #region 根据主键ID删除实体
        /// <summary>
        /// 根据主键ID删除实体
        /// 调用方法 例如：dbContext.DeleteEntity<Member/>(new Member { Id = 1 });
        /// CreateDate:2014年9月5日 17:17:51
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        public void DeleteEntity<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (TEntity entity in entities)
            {
                DbEntityEntry<TEntity> entry = Context.Entry(entity);
                Context.Configuration.ValidateOnSaveEnabled = false;
                entry.State = EntityState.Deleted;
            }
        }
        #endregion

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        public void ExecuteSqlCommand(string sql)
        {
            try
            {
                Context.Database.ExecuteSqlCommand(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("数据删除时发生异常:" + ex.Message);
            }
        }
       
    }
}