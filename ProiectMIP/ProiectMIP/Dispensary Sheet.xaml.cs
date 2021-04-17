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
    public partial class Dispensary_Sheet : ContentPage
    {
        string dispensary = "Dispensary";
        string entryDispensary = "EntryDispensary";
        public Dispensary_Sheet()
        {
            InitializeComponent();
            SetupStartingText();
            EntryDispensary.TextChanged += EntryDispensary_TextChanged;
            Setup_Rows();
        }

        private void Setup_Rows()
        {
            const int total_rows = 9;
            const int total_columns = 4;

            for (int row = 2; row < total_rows; row++)
            {
                for (int column = 0; column < total_columns; column++)
                {
                    Setup_Normal_Cell(row,column);
                }
            }
        }


        private void Setup_Normal_Cell(int row, int column)
        {
            string returnedValue = Preferences.Get($"Entry{dispensary}{row}{column}", "");
            grid_Dispensary.Children.Add(new Frame
            {
                Padding = 0,
                Content = new Entry
                {
                    Text = $"{returnedValue}",
                    StyleId = $"Entry{dispensary}{row}{column}",
                }
            }, column, row);
        }

        private void SaveAllData()
        {
            foreach (View gridChild in grid_Dispensary.Children)
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
            EntryDispensary.Text = Preferences.Get(entryDispensary, "Fișă dispensar nr....... Fișă polic.......");
        }

        private void EntryDispensary_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set(entryDispensary, e.NewTextValue);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SaveAllData();
        }

    }
}