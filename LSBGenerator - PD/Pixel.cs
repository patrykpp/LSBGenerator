using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;


namespace LSBGenerator___PD
{
   public class Pixel
    {
    
    public Bitmap bitmapa;
    public IntPtr intptr = IntPtr.Zero;
    public BitmapData bitmapData;

    public int wysokosc { get; set; }
    public int szerokosc { get; set; }
    public byte[] Pixels { get; set; }
    public int PixelFormatSize { get; set; }
    
 
    public Pixel(Bitmap bmp)
    {
        this.bitmapa = bmp;
    }
               
    public Bitmap Save()
    {
        UnlockBits();
        return bitmapa;
    }
    public void SetPixel(int x, int y, Color color)
    {
        int cCount = PixelFormatSize / 8;

        int i = ((y * wysokosc) + x) * cCount;

        if (PixelFormatSize == 32) 
        {
            Pixels[i] = color.B;
            Pixels[i + 1] = color.G;
            Pixels[i + 2] = color.R;
            Pixels[i + 3] = color.A;
        }
        if (PixelFormatSize == 24) 
        {
            Pixels[i] = color.B;
            Pixels[i + 1] = color.G;
            Pixels[i + 2] = color.R;
        }
        if (PixelFormatSize == 8)
        {
            Pixels[i] = color.B;
        }
    }

    public void LockBits()
    {
        try
        {
            szerokosc = bitmapa.Height;
            wysokosc = bitmapa.Width;
 
            int AllPixel = wysokosc * szerokosc;
 
            Rectangle rectangle = new Rectangle(0, 0, wysokosc, szerokosc);
 
            PixelFormatSize = System.Drawing.Bitmap.GetPixelFormatSize(bitmapa.PixelFormat);
 
            bitmapData = bitmapa.LockBits(rectangle, ImageLockMode.ReadWrite, 
                                         bitmapa.PixelFormat);
 
            int step = PixelFormatSize / 8;
            Pixels = new byte[AllPixel * step];
            intptr = bitmapData.Scan0;
 
            System.Runtime.InteropServices.Marshal.Copy(intptr, Pixels, 0, Pixels.Length);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Błąd");
        }
    }
 
    public void UnlockBits()
    {
        try
        {
            System.Runtime.InteropServices.Marshal.Copy(Pixels, 0, intptr, Pixels.Length);
 
            bitmapa.UnlockBits(bitmapData);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Błąd");
        }
    }
 
   
    public Color GetPixel(int x, int y)
    {
        Color color = Color.Empty;
 
        int cCount = PixelFormatSize / 8;
 
        int i = ((y * wysokosc) + x) * cCount;
 
             try
             {
                 if (i > Pixels.Length - cCount) ;
             }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd");

            }

        if (PixelFormatSize == 32)
        {
            int b = Pixels[i];
            int g = Pixels[i + 1];
            int r = Pixels[i + 2];
            int a = Pixels[i + 3]; 
            color = Color.FromArgb(a, r, g, b);
        }
        if (PixelFormatSize == 24) 
        {
            int b = Pixels[i];
            int g = Pixels[i + 1];
            int r = Pixels[i + 2];
            color = Color.FromArgb(r, g, b);
        }
        if (PixelFormatSize == 8)
        {
            int c = Pixels[i];
            color = Color.FromArgb(c, c, c);
        }
        return color;
    }
 

 

    }
}
