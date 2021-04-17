using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ProiectMIP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Annual_Visas : ContentPage
    {
        const string visa1String = "Visa1";
        const string visa2String = "Visa2";
        const string visa3String = "Visa3";
        const string visa4String = "Visa4";
        const string visa5String = "Visa5";
        const string visa6String = "Visa6";
        const string visa7String = "Visa7";
        const string visa8String = "Visa8";

        public Annual_Visas()
        {
            InitializeComponent();
            Setup_VisasText();
            EventHandlersTextChanged();
        }

        

        private void Visa1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(visa1String, e.NewTextValue);
        } 

        private void Visa2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(visa2String, e.NewTextValue);
        } 

        private void Visa3_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(visa3String, e.NewTextValue);
        } 
        
        private void Visa4_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(visa4String, e.NewTextValue);
        } 

        private void Visa5_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(visa5String, e.NewTextValue);
        } 

        private void Visa6_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(visa6String, e.NewTextValue);
        }

        private void Visa7_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(visa7String, e.NewTextValue);
        }

        private void Visa8_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(visa8String, e.NewTextValue);
        }


        private void Setup_VisasText()
        {
            Visa1.Text = Preferences.Get(visa1String, "20 ...... / 20 ......");
            Visa2.Text = Preferences.Get(visa2String, "20 ...... / 20 ......");
            Visa3.Text = Preferences.Get(visa3String, "20 ...... / 20 ......");
            Visa4.Text = Preferences.Get(visa4String, "20 ...... / 20 ......");
            Visa5.Text = Preferences.Get(visa5String, "20 ...... / 20 ......");
            Visa6.Text = Preferences.Get(visa6String, "20 ...... / 20 ......");
            Visa7.Text = Preferences.Get(visa7String, "20 ...... / 20 ......");
            Visa8.Text = Preferences.Get(visa8String, "20 ...... / 20 ......");
        }

        private void EventHandlersTextChanged()
        {
            Visa1.TextChanged += Visa1_TextChanged;
            Visa2.TextChanged += Visa2_TextChanged;
            Visa3.TextChanged += Visa3_TextChanged;
            Visa4.TextChanged += Visa4_TextChanged;
            Visa5.TextChanged += Visa5_TextChanged;
            Visa6.TextChanged += Visa6_TextChanged;
            Visa7.TextChanged += Visa7_TextChanged;
            Visa8.TextChanged += Visa8_TextChanged;
        }
    }
}