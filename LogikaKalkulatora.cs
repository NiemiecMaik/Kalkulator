using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kalkulator
{
    internal class LogikaKalkulatora : INotifyPropertyChanged
    {
        double?
            pierwszaLiczba = null,
            drugaLiczba = null
            ;
        string wyświetlanaLiczba = "0";
        string? buforDziałania = null;
        bool flagaDziałania = false;

        public string WyświetlanaLiczba
        {
            get => wyświetlanaLiczba;
            set {
                wyświetlanaLiczba = value;
                PropertyChanged?.Invoke(this, new("WyświetlanaLiczba"));
            }
        }
        public string WyświetlaneDziałanie
        {
            get
            {
                if (pierwszaLiczba == null)
                    return "";
                else if (buforDziałania == null)
                    return $"{pierwszaLiczba}";
                else if (drugaLiczba == null)
                    return $"{pierwszaLiczba} {buforDziałania}";
                else
                    return $"{pierwszaLiczba} {buforDziałania} {drugaLiczba} = ";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Cyfra(string cyfra)
        {
            if (flagaDziałania)
                WyczyśćWszystko();
            if(WyświetlanaLiczba == "0" || WyświetlanaLiczba == "-0")
                WyświetlanaLiczba = cyfra;
            else
                WyświetlanaLiczba += cyfra;
        }
        public void Przecinek()
        {
            if (flagaDziałania)
                WyczyśćWszystko();
            if (WyświetlanaLiczba.Contains(','))
                if (WyświetlanaLiczba[WyświetlanaLiczba.Length - 1] == ',')
                    KasujCyfrę();
                else
                    return;
            else
                WyświetlanaLiczba += ",";
        }
        public void KasujCyfrę()
        {
            if (flagaDziałania)
                WyczyśćWszystko();
            WyświetlanaLiczba = WyświetlanaLiczba.Substring(0, WyświetlanaLiczba.Length - 1);
            if (WyświetlanaLiczba == "-0" || WyświetlanaLiczba == "")
                WyświetlanaLiczba = "0";
        }
        public void ZmieńZnak()
        {
            if (flagaDziałania)
                WyczyśćWszystko();
            if (WyświetlanaLiczba[0] == '-')
                WyświetlanaLiczba = WyświetlanaLiczba.Substring(1);
            else
                WyświetlanaLiczba = "-" + WyświetlanaLiczba;
        }
        public void WyczyśćWyświetlanąLiczbę()
        {
            if (flagaDziałania)
                WyczyśćWszystko();
            WyświetlanaLiczba = "0";
        }
        public void WyczyśćWszystko()
        {
            flagaDziałania = false;
            WyczyśćWyświetlanąLiczbę();
            pierwszaLiczba = drugaLiczba = null;
            buforDziałania = null;
            PropertyChanged?.Invoke(this, new("WyświetlaneDziałanie"));
        }

        public void WprowadźDziałanie(string? działanie)
        {
            if (flagaDziałania)
            {
                drugaLiczba = null;
                flagaDziałania = false;
            }
            else if(buforDziałania != null)
            {
                WykonajDziałanie();
                drugaLiczba = null;
                flagaDziałania = false;
            }
            else
            {
                pierwszaLiczba = Convert.ToDouble(WyświetlanaLiczba);
            }
            buforDziałania = działanie;
            PropertyChanged?.Invoke(this, new("WyświetlaneDziałanie"));
            wyświetlanaLiczba = "0";
        }

        public void WykonajDziałanie()
        {
            if (drugaLiczba == null)
                if (wyświetlanaLiczba == "0")
                    drugaLiczba = pierwszaLiczba;
                else
                    drugaLiczba = Convert.ToDouble(WyświetlanaLiczba);
            PropertyChanged?.Invoke(this, new("WyświetlaneDziałanie"));

            switch (buforDziałania)
            {
                case "+":
                    WyświetlanaLiczba = $"{pierwszaLiczba + drugaLiczba}";
                    break;
                case "-":
                    WyświetlanaLiczba = $"{pierwszaLiczba - drugaLiczba}";
                    break;
                case "×":
                    WyświetlanaLiczba = $"{pierwszaLiczba * drugaLiczba}";
                    break;
                case "÷":
                    WyświetlanaLiczba = $"{pierwszaLiczba / drugaLiczba}";
                    break;
                case "√":
                    if (pierwszaLiczba >= 0)
                        WyświetlanaLiczba = $"{Math.Sqrt((double)pierwszaLiczba)}";
                    else
                        WyświetlanaLiczba = "Nieprawidłowe dane";
                    break;
                case "x²":
                    WyświetlanaLiczba = $"{Math.Pow((double)pierwszaLiczba, (double)drugaLiczba)}";
                    break;
                case "1/x":
                    if (pierwszaLiczba != 0)
                        WyświetlanaLiczba = $"{1 / (double)pierwszaLiczba}";
                    else
                        WyświetlanaLiczba = "Nieprawidłowe dane";
                    break;
                case "%":
                    double wynik = (double)pierwszaLiczba * ((double)drugaLiczba / 100);
                    WyświetlanaLiczba = $"{wynik}";
                    break;
                case "mod":
                    if (drugaLiczba != 0)
                    {
                        WyświetlanaLiczba = $"{pierwszaLiczba % drugaLiczba}";
                    }
                    else
                    {
                        WyświetlanaLiczba = "Nieprawidłowe dane";
                    }
                    break;
                case "x!":
                    WyświetlanaLiczba = ObliczSilnia(pierwszaLiczba);
                    break;
                case "log":
                    WyświetlanaLiczba = ObliczLogarytm10(pierwszaLiczba);
                    break;
                case "floor":
                    WyświetlanaLiczba = $"{Math.Floor((double)pierwszaLiczba)}";
                    break;
                case "ceil":
                    WyświetlanaLiczba = $"{Math.Ceiling((double)pierwszaLiczba)}";
                    break;
                default:
                    break;
            }
            pierwszaLiczba = Convert.ToDouble(WyświetlanaLiczba);
            flagaDziałania = true;
        }
        private string ObliczSilnia(double? liczba)
        {
            if (liczba == null)
                return "Nieprawidłowe dane";

            int n = Convert.ToInt32(liczba);

            if (n < 0)
                return "Nieprawidłowe dane";
            else if (n == 0 || n == 1)
                return "1";

            double wynik = 1;
            for (int i = 2; i <= n; i++)
            {
                wynik *= i;
            }

            return wynik.ToString();
        }
        private string ObliczLogarytm10(double? liczba)
        {
            if (liczba == null)
                return "Nieprawidłowe dane";

            double x = Convert.ToDouble(liczba);

            if (x <= 0)
                return "Nieprawidłowe dane";

            double wynik = Math.Log10(x);

            return wynik.ToString();
        }
    }
}
