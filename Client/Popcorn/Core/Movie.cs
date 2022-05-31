﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ClientCinematograf {


    public class MovieGenre {

        public static string Drama = "drama";
        public static string Comedie = "comedie";
        public static string Groaza = "groaza";
    }

    public class Movie {
      

        public string m_name = "none";
        public string m_description = "none";
        public List<string> m_genre = new List<string>();
        public float m_duration = 0;
        public int m_minimumAge = 0;
        public float m_rating = 0;
        public string m_type = "none"; //2d,3d,etc
        public Image m_image = null;
        
        //de forma YY.MM.ZZ
        //public List<MovieAvailability> m_availableDates = new List<MovieAvailability>(); //lista cu datele la care e valabil in cinema din ziua prezenta + 7zile
        public List<string> m_availableDates = new List<string>();

        public string m_poza = "none";


        //public float m_price = 0;
        //o imagine



    }

    public class MovieAvailability {

        public string m_date;
        public List<string> m_availableHours = new List<string>();
    }



    //fixa
    //void List<Movie> GetMoviesFromDatabase(Movie movieTemplate);


    //cum vrei tu fra
    // List<int> GetFreePlacesForMovie(string numeMovie, string ora);

    //void RezervaLoc(string numeFilm, string ora, int loc);
    //void StergeRezervareLoc();


}
