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
    /// Interaction logic for AddContinentForm.xaml
    /// </summary>
    public partial class AddContinentForm : Window
    {
        private MainWindow mainWindow;
        private SqlConnection sqlConnection;

        public AddContinentForm(System.Data.SqlClient.SqlConnection sqlConnection)
        {
            InitializeComponent();
            this.sqlConnection = sqlConnection;
        }

        public AddContinentForm(SqlConnection sqlConnection, MainWindow mainWindow) : this(sqlConnection)
        {
            this.mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string continentName = txtContinent.Text.Trim();

                if (string.IsNullOrEmpty(continentName))
                {
                    MessageBox.Show("Please enter a valid continent name.");
                    return;
                }
                using (SqlConnection connection = new SqlConnection("Server=DESKTOP-VF531U3\\SA;Database=WorldDB;User Id=sa;Password=softwareCCC2030;"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO cont (ContinentName) VALUES (@ContinentName)", connection);
                    cmd.Parameters.AddWithValue("@ContinentName", txtContinent.Text);
                    cmd.ExecuteNonQuery();

                    // Optionally clear the input field after saving
                    txtContinent.Text = "";

                    // Refresh the combo box on the main window
                    mainWindow.LoadContinents();

                    MessageBox.Show("Continent saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    }
