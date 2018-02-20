using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WatchLaterApp
{
    [DataContract]
    class MovieList
    {
        [DataMember]
        public List<Movie> results { get; set; }
    }
}
