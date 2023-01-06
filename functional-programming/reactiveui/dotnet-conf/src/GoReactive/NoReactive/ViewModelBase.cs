using ReactiveUI;

namespace NoReactive
{
    public abstract class ViewModelBase : ReactiveObject, Sextant.IViewModel
    {
        public virtual string Id { get; }
    }
}