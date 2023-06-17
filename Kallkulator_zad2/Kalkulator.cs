using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallkulator_zad2
{
    class Kalkulator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string?
            wynik = "0",
            operacja = null
            ;
        private double?
            operandLewy = null,
            operandPrawy = null
            ;

        public string Wynik
        {
            get => wynik;
            set
            {
                wynik = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs("Wynik")
                    );
            }
        }
        public string Działanie
        {
            get
            {
                if (operandLewy == null)
                    return "";
                else if (operandPrawy == null)
                    return $"{operandLewy} {operacja}";
                else
                    return $"{operandLewy} {operacja} {operandPrawy} = ";
            }
        }

        internal void WprowadźCyfrę(string? cyfra)
        {
            if (wynik == "0")
                if (cyfra == "0")
                    return;
                else
                    Wynik = cyfra;
            else
                Wynik += cyfra;
        }

        internal void WprowadźPrzecinek()
        {
            if (wynik.Contains(','))
                return;
            else
                Wynik += ',';
        }

        internal void ZmieńZnak()
        {
            if (wynik == "0")
                return;
            else if (wynik[0] == '-')
                Wynik = wynik.Substring(1);
            else
                Wynik = '-' + wynik;
        }

        internal void KasujZnak()
        {
            if (wynik == "0")
                return;
            else if (
                wynik.Length == 1
                || (wynik.Length == 2 && wynik[0] == '-')
                || wynik == "-0,"
                )
                Wynik = "0";
            else
                Wynik = wynik.Substring(0, wynik.Length - 1);
        }

        internal void Procent()
        {
            if (wynik == "0")
                return;
            else
                Wynik = (Convert.ToDouble(wynik) / 100).ToString();
        }

        internal void OdwrotnoscLiczby()
        {
            if (wynik == "0")
                return;
            else
                Wynik = (1 / Convert.ToDouble(wynik)).ToString();
        }

        internal void PierwiastekKwadratowy()
        {
            if (wynik == "0")
                return;
            else
                Wynik = (Math.Sqrt(Convert.ToDouble(wynik))).ToString();
        }

        internal void Silnia()
        {
            Wynik = (ObliczSilnie(Math.Floor(Convert.ToDouble(wynik)))).ToString();
        }

        double ObliczSilnie(double n)
        {
            if (n > 1)
                return ObliczSilnie(n - 1) * n;
            else
                return 1;
        }

        internal void LogarytmNaturalny()
        {
            Wynik = (Math.Log(Convert.ToDouble(wynik), Math.E)).ToString();
        }
        internal void LogarytmDziesietny()
        {
            Wynik = (Math.Log(Convert.ToDouble(wynik), 10)).ToString();
        }

        internal void ZaokraglWGore()
        {
            Wynik = (Math.Ceiling(Convert.ToDouble(wynik))).ToString();
        }

        internal void ZaokraglWDol()
        {
            Wynik = (Math.Floor(Convert.ToDouble(wynik))).ToString();
        }

        internal void CzyśćWszystko()
        {
            CzyśćWynik();
            operacja = null;
            operandLewy = operandPrawy = null;
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs("Działanie")
                );
        }

        internal void CzyśćWynik()
        {
            Wynik = "0";
        }

        internal void WprowadźOperacja(string operacja)
        {
            if (this.operacja != null)
            {
                WykonajDziałanie();
                this.operacja = operacja;
            }
            else
            {
                operandLewy = Convert.ToDouble(wynik);
                this.operacja = operacja;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs("Działanie")
                    );
            }

            wynik = "0";
        }

        internal void WykonajDziałanie()
        {
            if (operandPrawy == null)
                if (wynik == "0")
                    operandPrawy = operandLewy;
                else
                    operandPrawy = Convert.ToDouble(wynik);
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs("Działanie")
                );
            if (operacja == "+")
                Wynik = (operandLewy + operandPrawy).ToString();
            if (operacja == "-") 
                Wynik = (operandLewy - operandPrawy).ToString();
            if (operacja == "*") 
                Wynik = (operandLewy * operandPrawy).ToString();
            if (operacja == "/") 
                Wynik = (operandLewy / operandPrawy).ToString();
            if (operacja == "^") 
                Wynik = Math.Pow((double)operandLewy, (double)operandPrawy).ToString();
            if (operacja == "mod") 
                Wynik = (operandLewy % operandPrawy).ToString();

            operandLewy = Convert.ToDouble(wynik);
        }
    }
}


