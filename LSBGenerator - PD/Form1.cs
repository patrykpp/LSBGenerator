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
    public partial class Form1 : Form
    {
        ArrayList path = new ArrayList();
        int błąd;



        public Bitmap bmp;
        public Bitmap bmp_temp;
        public int poj_znakow;

        public Bitmap[] folder_bmp;
        int element = 0;

        Form2 form2_;




        string folder;
        int Count;



        OpenFileDialog dialog = new OpenFileDialog();
        string indeks = String.Empty;
        string extractedText = "";
        string text = "";
        string kodowanie = "";
        int zapis = 16; // domyslne ustawienie reprezentacji znaków
        int xor;
        bool xor_aktywacja;
        string file_name = Application.StartupPath + "\\__temp.jpg";
        string file_name2 = Application.StartupPath + "\\temp2.jpg";
        MemoryStream memoryStream = new MemoryStream();




        Image OriginalImage = null;








        public Form1()
        {
            InitializeComponent();

           
            // Ustawienia programu
            radio8bit.Checked = Properties.Settings.Default.radio8;
            radio9bit.Checked = Properties.Settings.Default.radio9;
            radio16bit.Checked = Properties.Settings.Default.radio16;
            radio32bit.Checked = Properties.Settings.Default.radio32;

            radioUTF8.Checked = Properties.Settings.Default.radioUTF8;
            radioUTF16.Checked = Properties.Settings.Default.radioUTF16;
            radioUTF32.Checked = Properties.Settings.Default.radioUTF32;
            radioASCII.Checked = Properties.Settings.Default.radioASCII;

            radio_xor.Checked = Properties.Settings.Default.radioXOR;
            radio_lsb.Checked = Properties.Settings.Default.radioLSB;

            red.Checked = Properties.Settings.Default.red;
            green.Checked = Properties.Settings.Default.green;
            blue.Checked = Properties.Settings.Default.blue;
            multi.Checked = Properties.Settings.Default.multi;

            radioorg.Checked = Properties.Settings.Default.radioorg;
            radiobmp.Checked = Properties.Settings.Default.radiobmp;
            radiopng.Checked = Properties.Settings.Default.radiopng;
            radiogif.Checked = Properties.Settings.Default.radiogif;
            radiojpg.Checked = Properties.Settings.Default.radiojpg;
            radiojpeg.Checked = Properties.Settings.Default.radiojpeg;



            if (obraz_kodowany.Image == null) savetool.Enabled = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Text = "AES"; // domyslne ustawienie
            comboBox2.Text = "AES"; // domyslne ustawienie
            comboKompresja.Text = "100";
            koncowy.Enabled = false;
            comboBox1.Enabled = false;
            radio16bit.Text = radio16bit.Text + Environment.NewLine + "(polskie znaki)";
            zajetosc.Enabled = false;
            skompresowany.Enabled = false;
            label10.Text = "- Wykorzystanie" + Environment.NewLine + "  nosnika";


            noweOkno1.Enabled = false;
            noweOkno2.Enabled = false;


            ToolTip buttonToolTip = new ToolTip();
            buttonToolTip.ToolTipTitle = "Kodowanie";
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = false;

            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 1000;
            buttonToolTip.ReshowDelay = 500;

            buttonToolTip.SetToolTip(koder, "Kliknij aby wygenerować znak wodny !" + Environment.NewLine + "Aby otworzyć obraz kliknij : Plik/Otwórz lub Plik/Zapisz aby zapisać");






        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                File.Delete(file_name);
                File.Delete(file_name2);


                Properties.Settings.Default.radio8 = radio8bit.Checked;
                Properties.Settings.Default.radio9 = radio9bit.Checked;
                Properties.Settings.Default.radio16 = radio16bit.Checked;
                Properties.Settings.Default.radio32 = radio32bit.Checked;

                Properties.Settings.Default.radioUTF8 = radioUTF8.Checked;
                Properties.Settings.Default.radioUTF16 = radioUTF16.Checked;
                Properties.Settings.Default.radioUTF32 = radioUTF32.Checked;
                Properties.Settings.Default.radioASCII = radioASCII.Checked;

                Properties.Settings.Default.radioXOR = radio_xor.Checked;
                Properties.Settings.Default.radioLSB = radio_lsb.Checked;

                Properties.Settings.Default.red = red.Checked;
                Properties.Settings.Default.green = green.Checked;
                Properties.Settings.Default.blue = blue.Checked;
                Properties.Settings.Default.multi = multi.Checked;

                Properties.Settings.Default.radioorg = radioorg.Checked;
                Properties.Settings.Default.radiobmp = radiobmp.Checked;
                Properties.Settings.Default.radiopng = radiopng.Checked;
                Properties.Settings.Default.radiojpg = radiojpg.Checked;
                Properties.Settings.Default.radiojpeg = radiojpeg.Checked;
                Properties.Settings.Default.radiogif = radiogif.Checked;

                Properties.Settings.Default.Save();

            }
            catch (Exception ex)
            {
            }

        }


       

        private void otworzFolder_Click(object sender, EventArgs e)
        {
            try
            {
                logo.Visible = false;
                noweOkno1.Enabled = false;
                noweOkno2.Enabled = false;
                path = new ArrayList();
                iloscznakow2.Text = " -";
                nosnik.Text = "≈";
                element = 1; // otwarcie folderu
                savetool.Enabled = false;
                if (tabControl1.SelectedIndex == 0) obraz_kodowany.Image = null;
                if (tabControl1.SelectedIndex == 1) obraz_dekodowany.Image = null;
                notesLabel3.Text = "";
                komunikat2.Text = "";



                if (tabControl1.SelectedIndex == 1) // Dekoder
                {

                    // MessageBox.Show("Nie możesz otworzyć folderu gdy odczytujesz znak wodny.", "Błąd !");


                    FolderBrowserDialog dialog_dekoder = new FolderBrowserDialog();
                    

                    koncowy.Enabled = false;
                    zajetosc.Enabled = false;
                    koder.Enabled = true;

                    // comboKompresja.Enabled = false;
                    skompresowany.Enabled = false;


                    dialog_dekoder.Description = "Otwórz folder ze zdjęciami który chcesz odczytać" + Environment.NewLine + "Upewnij się ze w folderze znajdują się tylko pliki zdjęciowe";

                    dialog_dekoder.ShowNewFolderButton = true;
                    DialogResult result = dialog_dekoder.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        folder = dialog_dekoder.SelectedPath;
                        Count = 0;





                        foreach (string plik in System.IO.Directory.GetFiles(folder))
                        {

                            if ((File.GetAttributes(plik) & FileAttributes.Hidden) != FileAttributes.Hidden)
                            {
                                path.Add(plik);
                                Count++;


                            }
                        }





                        komunikat2.ForeColor = Color.Black;
                        komunikat2.Text = "Wpisz tekst znaku wodnego a następnie kliknij" + Environment.NewLine + "aby zakodować.";


                    }
                }
                    if (tabControl1.SelectedIndex == 0) // Koder
                    {



                        FolderBrowserDialog dialog_koder = new FolderBrowserDialog();
                       

                        koncowy.Enabled = false;
                        koder.Enabled = true;
                        // comboKompresja.Enabled = false;
                        skompresowany.Enabled = false;
                        zajetosc.Enabled = false;



                        dialog_koder.Description = "Otwórz folder ze zdjęciami który chcesz wykorzystać do wygenerowania znaków wodnych" + Environment.NewLine + "Upewnij się ze w folderze znajdują się tylko pliki zdjęciowe";

                        dialog_koder.ShowNewFolderButton = true;
                        DialogResult result2 = dialog_koder.ShowDialog();

                        if (result2 == DialogResult.OK)
                        {
                            folder = dialog_koder.SelectedPath;
                            Count = 0;





                            foreach (string plik in System.IO.Directory.GetFiles(folder))
                            {

                                if ((File.GetAttributes(plik) & FileAttributes.Hidden) != FileAttributes.Hidden)
                                {
                                    path.Add(plik);
                                    Count++;


                                }
                            }





                            komunikat2.ForeColor = Color.Black;
                            komunikat2.Text = "Wpisz tekst znaku wodnego a następnie kliknij" + Environment.NewLine + "aby zakodować.";



                        }


                        // iloscznakow2.Text = dataTextBox.TextLength.ToString() + "/" + poj_znakow;

                    }
                






            }
            catch (Exception ex)
            {
                MessageBox.Show("Nieprawidłowy format pliku. Spróbuj ponownie", "Błąd !");
            }

        }

        private void imagePictureBox_Click(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                obraz_kodowany.Image = Image.FromFile(dialog.FileName);
                wykorzystanie_red.Visible = false;
                label10.Visible = false;

            }
            catch (Exception ex)
            {

            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

       

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        public void Aktualizacja2(string tekst, int tryb)
        {
            if (tryb == 1)
            {
                dataTextBox2.Text = tekst;
                dataTextBox2.Refresh();


            }
            if (tryb == 2)
            {
                dataTextBox.Text = tekst;
                dataTextBox.Refresh();
            }
        }

        private void koder_Click(object sender, EventArgs e)
        {
           


          
            try
            {

                komunikat2.Text = "Proszę czekać...";
                komunikat2.ForeColor = Color.Black;

                błąd = 0;
                komunikat.Text = "Komunikat :";
                komunikat.ForeColor = Color.Black;
                int count = 0;

                progressBar2.Value = 0;
                text = dataTextBox.Text;
                koder.Enabled = false;










                if (text.Equals("") == false)
                {
                    this.timer2.Start();


                }



                if (radio_xor.Checked)
                {
                    xor = 1;
                    xor_aktywacja = true;
                }
                else
                {
                    xor = 0;
                    xor_aktywacja = false;

                }


                // zapis znaków

                if (radio8bit.Checked)
                {
                    zapis = 8;
                }
                if (radio9bit.Checked)
                {
                    zapis = 10;
                }
                else if (radio16bit.Checked)
                {
                    zapis = 16;
                }
                else if (radio32bit.Checked)
                {
                    zapis = 32;
                }




                // bmp = new Bitmap(imagePictureBox.Image);
                if (red.Checked)
                {
                    indeks = "R";
                }
                if (green.Checked)
                {
                    indeks = "G";
                }
                if (blue.Checked)
                {
                    indeks = "B";
                }
                if (alfa.Checked)
                {
                    indeks = "A";
                }
                if (multi.Checked)
                {
                    indeks = "Multi";
                }



                if (text.Equals(""))
                {
                    błąd = 1;
                    MessageBox.Show("Tekst znaku wodnego nie może być pusty !", "Błąd !");
                    koder.Enabled = true;
                    komunikat2.Text = "Błąd ! Proszę spróbować ponownie.";
                    komunikat2.ForeColor = Color.OrangeRed;


                    return;
                }

                if (encryptCheckBox.Checked)
                {
                    if (haslo.Text.Length < 5)
                    {
                        błąd = 1;
                        MessageBox.Show("Hasło musi się składać z minimum 5 znaków", "Błąd !");
                        koder.Enabled = true;
                        komunikat2.Text = "Błąd ! Proszę spróbować ponownie.";
                        komunikat2.ForeColor = Color.OrangeRed;


                        return;
                    }
                    else
                    {
                        text = AES.Syfrowanie(text, haslo.Text);

                        if(text.Length > poj_znakow)
                        {
                            błąd = 1;
                            MessageBox.Show("Syfr AES : " + text.Length + " znaków przekracza pojemność nosnika. Proszę zmniejszyć ilość znaków", "Błąd !");
                            koder.Enabled = true;
                            komunikat2.Text = "Błąd ! Proszę spróbować ponownie.";
                            komunikat2.ForeColor = Color.OrangeRed;

                        }
                    }
                }






                savetool.Enabled = true;



                if (element == 0) /////////// WARIANT 1 "z pliku"
                {

                    // konwersja do streamu i odczyt

                    if (radioorg.Checked)
                    {
                        bmp = new Bitmap(Image.FromFile(dialog.FileName));

                    }

                    if (radiojpg.Checked)
                    {

                        long c = Convert.ToInt32(comboKompresja.Text);
                        var jpg = new Bitmap(dialog.FileName);
                        var stream = new System.IO.MemoryStream();

                        EncoderParameters encoder = new EncoderParameters(1);
                        encoder.Param[0] = new EncoderParameter(
                        System.Drawing.Imaging.Encoder.Quality, c);

                        ImageCodecInfo image_codec_info = GetEncoderInfo("image/jpeg");
                        jpg.Save(stream, image_codec_info, encoder);

                        bmp = new Bitmap(Image.FromStream(stream));



                    }
                    if (radiopng.Checked)
                    {
                        var png = new Bitmap(dialog.FileName);
                        var stream = new System.IO.MemoryStream();

                        png.Save(stream, ImageFormat.Png);

                        bmp = new Bitmap(Image.FromStream(stream));


                    }
                    if (radiogif.Checked)
                    {
                        var gif = new Bitmap(dialog.FileName);
                        var stream = new System.IO.MemoryStream();

                        gif.Save(stream, ImageFormat.Gif);

                        bmp = new Bitmap(Image.FromStream(stream));


                    }
                    if (radiobmp.Checked)
                    {
                        var bmp_ = new Bitmap(dialog.FileName);
                        var stream = new System.IO.MemoryStream();

                        bmp_.Save(stream, ImageFormat.Bmp);

                        bmp = new Bitmap(Image.FromStream(stream));


                    }

                    if (radiojpeg.Checked)
                    {
                        var jpeg = new Bitmap(dialog.FileName);
                        var stream = new System.IO.MemoryStream();

                        jpeg.Save(stream, ImageFormat.Jpeg);

                        bmp = new Bitmap(Image.FromStream(stream));


                    }

                   




                    memoryStream = new MemoryStream();
                  
                    bmp_temp = Steganografia.kodowanie(text, bmp, indeks, zapis, xor, xor_aktywacja);
                    bmp_temp.Save(memoryStream, ImageFormat.Bmp);



                }


                if (element == 1) ///// WARIANT 2 z folderu
                {
                    folder_bmp = new Bitmap[path.Count];



                    foreach (var p in path)
                    {

                        if (radioorg.Checked)
                        {

                            bmp = new Bitmap(Image.FromFile(p.ToString()));

                        }

                        if (radiojpg.Checked) // dla jpg
                        {

                            long c = Convert.ToInt32(comboKompresja.Text);
                            var jpg = new Bitmap(Image.FromFile(p.ToString()));
                            var stream = new System.IO.MemoryStream();

                            EncoderParameters encoder = new EncoderParameters(1);
                            encoder.Param[0] = new EncoderParameter(
                            System.Drawing.Imaging.Encoder.Quality, c);

                            ImageCodecInfo image_codec_info = GetEncoderInfo("image/jpeg");
                            jpg.Save(stream, image_codec_info, encoder);

                            bmp = new Bitmap(Image.FromStream(stream));



                        }
                        if (radiopng.Checked)
                        {
                            var png = new Bitmap(Image.FromFile(p.ToString()));
                            var stream = new System.IO.MemoryStream();

                            png.Save(stream, ImageFormat.Png);

                            bmp = new Bitmap(Image.FromStream(stream));


                        }
                        if (radiogif.Checked)
                        {
                            var gif = new Bitmap(Image.FromFile(p.ToString()));
                            var stream = new System.IO.MemoryStream();

                            gif.Save(stream, ImageFormat.Gif);

                            bmp = new Bitmap(Image.FromStream(stream));


                        }
                        if (radiobmp.Checked)
                        {
                            var bmp_ = new Bitmap(Image.FromFile(p.ToString()));
                            var stream = new System.IO.MemoryStream();

                            bmp_.Save(stream, ImageFormat.Bmp);

                            bmp = new Bitmap(Image.FromStream(stream));


                        }

                        if (radiojpeg.Checked)
                        {
                            var jpeg = new Bitmap(Image.FromFile(p.ToString()));
                            var stream = new System.IO.MemoryStream();

                            jpeg.Save(stream, ImageFormat.Jpeg);

                            bmp = new Bitmap(Image.FromStream(stream));


                        }
                        


                        folder_bmp[count] = Steganografia.kodowanie(text, bmp, indeks, zapis, xor, xor_aktywacja);
                        count++;

                    }


                }



            }
            catch (Exception ex)
            {
                błąd = 1;
                MessageBox.Show(ex.Message, "Błąd !");
                koder.Enabled = true;
                savetool.Enabled = false;

                if (element == 1)
                {
                    komunikat2.ForeColor = Color.OrangeRed;
                    komunikat2.Text = "Błąd ! Proszę się upewnić ze w folderze są tylko" + Environment.NewLine + "pliki zdjęciowe.";
                }
                else
                {
                    komunikat2.ForeColor = Color.OrangeRed;
                    komunikat2.Text = "Błąd ! Proszę spróbować ponownie.";
                }
            }


        }

        private void zapiszJpg(Image image, string file_name, long compression)
        {
            try
            {

                EncoderParameters encoder_params = new EncoderParameters(1);
                encoder_params.Param[0] = new EncoderParameter(
                    System.Drawing.Imaging.Encoder.Quality, compression);

                ImageCodecInfo image_codec_info = GetEncoderInfo("image/jpeg");
                File.Delete(file_name);
                image.Save(file_name, image_codec_info, encoder_params);



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file '" + file_name +
                    "'\nTry a different file name.\n" + ex.Message,
                    "Save Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private ImageCodecInfo GetEncoderInfo(string mime_type)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i <= encoders.Length; i++)
            {
                if (encoders[i].MimeType == mime_type) return encoders[i];
            }
            return null;
        }


        public void Rozmiar()
       {
           if (obraz_kodowany.Image != null)
           {
               int poj;
               FileInfo file_info = new FileInfo(file_name);
               lbl100.Text = file_info.Length.ToString() + " bajtów";
               nosnik.Text = "≈";
              
               poj = (OriginalImage.Height * OriginalImage.Width);

               poj_znakow = (poj / zapis) - Convert.ToString(dataTextBox.TextLength, 2).Length - 1;
               int procent = (dataTextBox.TextLength * 100) / poj_znakow;
               nosnik.Text = nosnik.Text + " " + poj.ToString() + " bitów" + Environment.NewLine + "   " + poj_znakow + " znaków" + Environment.NewLine + "   " + procent + "/100%" + Environment.NewLine + Environment.NewLine + "Zapis: " + zapis + " bitów";

           }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                logo.Visible = false;


                element = 0;

                notesLabel3.Text = "";
                komunikat2.Text = "";
                koncowy.Enabled = false;

                dialog.Filter = "pliki JPEG (*.jpeg)|*.jpeg|pliki PNG (*.png)|*.png|pliki JPG (*.jpg)|*.jpg|pliki BMP(*.bmp)|*.bmp|Wszystkie pliki (*.*)|*.*";
                dialog.Title = "Otwórz plik zdjęciowy w którm chcesz wygenerować lub odczytać znak wodny";
                koder.Enabled = true;

               


                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (tabControl1.SelectedIndex == 0)
                    {
                        obraz_kodowany.Image = Image.FromFile(dialog.FileName);
                    }
                    else
                    {
                        obraz_dekodowany.Image = Image.FromFile(dialog.FileName);
                    }
                    OriginalImage = Image.FromFile(dialog.FileName);


                    zapiszJpg(Image.FromFile(dialog.FileName), file_name, 100);
                    

                    komunikat2.ForeColor = Color.Black;
                    komunikat2.Text = "Wpisz tekst znaku wodnego a następnie kliknij" + Environment.NewLine + "aby zakodować.";
                    Rozmiar();
                    iloscznakow2.Text = dataTextBox.TextLength.ToString() + "/" + poj_znakow;

                    noweOkno1.Enabled = true;
                    noweOkno2.Enabled = true;

                    zajetosc.Enabled = true;
                    skompresowany.Enabled = true;




                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nieprawidłowy format pliku. Spróbuj ponownie", "Błąd !");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program został stworzony w ramach pracy dyplomowej." + Environment.NewLine + "Autor : Patryk Pieczara © 2017" + Environment.NewLine + Environment.NewLine + "Program służy do generowania ukrytych znaków wodnych metodą LSB z użyciem podstawowych algorytmów szyfrujących. Dodatkową funkcjonalnością programu może być wymiana ukrytych zaszyfrowanych wiadomości oraz możliwość dołączania plików." + Environment.NewLine + "ver 1.0", "O Mnie");

        }

        private void dekoder_Click(object sender, EventArgs e)
        {
            try
            {

                extractedText = "";
                notesLabel3.Text = "Proszę czekać...";
                notesLabel3.ForeColor = Color.Black;

                błąd = 0;

                progressBar1.Value = 0;

               
                            


                if (radio_xor.Checked)
                {
                    xor = 1;
                    xor_aktywacja = true;

                }
                else
                {
                    xor = 0;
                    xor_aktywacja = false;

                }


                // kodowanie znaków

                if (radioASCII.Checked)
                {
                    kodowanie = "ASCII";
                }
                else if (radioUTF32.Checked)
                {
                    kodowanie = "UTF32";
                }
                else if (radioUTF16.Checked)
                {
                    kodowanie = "UTF16";
                }
                else if (radioUTF8.Checked)
                {
                    kodowanie = "UTF8";
                }

                // zapis znaków

                if (radio8bit.Checked)
                {
                    zapis = 8;
                }
                if (radio9bit.Checked)
                {
                    zapis = 10;
                }
                if (radio16bit.Checked)
                {
                    zapis = 16;
                }
                if (radio32bit.Checked)
                {
                    zapis = 32;
                }





                // wybór kanału

                if (red.Checked)
                {
                    indeks = "R";
                }
                if (green.Checked)
                {
                    indeks = "G";
                }
                if (blue.Checked)
                {
                    indeks = "B";
                }
                if (alfa.Checked)
                {
                    indeks = "A";
                }
                if (multi.Checked)
                {
                    indeks = "Multi";
                }

                this.timer1.Start(); // start timera


                if(element == 0) // Wariant z pliku - dekoder //////////////////////////////////////////////////////////////
                { 

                bmp = new Bitmap(obraz_dekodowany.Image);
                extractedText = Steganografia.dekodowanie(bmp, indeks, kodowanie, zapis, xor, xor_aktywacja);


                if (checkDekodowanie.Checked)
                {
                    try
                    {
                        extractedText = AES.Deszyfrowanie(extractedText, haslo2.Text);


                    }
                    catch
                    {
                        błąd = 1;
                        MessageBox.Show("Nieprawidłowe hasło spróbuj ponownie.", "Błąd !");
                        notesLabel3.ForeColor = Color.OrangeRed;
                        notesLabel3.Text = "Błąd ! Proszę spróbować ponownie.";

                        return;
                    }

                }

                }



                if (element == 1) // Wariant z folderu - dekoder ////////////////////////////////////////////////////////////////////////
                {
                    folder_bmp = new Bitmap[path.Count];
                    int licznik = 0;
                    string extractedText_folder = "";


                    foreach (var p in path)
                    {

                        folder_bmp[licznik] = new Bitmap(Image.FromFile(p.ToString()));

                        extractedText += "---- " + Path.GetFileName(p.ToString()) + " ---" + Environment.NewLine + "Treść znaku :" + Environment.NewLine;

                        extractedText_folder = Steganografia.dekodowanie(folder_bmp[licznik], indeks, kodowanie, zapis, xor, xor_aktywacja);
                       

                        if (checkDekodowanie.Checked)
                        {
                            try
                            {

                                extractedText_folder = AES.Deszyfrowanie(extractedText_folder, haslo2.Text);
                                extractedText += extractedText_folder + Environment.NewLine + Environment.NewLine + "-------------------------------------------------------" + Environment.NewLine + Environment.NewLine;


                            }
                            catch
                            {
                                błąd = 1;
                                MessageBox.Show("Nieprawidłowe hasło spróbuj ponownie.", "Błąd !");
                                notesLabel3.ForeColor = Color.OrangeRed;
                                notesLabel3.Text = "Błąd ! Proszę spróbować ponownie.";

                                return;
                            }


                        }
                        else
                        {
                            extractedText += extractedText_folder + Environment.NewLine + Environment.NewLine + "-------------------------------------------------------" + Environment.NewLine + Environment.NewLine;
                        }
                     



                        licznik++;
                    }


                }

            }
            catch (Exception ex2)
            {
                błąd = 1;

                MessageBox.Show(ex2.Message, "Błąd !");

                notesLabel3.ForeColor = Color.OrangeRed;
                notesLabel3.Text = "Błąd ! Proszę spróbować ponownie.";

                dekoder.Enabled = true;

            }









        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void dataTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void encryptCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Form();

            PictureBox podglad = new PictureBox();
            podglad.Dock = DockStyle.Fill;
            podglad.SizeMode = PictureBoxSizeMode.StretchImage;
            form.Controls.Add(podglad);
            podglad.Image = obraz_kodowany.Image;

            if (radioButton1.Checked == true) form.Text = dialog.FileName + " - Obraz " + radioButton1.Text + "    Rozdzielczość : " + Image.FromFile(dialog.FileName).Width + "x" + Image.FromFile(dialog.FileName).Height;
            if (koncowy.Checked == true) form.Text = dialog.FileName + " - Obraz " + koncowy.Text + "    Rozdzielczość : " + Image.FromFile(dialog.FileName).Width + "x" + Image.FromFile(dialog.FileName).Height;
            if (skompresowany.Checked == true) form.Text = dialog.FileName + " - Obraz " + skompresowany.Text + "    Rozdzielczość : " + Image.FromFile(dialog.FileName).Width + "x" + Image.FromFile(dialog.FileName).Height;
            if (zajetosc.Checked == true) form.Text = dialog.FileName + " - Wykorzystana pojemność  " + "    Rozdzielczość : " + Image.FromFile(dialog.FileName).Width + "x" + Image.FromFile(dialog.FileName).Height;


            form.Size = new Size(800, 600);



            form.Show();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new Form();

            PictureBox podglad = new PictureBox();
            podglad.Dock = DockStyle.Fill;
            podglad.SizeMode = PictureBoxSizeMode.StretchImage;
            form.Controls.Add(podglad);
            podglad.Image = obraz_dekodowany.Image;

            form.Text = dialog.FileName + "    Rozdzielczość : " + Image.FromFile(dialog.FileName).Width + "x" + Image.FromFile(dialog.FileName).Height;
            form.Size = new Size(800, 600);
            form.Show();

        }

        private void red_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void green_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void blue_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(3);


            if (progressBar1.Value == 100 && błąd == 0)
            {

                dataTextBox2.Text = extractedText;
                dataTextBox2.Refresh();
                openfolder.Enabled = true;
                notesLabel2.ForeColor = System.Drawing.Color.Black;
                notesLabel2.Text = "Komunikat :";
                notesLabel3.Text = " Pomyslnie odczytano znak wodny !";
                notesLabel3.ForeColor = Color.Green;
                timer1.Stop();
            }


        }

        private void progressBar1_Click_1(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.progressBar2.Increment(3);

            if (progressBar2.Value == 100 && text.Equals("") == false && błąd == 0)
            {
                komunikat2.Text = "Pomyslnie zakodowano !" + Environment.NewLine + "Nie zapomnij dokonać zapisu.";
                komunikat2.ForeColor = Color.Green;
                openfolder.Enabled = true;
                timer2.Stop();
                // button1.Enabled = true;
                koncowy.Enabled = true;


            }


        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioUTF8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radio8bit_CheckedChanged(object sender, EventArgs e)
        {
            zapis = 8;
            if (obraz_kodowany.Image != null)
            {
                Rozmiar();
                iloscznakow2.Text = dataTextBox.TextLength.ToString() + "/" + poj_znakow;

            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void radioUTF16_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radio_xor_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void komunikat2_Click(object sender, EventArgs e)
        {

        }

        private void notesLabel2_Click(object sender, EventArgs e)
        {

        }


        private void pokazObraz_skompresowany()
        {
            string file_name = Application.StartupPath + "\\__temp.jpg";


            if (obraz_kodowany.Image == null) return;






            long kompresja = long.Parse(comboKompresja.Text);
            zapiszJpg(OriginalImage, file_name, kompresja);

            if (obraz_kodowany.Image != null && skompresowany.Checked)
            {
                obraz_kodowany.Image.Dispose();
                obraz_kodowany.Image = null;
                obraz_kodowany.Image = LoadBitmapUnlocked(file_name);
                // OriginalImage = new Bitmap(imagePictureBox.Image);

            }









            FileInfo file_info = new FileInfo(file_name);
            lblFileSize.Text = file_info.Length.ToString() + " bajtów";
        }

        private Bitmap LoadBitmapUnlocked(string file_name)
        {
            using (Bitmap bm = new Bitmap(file_name))
            {
                return new Bitmap(bm);
            }
        }
        private void comboKompresja_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblCI.Text = comboKompresja.Text;


            if (openfolder.Selected != true) pokazObraz_skompresowany();

        }


        private void lblCI_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pokazObraz_skompresowany();

                wykorzystanie_red.Visible = false;
                label10.Visible = false;
            }
            catch (Exception ex)
            {

            }

        }

        private void radiojpg_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioBmp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            this.obraz_kodowany.Image = Image.FromStream(memoryStream);
            obraz_kodowany.Refresh();
            wykorzystanie_red.Visible = false;
            label10.Visible = false;


        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void savetool_Click(object sender, EventArgs e)
        {
            SaveFileDialog zapisz = new SaveFileDialog();
            FolderBrowserDialog folder = new FolderBrowserDialog();


            zapisz.Filter = "pliki JPEG (*.jpeg)|*.jpeg|pliki PNG (*.png)|*.png|pliki JPG (*.jpg)|*.jpg|pliki BMP(*.bmp)|*.bmp|Wszystkie pliki (*.*)|*.*";
            if (element == 0)
            {

                if (zapisz.ShowDialog() == DialogResult.OK)
                {
                    bmp.Save(zapisz.FileName);

                    komunikat.Text = "Komunikat :";
                    komunikat2.Text = " Pomyslnie zapisano.";
                    komunikat2.ForeColor = Color.Green; // poprawić
                }

            }
            if (element == 1)
            {
                int count = 0;

                if (folder.ShowDialog() == DialogResult.OK)  // OK
                {
                    foreach (var b in folder_bmp)
                    {


                        b.Save(folder.SelectedPath + "\\" + Path.GetFileName(path[count].ToString()));

                        count++;
                    }
                    komunikat.Text = "Komunikat :";
                    komunikat2.Text = " Pomyslnie zapisano.";
                    komunikat2.ForeColor = Color.Green; // poprawić
                }

            }

         
        }



        

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void radio16bit_CheckedChanged(object sender, EventArgs e)
        {
            zapis = 16;

            if (obraz_kodowany.Image != null)
            {
                Rozmiar();
                iloscznakow2.Text = dataTextBox.TextLength.ToString() + "/" + poj_znakow;

            }
        }

        private void radio32bit_CheckedChanged(object sender, EventArgs e)
        {
            zapis = 32;
            if (obraz_kodowany.Image != null)
            {
                Rozmiar();
                iloscznakow2.Text = dataTextBox.TextLength.ToString() + "/" + poj_znakow;

            }
        }

        private void pojemnosc_Click(object sender, EventArgs e)
        {

        }

        private void iloscznakow2_Click(object sender, EventArgs e)
        {
        }

        private void lupa_dekoder_Click(object sender, EventArgs e)
        {
            form2_ = new Form2(this);
            form2_.Show();
            form2_.Aktualizacja(dataTextBox2.Text, 1);
        }

        private void lupa_koder_Click(object sender, EventArgs e)
        {
            form2_ = new Form2(this);
            form2_.Show();
            form2_.Aktualizacja(dataTextBox.Text, 2);



          
        }

        private void button_folder_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog tekstDialog = new OpenFileDialog();
                tekstDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
                tekstDialog.Title = "Otwórz plik tekstowy który chcesz wykorzystać do wygenerowania znaku wodnego";

                DialogResult result = tekstDialog.ShowDialog();
                if (result == DialogResult.OK)
                {

                    dataTextBox.Text = File.ReadAllText(tekstDialog.FileName, Encoding.Default);
                }

               
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.ToString());
            }                
        }

        private void info_Click(object sender, EventArgs e)
        {
            try
            {

                Form3 form = new Form3();

                FileInfo info = new FileInfo(dialog.FileName);

                form.box.Items.Add(
                    "Nazwa pliku: " + info.Name);
                form.box.Items.Add(
                   "Rozmiar pliku: " + (float)info.Length/1000000 + " MB");
                form.box.Items.Add(
                   "Data ostatniego dostępu: " + info.LastAccessTime.ToShortDateString() + " " + info.LastAccessTime.ToShortTimeString());
                form.box.Items.Add(
                    "Data ostatniej modyfikacji: " + info.LastWriteTime.ToShortDateString()+ " " + info.LastWriteTime.ToShortTimeString());
                form.box.Items.Add(
                    "Rozszerzenie: " + info.Extension);
                form.box.Items.Add(
                    "Atrybuty: " + info.Attributes.ToString());
                form.box.Items.Add(
                    "Data utworzenia: " + info.CreationTime.ToShortDateString()+ " " + info.CreationTime.ToShortTimeString());
                form.box.Items.Add(
                   "Scieżka: " + info.Directory);
                form.box.Items.Add(
                   "Rozdzielczość: " + Image.FromFile(dialog.FileName).Width + "x" + Image.FromFile(dialog.FileName).Height);
               

                form.Aktualizacja();
                form.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Najpierw otwórz plik !", "Błąd !");
            }
            

        }

        private void radio10bit_CheckedChanged(object sender, EventArgs e)
        {
            zapis = 10;
            if (obraz_kodowany.Image != null)
            {
                Rozmiar();
                iloscznakow2.Text = dataTextBox.TextLength.ToString() + "/" + poj_znakow;

            }
        }

        private void zajetosc_CheckedChanged(object sender, EventArgs e)
        {
            if (multi.Checked == true) this.obraz_kodowany.Image = Pojemnosc.sprawdz_pojemnosc(new Bitmap(Image.FromFile(dialog.FileName)), dataTextBox.Text, zapis, true);
            else this.obraz_kodowany.Image = Pojemnosc.sprawdz_pojemnosc(new Bitmap(Image.FromFile(dialog.FileName)), dataTextBox.Text, zapis, false);


            //obraz_kodowany.Refresh();

            wykorzystanie_red.Visible = true;
            label10.Visible = true;



        }

        private void nosnik_Click(object sender, EventArgs e)
        {

        }

        private void logo_Click(object sender, EventArgs e)
        {

        }


       
        private void clear1_Click_1(object sender, EventArgs e)
        {
            dataTextBox.Clear();
        }

        private void clear2_Click(object sender, EventArgs e)
        {
            dataTextBox2.Clear();


        }

        private void multi_CheckedChanged(object sender, EventArgs e)
        {
            Rozmiar();
        }

        private void zapisz_Click(object sender, EventArgs e)
        {
                SaveFileDialog tekstDialog = new SaveFileDialog();
                tekstDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
                tekstDialog.Title = "Zapisz tekst znaku wodnego w wybranej lokalizacji";

                DialogResult result = tekstDialog.ShowDialog();

                if (result == DialogResult.OK)
                {

                    File.WriteAllText(tekstDialog.FileName, dataTextBox2.Text, Encoding.Default);

                }
        }

        private void radiojpeg_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioorg_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataTextBox_TextChanged(object sender, EventArgs e)
        {

            if (element != 1)
            {
                iloscznakow2.Text = dataTextBox.TextLength.ToString() + "/" + poj_znakow;
                dataTextBox.MaxLength = poj_znakow;
                Rozmiar();
            }




            if (dataTextBox.Text.Length >= poj_znakow && obraz_kodowany.Image != null && element != 1)
            {
                komunikat.Text = "Komunikat : ";
                komunikat2.Text = "Osiągnięto maksymalną ilość znaków !";
                komunikat2.ForeColor = Color.Red;
            }


            else
            {
                komunikat2.Text = "";
                komunikat2.ForeColor = Color.Black;
            }
        }

        private void otworz_plik_Click(object sender, EventArgs e)
        {
            try
            {


                OpenFileDialog plikDialog = new OpenFileDialog();
                plikDialog.Filter = "Wszystkie pliki (*.*)|*.*";
                plikDialog.Title = "Wskaż plik który chcesz dołączyć (zostanie skonwertowany do postaci tekstowej)";


                DialogResult result = plikDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string[] szyfr = Konwertowanie_pliku_na_tekst.Koder(Konwertowanie_pliku_na_tekst.konwersja_pliku_na_bajty(plikDialog.FileName), plikDialog.FileName);
                    dataTextBox.Text = Konwertowanie_pliku_na_tekst.Konwertuj_tablice_na_string(szyfr);

                    Rozmiar();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }          
        }

        private void zapisz_plik_Click(object sender, EventArgs e)
        {
            try
            {


                string[] tab = Konwertowanie_pliku_na_tekst.Konwertuj_string_na_tablice(dataTextBox2.Text);
                SaveFileDialog tekstDialog = new SaveFileDialog();
                tekstDialog.Title = "Zapisz plik w wybranej lokalizacji (zostanie wyodrebniony z postaci tekstowej)";
                tekstDialog.Filter = "Pliki (*" + Konwertowanie_pliku_na_tekst.nazwa_pliku + ")|*" + Konwertowanie_pliku_na_tekst.nazwa_pliku;


                DialogResult result = tekstDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                   Konwertowanie_pliku_na_tekst.konwersja_bajow_na_plik(tekstDialog.FileName , Konwertowanie_pliku_na_tekst.Dekoder(tab, Konwertowanie_pliku_na_tekst.nazwa_pliku));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Dane są nie własciwe lub uszkodzone !");
            }
        }

       





    } 
         

}
