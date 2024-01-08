using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace WindowsFormsApplication2
{
    public partial class XtraUserControlQL : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlQL()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            XtraUserControlQLNV XtraUserControlQLNV = new XtraUserControlQLNV();
            panel1.Controls.Add(XtraUserControlQLNV);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            XtraUserControlQLP XtraUserControlQLP = new XtraUserControlQLP();
            panel1.Controls.Add(XtraUserControlQLP);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            XtraUserControlQLHH XtraUserControlQLHH = new XtraUserControlQLHH();
            panel1.Controls.Add(XtraUserControlQLHH);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            XtraUserControlQLKH XtraUserControlQLKH = new XtraUserControlQLKH();
            panel1.Controls.Add(XtraUserControlQLKH);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            XtraUserControlQLHD XtraUserControlQLHD = new XtraUserControlQLHD();
            panel1.Controls.Add(XtraUserControlQLHD);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            XtraUserControlQLGU XtraUserControlQLGU = new XtraUserControlQLGU();
            panel1.Controls.Add(XtraUserControlQLGU);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            XtraUserControlQLDC XtraUserControlQLDC = new XtraUserControlQLDC();
            panel1.Controls.Add(XtraUserControlQLDC);
        }
    }
}
