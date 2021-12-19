using System.ComponentModel;
using Xamarin.Forms;
using TestTimer.ViewModels;

namespace TestTimer.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
