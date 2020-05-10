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
    public partial class FormMain : Form
    {
        formDiem fDiem;
        formGV fGV;
        formLopHC fLopHC;
        formLopTC fLopTC;
        formMonHoc fMonHoc;
        formSV fSV;
        baocao fbc;
        public FormMain()
        {
            InitializeComponent();

            fDiem = new formDiem();     
            fGV = new formGV();         
            fLopHC = new formLopHC();   
            fLopTC = new formLopTC();   
            fMonHoc = new formMonHoc(); 
            fSV = new formSV();
            fbc = new baocao();

            fDiem.MdiParent = this; fDiem.Dock = DockStyle.Fill;
           
            fGV.MdiParent = this;fGV.Dock = DockStyle.Fill;
            
            fLopHC.MdiParent = this;  fLopHC.Dock = DockStyle.Fill;
          
            fLopTC.MdiParent = this; fLopTC.Dock = DockStyle.Fill;

            fMonHoc.MdiParent = this; fMonHoc.Dock = DockStyle.Fill;

            fSV.MdiParent = this; fSV.Dock = DockStyle.Fill;

            fbc.MdiParent = this; fbc.Dock = DockStyle.Fill;

            setAllItemMenuLeftColorDefault();
            hideAllForm();
        }
        private void hideAllForm()
        {
            fDiem.Hide();
            fGV.Hide();
            fLopHC.Hide();
            fLopTC.Hide();
            fMonHoc.Hide();
            fSV.Hide();
            fbc.Hide();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_MouseHover(object sender, EventArgs e)
        {
            ((Panel)sender).BackColor = Color.FromArgb(8, 142, 158);
        }

        private void panel6_MouseLeave(object sender, EventArgs e)
        {
            if(itemMenuDiemClicked==false)
            ((Panel)sender).BackColor = Color.FromArgb(64, 186, 201);
        }
        //--------------------
        private void Item_PanelDiem_MouseHover(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(8, 142, 158);
        }
        private void Item_PanelDiem_Leave(object sender, EventArgs e)
        {
            if(itemMenuDiemClicked==false)
            panel6.BackColor = Color.FromArgb(64, 186, 201);
        }
        //--------------------
        private void Item_PanelGV_MouseHover(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(8, 142, 158);
        }
        private void Item_PanelGV_Leave(object sender, EventArgs e)
        {
            if(itemMenuGVClicked==false)
            panel2.BackColor = Color.FromArgb(64, 186, 201);
        }
        //--------------------
        private void Item_PanelLopHC_MouseHover(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(8, 142, 158);
        }
        private void Item_PanelLopHC_Leave(object sender, EventArgs e)
        {
            if(itemMenuLopHCClicked==false)
            panel8.BackColor = Color.FromArgb(64, 186, 201);
        }
        //--------------------
        private void Item_PanelLopTC_MouseHover(object sender, EventArgs e)
        {
            panel10.BackColor = Color.FromArgb(8, 142, 158);
        }
        private void Item_PanelLopTC_Leave(object sender, EventArgs e)
        {
            if(itemMenuTCClicked==false)
            panel10.BackColor = Color.FromArgb(64, 186, 201);
        }
        //--------------------
        private void Item_PanelMonHoc_MouseHover(object sender, EventArgs e)
        {
            panel12.BackColor = Color.FromArgb(8, 142, 158);
        }
        private void Item_PanelMonHoc_Leave(object sender, EventArgs e)
        {
            if(itemMenuMonHocClicked==false)
            panel12.BackColor = Color.FromArgb(64, 186, 201);
        }
        //--------------------
        private void Item_PanelSV_MouseHover(object sender, EventArgs e)
        {
            panel14.BackColor = Color.FromArgb(8, 142, 158);
        }
        private void Item_PanelSV_Leave(object sender, EventArgs e)
        {
            if(itemMenuSVClicked==false)
            panel14.BackColor = Color.FromArgb(64, 186, 201);
           
        }

        bool itemMenuDiemClicked, itemMenuGVClicked, itemMenuLopHCClicked, itemMenuTCClicked, itemMenuMonHocClicked, itemMenuSVClicked;

        private void setAllItemMenuLeftColorDefault()
        {
            panel14.BackColor = Color.FromArgb(64, 186, 201);
            panel12.BackColor = Color.FromArgb(64, 186, 201);
            panel10.BackColor = Color.FromArgb(64, 186, 201);
            panel8.BackColor = Color.FromArgb(64, 186, 201);
            panel6.BackColor = Color.FromArgb(64, 186, 201);
            panel2.BackColor = Color.FromArgb(64, 186, 201);

            itemMenuDiemClicked = false;
            itemMenuGVClicked = false;
            itemMenuLopHCClicked = false;
            itemMenuTCClicked = false;
            itemMenuMonHocClicked = false;
            itemMenuSVClicked = false;
        }
        private void panel6_Click(object sender, EventArgs e)
        {
            //4, 107, 120 màu khi click
            setAllItemMenuLeftColorDefault();
            panel6.BackColor = Color.FromArgb(4, 107, 120);
            hideAllForm();
            itemMenuDiemClicked = true;
            fDiem.Show();
           
   
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            hideAllForm();           
            fbc.Show();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            panel2.BackColor = Color.FromArgb(4, 107, 120);
            hideAllForm();
            itemMenuGVClicked = true;
            fGV.Show();
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            panel8.BackColor = Color.FromArgb(4, 107, 120);
            hideAllForm();
            itemMenuLopHCClicked = true;
            fLopHC.Show();
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            panel10.BackColor = Color.FromArgb(4, 107, 120);
            hideAllForm();
            itemMenuTCClicked = true;
            fLopTC.Show();
        }

        private void panel12_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            panel12.BackColor = Color.FromArgb(4, 107, 120);
            hideAllForm();
            itemMenuMonHocClicked = true;
            fMonHoc.Show();
        }

        private void panel14_Click(object sender, EventArgs e)
        {
            setAllItemMenuLeftColorDefault();
            panel14.BackColor = Color.FromArgb(4, 107, 120);
            hideAllForm();
            itemMenuSVClicked = true;
            fSV.Show();
        }
    }
}
