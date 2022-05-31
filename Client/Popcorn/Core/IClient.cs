using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCinematograf {
    public interface IClient {

        //  OBLIGATORIU, primul lucru de facut
        //  Porneste ocnexiunea cu serverul
        public bool StartCommunication();

        // Creezi o clasa de Movie
        // Setezi toate campurile care te intereseaza pentru cautare
        // Apoi transmiti obiectul ca aprametru functiei.
        // Aceasta va returna o lista de filme ce respecta tempaltul dat de tine
        public List<Movie> GetMovie(Movie movieTemplate);

        public List<string> GetAllCategories();

        public List<string> ReserverPlace(Movie movie);

        //byte Login(string username, string password);


        ////filmul, data&ora, numarul locului -> face rezervare
        ////trimite mail cu biletul cu care se prezinta la cinema
        //byte Reserve(string movie, string date, int placeNo, string clientName, string clientEmail);

        ////trimite datele la server care trimite inapoi raspuns SUCCES/FAILED
        ////va fi mereu SUCCES, nu folosim plata pe bune
        ////doar simulam
        //byte Pay(string clientName, string cardNumber, string controlValue, int value);

        ////CELE 2 FUNCTII DE MAI SUS AR PUTEA FI SI INTEGRATE IN ACEEASI! ^


        ////codul e identificatorul unic din abza de date. E trimis prin email la rezervare 
        ////client name poate fi scos ?
        //byte DeleteReservation(string rezervationCode, string clientName);

        ////primeste data dorita ca parametru
        ////returneaza o lista de filme cu toate detalile acestora
        ////tipul 
        ////gen
        //List<Movie> GetMoviesFromDate(string date);

        ////primeste numele filmului ca parametru
        ////returneaza totate detaliile despre el
        //Movie GetMovieDetails(string movie);

    }
}
