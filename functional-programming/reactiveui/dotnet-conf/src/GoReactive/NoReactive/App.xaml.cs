using NoReactive.Load;
using Splat;
using Xamarin.Forms;

namespace NoReactive
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var startup = new Startup(new ModernDependencyResolver());

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
