using System.Data.Entity.Migrations;
using Component.Data;

namespace Core.Data.Initialize
{
    internal sealed class Configuration<TContext> : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            ///数据迁移-允许修改表结构
            AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        ///     重写方法-添加模拟数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(EFDbContext context)
        {
            //List<User> DepartmentList = new List<User> 
            //{
            //    new User{  UserName="AntonWong",PassWord="1232",PassWord2="123"} 
            //};
            // DbSet<User> roleSet = context.Set<User>();
            // roleSet.AddOrUpdate(u => new { u.UserName }, DepartmentList.ToArray());
        }
    }
}