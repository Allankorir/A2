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
using System.Windows.Shapes;
using System.Data.SqlClient;



namespace A2
{
    /// <summary>
    /// Interaction logic for City.xaml
    /// </summary>
    public partial class City : Window
    {
        public City()
        {
            InitializeComponent();
            PopulateCountries();
        }
        private void PopulateCountries()
        {
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDBFinal;User Id=sa;Password=softwareCCC2030"))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT CountryName FROM Country", connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        countryComboBox.Items.Add(reader["CountryName"].ToString());
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
            string selectedCountry = countryComboBox.SelectedItem as string;
            string cityName = txtCityName.Text;
            bool isCapital = chkIsCapital.IsChecked ?? false;
            int population = int.Parse(txtPopulation.Text);

            int countryID = GetCountryID(selectedCountry);
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDBFinal;User Id=sa;Password=softwareCCC2030"))
            {
                try
                {
                    connection.Open();

                    // Get the CountryID using the selected country name
                    SqlCommand countryIDCmd = new SqlCommand("SELECT CountryID FROM Country WHERE CountryName = @CountryName", connection);
                    countryIDCmd.Parameters.AddWithValue("@CountryName", selectedCountry);
                    int retrievedCountryID = (int)countryIDCmd.ExecuteScalar();

                    SqlCommand cmd = new SqlCommand("INSERT INTO City (CountryID, CityName, IsCapital, Population) VALUES (@CountryID, @CityName, @IsCapital, @Population)", connection);
                    cmd.Parameters.AddWithValue("@CountryID", retrievedCountryID); // Use the retrieved CountryID
                    cmd.Parameters.AddWithValue("@CityName", cityName);
                    cmd.Parameters.AddWithValue("@IsCapital", isCapital);
                    cmd.Parameters.AddWithValue("@Population", population);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("City saved successfully!");

                    // Close the City window...
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private int GetCountryID(string countryName)
        {
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDBFinal;User Id=sa;Password=softwareCCC2030"))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT CountryID FROM Country WHERE CountryName = @CountryName", connection);
                    cmd.Parameters.AddWithValue("@CountryName", countryName);

                    int countryID = (int)cmd.ExecuteScalar();
                    return countryID;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1; // Return a default value indicating an error
                }
            }
        }

    }
}

