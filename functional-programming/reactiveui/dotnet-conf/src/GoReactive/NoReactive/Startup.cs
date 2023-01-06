using System;
using GoReactive.Services;
using NoReactive.Load;
using ReactiveUI;
using Refit;
using Sextant;
using Sextant.Abstractions;
using Sextant.XamForms;
using Splat;
using Xamarin.Forms;

namespace NoReactive
{
    public class Startup
    {
        public Startup(IDependencyResolver dependencyResolver)
        {
            RegisterServices(dependencyResolver);
            RegisterViewModels(dependencyResolver);
            RegisterViews(dependencyResolver);
            Build(dependencyResolver);
        }

        private void Build(IDependencyResolver dependencyResolver) =>
            Locator.SetLocator(dependencyResolver);

        private void RegisterViews(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterView<LoadData, LoadDataViewModel>();
        }

        private void RegisterViewModels(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterViewModel(new LoadDataViewModel());
        }

        private void RegisterServices(IDependencyResolver dependencyResolver)
        {
            var navigationView = new NavigationView();
            dependencyResolver.RegisterNavigationView(() => navigationView);
            dependencyResolver.RegisterLazySingleton<IParameterViewStackService>(() => new ParameterViewStackService(navigationView));
            dependencyResolver.RegisterLazySingleton<IViewModelFactory>(() => new DefaultViewModelFactory());
            dependencyResolver.InitializeReactiveUI();

            dependencyResolver.RegisterLazySingleton<IDuckDuckGoApi>(() => RestService.For<IDuckDuckGoApi>("https://api.duckduckgo.com"));
        }

        public Page NavigateToStart<T>()
            where T : ViewModelBase
        {
            Locator.Current.GetService<IParameterViewStackService>().PushPage<T>().Subscribe();
            return Locator.Current.GetNavigationView(nameof(NavigationView));
        }
    }
}