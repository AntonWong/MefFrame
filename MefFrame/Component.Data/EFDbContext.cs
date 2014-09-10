using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Component.Data
{
    /// <summary>
    ///     EF数据访问上下文
    /// </summary>
    [Export("EF", typeof (DbContext))]
    public class EFDbContext : DbContext
    {
        /// <summary>
        ///     初始化一个 使用连接名称为“SchoolSharonSqlServer”的数据访问上下文类 的新实例
        /// </summary>
        public EFDbContext()
            : base("SqlServerConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        ///     初始化一个 使用指定数据连接名称或连接串 的数据访问上下文类 的新实例
        /// </summary>
        public EFDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public EFDbContext(DbConnection existingConnection)
            : base(existingConnection, true)
        {
        }

        [ImportMany(typeof (IEntityMapper))]
        public IEnumerable<IEntityMapper> EntityMappers { get; set; }

        #region 属性

        //public DbSet<UserDetail> UserDetail { get; set; }
        //public DbSet<User> User { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //移除复数表名的契约
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();//防止黑幕交易 要不然每次都要访问 
            base.OnModelCreating(modelBuilder);

            #region 配置表结构关系

            //modelBuilder.Entity<User>().HasRequired(a => a.Departments).WithMany(a => a.Users).Map(m => m.MapKey("DepartmentId"));
            //modelBuilder.Entity<SendPost>().HasRequired(a => a.users).WithMany(a => a.Posts);
            //modelBuilder.Entity<User>().HasMany(a => a.UserRole).WithMany(a => a.users).Map(a => a.ToTable("User_Role"));

            #endregion
        }
    }
}