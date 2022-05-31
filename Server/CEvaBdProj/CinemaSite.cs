using CEvaBdProj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FinalDbCinemaProject
{
    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Marcel _db = new Marcel();
    //        CinemaSite a = new CinemaSite(_db);

    //      var test= a.GetProgramariFilme("Venom");

    //        foreach (var aa in test)
    //        {
    //            Console.WriteLine(aa.Start);
    //        }
    //    }
    //}


    public class CinemaSite
    {
        public List<Filme> AllMovies = new List<Filme>();
        public List<ProgramariFilme> AllProgramari = new List<ProgramariFilme>();
        public Marcel _db;
        System.DateTime searchDate = System.DateTime.Now;    //va fi implicit ora sistemului, si se va schimba la cerererea userului
        public CinemaSite(Marcel db)
        {
            _db = db;
        }

        public List<Filme> GetAllMovies()
        {

            AllMovies = _db.Filmes.ToList();
            return AllMovies;
        }

        //toate filmele de un anumit gen (nu sunt mai multe genuri pe cinemacity)
        public List<Filme> FilmeDupaGen(string genDorit)
        {
            List<Filme> matches = new List<Filme>();

            var result = from F in _db.Filmes
                         join GF in _db.GenFilmes
                         on F.IdFilm equals GF.IdFilm
                         join G in _db.Genuris
                         on GF.IdGen equals G.IdGen
                         where G.Denumire == genDorit
                         select F;

            matches = result.ToList();
            
            return matches;
        }


        

        //toate orele la care se difuzeaza un film (eventual in functie de zi)
        public List<ProgramariFilme> GetProgramariFilme(string NumeFilm, System.DateTime dataCurenta = default(System.DateTime))    // returneaza o lista de forma : Start1, End1, Start2, End2, .... Start100, End100;
        {

            if (dataCurenta == default(System.DateTime))
                dataCurenta = System.DateTime.Now;

            var result = from F in _db.Filmes
                         join PF in _db.ProgramariFilmes
                         on F.IdFilm equals PF.IdFilm
                         where dataCurenta.Day == PF.Start.Day
                         select PF;

           

           return result.ToList();

            
           
        }


        //locurile libere ale unei programari

        public int RezervaLoc(int idProgramare)
        {
            var progra = from L in _db.LocuriSalis
                         where L.IdProgramare == idProgramare
                         select L;

            
            foreach (var p in progra)
            {
                if(p.State == 0)
                {
                    //p.State = 1;
     
                    //_db.SaveChanges();
                    return p.NrLoc;
                }
                    
            }

            return -1;  //nu sunt locuri libere in sala
        }

        public List<string> getGenuri() {

            var list = from G in _db.Genuris
                       select G.Denumire;



            var list1 = list.ToList();

            return list1;
        }
    }



}

