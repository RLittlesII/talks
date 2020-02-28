using System;
using GoReactive.Load;
using GoReactive.Search;
using Sextant.XamForms;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoReactive
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var startup = new Startup();

            MainPage = startup.NavigateToStart<LoadDataViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
