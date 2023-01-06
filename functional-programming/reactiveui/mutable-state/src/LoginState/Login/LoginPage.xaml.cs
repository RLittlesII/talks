using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginState
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPageBase<LoginViewModel>, IEnableLogger
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.Bind(ViewModel, vm => vm.EmailAddress, page => page.EmailEntry.Text)
                .DisposeWith(ViewBindings);

            this.Bind(ViewModel, vm => vm.Password, page => page.PasswordEntry.Text)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, vm => vm.Login, page => page.LoginButton)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, vm => vm.Cancel, page => page.Activity.TapGesture,nameof(Activity.TapGesture.Tapped))
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, vm => vm.Cancel, page => page.Activity.ClickGesture, nameof(Activity.ClickGesture.Clicked))
                .DisposeWith(ViewBindings);

            this.OneWayBind(ViewModel, vm => vm.LoginText, page => page.LoginButton.Text)
                .DisposeWith(ViewBindings);

            this.OneWayBind(ViewModel, vm => vm.IsLoading, page => page.Activity.IsVisible)
                .DisposeWith(ViewBindings);

            this.OneWayBind(ViewModel, vm => vm.IsLoading, page => page.Activity.IsRunning)
                .DisposeWith(ViewBindings);

            this.OneWayBind(ViewModel, vm => vm.IsLoading, page => page.Activity.IsEnabled)
                .DisposeWith(ViewBindings);

            Activity
                .TapGesture
                .Events()
                .Tapped
                .Do(_ => this.Log().Debug("Activity Tapped"))
                .Select(_ => Unit.Default) // Explain why this isn't TappedEventArgs
                .InvokeCommand(this, x => x.ViewModel.Cancel);

            Activity
                .ClickGesture
                .Events()
                .Clicked
                .Do(_ => this.Log().Debug("Activity Clicked"))
                .Select(_ => Unit.Default)// Explain why this isn't TappedEventArgs
                .InvokeCommand(this, x => x.ViewModel.Cancel);

            Interactions.AlertInteraction.RegisterHandler(DisplayAlert);
        }

        private async Task DisplayAlert(InteractionContext<(string, string, string), Unit> context)
        {
            await DisplayAlert(context.Input.Item1, context.Input.Item2, context.Input.Item3).ConfigureAwait(false);
            context.SetOutput(Unit.Default);
        }
    }
}