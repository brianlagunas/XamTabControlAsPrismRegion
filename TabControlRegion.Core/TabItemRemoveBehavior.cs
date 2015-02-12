using Infragistics.Windows.Controls;
using Infragistics.Windows.Controls.Events;
using Microsoft.Practices.Prism.Regions;
using System.Windows;
using System.Windows.Interactivity;

namespace TabControlRegion.Core
{
    public class TabItemRemoveBehavior : Behavior<XamTabControl>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(TabItemEx.ClosingEvent, new RoutedEventHandler(TabItem_Closing));
            AssociatedObject.AddHandler(TabItemEx.ClosedEvent, new RoutedEventHandler(TabItem_Closed));
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.RemoveHandler(TabItemEx.ClosingEvent, new RoutedEventHandler(TabItem_Closing));
            AssociatedObject.RemoveHandler(TabItemEx.ClosedEvent, new RoutedEventHandler(TabItem_Closed));
        }

        void TabItem_Closing(object sender, RoutedEventArgs e)
        {
            IRegion region = RegionManager.GetObservableRegion(AssociatedObject).Value;
            if (region == null)
                return;

            var args = (TabClosingEventArgs)e;

            args.Cancel = !CanRemoveItem(GetItemFromTabItem(args.OriginalSource), region);
        }

        void TabItem_Closed(object sender, RoutedEventArgs e)
        {
            IRegion region = RegionManager.GetObservableRegion(AssociatedObject).Value;
            if (region == null)
                return;

            RemoveItemFromRegion(GetItemFromTabItem(e.OriginalSource), region);
        }

        object GetItemFromTabItem(object source)
        {
            var tabItem = source as TabItemEx;
            if (tabItem == null)
                return null;

            return tabItem.Content;
        }

        bool CanRemoveItem(object item, IRegion region)
        {
            bool canRemove = true;

            var context = new NavigationContext(region.NavigationService, null);

            var confirmRequestItem = item as IConfirmNavigationRequest;
            if (confirmRequestItem != null)
            {
                confirmRequestItem.ConfirmNavigationRequest(context, result =>
                {
                    canRemove = result;
                });
            }

            FrameworkElement frameworkElement = item as FrameworkElement;
            if (frameworkElement != null && canRemove)
            {
                IConfirmNavigationRequest confirmRequestDataContext = frameworkElement.DataContext as IConfirmNavigationRequest;
                if (confirmRequestDataContext != null)
                {
                    confirmRequestDataContext.ConfirmNavigationRequest(context, result =>
                    {
                        canRemove = result;
                    });
                }
            }

            return canRemove;
        }

        void RemoveItemFromRegion(object item, IRegion region)
        {
            var context = new NavigationContext(region.NavigationService, null);

            InvokeOnNavigatedFrom(item, context);

            region.Remove(item);
        }

        void InvokeOnNavigatedFrom(object item, NavigationContext navigationContext)
        {
            var navigationAwareItem = item as INavigationAware;
            if (navigationAwareItem != null)
            {
                navigationAwareItem.OnNavigatedFrom(navigationContext);
            }

            FrameworkElement frameworkElement = item as FrameworkElement;
            if (frameworkElement != null)
            {
                INavigationAware navigationAwareDataContext = frameworkElement.DataContext as INavigationAware;
                if (navigationAwareDataContext != null)
                {
                    navigationAwareDataContext.OnNavigatedFrom(navigationContext);
                }
            }
        }
    }
}
