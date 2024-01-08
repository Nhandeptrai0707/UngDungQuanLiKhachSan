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
    public partial class XtraUserControlQLGU : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlQLGU()
        {
            InitializeComponent();
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void XtraUserControlQLGU_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.DICHVUGUs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var giatui = (from GU in data.DICHVUGUs
                          where GU.MAGU == textBox1.Text
                          select GU).FirstOrDefault();
            if (giatui != null)
            {
                MessageBox.Show("Dịch vụ đã tồn tại", "Thông báo");
            }
            else
            {
                DICHVUGU gu = new DICHVUGU();
                gu.MAGU = textBox1.Text;
                gu.LOAIGU = textBox2.Text;
                gu.GIAGU = int.Parse(textBox3.Text);
                data.DICHVUGUs.InsertOnSubmit(gu);
                data.SubmitChanges();
                data.GetChangeSet();
                dataGridView1.DataSource = data.DICHVUGUs;
            }
            
            
        }
    }
}
