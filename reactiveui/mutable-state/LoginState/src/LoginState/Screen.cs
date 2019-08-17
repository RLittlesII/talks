using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms;

namespace LoginState
{
    internal class Screen : ReactiveObject, IScreen
    {
        public RoutingState Router { get; }

        public Screen()
        {
            Router = new RoutingState();

            var viewModel = Locator.Current.GetService<LoginViewModel>();

            Router
                .NavigateAndReset
                .Execute(viewModel)
                .Subscribe();
        }

        public Page PresentDefaultView() => new RoutedViewHost();
    }
}
