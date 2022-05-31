using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing; //DE INSTALAT NuGet System.Drawin.Common

namespace ClientCinematograf {
    class CClient : IClient{

        private string pathBase = "D:\\ATM\\BazeDate\\vsProjects\\MultiThread_ClientServer\\BD App\\old\\Popcorn\\Popcorn\\bin\\Debug\\netcoreapp3.1\\images\\";
        public bool StartCommunication() {

            return MyConnection.Instance.ConnectToServer();
        }
        public List<Movie> GetMovie(Movie movieTemplate) {

            //trimitem mesaj de cerere lista de filme
            Message message = new Message();
            message.Construct(Protocol.MovieList, movieTemplate);
            MyConnection.Instance.SendToServer(message);

            //primim raspuns care ne zice cate filme vom primi o continuare
            Message response = MyConnection.Instance.GetFromServer();
            if (response.m_instruction != Protocol.MovieDetails) {

                Console.WriteLine("ERROR");
                SendFailMessageToServer();
                return new List<Movie>();
            }
            else
                SendSuccesMessageToServer();

            //nr de filme
            int numberOfMovies = int.Parse(response.m_parameters[0]);

            //rpimim cate un film pe rand si il adaugam in lista
            //cu imaginile inca nu sunt hotarat ce sa fac. Momentan le stochez
            //as putea eventual sa adaug un camp la Movie care spune unde e stocata iamginea lui pentru vitoare citiri
            //sau sa fie direct un camp de tip Image
            string filename = "poza.jpg";
            List<Movie> movieList = new List<Movie>();
            for(int i = 0; i < numberOfMovies; i++) {

                //preia detalii despre un film
                response = MyConnection.Instance.GetFromServer();
                if (response.m_instruction == Protocol.MovieDetails)
                    SendSuccesMessageToServer();
                else {
                    SendFailMessageToServer();
                    return new List<Movie>();
                }

                // creem obiect de tip Movie din mesajul primit
                // si il adaugam in lsita de returnat
                Movie movie = response.GetMovieFromMessage();
                movieList.Add(movie);

                //primim si poza asociata filmului
                byte[] responseBytes = MyConnection.Instance.GetFromServerBytes();
                MemoryStream ms = new MemoryStream(responseBytes);
                Image img;
                try {
                    //daca asta esuaeaza inseamna 
                    // ori ca serverul a trimis ceva gresit
                    // ori ca trimiterea a esuat
                    img = Image.FromStream(ms);
                }catch(Exception e) {

                    SendFailMessageToServer();
                    return new List<Movie>();
                };
                SendSuccesMessageToServer();

                //fa ceva cu iamginea
                filename = pathBase + movie.m_poza + ".jfif";
                img.Save(filename);
                movie.m_image = img;
                
            }


            //daca totul a mers bine, intoarce lista de filme
            return movieList;
        }

        public List<string> GetAllCategories() {

            Message message = new Message();
            message.Construct(Protocol.MovieCategories,"none");
            MyConnection.Instance.SendToServer(message);

            Message response = MyConnection.Instance.GetFromServer();
            //if (response.m_instruction != Protocol.MovieCategories) {

            //    Console.WriteLine("ERROR");
            //    SendFailMessageToServer();
            //    return new List<string>();
            //}
            //else
            //    SendSuccesMessageToServer();

            //nr de filme
            List<string> categories = new List<string>();
            foreach(var category in response.m_parameters) {
                categories.Add(category);
            }


            return categories;
        }


        public List<string> ReserverPlace(Movie movie) {

            Message message = new Message();
            message.Construct(Protocol.GetDatesOfMovies, movie.m_name);
            MyConnection.Instance.SendToServer(message);

            Message response = MyConnection.Instance.GetFromServer();

            //aici ar trebui afisate datele in interfata grtafica
            //nu avem suport grafic

            return response.m_parameters;
        }





        private void SendSuccesMessageToServer() {

            Message okay = new Message();
            okay.Construct(Protocol.SUCCES, "da");

            MyConnection.Instance.SendToServer(okay);
        }
        private void SendFailMessageToServer() {

            Message notOkay = new Message();
            notOkay.Construct(Protocol.FAILED, "nu");

            MyConnection.Instance.SendToServer(notOkay);
        }


        //public byte Login(string username, string password) {

        //    Message message = new Message();
        //    message.Construct(Protocol.Login, username, password);

        //    MyConnection.Instance.SendToServer(message);

        //    //primeste mesaj cu SUCCED/FAILED
        //    message = MyConnection.Instance.GetFromServer();
        //    return message.m_instruction;
        //}
    }
}
