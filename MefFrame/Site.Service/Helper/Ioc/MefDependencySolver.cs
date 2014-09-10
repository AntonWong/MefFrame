using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Web;
using System.Web.Http.Dependencies;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace Site.Service.Helper.Ioc
{
    public class MefDependencySolver : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        private const string MefContainerKey = "MefContainerKey";
        private readonly ComposablePartCatalog _catalog;

        public MefDependencySolver(ComposablePartCatalog catalog)
        {
            _catalog = catalog;
        }

        public CompositionContainer Container
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(MefContainerKey))
                {
                    //原框架中没有[CompositionOptions.DisableSilentRejection],加上后会显示MEF配置错误时 会显示详细信息
                    HttpContext.Current.Items.Add(MefContainerKey,
                        new CompositionContainer(_catalog, CompositionOptions.DisableSilentRejection));
                }
                var container = (CompositionContainer) HttpContext.Current.Items[MefContainerKey];
                HttpContext.Current.Application["Container"] = container;
                return container;
            }
        }

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            string contractName = AttributedModelServices.GetContractName(serviceType);
            return Container.GetExportedValueOrDefault<object>(contractName);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetExportedValues<object>(serviceType.FullName);
        }

        #endregion

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}