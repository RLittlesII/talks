using ReactiveUI;

namespace GoReactive
{
    public abstract class ViewModelBase : ReactiveObject, Sextant.IViewModel
    {
        public virtual string Id { get; }
    }
}