using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace WindowsFormsApplication2
{
    public partial class XtraUserControlQLHD : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlQLHD()
        {
            InitializeComponent();
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void XtraUserControlQLHD_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.HOADONs;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var hd = (from HD in data.HOADONs
                      where HD.MAHD == textBox1.Text
                      select HD).FirstOrDefault();
            var ctphong = (from Ctphong in data.CHITIETPHONGs
                           where Ctphong.MAHD == textBox1.Text
                           select Ctphong).FirstOrDefault();
            HOADON hoadon = new HOADON();
            CHITIETPHONG ct = new CHITIETPHONG();
            ct = ctphong;
            data.CHITIETPHONGs.DeleteOnSubmit(ct);
            data.GetChangeSet();
            data.SubmitChanges();
            hoadon = hd;
            data.HOADONs.DeleteOnSubmit(hoadon);
            data.GetChangeSet();
            data.SubmitChanges();
            dataGridView1.DataSource = data.HOADONs;

        }
    }
}
