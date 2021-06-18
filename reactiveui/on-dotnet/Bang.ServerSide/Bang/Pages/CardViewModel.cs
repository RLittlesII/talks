using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using ReactiveUI;
using Rocket.Surgery.Airframe.Data;

namespace Bang.Pages
{
    public class CardViewModel : ReactiveObject
    {
        private ISubject<NewsModel> _newsSubject = new Subject<NewsModel>();
        private NewsModel _news;

        public CardViewModel()
        {
            var hubConnection = new HubConnectionBuilder().WithUrl("https://functionapp-210623013109.azurewebsites.net/api").Build();
            // HubConnection hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:7071/api").Build();
            hubConnection.On<NewsModel>("newsStream", newsModel => { _newsSubject.OnNext(newsModel); });

            _newsSubject
                .Do(_ => { })
                .BindTo(this, x => x.News);

            InitializeCommand = ReactiveCommand.CreateFromTask(async () => await hubConnection.StartAsync());
        }

        public ReactiveCommand<Unit, Unit> InitializeCommand { get; set; }

        public NewsModel News
        {
            get => _news;
            set => this.RaiseAndSetIfChanged(ref _news, value);
        }
    }
}