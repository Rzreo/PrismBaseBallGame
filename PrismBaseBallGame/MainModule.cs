using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismBaseBallGame.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismBaseBallGame
{
    internal class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var region = containerProvider.Resolve<IRegionManager>();
            region.RegisterViewWithRegion("ContentRegion", typeof(Game));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
