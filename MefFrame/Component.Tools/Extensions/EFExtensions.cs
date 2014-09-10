using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;

namespace Component.Tools.Extensions
{
    public static class EFExtensions
    {
        #region 按需修改实体
        /// <summary>
        /// 按需修改实体 调用方法 例如：dbContext.UpdateEntity<Member/>(m => new  {m.Password,m.AddDate}, member);
        /// CreateDate:2014年9月5日 17:17:32
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext">上下文</param>
        /// <param name="propertyExpression">需要修改的属性字段</param>
        /// <param name="entities">实体-可变长度数组</param>
        public static void UpdateEntity<TEntity>(this DbContext dbContext,
            Expression<Func<TEntity, object>> propertyExpression, params TEntity[] entities)
            where TEntity : class 
        {
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            if (entities == null) throw new ArgumentNullException("entities");
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
            foreach (TEntity entity in entities)
            {
                DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                entry.State = EntityState.Unchanged;
                foreach (var memberInfo in memberInfos)
                {
                    entry.Property(memberInfo.Name).IsModified = true;
                }
                dbContext.Configuration.ValidateOnSaveEnabled = false;
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
        /// <param name="dbContext">上下文</param>
        /// <param name="entities"></param>
        public static void DeleteEntity<TEntity>(this DbContext dbContext, params TEntity[] entities) where TEntity : class 
        {
            foreach (TEntity entity in entities)
            {
                DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                dbContext.Configuration.ValidateOnSaveEnabled = false;
                entry.State = EntityState.Deleted;
            }
        }
        #endregion

        #region 上下文修改方法
        public static void Update<TEntity>(this DbContext dbContext,
            Expression<Func<TEntity, object>> propertyExpression, params TEntity[] entities) where TEntity : class
        {
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            if (entities == null) throw new ArgumentNullException("entities");
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
            foreach (TEntity entity in entities)
            {
                try
                {
                    DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                    dbContext.Configuration.ValidateOnSaveEnabled = false;
                    entry.State = EntityState.Unchanged;
                    foreach (var memberInfo in memberInfos)
                    {
                        entry.Property(memberInfo.Name).IsModified = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion 

    }
}
