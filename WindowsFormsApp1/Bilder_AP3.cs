using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WittEyE
{
    public partial class Bilder_AP3 : Form
    {
        public Bilder_AP3()
        {
            InitializeComponent();


          

            //pictureBox1.Image = Image.FromFile(filePaths[0]);
           // pictureBox2.Image = Image.FromFile(filePaths[1]);
           // pictureBox3.Image = Image.FromFile(filePaths[2]);
           // pictureBox4.Image = Image.FromFile(filePaths[3]);
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

        private void LoadBild()
        {
            string destPath = @"C:\Users\Aufschrauberportal\AWICO\AP3\";

            string[] filePaths = Directory.GetFiles(destPath, "*.jpg",
                                           SearchOption.TopDirectoryOnly);
            pictureBox1.Image = Image.FromFile(filePaths[0]);
            pictureBox2.Image = Image.FromFile(filePaths[1]);
            pictureBox3.Image = Image.FromFile(filePaths[2]);
            pictureBox4.Image = Image.FromFile(filePaths[3]);



        }

        private void Bilder_AP3_Shown(object sender, EventArgs e)
        {
            LoadBild();
        }
    }
}
