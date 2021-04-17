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
    public partial class Grades : ContentPage
    {
        string grades = "Grades";
        string universityYear = "UniversityYear";
        string yearOfStudy = "YearOfStudy";

        public Grades()
        {
            InitializeComponent();
            SetupElements();
        }
        

        private void Button_Clicked(object sender, EventArgs e)
        {
            SaveAllData();
        }


        private void SetupElements()
        {
            const int total_rows = 102;
            const int total_columns = 5;

            Entry_UniversityYear.Text = Preferences.Get($"{universityYear}", "20 ......./20 .......");
            Entry_UniversityYearOfStudy.Text = Preferences.Get($"{yearOfStudy}", "...........");


            for (int row = 2; row < total_rows; row++)
            {
                for (int column = 0; column < total_columns; column++)
                {   
                    if( column == 0)
                    {
                        SetupFirstColumn(row, column);
                    }
                    else if (column == 4)
                    {
                        SetupLastColumn(row, column);
                    }
                    else
                    {
                        SetupNormalColumn(row, column);
                    }


                }

            }

        }

        private void SaveAllData()
        {
            foreach(View gridChild in  grid_Grades.Children) { 

                if (gridChild is Frame frame)
                {
                    if(frame.Content is Entry entry)
                    {
                        Preferences.Set(entry.StyleId, entry.Text);
                    }
                }
            }
        }

        private void SetupNormalColumn(int row, int column)
        {
            string returnedValue = Preferences.Get($"Entry{grades}{row}{column}", "");
            

            grid_Grades.Children.Add(new Frame
            {
                Padding = 0,
                Content = new Entry 
                { 
                    Text = $"{returnedValue}",
                    StyleId = $"Entry{grades}{row}{column}",
                }
            }, column, row);
        }

        private void SetupLastColumn(int row, int column)
        {
            string returnedValue = Preferences.Get($"Entry{grades}{row}{column}", "");
            grid_Grades.Children.Add(new Frame
            {
                Padding = 0,
                Content = new Entry
                {
                    Text = $"{returnedValue}",
                    Placeholder = $"{row - 1}.",
                    HorizontalTextAlignment = TextAlignment.End,
                    VerticalTextAlignment = TextAlignment.Center,
                    StyleId = $"Entry{grades}{row}{column}",
                }        
            }, column, row);
        }
                
        

        private void SetupFirstColumn(int row, int column)
        {
            string returnedValue = Preferences.Get($"Entry{grades}{row}{column}", "");
            grid_Grades.Children.Add(new Frame
            {
                Padding = 0,
                Content = new Entry
                { 
                    Text = $"{returnedValue}",
                    Placeholder = $"{row - 1}.",
                    VerticalTextAlignment = TextAlignment.Center,
                    StyleId = $"Entry{grades}{row}{column}",
                }
            }, column, row);

        }

        private void UniversityYearTextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set($"{universityYear}", e.NewTextValue);
        }
        
        private void UniversityYearOfStudyTextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set($"{yearOfStudy}", e.NewTextValue);
        }

    }
}