using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProiectMIP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderContentView : ContentView
    {
        public HeaderContentView()
        {
            InitializeComponent();
            refresh_header();
        }

      

        private void refresh_header()
        {
            var second = TimeSpan.FromSeconds(1);
            Device.StartTimer(second, () => {
            account_image.Source = Preferences.Get("ProfilePhotoFolder", "account_large.png");
            account_name.Text = Preferences.Get("numele", "Nume");
            account_surename.Text = Preferences.Get("prenumele", "Prenume");
            return true;
            });
        }
    }
}