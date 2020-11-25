using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;



namespace LSBGenerator___PD
{
    class Steganografia
    {


        public static int Konwertuj_string_binarny(string bity)
        {
            var Bit_R = bity.Reverse().ToArray();
            var n = 0;
            for (var j = 0; j < Bit_R.Count(); j++)
            {
                var currentBit = Bit_R[j];
                if (currentBit == '1')
                {
                    var numer = Convert.ToInt32(Math.Pow(2, j));
                    n += numer;
                }
            }

            return n;
        }


        public static Bitmap kodowanie(string text, Bitmap bmp, string indeks, int zapis, int xor, bool xor_aktywacja)
        {
            Color pixel;
            Pixel Bitmap = new Pixel(bmp);
            Bitmap.LockBits();
            int licznik_multi = 0;


            string t = text;
            long zakres = t.Length * (zapis + 1);
            int licz = 0;
            int licz2 = 0;


            int[] value = new int[text.Length];
            string[] binary = new string[text.Length];
            int licz_a = 0;
            string[] znak = new string[t.Length];
            string[] test = new string[t.Length];

            char[] wiadomosc = new char[text.Length];
            string[] temp_binary = new string[binary.Length];
            string ilosc_bitow = Convert.ToString(t.Length, 2);
            int l = 0;




            for (int j = (Bitmap.szerokosc - 1); j > (Bitmap.szerokosc - 1) - ilosc_bitow.Length; j--)
            {
                pixel = Bitmap.GetPixel(Bitmap.wysokosc - 1, j);

                int lsb_ilosc_bitow = (Convert.ToInt32((pixel.R & 254)) + Convert.ToInt32(ilosc_bitow[l].ToString()));
                Bitmap.SetPixel(Bitmap.wysokosc - 1, j, Color.FromArgb(lsb_ilosc_bitow, pixel.G, pixel.B));
                l++;

            }


            foreach (var a in t)
            {
                binary[licz_a] = Convert.ToString(Convert.ToChar(a), 2);


                while (binary[licz_a].Length < zapis)
                {

                    binary[licz_a] = "0" + binary[licz_a];

                }

                for (int k = 0; k < binary[licz_a].Length; k++)
                {


                    
                 temp_binary[licz_a] = temp_binary[licz_a] + Convert.ToString(binary[licz_a][binary[licz_a].Length - (k + 1)]); // teraz dobrze 




                }




                znak[licz_a] = temp_binary[licz_a];

                licz_a++;

            }



            for (int i = 0; i < Bitmap.wysokosc; i++)
            {

                for (int j = 0; j < Bitmap.szerokosc; j++)
                {
                    pixel = Bitmap.GetPixel(i, j);


                    if (licz < znak.Length)
                    {
                    
                        if (indeks == "Multi") // test wszystkie kanały - multi 
                        {



                            if (xor_aktywacja == true)
                            {
                               

                                

                                if (licznik_multi == 4) licznik_multi = 0;

                                if (licznik_multi == 0)
                                {

                                    int lsb_M = (Convert.ToInt32((pixel.A & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) ^ (xor))); // operacja xor
                                    Bitmap.SetPixel(i, j, Color.FromArgb(lsb_M, pixel.R, pixel.G, pixel.B));  // A

                                }

                                if (licznik_multi == 1) 
                                {
                                    int lsb_M = (Convert.ToInt32((pixel.B & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) ^ (xor))); // operacja xor
                                    Bitmap.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, lsb_M));  // B

                                }
                                if (licznik_multi == 2) 
                                {
                                    int lsb_M = (Convert.ToInt32((pixel.R & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) ^ (xor))); // operacja xor
                                    Bitmap.SetPixel(i, j, Color.FromArgb(lsb_M, pixel.G, pixel.B));  // R

                                }
                                if (licznik_multi == 3) 
                                {
                                    int lsb_M = (Convert.ToInt32((pixel.G & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) ^ (xor))); // operacja xor
                                    Bitmap.SetPixel(i, j, Color.FromArgb(pixel.R, lsb_M, pixel.B)); // G

                                }


                                licznik_multi++;

                            }
                            else
                            {
                                if (licznik_multi == 4) licznik_multi = 0;

                                if (licznik_multi == 0)
                                {
                                    int lsb_M = (Convert.ToInt32((pixel.A & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) )); // operacja xor
                                    Bitmap.SetPixel(i, j, Color.FromArgb(lsb_M, pixel.R, pixel.G, pixel.B));  // A
                                }

                                if (licznik_multi == 1)
                                {
                                    int lsb_M = (Convert.ToInt32((pixel.B & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) )); // operacja xor
                                    Bitmap.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, lsb_M));  // B
                                }
                                if (licznik_multi == 2)
                                {
                                    int lsb_M = (Convert.ToInt32((pixel.R & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) )); // operacja xor
                                    Bitmap.SetPixel(i, j, Color.FromArgb(lsb_M, pixel.G, pixel.B));  // R
                                }
                                if (licznik_multi == 3)
                                {
                                    int lsb_M = (Convert.ToInt32((pixel.G & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) )); // operacja xor
                                    Bitmap.SetPixel(i, j, Color.FromArgb(pixel.R, lsb_M, pixel.B)); // G
                                }

                                licznik_multi++;
                            }


                        }


                        if (indeks == "R")
                        {



                            if (xor_aktywacja == true)
                            {

                                int lsb_R = (Convert.ToInt32((pixel.R & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) ^ (xor))); // operacja xor
                                Bitmap.SetPixel(i, j, Color.FromArgb(lsb_R, pixel.G, pixel.B));

                            }
                            else
                            {
                                int lsb_R = (Convert.ToInt32((pixel.R & 254)) + Convert.ToInt32(znak[licz][licz2].ToString()));
                                Bitmap.SetPixel(i, j, Color.FromArgb(lsb_R, pixel.G, pixel.B));

                            }


                        }
                        else if (indeks == "G")
                        {


                            if (xor_aktywacja == true)
                            {

                                int lsb_G = (Convert.ToInt32((pixel.G & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) ^ (xor))); // operacja xor
                                Bitmap.SetPixel(i, j, Color.FromArgb(pixel.R, lsb_G, pixel.B));

                            }
                            else
                            {
                                int lsb_G = (Convert.ToInt32((pixel.G & 254)) + Convert.ToInt32(znak[licz][licz2].ToString()));
                                Bitmap.SetPixel(i, j, Color.FromArgb(pixel.R, lsb_G, pixel.B));

                            }
                        }
                        else if (indeks == "B")
                        {

                            if (xor_aktywacja == true)
                            {

                                int lsb_B = (Convert.ToInt32((pixel.B & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) ^ (xor))); // operacja xor
                                Bitmap.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, lsb_B));

                            }
                            else
                            {
                                int lsb_B = (Convert.ToInt32((pixel.B & 254)) + Convert.ToInt32(znak[licz][licz2].ToString()));
                                Bitmap.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, lsb_B));

                            }
                        }

                            // kanał A
                        else if (indeks == "A")
                        {

                            if (xor_aktywacja == true)
                            {

                                int lsb_A = (Convert.ToInt32((pixel.A & 254)) + (Convert.ToInt32(znak[licz][licz2].ToString()) ^ (xor))); // operacja xor
                                Bitmap.SetPixel(i, j, Color.FromArgb(lsb_A, pixel.R, pixel.G, pixel.B));

                            }
                            else
                            {
                                int lsb_A = (Convert.ToInt32((pixel.A & 254)) + Convert.ToInt32(znak[licz][licz2].ToString()));
                                Bitmap.SetPixel(i, j, Color.FromArgb(lsb_A, pixel.R, pixel.G, pixel.B));

                            }



                        }

                        licz2++;



                        if (licz2 == zapis - 1 && znak.Length > licz)
                        {
                            licz2 = 0;
                            licz++;
                            j++;

                        }



                    }

                    if (i == Bitmap.wysokosc - 1 && j == Bitmap.szerokosc - 1)
                    {
                        Bitmap.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, ilosc_bitow.Length ^ xor));
                    }

                }




            }

            Bitmap bmp_save = Bitmap.Save();
            // MessageBox.Show(Convert.ToString(test));

            return bmp_save;
        }




        public static string dekodowanie(Bitmap bmp, string indeks, string kodowanie, int zapis, int xor, bool xor_aktywacja)
        {
            Pixel Bitmap = new Pixel(bmp);
            Bitmap.LockBits();
            int licznik_multi = 0;


            StringBuilder wiadomosc = new StringBuilder();
            Color pixel;

            string value2 = "";
            string s_msgLength = "";
            long licz = 0;

            Color lastpixel = Bitmap.GetPixel(Bitmap.wysokosc - 1, Bitmap.szerokosc - 1); // ilość początkowych bitów do odczytania rozmiaru wiadomosci



            for (int j = (Bitmap.szerokosc - 1); j > (Bitmap.szerokosc - 1) - (lastpixel.B ^ xor); j--)
            {
                pixel = Bitmap.GetPixel((Bitmap.wysokosc - 1), j);

                int lsb = (pixel.R & 1);
                s_msgLength = s_msgLength + lsb.ToString();

            }


            int msgLength = Konwertuj_string_binarny(s_msgLength);








            for (int i = 0; i < Bitmap.wysokosc; i++)
            {
                for (int j = 0; j < Bitmap.szerokosc; j++)
                {

                 //   if (j % 2 == 0) xor = 1;
                 //   else xor = 0;

                    if (licz < (msgLength * (zapis)))
                    {
                        pixel = Bitmap.GetPixel(i, j);


                        if (indeks == "Multi") // test multi
                        {



                            if (value2.Length < zapis - 1)
                            {


                                if (xor_aktywacja == true)
                                {
                                    if (licznik_multi == 4) licznik_multi = 0;
                                    if (licznik_multi == 0) value2 += ((pixel.A & 1) ^ (xor)).ToString();  // A
                                    if (licznik_multi == 1) value2 += ((pixel.B & 1) ^ (xor)).ToString();  // B
                                    if (licznik_multi == 2) value2 += ((pixel.R & 1) ^ (xor)).ToString();  // R
                                    if (licznik_multi == 3) value2 += ((pixel.G & 1) ^ (xor)).ToString();  // G


                                    licznik_multi++;

                                }
                                else
                                {
                                    if (licznik_multi == 4) licznik_multi = 0;
                                    if (licznik_multi == 0) value2 += ((pixel.A & 1) ).ToString();  // A
                                    if (licznik_multi == 1) value2 += ((pixel.B & 1) ).ToString();  // B
                                    if (licznik_multi == 2) value2 += ((pixel.R & 1) ).ToString();  // R
                                    if (licznik_multi == 3) value2 += ((pixel.G & 1) ).ToString();  // G

                                    Console.WriteLine(value2);

                                    licznik_multi++;
                                }





                            }
                            else
                            {

                                string temp_value = "";


                                for (int k = 0; k < value2.Length; k++)
                                {


                                    temp_value += Convert.ToString(value2[(value2.Length) - (k + 1)]); // teraz dobrze 



                                }







                                if (kodowanie == "UTF8")
                                {
                                    string letter = System.Text.Encoding.UTF8.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF32")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));
                                }
                                if (kodowanie == "ASCII")
                                {
                                    string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF16")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));

                                }

                                value2 = "";
                                temp_value = "";


                            }

                        }






                        /////////////////////////////////////////////////////////////////////////////////////////////////
                        if (indeks == "R")
                        {



                            if (value2.Length < zapis - 1)
                            {


                                if (xor_aktywacja == true)
                                {
                                    value2 += ((pixel.R & 1) ^ (xor)).ToString();

                                }

                                else
                                {
                                    value2 += ((pixel.R & 1)).ToString();

                                }





                            }
                            else
                            {

                                string temp_value = "";


                                for (int k = 0; k < value2.Length; k++)
                                {


                                    temp_value += Convert.ToString(value2[(value2.Length) - (k + 1)]); // teraz dobrze 



                                }







                                if (kodowanie == "UTF8")
                                {
                                    string letter = System.Text.Encoding.UTF8.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF32")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));
                                }
                                if (kodowanie == "ASCII")
                                {
                                    string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF16")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));

                                }

                                value2 = "";
                                temp_value = "";


                            }

                        }
                        else if (indeks == "G")
                        {



                            if (value2.Length < zapis - 1)
                            {


                                if (xor_aktywacja == true)
                                {
                                    value2 += ((pixel.G & 1) ^ (xor)).ToString();

                                }

                                else
                                {
                                    value2 += ((pixel.G & 1)).ToString();

                                }





                            }
                            else
                            {
                                string temp_value = "";

                                for (int k = 0; k < value2.Length; k++)
                                {



                                    temp_value = temp_value + Convert.ToString(value2[(value2.Length) - (k + 1)]); // teraz dobrze 




                                }



                                if (kodowanie == "UTF8")
                                {
                                    string letter = System.Text.Encoding.UTF8.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF32")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));
                                }
                                if (kodowanie == "ASCII")
                                {
                                    string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF16")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));

                                }

                                value2 = "";
                                temp_value = "";
                            }

                        }
                        else if (indeks == "B")
                        {

                            if (value2.Length < zapis - 1)
                            {




                                if (xor_aktywacja == true)
                                {
                                    value2 += ((pixel.B & 1) ^ (xor)).ToString();

                                }

                                else
                                {
                                    value2 += ((pixel.B & 1)).ToString();

                                }





                            }
                            else
                            {
                                string temp_value = "";

                                for (int k = 0; k < value2.Length; k++)
                                {



                                    temp_value = temp_value + Convert.ToString(value2[(value2.Length) - (k + 1)]); // teraz dobrze 



                                }


                                if (kodowanie == "UTF8")
                                {
                                    string letter = System.Text.Encoding.UTF8.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF32")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));
                                }
                                if (kodowanie == "ASCII")
                                {
                                    string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF16")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));

                                }

                                value2 = "";
                                temp_value = "";
                            }

                        }

                        else if (indeks == "A") // prawidłowo kanał A
                        {

                            if (value2.Length < zapis - 1)
                            {

                                if (xor_aktywacja == true)
                                {
                                    value2 += ((pixel.A & 1) ^ (xor)).ToString();

                                }

                                else
                                {
                                    value2 += ((pixel.A & 1)).ToString();

                                }




                            }
                            else
                            {
                                string temp_value = "";

                                for (int k = 0; k < value2.Length; k++)
                                {


                                    temp_value = temp_value + Convert.ToString(value2[(value2.Length) - (k + 1)]); // teraz dobrze 


                                }


                                if (kodowanie == "UTF8")
                                {
                                    string letter = System.Text.Encoding.UTF8.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF32")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));
                                }
                                if (kodowanie == "ASCII")
                                {
                                    string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(Convert.ToChar(Konwertuj_string_binarny(temp_value))) });
                                    wiadomosc.Append(letter);
                                }
                                if (kodowanie == "UTF16")
                                {
                                    string letter = Konwertuj_string_binarny(temp_value).ToString();
                                    wiadomosc.Append(Convert.ToChar(Convert.ToInt32(letter)));

                                }

                            
                                value2 = "";
                                temp_value = "";




                            }









                        }




                    }
                    licz++;


                }




            }

            return wiadomosc.ToString();

        }


    }
}

