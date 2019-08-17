using LoginState.Services;
using ReactiveUI;
using Splat;

namespace LoginState
{
    public class Composition
    {
        private static readonly IMutableDependencyResolver _registrar = Locator.CurrentMutable;

        public static void RegisterViews()
        {
            _registrar.RegisterConstant<IScreen>(new Screen());

            _registrar.Register<IViewFor<LoginViewModel>>(() => new LoginPage());
        }

        public static void RegisterServices()
        {
            _registrar.Register<IAuthenticator>(() => new Authenticator());
        }

        public static void RegisterViewModels()
        {
            _registrar.Register(() => new LoginViewModel(Locator.Current.GetService<IAuthenticator>()));
        }

        public static void RegisterPlatform() { }

        public static void Compose()
        {
            RegisterServices();
            RegisterPlatform();
            RegisterViewModels();
            RegisterViews();
        }
    }
}