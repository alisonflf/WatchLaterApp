using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WatchLaterApp
{
	public partial class MainPage : ContentPage
	{

        WebRequest request;
        MovieList movielist;
        String apiKey = "4277f0776300025895ed7999e22fb605";
        String language = "pt-BR";

        public MainPage()
		{
			InitializeComponent();

            if (Application.Current.Properties.ContainsKey("texto"))
            {
                entry.Text = Application.Current.Properties["texto"].ToString();
            }

            loading.IsRunning = false;
            loading.IsVisible = false;
        }

        protected void OnSleep()
        {
            SalvarTexto();
        }

        void Entry_Completed(object sender, EventArgs e)
        {
            loading.IsRunning = true;
            loading.IsVisible = true;
            var searchbartext = ((SearchBar)sender).Text;
            Uri uri = new Uri("https://api.themoviedb.org/3/search/movie?page=1&query=" +
                searchbartext + "&api_key=" + apiKey + "&language=" + language);
            request = WebRequest.Create(uri);
            request.BeginGetResponse(HandleAsyncCallback, null);
        }

        private void View_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new MovieDetailPage(e.SelectedItem));
        }

        public void SalvarTexto()
        {
            Application.Current.Properties["texto"] = entry.Text;
        }

        void HandleAsyncCallback(IAsyncResult result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    Stream stream = request.EndGetResponse(result).GetResponseStream();
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MovieList));
                    movielist = (MovieList)serializer.ReadObject(stream);
                    list.ItemsSource = movielist.results;
                    loading.IsRunning = false;
                    loading.IsVisible = false;
                }
                catch (Exception e)
                {
                    loading.IsRunning = false;
                    loading.IsVisible = false;
                }
            });
        }
    }
}
