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

namespace moneyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Kasa> kasa;

        static bool isEdited;

        static int selectedIndexx;

        List<ListViewItem> ITEMS;
        public MainWindow()
        {
            InitializeComponent();
            kasa = new List<Kasa>();
            isEdited = false;
            selectedIndexx = 0;
            ITEMS = new List<ListViewItem>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        

            kasa.Add(new Kasa()
            {
                Data = dataa.Text,
                Kwota = Decimal.Parse(kwotaa.Text),
                NazwaTransakcji = nazwaTransakcjii.Text,
                RodzajTransakcji = RodzajTransakcjii.Text

            });
            ListViewItem OneItem = new ListViewItem();
            OneItem.Content = kasa.Last();

           if(RodzajTransakcjii.Text == "Przychod")
            {
                OneItem.Foreground = Brushes.Green;
            }
            else
            {
                OneItem.Foreground = Brushes.Red;
            }

       
            //lista.Items.Add(OneItem);
         
            //lista.Items.Refresh();

            //lista.ItemsSource = kasa;
            if (!isEdited)
            {
              
                lista.Items.Add(OneItem);
               lista.UpdateLayout();
            }
            else
            {
                lista.Foreground = Brushes.Red;
                lista.Items[selectedIndexx] = OneItem;

                kasa.RemoveAt(selectedIndexx);
                isEdited = false;
            }
            var suma = ObliczWydatki();

            Rekalkulacja(suma);

            

           

            Console.WriteLine("");
        }

        private void Rekalkulacja(decimal [] suma)
        {
            var pod = "Podsumowanie: ";
            var przych = "Przychod: ";
            var wydatki = "Wydatki: ";

            Podsumowanie.Content =  pod + suma[0].ToString();
            Przychod.Content = przych+ suma[1].ToString();
            Wydatki.Content = wydatki+ suma[2].ToString();
        }

        public static decimal [] ObliczWydatki()
        {
            decimal  [] suma = new decimal[3];
            if (kasa.Count > 0)
            {
                for (int i = 0; i < kasa.Count; i++)
                {
                    if (kasa[i].RodzajTransakcji == "Przychod")
                    {
                        suma[0] += kasa[i].Kwota;
                        suma[1] += kasa[i].Kwota;
                        suma[2] += 0;

                    }
                    else if (kasa[i].RodzajTransakcji == "Wydatek")
                    {
                        suma[0] -= kasa[i].Kwota;
                        suma[1] += 0;
                        suma[2] += kasa[i].Kwota;
                    }
                }
            }
            return suma;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lista.SelectedIndex;
            kasa.RemoveAt(selectedIndex);
            lista.Items.Remove(lista.SelectedItem);

            var suma = ObliczWydatki();
       
            Rekalkulacja(suma);

            Console.WriteLine("");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lista.SelectedIndex;
            var listaa = lista.Items[selectedIndex];



            dataa.Text = kasa[selectedIndex].Data;
            kwotaa.Text = kasa[selectedIndex].Kwota.ToString();
            nazwaTransakcjii.Text = kasa[selectedIndex].NazwaTransakcji;
            RodzajTransakcjii.Text = kasa[selectedIndex].RodzajTransakcji;

            isEdited = true;
            selectedIndexx = selectedIndex;

            

            Console.WriteLine("");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Wydatki wydatki = new Wydatki();
            wydatki.Show();
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            KasaEntities kas = new KasaEntities();


            if (kasa.Count > 0)
            {
                MessageBox.Show("Dziala?");

                for (int i = 0; i < kasa.Count; i++)
                {
                    var data = kasa[i].
                        Data.ToString();
                    var Wydatkii = new Wydatkii
                    {
                        
                        Kwota = kasa[i].Kwota,
                        Nazwa_Transakcji = kasa[i].NazwaTransakcji,
                        Rodzaj_Transakcji = kasa[i].RodzajTransakcji,
                        Data = DateTime.Parse(data)
                    };
                    kas.Wydatkii.Add(Wydatkii);
                }

                kas.SaveChanges();
            }
        }
    }
}
