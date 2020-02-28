using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace GoReactive
{
    public abstract class ContentPageBase<T> : ReactiveContentPage<T>
        where T : class, IReactiveObject
    {
        protected CompositeDisposable ViewBindings { get; } = new CompositeDisposable();
    }
}