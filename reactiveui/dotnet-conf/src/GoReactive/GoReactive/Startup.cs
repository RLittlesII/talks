using System;
using GoReactive.Load;
using GoReactive.Search;
using GoReactive.Services;
using GoReactive.Services.Client;
using GoReactive.Services.Orders;
using GoReactive.Services.SignalR;
using ReactiveUI;
using Refit;
using Sextant;
using Sextant.Abstractions;
using Sextant.XamForms;
using Splat;
using Xamarin.Forms;

namespace GoReactive
{
    public class Startup
    {
        public Startup(IDependencyResolver dependencyResolver = null)
        {
            if (dependencyResolver == null)
            {
                dependencyResolver = new ModernDependencyResolver();
            }

            RxApp.DefaultExceptionHandler = new GoReactiveExceptions();
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
            dependencyResolver.RegisterView<SearchList, SearchListViewModel>();
        }

        private void RegisterViewModels(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterViewModel(new LoadDataViewModel(dependencyResolver.GetService<IOrderService>()));
            dependencyResolver.RegisterViewModel(new SearchListViewModel(dependencyResolver.GetService<IDuckDuckGoService>()));
        }

        private void RegisterServices(IDependencyResolver dependencyResolver)
        {
            var navigationView = new NavigationView();
            dependencyResolver.RegisterNavigationView(() => navigationView);
            dependencyResolver.RegisterLazySingleton<IParameterViewStackService>(() => new ParameterViewStackService(navigationView));
            dependencyResolver.RegisterLazySingleton<IViewModelFactory>(() => new DefaultViewModelFactory());
            dependencyResolver.InitializeReactiveUI();

            dependencyResolver.RegisterLazySingleton<IDuckDuckGoApi>(() => RestService.For<IDuckDuckGoApi>("https://api.duckduckgo.com"));
            dependencyResolver.RegisterLazySingleton<IDuckDuckGoService>(() => new DuckDuckGoService(dependencyResolver.GetService<IDuckDuckGoApi>()));
            dependencyResolver.RegisterLazySingleton<IHubClient>(() => new HubClientMock());
            dependencyResolver.RegisterLazySingleton<IOrderClient>(() => new OrderClientMock());
            dependencyResolver.RegisterLazySingleton<IOrderService>(() => new OrderService(dependencyResolver.GetService<IOrderClient>()));
        }

        public Page NavigateToStart<T>()
            where T : ViewModelBase
        {
            Locator.Current.GetService<IParameterViewStackService>().PushPage<T>().Subscribe();
            return Locator.Current.GetNavigationView(nameof(NavigationView));
        }
    }
}