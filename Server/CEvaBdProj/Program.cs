

namespace Server {
    class Program {
        static void Main(string[] args) {

            //  in functia start se porneste serverul
            //  se asculta daca avem clienti noi
            //  si se creeaza cate un thread pentru fiecare client nou
            //  unde se va face comunicarea

            MyConnection.Instance.Start();
        }
    }
}