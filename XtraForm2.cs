using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace WindowsFormsApplication2
{
    public partial class XtraForm2 : DevExpress.XtraEditors.XtraForm
    {
        private string sp;
        public XtraForm2()
        {
            InitializeComponent();
        }
        public XtraForm2(string sophong)
        {
            InitializeComponent();
            this.sp = sophong;
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn Có chắc Muốn thoát", "Thông Báo",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) {
                this.Close();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var kh = (from KH in data.KHACHHANGs
                      where KH.MAKH == textBox1.Text
                      select KH).FirstOrDefault();
            if (kh == null)
            {
                KHACHHANG khm = new KHACHHANG();
                khm.MAKH = textBox1.Text;
                khm.TENKH = textBox2.Text;
                khm.CCCDKH = textBox3.Text;
                khm.SDTKH = textBox4.Text;
                khm.NGAYSINHKH = dateTimePicker1.Value;
                if (radioButton1.Checked == true)
                {
                    khm.GIOITINH = true;

                }
                else
                {
                    khm.GIOITINH = false;
                }
                data.KHACHHANGs.InsertOnSubmit(khm);
                data.GetChangeSet();
                data.SubmitChanges();
                var hd = (from HD in data.HOADONs
                          select HD).ToList();
                var nv = (from NV in data.NHANVIENs
                          where NV.MANV == "1"
                          select NV).FirstOrDefault();
                HOADON hoadon = new HOADON();
                hoadon.MAHD = hd.Count.ToString();
                hoadon.MANV = nv.MANV;
                hoadon.MAKH = khm.MAKH;
                hoadon.TONGTIENHD = 0;
                data.HOADONs.InsertOnSubmit(hoadon);
                data.SubmitChanges();
                data.GetChangeSet();
                CHITIETPHONG ctphong = new CHITIETPHONG();
                ctphong.SOPHONG = sp;
                ctphong.MAHD = hoadon.MAHD;
                DateTime ngaynhan = DateTime.Now;
                ctphong.NGAYNHANP = ngaynhan;
                data.CHITIETPHONGs.InsertOnSubmit(ctphong);
                var phong = (from p in data.PHONGs
                             where p.SOPHONG == sp
                             select p).FirstOrDefault();
                phong.TRANGTHAI = "đang thuê";
                data.SubmitChanges();
                data.GetChangeSet();
                this.Close();

            }
            else {
                KHACHHANG khc = new KHACHHANG();
                khc = kh;
                var hd = (from HD in data.HOADONs
                          select HD).ToList();
                var nv = (from NV in data.NHANVIENs
                          where NV.MANV == "1"
                          select NV).FirstOrDefault();
                HOADON hoadon = new HOADON();
                hoadon.MAHD = hd.Count.ToString();
                hoadon.MANV = nv.MANV;
                hoadon.MAKH = khc.MAKH;
                hoadon.TONGTIENHD = 0;
                data.HOADONs.InsertOnSubmit(hoadon);
                data.SubmitChanges();
                data.GetChangeSet();
                CHITIETPHONG ctphong = new CHITIETPHONG();
                ctphong.SOPHONG = sp;
                ctphong.MAHD = hoadon.MAHD;
                DateTime ngaynhan = DateTime.Now;
                ctphong.NGAYNHANP = ngaynhan;
                data.CHITIETPHONGs.InsertOnSubmit(ctphong);
                var phong = (from p in data.PHONGs
                             where p.SOPHONG == sp
                             select p).FirstOrDefault();
                phong.TRANGTHAI = "đang thuê";
                data.SubmitChanges();
                data.GetChangeSet();
                this.Close();
            }

            

        }
    }
}