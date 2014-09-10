//------------------------------------------------------------------------------
//Copyright ©车易拍-公共服务组团队. All Rights Reserved.
//------------------------------------------------------------------------------
using Core.Data.Repositories;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using Core.Models;


namespace Core.Service.Impl
{
    /// <summary>
    /// ——TestData
    /// </summary>    
    public class TestDataService : CoreServiceBase, ITestDataContract
    {
        #region 受保护属性 获取或设置数据访问对象

        [Import]
        protected ITestDataRepository TestDataRepository { get; set; }  
 
        #endregion

        #region 公共属性
        /// <summary>
        /// 数据对象
        /// </summary>
        public IQueryable<TestData> TestDatas
        {
            get { return TestDataRepository.Entities; }
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 添   加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Insert(TestData entity)
        {
            return TestDataRepository.Insert(entity);
        }
        
		/// <summary>
        /// 删  除
        /// </summary>
        /// <param name="id"> ID主键 </param>
		/// <param name="isSave">是否执行保存</param>
        /// <returns> 操作影响的行数 </returns>
        public int Delete(int id, bool isSave = false)
        {
            return TestDataRepository.DeleteEntity(isSave,new TestData{Id=id});
			//return 0;
        }

        /// <summary>
        /// 按需修改
        /// </summary>
        /// <param name="propertyExpression">需要修改的字段：.UpdateEntity<Member/>(m => new  {m.Password,m.ModifyDate}, member);</param>
        /// <param name="entity">实体</param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public int Update(Expression<Func<TestData, object>> propertyExpression,TestData entity,bool isSave=false)
        {
            return TestDataRepository.UpdateEntity(propertyExpression, isSave, entity);
        }

        #endregion
       
    }
}
