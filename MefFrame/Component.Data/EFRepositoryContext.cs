using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace Component.Data
{
    /// <summary>
    ///     数据单元操作类
    /// </summary>
    [Export(typeof (IUnitOfWork))]
    public class EFRepositoryContext : UnitOfWorkContextBase
    {
        /// <summary>
        ///     获取 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get
            {
                var dbcontext = CallContext.GetData("DbContext") as DbContext;
                //判断线程里面是否有数据
                if (dbcontext == null) //线程的数据槽里面没有次上下文
                {
                    dbcontext = EFDbContext; //创建了一个EF上下文
                    //存储指定对象
                    //CallContext:是线程内部唯一的独用的数据槽(一块内存空间)
                    CallContext.SetData("DbContext", dbcontext);
                }
                return dbcontext;
            }
        }

        [Import("EF", typeof (DbContext))]
        public EFDbContext EFDbContext { get; set; }
    }
}