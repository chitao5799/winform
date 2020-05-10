using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL
{
    public partial class loading : Form
    {
        public loading()
        {
            InitializeComponent();
            //this.BackColor = Color.Purple;
            //this.TransparencyKey = Color.Purple;

            //this.BackColor = Color.Black;
            //this.TransparencyKey = Color.Black;

            //panel1.BackColor = Color.FromArgb(0,255,255,255);
            //this.TransparencyKey = Color.FromArgb(0, 255, 255, 255);
            //System.Threading.Thread.Sleep(3000);//Thread.Sleep(2000);
            timer1.Start();

        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
            if (progressBar1.Value >= progressBar1.Maximum)
            {
                this.Close();
            }
        }
    }
}
