using System.Reactive.Disposables;
using ReactiveUI.XamForms;

namespace LoginState
{
    public abstract class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel>
        where TViewModel : class
    {
        protected CompositeDisposable ViewBindings { get; } = new CompositeDisposable();
    }
}