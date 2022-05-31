using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Popcorn
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static IClient client;

		public MainWindow()
		{
			InitializeComponent();
			client = new CClient();
			client.StartCommunication();


			List<string> categories = client.GetAllCategories();
			CategoryComboBox.ItemsSource = categories;
		}

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			
		}


		private void RadioButton_Checked_1() {

        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

			Movie template = new Movie();
			template.m_genre.Add(CategoryComboBox.Text);


		}
    }
}
