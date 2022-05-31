using ClientCinematograf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Popcorn.MVVM.View {
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    public partial class MovieView : UserControl {
        private string pathBase = "D:\\ATM\\BazeDate\\vsProjects\\MOCANU_BD\\CEvaBdProj\\CEvaBdProj\\bin\\Debug\\images\\";

        UIElement _home;
        Grid _controller;
        Movie _movie;
        public MovieView(Movie movie, UIElement home, Grid controller) {
            InitializeComponent();

            _movie = movie;

            _controller = controller;
            _home = home;

            movieDescription.Text = movie.m_description;
            ImageSourceConverter imgs = new ImageSourceConverter();
            string filename = pathBase + movie.m_poza + ".jfif";
            movieImage.SetValue(Image.SourceProperty, imgs.ConvertFromString(filename));
            movieTitle.Text = movie.m_name;
        }

        private void BackHomeButtonClicked(object sender, RoutedEventArgs e) {

            _controller.Children.Clear();
            _controller.Children.Add(_home);
        }

        private void ReserveButtonClicked(object sender, RoutedEventArgs e) {

            //interogheaza server
            //primeste raspuns
            //afisam raspuns

            List<string> detaliiRezervare = MainWindow.client.ReserverPlace(_movie);

            Random rnd = new Random();
            int sala = rnd.Next(1, 5);  
            int loc = rnd.Next(1, 30);

            //detaliiRezervare[0] = "-1";

            if (detaliiRezervare[0] == "-1")
                MessageBox.Show("Nu mai sunt locuri la acest film!");
            else
                //MessageBox.Show($"Rezervare reusita! Sala: {detaliiRezervare[1]} Loc: {detaliiRezervare[2]} Data: {detaliiRezervare[2]}");
                MessageBox.Show($"Rezervare reusita! Sala: {sala} Loc: {loc}");
        }
    }
}
