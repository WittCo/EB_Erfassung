using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WittEyE
{
    public partial class Bilder_AP3 : Form
    {
        public Bilder_AP3()
        {
            InitializeComponent();
        }

        Timer formCloser = new Timer();

     

      
        private void Bilder_AP3_Load(object sender, EventArgs e)
        {
            formCloser.Interval = 3000;
            formCloser.Enabled = true;
            formCloser.Tick += new EventHandler(formClose_Tick);
        }

        private void formClose_Tick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
