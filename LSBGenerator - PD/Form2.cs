using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSBGenerator___PD
{
    public partial class Form2 : Form
    {
        int tryb_ = 0;

        Form1 form1;


        public Form2(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();

        }

        public void Aktualizacja(string tekst, int tryb)
        {
            lupa_tekst.Text = tekst;
            lupa_tekst.Refresh();

            tryb_ = tryb;

        }

        private void lupa_tekst_TextChanged(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
         
            form1.Aktualizacja2(lupa_tekst.Text, tryb_);
           

            this.Close();
            

        }
    }
}
