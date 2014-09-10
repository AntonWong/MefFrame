using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Site.Models;
using Site.Service;

namespace MyMvc1.Controllers
{
    [Export]
    public class HomeController : Controller
    {

        [Import]
        public ITestDataSiteContract TestDataContract { get; set; }

        /// <summary>
        /// 列表
        /// CreateDate:2014年9月5日 10:25:29
        /// Author:王旭东
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var list = TestDataContract.Menus();
            return View(list);
        }

        /// <summary>
        /// 单条实体
        /// CreateDate:2014-09-05 10:26:25
        /// Author:王旭东
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            //修改读取
            if (id > 0)
            {
                var model = TestDataContract.Menu(id)??new MenuView();
               
                return View(model);
            }
            //新增
            return View(new MenuView());

        }

        /// <summary>
        /// 删除
        /// CreateDate:2014-09-05 10:26:52
        /// Author:王旭东
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var i = TestDataContract.DeleteMenu(id);
           
            if (i < 1)
            {
                var result = new { status = i, message = "删除失败！" };
                return Json(result);
            }
            return Json(new { status = i, message = "删除成功！" });
        }

        /// <summary>
        /// 保存 （新增、修改）
        /// CreateDate:2014年9月5日 10:27:27
        /// Author：王旭东
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Sava(MenuView model)
        {
            TestDataContract.SaveMenu(model);
            var list = TestDataContract.Menus();
            return View("List", list);
        }

       

    }
}