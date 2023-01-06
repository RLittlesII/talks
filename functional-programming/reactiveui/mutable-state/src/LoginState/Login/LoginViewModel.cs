using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LoginState.Services;
using Splat;
using Xamarin.Essentials;

namespace LoginState
{
    public class LoginViewModel : RoutableViewModelBase
    {
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private readonly ObservableAsPropertyHelper<bool> _isConnected;
        private readonly ObservableAsPropertyHelper<bool> _isWirelessConnection;
        private readonly IAuthenticator _authenticator;
        private readonly IConnectivity _connectivity;
        private string _loginText;
        private string _password;
        private string _emailAddress;

        public LoginViewModel(IAuthenticator authenticator, IConnectivity connectivity)
        {
            _authenticator = authenticator;
            _connectivity = connectivity;

            LoginText = "Login";

            IsValid = this.WhenAnyValue(x => x.EmailAddress, x => x.Password, Validate);

            Login = ReactiveCommand.CreateFromObservable(() => Observable.FromAsync(ExecuteLogin).TakeUntil(Cancel), IsValid);
            Cancel = ReactiveCommand.Create(() => { });

            _isLoading =
                this.WhenAnyObservable(x => x.Login.IsExecuting)
                    .ToProperty(this, nameof(Login), false, scheduler: RxApp.TaskpoolScheduler, deferSubscription: true)
                    .DisposeWith(Subscriptions);

            var connectionState = _connectivity.ConnectionStateChanges;

            _isConnected = 
                connectionState
                .Select(x => x.NetworkAccess == NetworkAccess.Internet)
                .StartWith(true)
                .DistinctUntilChanged()
                .ToProperty(this, x => x.IsConnected)
                .DisposeWith(Subscriptions);

            _isWirelessConnection =
                connectionState
                    .SelectMany(x => x.ConnectionProfiles)
                    .Select(x => x != ConnectionProfile.WiFi)
                    .DistinctUntilChanged()
                    .ToProperty(this, x => x.IsWirelessConnection);

            connectionState
                .Where(x => x.NetworkAccess != NetworkAccess.Internet)
                .Subscribe(_ =>
                    Interactions
                        .AlertInteraction
                        .Handle(("Network Activity", "Oops! You appear to have lost internet!", "Okay")).Subscribe());
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set => this.RaiseAndSetIfChanged(ref _emailAddress, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string LoginText
        {
            get => _loginText;
            set => this.RaiseAndSetIfChanged(ref _loginText, value);
        }

        public bool IsWirelessConnection => _isWirelessConnection.Value;

        public bool IsConnected => _isConnected.Value;

        public override bool IsLoading => _isLoading.Value;

        public IObservable<bool> IsValid { get; set; }

        public ReactiveCommand<Unit, Unit> Login { get; set; }

        public ReactiveCommand<Unit, Unit> Cancel { get; set; }

        private async Task ExecuteLogin() => await _authenticator.Authenticate(_emailAddress, _password);

        private Task ExecuteCancel()
        {
            this.Log().Debug(nameof(ExecuteCancel));
            return Task.CompletedTask;
        }

        private bool Validate(string email, string pass) => ValidateEmail(email) && ValidatePassword(pass);

        private static bool ValidateEmail(string email) =>
            !string.IsNullOrEmpty(email) &&
            Regex.Matches(email, "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$").Count == 1;

        private static bool ValidatePassword(string password) => !string.IsNullOrEmpty(password) && password.Length > 5;
    }
}
