using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Essentials;

namespace GoReactive.Load
{
    public partial class LoadData : ContentPageBase<LoadDataViewModel>
    {
        public LoadData()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.Orders)
                .Where(x => x != null)
                .BindTo(this, x => x.LoadDataListView.ItemsSource)
                .DisposeWith(ViewBindings);

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(ViewBindings);
        }
    }
}
