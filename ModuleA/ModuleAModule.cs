using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IUnityContainer _container;

        public ModuleAModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            //register for nav
            _container.RegisterType(typeof(Object), typeof(ViewA), "ViewA");
            _container.RegisterType(typeof(Object), typeof(ViewB), "ViewB");
        }
    }
}
