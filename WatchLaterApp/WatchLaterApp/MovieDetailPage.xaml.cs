using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchLaterApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieDetailPage : ContentPage
	{
        Movie selectedMovie;

        public MovieDetailPage (object SelectedItem )
		{
			InitializeComponent ();

            selectedMovie = (Movie)SelectedItem;
            
            label.Text = selectedMovie.title;
            poster.Source = "https://image.tmdb.org/t/p/w500" + selectedMovie.poster_path;
            vote.Text = selectedMovie.vote_average;
		}

        void Handle_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}