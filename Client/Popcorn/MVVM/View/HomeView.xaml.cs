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

using ClientCinematograf;
using System.IO;

namespace Popcorn.MVVM.View
{
	/// <summary>
	/// Interaction logic for HomeView.xaml
	/// </summary>
	public partial class HomeView : UserControl
	{

		private string pathBase = "D:\\ATM\\BazeDate\\vsProjects\\MOCANU_BD\\CEvaBdProj\\CEvaBdProj\\bin\\Debug\\images\\";
		private DirectoryInfo dir;

		private UIElement mainGridHome;

		private List<Movie> movies;
		private Movie GetMovieByName(string name) {

			foreach(var movie in movies) {
				if (movie.m_name == name)
					return movie;
            }
			return null;
        }

		public HomeView()
		{
			InitializeComponent();
			dir = new DirectoryInfo(pathBase);

			Movie movietest = new Movie();
			//List<Movie> movies = MainWindow.client.GetMovieProvizoriu(movietest);
			movietest.m_genre.Add("Fantezie");
			movies = MainWindow.client.GetMovie(movietest);

			

			int count = 0;
			
			foreach(var stackPanel in stackpanel1.Children) {

				if (count >= movies.Count)
					break;

				StackPanel currentStack = stackPanel as StackPanel;
				ImageSourceConverter imgs = new ImageSourceConverter();

				string path = pathBase + movies[count].m_poza + ".jfif";
				currentStack.Children[0].SetValue(Image.SourceProperty, imgs.ConvertFromString(path));

				//currentStack.Children[0].MouseLeftButtonDown += ImageClicked;
				currentStack.MouseLeftButtonDown += ImageClicked;
				

				Label label = currentStack.Children[1] as Label;
				label.Content = movies[count].m_name;
				count++;
			}

			foreach (var stackPanel in stackpanel2.Children) {
				if (count >= movies.Count)
					break;

				StackPanel currentStack = stackPanel as StackPanel;
				ImageSourceConverter imgs = new ImageSourceConverter();

				string path = pathBase + movies[count].m_poza + ".jfif";
				currentStack.Children[0].SetValue(Image.SourceProperty, imgs.ConvertFromString(path));

				currentStack.MouseLeftButtonDown += ImageClicked;

				Label label = currentStack.Children[1] as Label;
				label.Content = movies[count].m_name;
				count++;
			}

			foreach (var stackPanel in stackpanel3.Children) {
				if (count >= movies.Count)
					break;

				StackPanel currentStack = stackPanel as StackPanel;
				ImageSourceConverter imgs = new ImageSourceConverter();

				string path = pathBase + movies[count].m_poza + ".jfif";
				currentStack.Children[0].SetValue(Image.SourceProperty, imgs.ConvertFromString(path));

				currentStack.MouseLeftButtonDown += ImageClicked;

				Label label = currentStack.Children[1] as Label;
				label.Content = movies[count].m_name;
				count++;
			}

			foreach (var stackPanel in stackpanel4.Children) {
				if (count >= movies.Count)
					break;

				StackPanel currentStack = stackPanel as StackPanel;
				ImageSourceConverter imgs = new ImageSourceConverter();

				string path = pathBase + movies[count].m_poza + ".jfif";
				currentStack.Children[0].SetValue(Image.SourceProperty, imgs.ConvertFromString(path));

				currentStack.MouseLeftButtonDown += ImageClicked;

				Label label = currentStack.Children[1] as Label;
				label.Content = movies[count].m_name;
				count++;
			}

			foreach (var stackPanel in stackpanel5.Children) {
				if (count >= movies.Count)
					break;

				StackPanel currentStack = stackPanel as StackPanel;
				ImageSourceConverter imgs = new ImageSourceConverter();

				string path = pathBase + movies[count].m_poza + ".jfif";
				currentStack.Children[0].SetValue(Image.SourceProperty, imgs.ConvertFromString(path));

				currentStack.MouseLeftButtonDown += ImageClicked;

				Label label = currentStack.Children[1] as Label;
				label.Content = movies[count].m_name;
				count++;
			}



			//ImageSourceConverter imgs = new ImageSourceConverter();
			//item00_img.SetValue(Image.SourceProperty, imgs.ConvertFromString("D:\\ATM\\download2.jfif"));
			//item00_title.Content = "pa";
		}




		private void ImageClicked(object sender, RoutedEventArgs e) {

			StackPanel stackPanel = sender as StackPanel;
			Image img = stackPanel.Children[0] as Image;
			string title = (stackPanel.Children[1] as Label).Content as string;

			Movie movie = GetMovieByName(title);
			MovieView movieView = new MovieView(movie, mainGrid.Children[0], mainGrid);

			//mainGridHome = mainGrid.Children[0];

			mainGrid.Children.Clear();
			mainGrid.Children.Add(movieView);
        }

	

	}


}
