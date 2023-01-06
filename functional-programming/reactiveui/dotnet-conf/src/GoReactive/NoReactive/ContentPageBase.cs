using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace NoReactive
{
    public abstract class ContentPageBase<T> : ReactiveContentPage<T>
        where T : class, IReactiveObject
    {
    }
}