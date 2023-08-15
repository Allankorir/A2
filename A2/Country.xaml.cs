using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;


namespace A2
{
    /// <summary>
    /// Interaction logic for Country.xaml
    /// </summary>
    public partial class Country : Window
    {
        public Country()
        {
            InitializeComponent();
            PopulateContinents();
        }
        private void PopulateContinents()
        {
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDBFinal;User Id=sa;Password=softwareCCC2030"))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ContinentName FROM Continent", connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        continentComboBoxs.Items.Add(reader["ContinentName"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedContinent = continentComboBoxs.SelectedItem as string;
            string country = txtCountryName.Text;
            string language = txtlanguage.Text;
            string currency = txtCurrency.Text;

            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDBFinal;User Id=sa;Password=softwareCCC2030"))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Country (CountryName, Language, Currency) VALUES (@CountryName, @Language, @Currency)", connection);
                    cmd.Parameters.AddWithValue("@CountryName", country);
                    cmd.Parameters.AddWithValue("@Language", language);
                    cmd.Parameters.AddWithValue("@Currency", currency);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Country saved successfully!");

                    // Close the AddCountryWindow...
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
       
