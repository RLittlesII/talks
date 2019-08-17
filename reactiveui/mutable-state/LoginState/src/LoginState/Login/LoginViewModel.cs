using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LoginState.Services;
using Splat;

namespace LoginState
{
    public class LoginViewModel : RoutableViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private string _loginText;
        private string _password;
        private string _emailAddress;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;

        public LoginViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            LoginText = "Login";

            IsValid = this.WhenAnyValue(x => x.EmailAddress, x => x.Password, (email, pass) => Validate(email, pass));

            Login = ReactiveCommand.CreateFromObservable(() => Observable.FromAsync(ExecuteLogin).TakeUntil(Cancel), IsValid);
            Cancel = ReactiveCommand.CreateFromTask(ExecuteCancel, Login.IsExecuting);

            _isLoading = this.WhenAnyObservable(x => x.Login.IsExecuting)
                .ToProperty(this, x => x.IsLoading, false)
                .DisposeWith(Subscriptions);
        }

        public override bool IsLoading => _isLoading.Value;

        public IObservable<bool> IsValid { get; set; }

        public ReactiveCommand<Unit, Unit> Login { get; set; }

        public ReactiveCommand<Unit, Unit> Cancel { get; set; }

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

        private async Task ExecuteLogin() => await _authenticator.Authenticate(_emailAddress, _password);

        private async Task ExecuteCancel()
        {
            this.Log().Debug(nameof(ExecuteCancel));
            await Task.CompletedTask;
        }

        private bool Validate(string email, string pass) => ValidateEmail(email) && ValidatePassword(pass);

        private static bool ValidateEmail(string email) =>
            !string.IsNullOrEmpty(email) &&
            Regex.Matches(email, "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$").Count == 1;

        private static bool ValidatePassword(string password) => !string.IsNullOrEmpty(password) && password.Length > 5;
    }
}
