using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using ReactiveUI;
using Splat;

namespace LoginState
{
    public abstract class RoutableViewModelBase : ReactiveObject, IRoutableViewModel
    {
        protected CompositeDisposable Subscriptions { get; } = new CompositeDisposable();

        public virtual bool IsLoading { get; }

        public virtual string UrlPathSegment { get; }

        public IScreen HostScreen => Locator.Current.GetService<IScreen>();
    }
}
