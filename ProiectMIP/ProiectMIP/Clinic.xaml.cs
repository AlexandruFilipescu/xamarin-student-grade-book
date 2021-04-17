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
    public partial class Clinic : ContentPage
    {
        string clinic = "Clinic";
        string entryClinic = "EntryClinic";
        public Clinic()
        {
            InitializeComponent();
            SetupStartingText();
            EntryClinic.TextChanged += EntryClinic_TextChanged;
            Setup_Rows();
        }

        private void Setup_Rows()
        {
            const int total_rows = 12;
            const int total_columns = 4;

            for (int row = 3; row < total_rows; row++)
            {
                for (int column = 0; column < total_columns; column++)
                {
                    Setup_Normal_Cell(row, column);
                }
            }
        }

        private void Setup_Normal_Cell(int row, int column)
        {
            string returnedValue = Preferences.Get($"Entry{clinic}{row}{column}", "");
            grid_clinic.Children.Add(new Frame
            {
                Padding = 0,
                Content = new Entry
                {
                    Text = $"{returnedValue}",
                    StyleId = $"Entry{clinic}{row}{column}",
                }
            }, column, row);
        }

        private void SaveAllData()
        {
            foreach (View gridChild in grid_clinic.Children)
            {

                if (gridChild is Frame frame)
                {
                    if (frame.Content is Entry entry)
                    {
                        Preferences.Set(entry.StyleId, entry.Text);
                    }
                }
            }
        }

        private void SetupStartingText()
        {
            EntryClinic.Text = Preferences.Get(entryClinic, "Clinică nr....... Fișă T.B.C. nr.......");
        }

        private void EntryClinic_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(entryClinic, e.NewTextValue);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SaveAllData();
        }
    }
}