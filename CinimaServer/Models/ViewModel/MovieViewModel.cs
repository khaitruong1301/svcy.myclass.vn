using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinimaServer.Models.ViewModel
{
    public class MovieViewModel
    {


        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string GenreName { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Producer { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public string TrailerURI { get; set; }
        public string Genre { get; set; }

    

    }
}