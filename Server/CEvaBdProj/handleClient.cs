using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.IO;
using System.Drawing;

using FinalDbCinemaProject;
using CEvaBdProj;

namespace Server {
    public class handleClinet {
        
        

        TcpClient m_clientSocket;
        string m_clientNo;

        NetworkStream m_dataStream;
        const int m_streamSize = Protocol.MESSAGE_SIZE;

        CinemaSite cinemaDB;

        int idProgramareLastAccesed = -1;

        public void startClient(TcpClient inClientSocket, string clineNo) {

            Marcel _db = new Marcel();
            cinemaDB =  new CinemaSite(_db);

            this.m_clientSocket = inClientSocket;
            this.m_clientNo = clineNo;
            m_clientSocket.ReceiveBufferSize = m_streamSize;

            m_dataStream = m_clientSocket.GetStream();

            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }
        private void doChat() {

            Message message;

            while ((true)) {
                try {
                    message = GetFromClient();
                    message.Print();

                    SelectInstruction(message);
                }
                catch (Exception ex) {
                    //Console.WriteLine(" >> " + ex.ToString());

                    Console.WriteLine(" >> Close connection with client number " + m_clientNo);
                    if(m_clientSocket != null) {
                        m_dataStream.Close();
                        m_clientSocket.Close();
                        m_clientSocket = null;
                    }
                    return;
                }
            }
        }

        private void SelectInstruction(Message message) {

            //pe baza primului byte din mesaj, apelam functia corespunzatoare
            byte option = message.m_instruction;

            switch (option) {
                case Protocol.Login:
                    CheckLogin(message);
                    break;
                case Protocol.MovieList:
                    DeliverMovieList(message);
                    break;
                case Protocol.MovieCategories:
                    DeliverMovieCategories();
                    break;
                case Protocol.GetDatesOfMovies:
                    DeliverMovieDates(message);

                    break;
                default:
                    break;
            }  
        }

        private void DeliverMovieDates(Message message) {

            string numeFilm = message.m_parameters[0];
            string date = "";
            if (message.m_parameters.Count > 1)
                date = message.m_parameters[1];

            List<ProgramariFilme> programariFilme;


            programariFilme = cinemaDB.GetProgramariFilme(numeFilm);

            int locRezervat = 1;
            string dataRezervat = "";
            int salaRezervat = -1;
            foreach(var programare in programariFilme) {

                locRezervat = cinemaDB.RezervaLoc(programare.IdProgramare);
                if (locRezervat != -1) {
                    dataRezervat = programare.Start.ToString();
                    salaRezervat = programare.IdSala;
                    break;
                }
            }

            Message response = new Message();
            response.Construct(Protocol.GetDatesOfMovies, salaRezervat.ToString(), locRezervat.ToString(), dataRezervat);
            SendToClient(response);
        }

        private void DeliverMovieList(Message message) {


            Movie movie = message.GetMovieFromMessage();

            List<Filme> moviesFromDatabase = new List<Filme>();
            if (movie.m_genre.Count > 0)
                moviesFromDatabase = cinemaDB.FilmeDupaGen(movie.m_genre[0]);
            else
                moviesFromDatabase = cinemaDB.GetAllMovies();

           
            Message movieDetails = new Message();
            movieDetails.Construct(Protocol.MovieDetails, moviesFromDatabase.Count.ToString());
            SendToClient(movieDetails);

            GetFromClient();

            foreach (var currentMovie in moviesFromDatabase) {

                movie = new Movie();

                movie.m_name = currentMovie.Nume;
                movie.m_description = currentMovie.Descriere;
                if (movie.m_description == null)
                    movie.m_description = "none";
                movie.m_duration = 123f;
                movie.m_poza = currentMovie.Poze;

                movieDetails.Construct(Protocol.MovieDetails, movie);
                SendToClient(movieDetails);
                GetFromClient();

                SendImageToClient("images\\" +  currentMovie.Poze + ".jfif");
                GetFromClient();

            }

        }

        private void DeliverMovieCategories() {

            Message response = new Message();
            response.m_instruction = Protocol.MovieCategories;

            response.m_parameters = cinemaDB.getGenuri();

            //response.m_parameters.Add("actiune");
            //response.m_parameters.Add("fantezie");

            SendToClient(response);
        }

        private void CheckLogin(Message message) {

            string username = "user";
            string password = "pass";
            //INSERT FUNCTIE DIN BAZA DE DATE CARE PREIA USERNAME SI PAROLA
            //functia va actualiza valorile din username si parola

            Message response = new Message();
            if (message.m_parameters[0] == username && message.m_parameters[1] == password)
                response.Construct(Protocol.SUCCES);
            else
                response.Construct(Protocol.FAILED);

            SendToClient(response);
        }



        //trimite la client!
        private void SendToClient(Message message) {

            if (!m_dataStream.CanWrite) {

                //log error message
                return;
            }
            byte[] bytes = Protocol.ConvertToBytes(message);
            m_dataStream.Write(bytes, 0, bytes.Length);
            m_dataStream.Flush();

            Console.WriteLine(">> Server sent message to client number " + m_clientNo);
        }

        //trimite poza la client (se da numele pozei ca aprametru + path daca e neevoie)
        private void SendImageToClient(string imageName) {

            //citim imaginea de pe disk
            Image img = Image.FromFile(imageName);

            // o transpunem intr-un vector de bytes
            MemoryStream ms = new MemoryStream();
            img.Save(ms, img.RawFormat);
            byte[] imgBytes = ms.ToArray();

            m_dataStream.Write(imgBytes, 0, imgBytes.Length);
            m_dataStream.Flush();
        }
        
        //primeste mesaj de la client
        public Message GetFromClient() {

            byte[] bytes = new byte[m_streamSize];
            if (!m_dataStream.CanRead) {

                //log error message
                //return ; //CREAZA O CLASA DERVIATA DIN MESSAGe -> message_error;
            }
            m_dataStream.Read(bytes, 0, bytes.Length);
            Console.WriteLine(">> Server got message from client number " + m_clientNo);
            return Protocol.ConvertFromBytes(bytes);
        }


        bool SocketConnected(Socket s) {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

    }
}
