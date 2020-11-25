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
    public partial class Form3 : Form
    {
        public ListBox box = new ListBox();
        public Form3()
        {
            InitializeComponent();
        }

        public void Aktualizacja()
        {
            int i = 0;

            foreach(var b in box.Items)
            {

                listBox1.Items.Add(box.Items[i].ToString());
                i++;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
