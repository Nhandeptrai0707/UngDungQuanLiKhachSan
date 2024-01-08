using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void nhânToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dịchVụĂnUốngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            XtraUserControlMD XtraUserControlMD = new XtraUserControlMD();
            panelControl1.Controls.Add(XtraUserControlMD);
            XtraUserControlMD.Dock = DockStyle.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XtraUserControlMD XtraUserControlMD = new XtraUserControlMD();
            panelControl1.Controls.Add(XtraUserControlMD);
            XtraUserControlMD.Dock = DockStyle.Fill;
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            XtraUserControlTC XtraUserControlTC = new XtraUserControlTC();
            panelControl1.Controls.Add(XtraUserControlTC);
            XtraUserControlTC.Dock = DockStyle.Fill;
        }

        private void traCứuQuảnLíToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            XtraUserControlQL XtraUserControlQL = new XtraUserControlQL();
            panelControl1.Controls.Add(XtraUserControlQL);
            XtraUserControlQL.Dock = DockStyle.Fill;
        }

        private void dịchVụĂnUốngToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            XtraUserControlDVAU XtraUserControlDVAU = new XtraUserControlDVAU();
            panelControl1.Controls.Add(XtraUserControlDVAU);
            XtraUserControlDVAU.Dock = DockStyle.Fill;
        }

        private void dịchVụGiặtỦiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            XtraUserControlDVGU XtraUserControlDVGU = new XtraUserControlDVGU();
            panelControl1.Controls.Add(XtraUserControlDVGU);
            XtraUserControlDVGU.Dock = DockStyle.Fill;
        }

        private void dịchVụDiCHuyểnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            XtraUserControlDVDC XtraUserControlDVDC = new XtraUserControlDVDC();
            panelControl1.Controls.Add(XtraUserControlDVDC);
            XtraUserControlDVDC.Dock = DockStyle.Fill;
        }
    }
}
