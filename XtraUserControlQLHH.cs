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
    public partial class XtraUserControlQLHH : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlQLHH()
        {
            InitializeComponent();
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void XtraUserControlQLHH_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.DICHVUAUs;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DICHVUAU mh = new DICHVUAU();
            mh.MAMH = textBox2.Text;
            mh.TENMH = textBox3.Text;
            mh.GIAMH = int.Parse((textBox4.Text).ToString());
            DateTime ngaynhap = dateTimePicker1.Value;
            mh.NGAYNHAP = ngaynhap;
            data.DICHVUAUs.InsertOnSubmit(mh);
            data.GetChangeSet();
            data.SubmitChanges();
            dataGridView1.Update();
            MessageBox.Show("Thêm Thành Công", "Thông Báo");
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DICHVUAU mh = new DICHVUAU();
                mh.MAMH = textBox2.Text;
                mh.TENMH = textBox3.Text;
                mh.GIAMH = int.Parse((textBox4.Text).ToString());
                DateTime ngaynhap = dateTimePicker1.Value;
                mh.NGAYNHAP = ngaynhap;
                data.DICHVUAUs.DeleteOnSubmit(mh);
                data.GetChangeSet();
                data.SubmitChanges();
                dataGridView1.Update();
                MessageBox.Show("Xóa Thành Công", "Thông Báo");
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

            }
        }
    }
}
