using System;
using NetworkFetch.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetworkFetch
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BreweryListView();
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
