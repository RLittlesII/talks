using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using ReactiveUI;

namespace Bang.Pages
{
    public partial class CardView
    {
        public CardView()
        {
            this.WhenAnyValue(x => x.ViewModel.News)
                .WhereNotNull()
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.MainThreadScheduler)
                .Subscribe(_ => StateHasChanged());

            this.WhenAnyValue(x => x.ViewModel.InitializeCommand)
                .WhereNotNull()
                .Select(_ => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.InitializeCommand);
        }

        protected override async Task OnInitializedAsync()
        {
            // var hubConnection = new HubConnectionBuilder()
            //     .WithUrl("http://localhost:7071/api")
            //     .WithAutomaticReconnect()
            //     .Build();
            //
            // hubConnection.On<NewsModel>(NewsModel.Stream, message =>
            // {
            //     ViewModel.News = message;
            // });
            //
            // await hubConnection.StartAsync();
            await base.OnInitializedAsync();
        }

        protected override void OnInitialized()
        {
            ViewModel = _cardViewModel;
        }
    }
}