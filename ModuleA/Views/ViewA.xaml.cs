using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;

namespace ModuleA
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    [RegionMemberLifetime(KeepAlive=true)]
    public partial class ViewA : UserControl, IView, INavigationAware
    {
        public ViewA()
        {
            InitializeComponent();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}
