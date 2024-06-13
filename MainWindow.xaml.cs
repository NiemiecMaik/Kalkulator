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

namespace kalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LogikaKalkulatora logika { get; } = new();
        public MainWindow()
        {
            DataContext = logika;
            InitializeComponent();
        }

        private void WprowadźCyfrę(object sender, RoutedEventArgs e)
        {
            string cyfra = ((ContentControl)sender).Content.ToString();
            logika.Cyfra(cyfra);
        }

        private void Przecinek(object sender, RoutedEventArgs e)
            => logika.Przecinek();

        private void ZmieńZnak(object sender, RoutedEventArgs e)
            => logika.ZmieńZnak();

        private void Kasuj(object sender, RoutedEventArgs e)
            => logika.KasujCyfrę();

        private void Wyczyść(object sender, RoutedEventArgs e)
            => logika.WyczyśćWyświetlanąLiczbę();

        private void WyczyśćWszystko(object sender, RoutedEventArgs e)
            => logika.WyczyśćWszystko();

        private void WprowadźDziałanie(object sender, RoutedEventArgs e)
        {
            string działanie = ((ContentControl)sender).Content.ToString();
            logika.WprowadźDziałanie(działanie);
        }

        private void WykonajDziałanie(object sender, RoutedEventArgs e)
            => logika.WykonajDziałanie();
    }
}
