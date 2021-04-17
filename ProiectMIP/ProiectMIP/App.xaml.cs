using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProiectMIP
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            Plugin.Media.CrossMedia.Current.Initialize();
            //MainPage = new NavigationPage(new BasicGridPage());
            MainPage = new MainPage(); 
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
