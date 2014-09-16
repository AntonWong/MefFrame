using System.Web.Mvc;

namespace Controllers.Service.MemuModule
{
    public class MemuModuleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MemuModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MemuModule_default",
                "MemuModule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
