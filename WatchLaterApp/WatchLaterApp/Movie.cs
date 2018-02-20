using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WatchLaterApp
{
    [DataContract]
    class Movie
    {
        [DataMember]
        public String title { get; set; }

        [DataMember]
        public String poster_path { get; set; }
    }
}
