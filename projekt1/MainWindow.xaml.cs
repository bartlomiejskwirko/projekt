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
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;

namespace projekt1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnection connection;
        string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["projekt1.Properties.Settings.Database1ConnectionString"].ConnectionString;

            DisplayBMI();
        }

        //private void MainWindow_Load(object sender, EventArgs e)
        //{
        //  DisplayBMI();
        //}

        private void DisplayBMI()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM BMITABLE ", connection))
            {
                DataTable tabelabmi = new DataTable();
                adapter.Fill(tabelabmi);

                listbmi.SelectedValue = "Id";
                listbmi.DisplayMemberPath = "BMI";
                listbmi.ItemsSource = tabelabmi.DefaultView;
            }
        }

        private void DodajBMI_Click(object sender, RoutedEventArgs e)
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("INSERT INTO BMITABLE VALUES(@bmiValue)",connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@bmiValue", bmi.ToString());
                command.ExecuteNonQuery();
            }
            DisplayBMI();
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("TRUNCATE TABLE BMITABLE", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();

            }
            DisplayBMI();
        }




        double wzrost;
        double waga;
        double wynik;
        double bmi;
        double wiek;
        double bmrkobieta;
        double bmrmezczyzna;


        //zakladka BMI
        void Button_Click(object sender, RoutedEventArgs e)
        {





            try
                {
                    wzrost = double.Parse(TXBWZR.Text);
                    waga = double.Parse(TXBWAGA.Text);
                    wynik = (waga / (wzrost * wzrost)) * 10000;

                    bmi = Math.Round(wynik, MidpointRounding.AwayFromZero);

                }
                catch (FormatException)//podana wartosc nie jest cyrfra
                {
                    MessageBox.Show("zly format");
                }
                catch (OverflowException)//za duza liczba
                {
                    MessageBox.Show("Podales za duze liczby");
                }

                catch (Exception)
                {
                    MessageBox.Show("cos poszlo nie tak");
                }
            if (wzrost >= 100 && wzrost<=250 && waga >=30 && waga<=150) { //warunek ograniczajacy wzrost i wage


            if (KobietaBT.IsChecked == true)
            {
                if (bmi >= 24)
                {
                    MessageBox.Show("Dla Kobiety " + bmi.ToString() + " BMI" + " jest za duże - masz nadwagę");
                }

                else if (bmi <= 17)
                {

                    MessageBox.Show("Dla Kobiety " + bmi.ToString() + " BMI" + " jest za małe - masz niedowagę");
                }

                else
                {
                    MessageBox.Show("Dla Kobiety " + bmi.ToString() + " BMI" + " jest w normie.");
                }

            }
            else if (KobietaBT.IsChecked == false && MezczyznaBT.IsChecked == false)
            {
                MessageBox.Show("Wybierz płeć!");
            }

            else if (MezczyznaBT.IsChecked == true)
            {
                if (bmi >= 26)
                {
                    MessageBox.Show("Dla mężczyzny " + bmi.ToString() + " BMI" + " jest za duże - masz nadwagę");
                }

                else if (bmi <= 16)
                {

                    MessageBox.Show("Dla mężczyzny " + bmi.ToString() + " BMI" + " jest za małe - masz niedowagę");
                }

                else
                {
                    MessageBox.Show("Dla mężczyzny " + bmi.ToString() + " BMI" + " jest w normie.");
                }
            }
            }//koniec warunku wzrost 50-250cm
            else
            {
                MessageBox.Show("Wprowadz wzrost w zakresie 100-250cm oraz wagę w zakresie 30-150kg");
            }
        }






        // Zakladka zapotrzebowanie kaloryczne
        void Button_Click_1(object sender, RoutedEventArgs e)
        {



            try
            {
                wzrost = double.Parse(TXBWZROST.Text);
                waga = double.Parse(TXBWAGA2.Text);
                wiek = double.Parse(TXBWIEK.Text);
                bmrkobieta = ((9 * waga + (6.25 * wzrost) - (5 * wiek)) - 161);
                bmrmezczyzna = (9 * waga + (6.25 * wzrost) - (5 * wiek) + 5);
            }

            catch (FormatException)
            {
                MessageBox.Show("zly format");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Podales za duze liczby");
            }
            catch (Exception)
            {
                MessageBox.Show("cos poszlo nie tak");
            }

            if (wzrost >= 100 && wzrost <= 250 && waga >= 30 && waga <= 150 && wiek >= 10 && wiek <= 99)//warunek na wiek/wage/wzrost
            {
                if (MezczyznaZap.IsChecked == false && KobietaZap.IsChecked == false)//warunek - wybierz plec
                {
                    MessageBox.Show("Wybierz plec");
                }
                else
                {
                    if (MezczyznaZap.IsChecked == true)//sprawdzenie wybrania radiobuttonow
                    {
                        if (BrakAk.IsChecked == true)//sprawdzenie wybrania radiobuttonow w drugim kontenerze
                        {
                            MessageBox.Show("Zapotrzebowanie kaloryczne mezczyzny wynosi: " + bmrmezczyzna.ToString());

                        }

                        else if (MalaAk.IsChecked == true)
                        {
                            double malaakt = bmrmezczyzna * 1.2;

                            MessageBox.Show("Zapotrzebowanie kaloryczne dla mezczyzny wynosi: " + malaakt.ToString());
                        }

                        else if (SredniaAk.IsChecked == true)
                        {

                            double sredniaakt = bmrmezczyzna * 1.4;
                            MessageBox.Show("Zapotrzebowanie kaloryczne dla mezczyzny wynosi: " + sredniaakt.ToString());

                        }

                        else if (DuzaAk.IsChecked == true)
                        {

                            double duzaakt = bmrmezczyzna * 1.6;
                            MessageBox.Show("Zapotrzebowanie kaloryczne dla mezczyzny wynosi: " + duzaakt.ToString());

                        }

                        else if (BardzoDuzaAk.IsChecked == true)
                        {

                            double bduzaakt = bmrmezczyzna * 1.9;
                            MessageBox.Show("Zapotrzebowanie kaloryczne dla mezczyzny wynosi: " + bduzaakt.ToString());

                        }

                        else
                        {

                            MessageBox.Show("Wybierz aktywność!");
                        }
                    }

                    if (KobietaZap.IsChecked == true)
                    {
                        if (BrakAk.IsChecked == true)
                        {
                            MessageBox.Show("Zapotrzebowanie kaloryczne kobiety wynosi: " + bmrkobieta.ToString());

                        }

                        else if (MalaAk.IsChecked == true)
                        {
                            double malaakt = bmrkobieta * 1.2;

                            MessageBox.Show("Zapotrzebowanie kaloryczne dla kobiety wynosi: " + malaakt.ToString());
                        }

                        else if (SredniaAk.IsChecked == true)
                        {

                            double sredniaakt = bmrkobieta * 1.4;
                            MessageBox.Show("Zapotrzebowanie kaloryczne dla kobiety wynosi: " + sredniaakt.ToString());

                        }

                        else if (DuzaAk.IsChecked == true)
                        {

                            double duzaakt = bmrkobieta * 1.6;
                            MessageBox.Show("Zapotrzebowanie kaloryczne dla kobiety wynosi: " + duzaakt.ToString());

                        }

                        else if (BardzoDuzaAk.IsChecked == true)
                        {

                            double bduzaakt = bmrkobieta * 1.9;
                            MessageBox.Show("Zapotrzebowanie kaloryczne dla kobiety wynosi: " + bduzaakt.ToString());

                        }

                        else
                        {

                            MessageBox.Show("Wybierz aktywność!");
                        }


                    }
                }//koniec warunku wybierz plec
            
            }
            else 
            {

                MessageBox.Show("Parametry muszą być podane w odpowiednim zakresie: Wiek: 10-99, wzrost 100-250, waga: 30-150");

            }
            //koniec funkcji if - warunek ograniczajacy wiek wzrost i wage
        }


    }
}

