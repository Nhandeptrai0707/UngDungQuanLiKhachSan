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
    public partial class XtraUserControlQLNV : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlQLNV()
        {
            InitializeComponent();
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void XtraUserControlQLNV_Load(object sender, EventArgs e){
            var nhanvien = from nv in data.NHANVIENs
                           select nv;
            dataGridView1.DataSource = nhanvien;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NHANVIEN nhanvien = new NHANVIEN();
            nhanvien.MANV = textBox7.Text;
            nhanvien.TENNV = textBox2.Text;
            DateTime ngaysinh = dateTimePicker1.Value;
            nhanvien.NGAYSINHNV = ngaysinh;
            if (checkBox1.Checked == true) {
                nhanvien.GIOITINH = true;
            }
            else if (checkBox2.Checked == true) {
                nhanvien.GIOITINH = false;
            }
            nhanvien.CCCD = textBox3.Text;
            nhanvien.SDT = textBox4.Text;
            nhanvien.CHUCVU = textBox5.Text;
            nhanvien.DIACHI = textBox6.Text;
            data.NHANVIENs.InsertOnSubmit(nhanvien);
            data.SubmitChanges();
            data.GetChangeSet();
            dataGridView1.Update();
            MessageBox.Show("Thêm Thành Công", "Thông Báo");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var nhanvien = (from nv in data.NHANVIENs
                              where  nv.MANV == textBox7.Text
                              select nv).FirstOrDefault();

                NHANVIEN nhanvienx = nhanvien;
                data.NHANVIENs.DeleteOnSubmit(nhanvienx);
                data.GetChangeSet();
                data.SubmitChanges();
                dataGridView1.Update();
                MessageBox.Show("Xóa Thành Công", "Thông Báo");
                textBox7.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nhanvient = (from nv in data.NHANVIENs
                             where nv.TENNV == textBox1.Text || nv.MANV == textBox1.Text
                             select nv).FirstOrDefault();
            NHANVIEN nhanvien = nhanvient;
            if (nhanvien == null)
            {
                MessageBox.Show("Không có nhân viên", "thông báo");
            }
            else {
                textBox7.Text = nhanvien.MANV.ToString();
                textBox2.Text = nhanvien.TENNV.ToString();
                DateTime ngaysinh = nhanvien.NGAYSINHNV.Value;
                dateTimePicker1.Value = ngaysinh;
                if (nhanvien.GIOITINH == true)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox2.Checked = true;
                }
                textBox3.Text = nhanvien.CCCD.ToString();
                textBox4.Text = nhanvien.SDT.ToString();
                textBox5.Text = nhanvien.CHUCVU.ToString();
                textBox6.Text = nhanvien.DIACHI.ToString();
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            NHANVIEN nhanviens = new NHANVIEN();

            var nhanvien = (from nv in data.NHANVIENs
                     where nv.MANV == textBox7.Text
                     select nv).FirstOrDefault();
            nhanviens = nhanvien;
            nhanviens.TENNV = textBox2.Text;
            DateTime ngaysinh = dateTimePicker1.Value;
            nhanviens.NGAYSINHNV = ngaysinh;
            if (checkBox1.Checked == true)
            {
                nhanviens.GIOITINH = true;
            }
            else
            {
                nhanviens.GIOITINH = false;
            }
            nhanviens.CCCD = textBox3.Text;
            nhanviens.SDT = textBox4.Text;
            nhanviens.CHUCVU = textBox5.Text;
            nhanviens.DIACHI = textBox6.Text;
            data.GetChangeSet();
            data.SubmitChanges();
            dataGridView1.Update();
            MessageBox.Show("Sửa Thành Công", "Thông Báo");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }
    }
}
