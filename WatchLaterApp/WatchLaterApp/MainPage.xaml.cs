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
        String language = "pt=BR";

        public MainPage()
		{
			InitializeComponent();
		}

        void Entry_Completed(object sender, EventArgs e)
        {
            var entryText = ((Entry)sender).Text;
            label.Text = "buscando por ..." + entryText;
            Uri uri = new Uri("https://api.themoviedb.org/3/search/movie?page=1&query=" +
                entryText + "&api_key=" + apiKey + "&language=" + language);
            request = WebRequest.Create(uri);
            request.BeginGetResponse(HandleAsyncCallback, null);

        }

        void loadPicture(String poster_path)
        {
            UriImageSource imageSource = new UriImageSource();
            imageSource.Uri = new Uri("https://image.tmdb.org/t/p/w500" + poster_path);
            imageSource.CacheValidity = TimeSpan.FromDays(2);
            poster.Source = imageSource;
        }

        void Handle_Property_Changed(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLoading")
            {
                loading.IsRunning = poster.IsLoading;
            }
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

                    label.Text = movielist.results[0].title;
                    loadPicture(movielist.results[0].poster_path);
                }
                catch (Exception e)
                {
                    label.Text = "busca sem resultados";
                }
            });
        }
    }
}
