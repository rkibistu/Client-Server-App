using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server {

    public class Message {


        public Message() {

            m_parameters = new List<string>();
        }
        public byte m_instruction;
        public List<string> m_parameters;
        public void Construct(byte instruction, params string[] parameters) {

            m_instruction = instruction;

            m_parameters.Clear();
            foreach (string param in parameters) {

                m_parameters.Add(param);
            }
        }

        // creeaza mesaj cu toate datele despre un film
        public void Construct(byte instruction, Movie movie) {

            m_instruction = instruction;

            m_parameters.Clear();
            m_parameters.Add(movie.m_name);
            m_parameters.Add(movie.m_description);
            m_parameters.Add(movie.m_duration.ToString());
            m_parameters.Add(movie.m_minimumAge.ToString());
            m_parameters.Add(movie.m_rating.ToString());
            m_parameters.Add(movie.m_type);
            m_parameters.Add(movie.m_poza);

            int numberOfGenres = movie.m_genre.Count();
            m_parameters.Add(numberOfGenres.ToString());
            foreach (string genre in movie.m_genre)
                m_parameters.Add(genre);

            int numberOfDates = movie.m_availableDates.Count();
            m_parameters.Add(numberOfDates.ToString());
            foreach (string date in movie.m_availableDates)
                m_parameters.Add(date);

        }


        public void Construct(byte instruction, byte[] byteArray) {

            m_instruction = instruction;
            m_parameters.Add(Encoding.ASCII.GetString(byteArray));
        }

        // Cosntruieste un obiect de tip Movie,
        // plecand de la un mesaj ce contine datele despre un film
        // mesajul trebuie sa fie cosntruit cu funcita Construct specifica!
        public Movie GetMovieFromMessage() {

            Movie movie = new Movie();

            int index = 0;
            movie.m_name = this.m_parameters[index++];
            movie.m_description = this.m_parameters[index++];
            movie.m_duration = float.Parse(this.m_parameters[index++]);
            movie.m_minimumAge = int.Parse(this.m_parameters[index++]);
            movie.m_rating = float.Parse(this.m_parameters[index++]);
            movie.m_type = this.m_parameters[index++];

            int numberOfGenres = int.Parse(this.m_parameters[index++]);
            for (int i = 0; i < numberOfGenres; i++)
                movie.m_genre.Add(this.m_parameters[index++]);

            int numberOfDates = int.Parse(this.m_parameters[index++]);
            for (int i = 0; i < numberOfDates; i++)
                movie.m_availableDates.Add(this.m_parameters[index++]);

            return movie;
        }

        public void Print() {

            foreach (string param in m_parameters) {

                Console.Write(param + " ");
            }
            Console.WriteLine();
        }

    }
    public static class Protocol {

        //forma mesaj:
        //instructiune(1byte) numberOfParameters(1byte) lenhtParam1(1byte) param1(63bytes) lenghtParam2(1byte) param2(63bytes) ....

        //constante
        public const int MESSAGE_SIZE = 10025;
        public const int MESSAGE_IMAGE_SIZE = 3000000;
        private const int PARAMETER_SIZE = 64;

        //lista raspunsuri
        public const byte FAILED = 0;
        public const byte SUCCES = 1;

        //lista comenzi
        public const byte Login = 100;
        public const byte MovieDetails = 101;
        public const byte MovieList = 102;
        public const byte ReservePlace = 103;
        public const byte MovieCategories = 104;
        public const byte GetDatesOfMovies = 105;

        public const byte ImageNotEnded = 200;
        public const byte ImageEnded = 201;


        static public byte[] ConvertToBytes(Message message) {

            byte[] bytes = new byte[MESSAGE_SIZE];
            int index = 0;
            bytes[index++] = message.m_instruction;

            bytes[index++] = BitConverter.GetBytes(message.m_parameters.Count)[0];

            byte[] tempBytes;
            foreach (string parametru in message.m_parameters) {

                tempBytes = System.Text.Encoding.ASCII.GetBytes(parametru);
                bytes[index++] = BitConverter.GetBytes(tempBytes.Length)[0]; //aici putem cu [0] pt ca stim ca nici un parametru nu e mai lung de 255
                Array.Copy(tempBytes, 0, bytes, index, tempBytes.Length);
                index += PARAMETER_SIZE - 1;
            }

            return bytes;
        }
        static public Message ConvertFromBytes(byte[] bytes) {

            int index = 0;

            Message response = new Message();
            response.m_instruction = bytes[index++]; //preluam instructiunea/raspunsul

            int numberOfParameters = bytes[index++]; //vedem cati parametrii s au trimis
            for (int i = 0; i < numberOfParameters; i++) {

                int parameterLenght = bytes[index++]; //luam lungimea in bytes a parametruui curent
                byte[] tempBytes = new byte[parameterLenght]; // ne declaram un vector temporar de bytes
                Array.Copy(bytes, index, tempBytes, 0, parameterLenght); //copiem in el parametrul curent
                string tempString = System.Text.Encoding.ASCII.GetString(tempBytes); // il convertmin la string
                response.m_parameters.Add(tempString); //adaugam stringul la raspuns (clasa mesaj)

                index += PARAMETER_SIZE - 1; //crestem indexul (scadem 1 pentru ca am adunat 1 cand am preluat parameterLenght)
            }

            return response;
        }
    }
}
