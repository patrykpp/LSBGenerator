using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LSBGenerator___PD
{
    class Konwertowanie_pliku_na_tekst
    {

       static public string nazwa_pliku = "";
       static public bool kropka = false;

        static public void konwersja_bajow_na_plik(string sciezka_do_zapisu, byte[] tablica)
        {

            using (Stream w = File.Open(sciezka_do_zapisu, FileMode.Create))
            {
                w.Write(tablica, 0, tablica.Length);

            }

        }

        static public byte[] konwersja_pliku_na_bajty(string sciezka_pliku)
        {


            FileInfo info = new FileInfo(sciezka_pliku);
            long a1 = info.Length;
            byte[] bufor = new byte[a1];
            Stream s = new MemoryStream();


            using (Stream source = File.OpenRead(sciezka_pliku))
            {
                int bajty_odczytane;
                while ((bajty_odczytane = source.Read(bufor, 0, bufor.Length)) > 0)
                {
                    s.Write(bufor, 0, bajty_odczytane);
                }
            }




            return bufor;
        }


        static public string[] Koder(byte[] plik, string sciezka) // do poprawy
        {
            FileInfo f = new FileInfo(sciezka);
            string[] tekst;
            int licz = 0;

            tekst = new string[plik.Length + f.Extension.Length];

            for (int i = 0; i < plik.Length; i++)
            {
                tekst[i] += Convert.ToString(plik[i], 16);

                while (tekst[i].Length < 2)
                {
                    tekst[i] = "0" + tekst[i];
                }

            }

            for (int i = tekst.Length - (f.Extension.Length); i < tekst.Length; i++)
            {
                tekst[i] = f.Extension[licz].ToString();
                licz++;
                
            }

            return tekst;

        }


        static public byte[] Dekoder(string[] tekst, string nazwa)
        {
            byte[] dane = new byte[tekst.Length];
            int l = 0;

            for (int i = 0; i < tekst.Length; i++)
            {
                dane[i] = byte.Parse(tekst[i], System.Globalization.NumberStyles.HexNumber);
                l++;
                
            }
           

           
            return dane;

        }

        static public string Konwertuj_tablice_na_string(string[] tablica)
        {
            StringBuilder b = new StringBuilder();
            foreach (string wartosc in tablica)
            {
                b.Append(wartosc);
            }
            return b.ToString();
        }

        static public string [] Konwertuj_string_na_tablice(string array)
        {
            int licznik = 0;
            int licznik2 = 0;
            string bit = "";
           
            for (int i = array.Length - 1; i > 0; i--)
            {
                licznik++;

                if (Convert.ToString(array[i]) == ".")
                {
                    kropka = true;
                    break;
                }

            }
            nazwa_pliku = array.Substring((array.Length - licznik), licznik);
            array = array.Remove(array.Length - nazwa_pliku.Length, nazwa_pliku.Length);
            string[] tablica = new string[array.Length / 2];
            licznik = 0;

            foreach (var wartosc in array)
            {
                licznik++;

                bit = bit + wartosc;

                if(licznik == 2)
                {
                    tablica[licznik2] = bit;
                    bit = "";
                    licznik = 0;
                    licznik2++;
                }

            }

            return tablica;
        }


    }
}
