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
    public partial class XtraUserControlQLP : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlQLP()
        {
            InitializeComponent();
        }
        private DataClasses1DataContext data = new DataClasses1DataContext();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void XtraUserControlQLP_Load(object sender, EventArgs e)
        {
            var sophong = (from p in data.PHONGs
                           select p);
            
            dataGridView1.DataSource = sophong;
            dataGridView1.Columns[0].HeaderText = "Số Phòng";
            dataGridView1.Columns[1].HeaderText = "Trạng Thái";
            dataGridView1.Columns[2].HeaderText = "Loại Phòng";
            dataGridView1.Columns[3].HeaderText = "Giá Phòng";
            dataGridView1.Columns[4].HeaderText = "Số Giường";

            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sophong = (from p in data.PHONGs
                           select p.SOPHONG).ToList();
            var loaiphong = (from p in data.PHONGs
                           select p.LOAIPHONG).ToList();
            var sogiuong = (from p in data.PHONGs
                           select p.SOGIUONG).ToList();
            var trangthai = (from p in data.PHONGs
                           select p.TRANGTHAI).ToList();
            var gia = (from p in data.PHONGs
                           select p.GIAP).ToList();
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("mời bạn nhập", "Thông báo");
            }
            else {
                for (int i = 0; i < sophong.Count; i++) {
                    if (textBox1.Text == sophong[i].ToString()) {
                        textBox2.Text = sophong[i].ToString();
                        textBox3.Text = loaiphong[i].ToString();
                        textBox4.Text = sogiuong[i].ToString();
                        textBox5.Text = trangthai[i].ToString();
                        textBox7.Text = gia[i].ToString();
                    }
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PHONG p = new PHONG();
            p.SOPHONG = textBox2.Text;
            p.LOAIPHONG = textBox3.Text;
            p.SOGIUONG = textBox4.Text;
            p.TRANGTHAI = textBox5.Text;
            p.GIAP = int.Parse((textBox7.Text).ToString());
            data.PHONGs.InsertOnSubmit(p);
            data.GetChangeSet();
            data.SubmitChanges();
            dataGridView1.Update();
            MessageBox.Show("Thêm Thành Công", "Thông Báo");
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
            var sophong = (from phong in data.PHONGs
                           select phong);

            dataGridView1.DataSource = sophong;
            dataGridView1.Columns[0].HeaderText = "Số Phòng";
            dataGridView1.Columns[1].HeaderText = "Trạng Thái";
            dataGridView1.Columns[2].HeaderText = "Loại Phòng";
            dataGridView1.Columns[3].HeaderText = "Giá Phòng";
            dataGridView1.Columns[4].HeaderText = "Số Giường";                                                    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PHONG phong = new PHONG();

            phong = (from p in data.PHONGs
                     where p.SOPHONG == textBox2.Text
                     select p).FirstOrDefault();
            phong.LOAIPHONG = textBox3.Text;
            phong.SOGIUONG = textBox4.Text;
            phong.TRANGTHAI = textBox5.Text;
            phong.GIAP = int.Parse(textBox7.Text);
            data.GetChangeSet();
            data.SubmitChanges();
            dataGridView1.Update();
            MessageBox.Show("Sửa Thành Công", "Thông Báo");
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
            var sophong = (from p in data.PHONGs
                           select p);

            dataGridView1.DataSource = sophong;
            dataGridView1.Columns[0].HeaderText = "Số Phòng";
            dataGridView1.Columns[1].HeaderText = "Trạng Thái";
            dataGridView1.Columns[2].HeaderText = "Loại Phòng";
            dataGridView1.Columns[3].HeaderText = "Giá Phòng";
            dataGridView1.Columns[4].HeaderText = "Số Giường";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var phong1 = (from p in data.PHONGs
                             where p.SOPHONG == textBox2.Text
                             select p).FirstOrDefault();
                PHONG phong = phong1;
                data.PHONGs.DeleteOnSubmit(phong);
                data.GetChangeSet();
                data.SubmitChanges();
                dataGridView1.Update();
                MessageBox.Show("Xóa Thành Công", "Thông Báo");
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox7.Clear();
                var sophong = (from p in data.PHONGs
                               select p);

                dataGridView1.DataSource = sophong;
                dataGridView1.Columns[0].HeaderText = "Số Phòng";
                dataGridView1.Columns[1].HeaderText = "Trạng Thái";
                dataGridView1.Columns[2].HeaderText = "Loại Phòng";
                dataGridView1.Columns[3].HeaderText = "Giá Phòng";
                dataGridView1.Columns[4].HeaderText = "Số Giường";
                
            }
        }
    }
}
