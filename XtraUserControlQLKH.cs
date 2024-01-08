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
    public partial class XtraUserControlQLKH : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlQLKH()
        {
            InitializeComponent();
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void XtraUserControlQLKH_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.KHACHHANGs;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            KHACHHANG khachhang = new KHACHHANG();
            khachhang.MAKH = textBox1.Text;
            khachhang.TENKH = textBox3.Text;
            khachhang.CCCDKH = textBox4.Text;
            khachhang.SDTKH = textBox5.Text;
            DateTime ngaysinhkh = dateTimePicker1.Value;
            khachhang.NGAYSINHKH = ngaysinhkh;
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Vui lòng nhập giới tính", "Thông Báo");
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    khachhang.GIOITINH = true;
                }
                else
                {
                    khachhang.GIOITINH = false;
                }
            }
            data.KHACHHANGs.InsertOnSubmit(khachhang);
            data.GetChangeSet();
            data.SubmitChanges();
            MessageBox.Show("Thêm Thành Công", "Thông báo");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                var kh = (from mkh in data.KHACHHANGs
                          where mkh.MAKH == textBox1.Text
                          select mkh).FirstOrDefault();
                if (kh == null)
                {
                    MessageBox.Show("Không có khách hàng", "Thông báo");
                }
                else
                {
                    KHACHHANG khach = new KHACHHANG();
                    khach = kh;
                    data.KHACHHANGs.DeleteOnSubmit(kh);
                    data.GetChangeSet();
                    data.SubmitChanges();
                    
                }
                
               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var mkh = (from kh in data.KHACHHANGs
                       where kh.MAKH == textBox2.Text
                       select kh).FirstOrDefault();
            if (textBox2.Text == ""||mkh == null)
            {
                MessageBox.Show("Vui lòng nhập căn cước khách hàng", "Thông báo");
            }
            else {
                
                textBox1.Text = mkh.MAKH.ToString();
                textBox3.Text = mkh.TENKH.ToString();
                textBox4.Text = mkh.CCCDKH.ToString();
                textBox5.Text = mkh.SDTKH.ToString();
                dateTimePicker1.Value = mkh.NGAYSINHKH.Value;
                if (mkh.GIOITINH == true) {
                    radioButton1.Checked =true;
                }else{
                    radioButton2.Checked = true;
               }
            }
           
        }
    }
}
