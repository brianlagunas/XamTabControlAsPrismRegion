using Microsoft.Practices.Prism.Regions;
using System;
using TabControlRegion.Core;

namespace ModuleA
{
    public class ViewAViewModel : ViewModelBase
    {
        public ViewAViewModel()
        {
            Title = "View A";
        }

        public override void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            //prompt for confirmation here
            continuationCallback(true);
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //set to false to use different instances
            return false;
        }
    }
}
