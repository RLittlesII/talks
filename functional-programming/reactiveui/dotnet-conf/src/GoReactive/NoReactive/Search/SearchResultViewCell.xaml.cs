using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoReactive.Search
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultViewCell : ReactiveViewCell<SearchResultViewModel>
    {
        public SearchResultViewCell()
        {
            InitializeComponent();
        }
    }

    public class SearchResultViewModel : ReactiveObject
    {
    }
}