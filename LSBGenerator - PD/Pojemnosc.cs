using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;


namespace LSBGenerator___PD
{
    class Pojemnosc
    {

        public static Bitmap sprawdz_pojemnosc(Bitmap bmp, string text, int zapis, bool indeks)
        {
            Color pixel;
            Pixel Bitmap = new Pixel(bmp);
            Bitmap.LockBits();
            string t = text;
            int licz = 0;
            int licz2 = 0;
            string[] znak = new string[t.Length]; 

             


            for (int i = 0; i < Bitmap.wysokosc; i++)
            {

                for (int j = 0; j < Bitmap.szerokosc; j++)
                {


                    if (licz < znak.Length)
                    {
                        pixel = Bitmap.GetPixel(i, j);


                        Bitmap.SetPixel(i, j, Color.FromArgb(255, pixel.G, pixel.B));



                        licz2++;



                        if (licz2 == zapis - 1 && znak.Length > licz)
                        {
                            licz2 = 0;
                            licz++;
                            j++;

                        }
                    }

                }

            }

            Bitmap bmp_save = Bitmap.Save();
            // MessageBox.Show(Convert.ToString(test));

            return bmp_save;
        }




    }
}
