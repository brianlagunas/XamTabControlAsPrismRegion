using Microsoft.Practices.Prism.Regions;
using System;
using TabControlRegion.Core;

namespace ModuleA
{
    public class ViewBViewModel : ViewModelBase
    {
        public ViewBViewModel()
        {
            Title = "View B";
        }

        public override void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            //don't use MessageBox in your ViewModel in product code!
            System.Windows.MessageBox.Show("Can't close me");

            continuationCallback(false);
        }
    }
}
