using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using GoReactive.Services.Orders;
using GoReactive.Services.SignalR;
using ReactiveUI;

namespace GoReactive.Load
{
    public class LoadDataViewModel : ViewModelBase
    {
        private readonly IOrderService _hubConnectionService;
        private ReadOnlyObservableCollection<LoadDataItemViewModel> _orders;

        public LoadDataViewModel(IOrderService hubConnectionService)
        {
            _hubConnectionService = hubConnectionService;

            _hubConnectionService
                .Orders
                .Connect()
                .Publish()
                .RefCount()
                .Transform(x =>  new LoadDataItemViewModel { Name = x.Name, Ordered = x.DrinkName, Size = x.Size.ToString() })
                .Sort(SortExpressionComparer<LoadDataItemViewModel>.Ascending(x => x.Size))
                .Bind(out _orders)
                .DisposeMany()
                .Subscribe();

            InitializeData = ReactiveCommand.CreateFromTask(ExecuteInitialize);
        }

        public ReadOnlyObservableCollection<LoadDataItemViewModel> Orders => _orders;

        public ReactiveCommand<Unit, Unit> InitializeData { get; set; }

        private async Task ExecuteInitialize() => await _hubConnectionService.GetOrders();
    }
}
