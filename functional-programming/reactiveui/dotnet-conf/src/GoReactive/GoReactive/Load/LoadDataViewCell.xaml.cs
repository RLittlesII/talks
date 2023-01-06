using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoReactive.Load
{
    public partial class LoadDataViewCell : ReactiveViewCell<LoadDataItemViewModel>
    {
        public LoadDataViewCell()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Name, x => x.Customer.Text);

            this.OneWayBind(ViewModel, x => x.Ordered, x => x.Drink.Text);

            this.OneWayBind(ViewModel, x => x.Size, x => x.Size.Text);
        }
    }
}