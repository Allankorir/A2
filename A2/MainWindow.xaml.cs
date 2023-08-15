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
using System.Data.SqlClient;
using System.Data;



namespace A2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection;


        public MainWindow()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDBFinal;User Id=sa;Password=softwareCCC2030;");


            LoadContinents();
        }
        public void LoadContinents()
        {
            continentComboBox.Items.Clear();

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ContinentName FROM Continent", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    continentComboBox.Items.Add(reader["ContinentName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Country addCountryWindow = new Country();
            addCountryWindow.ShowDialog();
        }


        private void continentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedContinent = continentComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedContinent))
            {
                PopulateCountries(selectedContinent);
            }
        }
        private void PopulateCountries(string continent)
        {
            Listbox.Items.Clear();

            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDBFinal;User Id=sa;Password=softwareCCC2030"))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT CountryName FROM Country C JOIN Continent CT ON C.ContinentID = CT.ContinentID WHERE CT.ContinentName = @ContinentName", connection);
                    cmd.Parameters.AddWithValue("@ContinentName", continent);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Listbox.Items.Add(reader["CountryName"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void txtCurency_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Country addCountryWindow = new Country();
            addCountryWindow.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            City addCountryWindow = new City();
            addCountryWindow.ShowDialog();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCountry = Listbox.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedCountry))
            {
                DisplayCountryDetails(selectedCountry);
                PopulateCityDataGrid(selectedCountry);
            }
        }

            private void PopulateCityDataGrid(string countryName)
            {
                using (SqlConnection connection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDBFinal;User Id=sa;Password=softwareCCC2030;"))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("SELECT City.CityID, City.CityName, City.IsCapital, City.Population, Country.CountryName " +
                                                        "FROM City JOIN Country ON City.CountryID = Country.CountryID " +
                                                        "WHERE Country.CountryName = @CountryName", connection);
                        cmd.Parameters.AddWithValue("@CountryName", countryName);

                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        datagrid.ItemsSource = dt.DefaultView;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }





        


        private void DisplayCountryDetails(string country)
        {
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDBFinal;User Id=sa;Password=softwareCCC2030;"))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Language, Currency FROM Country WHERE CountryName = @CountryName", connection);
                    cmd.Parameters.AddWithValue("@CountryName", country);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtLang.Text = reader["Language"].ToString();
                        Curr.Text = reader["Currency"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

