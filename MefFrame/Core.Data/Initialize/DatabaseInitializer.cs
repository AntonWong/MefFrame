using System;
using System.Data.Entity;
using Component.Data;

namespace Core.Data.Initialize
{
    /// <summary>
    ///     数据库初始化操作类
    /// </summary>
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Configuration<EFDbContext>>());
            using (var db = new EFDbContext())
            {
                db.Database.Initialize(false);
            }
            Console.WriteLine("数据库初始化成功");
            Console.ReadKey();
        }
    }
}