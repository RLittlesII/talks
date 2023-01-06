using System.Threading.Tasks;

namespace NoReactive.Load
{
    public class LoadDataViewModel : ViewModelBase
    {
        public LoadDataViewModel()
        {
            OnInitialize();
        }

        public async void OnInitialize()
        {
            await Task.CompletedTask;
        }
    }
}
