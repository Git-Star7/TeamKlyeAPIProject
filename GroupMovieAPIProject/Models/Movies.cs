﻿using System;
using System.Collections.Generic;

namespace GroupMovieAPIProject.Models
{
    public partial class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Actors { get; set; }
        public string Director { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
