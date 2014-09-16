using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Core.Models;
using Core.Service;
using Site.Models;

namespace Controllers.Service.MemuModule
{
    [Export]
    public class HomeController : Controller
    {

        [Import]
        public ITestDataContract TestDataContract { get; set; }

        /// <summary>
        /// 列表
        /// CreateDate:2014年9月5日 10:25:29
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var list = TestDataContract.TestDatas.Select(s =>
                new MenuView
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList();
            return View(list);
        }

        /// <summary>
        /// 单条实体
        /// CreateDate:2014-09-05 10:26:25
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            //修改读取
            if (id > 0)
            {
                var model = TestDataContract.TestDatas
                    .Select(s =>
                        new MenuView
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Description = s.Description
                        })
                    .FirstOrDefault(m => m.Id == id);
                return View(model);
            }
            //新增
            return View(new MenuView());

        }

        /// <summary>
        /// 删除
        /// CreateDate:2014-09-05 10:26:52
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var i = TestDataContract.Delete(id, true);

            if (i < 1)
            {
                var result = new {status = i, message = "删除失败！"};
                return Json(result);
            }
            return Json(new {status = i, message = "删除成功！"});
        }

        /// <summary>
        /// 保存 （新增、修改）
        /// CreateDate:2014年9月5日 10:27:27
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(MenuView model)
        {
            int result;
            try
            {
                if (model.Id > 0)
                {
                    result = TestDataContract.Update(m =>
                        new TestData
                        {
                            Id = model.Id,
                            Name = model.Name,
                            //Description = model.Name
                        });
                    //  return UnitOfWork.Commit();
                }
                else
                {
                    result = TestDataContract.Insert(new TestData { Name = model.Name});
                }

                if (result < 1)
                {
                    ModelState.AddModelError("", "添加失败");
                    return View("Edit", model);
                }
               return Redirect("List");

            }
            catch
            {
                ModelState.AddModelError("", "添加失败");
                return View("Edit", model);
            }
        }
    }
}