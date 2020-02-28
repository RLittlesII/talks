using ReactiveUI;

namespace GoReactive.Load
{
    public class LoadDataItemViewModel : ReactiveObject
    {
        private string _size;
        private string _ordered;
        private string _name;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string Ordered
        {
            get => _ordered;
            set => this.RaiseAndSetIfChanged(ref _ordered, value);
        }

        public string Size
        {
            get => _size;
            set => this.RaiseAndSetIfChanged(ref _size, value);
        }
    }
}