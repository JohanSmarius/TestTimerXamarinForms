using System;
using System.Collections.Generic;
using TestTimer.ViewModels;
using TestTimer.Views;
using Xamarin.Forms;

namespace TestTimer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}

