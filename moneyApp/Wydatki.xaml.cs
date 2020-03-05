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

namespace moneyApp
{
    /// <summary>
    /// Interaction logic for Wydatki.xaml
    /// </summary>
    public partial class Wydatki : Window
    {
        public Wydatki()
        {
            InitializeComponent();

            KasaEntities kas = new KasaEntities();
            var kasa = kas.Wydatkii.ToList();
            var months = kasa.Select(c => new
            {   
              MonthName = ((DateTime)c.Data.Value).ToString("MMMM")
           
            }).Distinct();

         

            foreach (var item in months)
            {
                Mies.Items.Add(item.MonthName);
            }

         
            
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
          

        }

        private void Mies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = Mies.SelectedItem.ToString();

            DisplayList(selectedItem);

        }

        private void DisplayList(string selectedItem)
        {
            lista.Items.Clear();

            KasaEntities kas = new KasaEntities();
            var kasaa = kas.Wydatkii.ToList();
            var months2 = kasaa.Select(c => new
            {
                Kwota = c.Kwota,
                Nazwa_Transakcji = c.Nazwa_Transakcji,
                RT = c.Rodzaj_Transakcji,
                MonthName = ((DateTime)c.Data.Value).ToString("MMMM"),
                c.Data

            }).ToList();


            List<Kasa> kasa = new List<Kasa>();

            List<Oblicz> money = new List<Oblicz>();

            decimal kosztyy = 0;
            decimal przychodd = 0;
            decimal podsumowaniee = 0;


            for (int i = 0; i < months2.Count; i++)
            {
                if (months2[i].MonthName == selectedItem)
                {
                    kasa.Add(new Kasa()
                    {
                        Data = months2[i].Data.ToString(),
                        Kwota = months2[i].Kwota,
                        NazwaTransakcji = months2[i].Nazwa_Transakcji,
                        RodzajTransakcji = months2[i].RT

                    });
                    ListViewItem OneItem = new ListViewItem();
                    OneItem.Content = kasa.Last();

                    if (months2[i].RT == "Przychod")
                    {
                        przychodd += months2[i].Kwota;
                        OneItem.Foreground = Brushes.Green;

                    }
                    else
                    {
                        kosztyy += months2[i].Kwota;
                        OneItem.Foreground = Brushes.Red;
                    }
                    lista.Items.Add(OneItem);
                }


            }

            money.Add(new Oblicz()
            {
                Przychód = przychodd,
                Koszty = kosztyy,
                Podsumowanie = przychodd - kosztyy
            });



            pod.ItemsSource = money;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
