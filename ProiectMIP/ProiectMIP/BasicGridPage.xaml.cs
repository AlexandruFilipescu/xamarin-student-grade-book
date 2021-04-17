using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace ProiectMIP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasicGridPage : ContentPage
    {
        protected override bool OnBackButtonPressed(){
            new NavigationPage(new BasicGridPage());
            return false;    
        }
        protected override void OnDisappearing(){
            new NavigationPage(new BasicGridPage());
        }
        protected override void OnBindingContextChanged(){}

        const string UNIVERSITY = "universitatea";
        const string REGISTRATION_NUMBER = "numar_matricol";
        const string NAME = "numele";
        const string PRENAME = "prenumele";
        const string FACULTY = "facultatea";
        const string SPECIALIZATION = "specializarea";
        const string RELEASE_DATE = "data_eliberare";
        string IMAGE_PROFILE = "ProfilePhotoFolder";
        string IMAGE_SIGNATURE = "SignaturePhotoFolder";
        

        public BasicGridPage()
        {
            InitializeComponent();
            InitializeForm();
            ElementEvents();
            btnCaptureProfile.Clicked += BtnCaptureProfile_Clicked;
            btnGalleryProfile.Clicked += BtnGalleryProfile_Clicked;
            btnCaptureSignature.Clicked += BtnCaptureSignature_Clicked;
            btnGallerySignature.Clicked += BtnGallerySignature_Clicked;

        }



        private async void BtnGallerySignature_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Not supported", "Your device does not currently support this functionality", "Ok");
                    return;
                }
                var mediaOptions = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (imageProfile == null)
                {
                    await DisplayAlert("Error", "Could not get the image, please try again.", "Ok");
                    return;
                }
                imageSignature.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
                Preferences.Set(IMAGE_SIGNATURE, selectedImageFile.Path);
            }
            catch
            {
                imageProfile.Source = Preferences.Get(IMAGE_PROFILE, " ");
                
            }
        }

        private async void BtnCaptureSignature_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Imagine Semnatura",
                    Name = "Semnatura.jpg"
                });
                var Path = file.AlbumPath;
                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                imageSignature.Source = ImageSource.FromStream(() =>
                {
                    Preferences.Set(IMAGE_SIGNATURE, Path);
                    imageSignature.Source = Path;
                    var stream = file.GetStream();
                    return stream;
                });
            }
            catch (Exception ex)
            {

            }
        }

        private async void BtnGalleryProfile_Clicked(object sender, EventArgs e)
        {
            try
            {        
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not supported", "Your device does not currently support this functionality", "Ok");
                return;
            }
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            
            if (imageProfile == null)
            {
                await DisplayAlert("Error", "Could not get the image, please try again.", "Ok");
                return;
            }  
            imageProfile.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
            Preferences.Set(IMAGE_PROFILE, selectedImageFile.Path);
            } catch
            {
                imageProfile.Source = Preferences.Get(IMAGE_PROFILE, " ");
            }
        }
        private async void BtnCaptureProfile_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Imagine Profil",
                    Name = "Profil.jpg"
                });
                var Path = file.AlbumPath;
                if (file == null)
                    return;
                await DisplayAlert("File Location", file.Path, "OK");

                imageProfile.Source = ImageSource.FromStream(() =>
                {
                    Preferences.Set(IMAGE_PROFILE,Path);
                    imageProfile.Source = Path;
                    var stream = file.GetStream();
                    return stream;
                });
            }
            catch (Exception ex)
            {
                
            }
        }

        private void ElementEvents()
        {
            university.TextChanged += Universitatea_TextChanged;
            registrationNumber.TextChanged += NumarMatricol_TextChanged;
            name.TextChanged += Numele_TextChanged;
            preName.TextChanged += Prenumele_TextChanged;
            faculty.TextChanged += Faculatea_TextChanged;
            specialization.TextChanged += Specializarea_TextChanged;
            releaseDate.DateSelected += DataEliberare_DateSelected;
        }

        private async void InitializeForm()
        {
            try {
                var registrationNumberSecure = await SecureStorage.GetAsync(REGISTRATION_NUMBER);
                registrationNumber.Text = registrationNumberSecure;
            } catch (Exception ex) {

            }     
            university.Text = Preferences.Get(UNIVERSITY, "Universitatea"); 
            name.Text = Preferences.Get(NAME, " ");
            preName.Text = Preferences.Get(PRENAME, " ");
            faculty.Text = Preferences.Get(FACULTY, " ");
            specialization.Text = Preferences.Get(SPECIALIZATION, " ");
            releaseDate.Date = Preferences.Get(RELEASE_DATE, DateTime.Now);
            imageProfile.Source = Preferences.Get(IMAGE_PROFILE, " ");
            imageSignature.Source = Preferences.Get(IMAGE_SIGNATURE, " ");
            
        }

        private void DataEliberare_DateSelected(object sender, DateChangedEventArgs e)
        {
            var enteredDate = e.NewDate;
            Preferences.Set(RELEASE_DATE, enteredDate);
        }

        private void Universitatea_TextChanged(object sender, TextChangedEventArgs e)
        {
            var enteredText = e.NewTextValue;
            Preferences.Set(UNIVERSITY, enteredText);
        }

        private async void NumarMatricol_TextChanged(object sender, TextChangedEventArgs e)
        {
            var enteredText = ((Entry)sender).Text;
            try {
                await SecureStorage.SetAsync(REGISTRATION_NUMBER, enteredText);
            } catch (Exception ex){

            }
        }

        private void Numele_TextChanged(object sender, TextChangedEventArgs e)
        {
            var enteredText = ((Entry)sender).Text;
            Preferences.Set(NAME, enteredText);
        }


        private void Prenumele_TextChanged(object sender, TextChangedEventArgs e)
        {
            var enteredText = ((Entry)sender).Text;
            Preferences.Set(PRENAME, enteredText);
        }

        private void Faculatea_TextChanged(object sender, TextChangedEventArgs e)
        {
            var enteredText = ((Entry)sender).Text;
            Preferences.Set(FACULTY, enteredText);
        }

        private void Specializarea_TextChanged(object sender, TextChangedEventArgs e)
        {
            var enteredText = ((Entry)sender).Text;
            Preferences.Set(SPECIALIZATION, enteredText);
        }


    }
}