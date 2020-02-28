using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace NoReactive.Search
{
    public partial class SearchList : ContentPageBase<SearchListViewModel>
    {
        public SearchList()
        {
            InitializeComponent();

            SearchBar.TextChanged += (sender, args) => { }; // This immediately creates a memory leak

            SearchBar.TextChanged += SearchBarOnTextChanged; // Better but you need to be sure to deallocate on disposal.
        }

        private void SearchBarOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var model = BindingContext as SearchListViewModel;
            // This will execute for every single text change event
            // How can we time shift this?  Delay processing with some sort of buffer 
            model?.Search.Execute(e.NewTextValue);
        }
    }
}
