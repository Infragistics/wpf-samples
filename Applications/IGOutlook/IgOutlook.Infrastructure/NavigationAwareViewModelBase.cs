using Prism.Regions;
using System;

namespace IgOutlook.Infrastructure
{
    public class NavigationAwareViewModelBase : ViewModelBase, IConfirmNavigationRequest
    {

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback.Invoke(true);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}
