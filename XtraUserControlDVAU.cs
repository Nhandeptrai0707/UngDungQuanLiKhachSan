using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Collections;
using System.Threading;

namespace WindowsFormsApplication2
{
    public partial class XtraUserControlDVAU : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlDVAU()
        {
            InitializeComponent();
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        public int? tongtien = 0;
        private void XtraUserControlDVAU_Load(object sender, EventArgs e)
        {
            var p = (from P in data.PHONGs
                     where P.TRANGTHAI == "đang thuê"
                     select P).ToList();
            for (int i = 0; i < p.Count; i++) {
                comboBox1.Items.Add(p[i].SOPHONG);
            }
                
            var mh = (from MH in data.DICHVUAUs 
                      select MH).ToList();
            for (int i = 0; i < mh.Count; i++)
            {
                ListViewItem item = new ListViewItem(mh[i].TENMH);
                item.SubItems.Add(mh[i].GIAMH.ToString());
                item.SubItems.Add(mh[i].NGAYNHAP.ToString());
                listView1.Items.Add(item);
            }
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phòng", "Thông báo");
               
            }
            else
            {
                var pdc = (from PDC in data.CHITIETPHONGs
                           where PDC.SOPHONG == comboBox1.SelectedText
                           select PDC).FirstOrDefault();
                var ctau = (from CTAU in data.CHITIETAUs
                            where CTAU.MAHD == pdc.MAHD
                            select CTAU).ToList();
                for (int i = 0; i < ctau.Count; i++)
                {
                    ListViewItem item = new ListViewItem(ctau[i].MAMH);
                    item.SubItems.Add(ctau[i].SOLUONG.ToString());
                    item.SubItems.Add(ctau[i].SOLUONG.ToString());
                    listView2.Items.Add(item);
                }
            }
            
            

        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên mặt hàng", "Thông báo");
                listView1.Items.Clear();
                var mh = (from MH in data.DICHVUAUs
                          select MH).ToList();
                for (int i = 0; i < mh.Count; i++)
                {
                    ListViewItem item = new ListViewItem(mh[i].TENMH);
                    item.SubItems.Add(mh[i].GIAMH.ToString());
                    item.SubItems.Add(mh[i].NGAYNHAP.ToString());
                    listView1.Items.Add(item);
                }
            }
            else {
                var mh = (from MH in data.DICHVUAUs
                          where MH.TENMH == textBox3.Text
                          select MH).FirstOrDefault();

                if (mh == null)
                {
                    MessageBox.Show("Không tìm thấy" + textBox3.Text, "thông báo");
                    listView1.Items.Clear();
                    var mh1 = (from MH in data.DICHVUAUs
                              select MH).ToList();
                    for (int i = 0; i < mh1.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(mh1[i].TENMH);
                        item.SubItems.Add(mh1[i].GIAMH.ToString());
                        item.SubItems.Add(mh1[i].NGAYNHAP.ToString());
                        listView1.Items.Add(item);
                    }
                }
                else {
                    listView1.Items.Clear();
                    ListViewItem item = new ListViewItem(mh.TENMH);
                    item.SubItems.Add(mh.GIAMH.ToString());
                    item.SubItems.Add(mh.NGAYNHAP.ToString());
                    listView1.Items.Add(item);
                }
            }
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            if (listView1.SelectedItems.Count > 0)
            {
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng", "Thông báo");
                }
                else {
                    var pdc = (from PDC in data.CHITIETPHONGs
                               where PDC.SOPHONG == comboBox1.SelectedItem.ToString() && PDC.NGAYTRAP == null
                               select PDC).FirstOrDefault();
                    
                    ListViewItem selectedItem = listView1.SelectedItems[0];

                    string column1Value = selectedItem.SubItems[0].Text;
                    var mh = (from MH in data.DICHVUAUs
                              where MH.TENMH == column1Value
                              select MH).FirstOrDefault();
                    CHITIETAU ctau = new CHITIETAU();
                    ctau.MAHD = pdc.MAHD;
                    ctau.MAMH = mh.MAMH;
                    ctau.SOLUONG = 1;
                    ctau.TONGTIENAU = mh.GIAMH * ctau.SOLUONG;
                    var ctau2 = (from CTAU in data.CHITIETAUs
                                where CTAU.MAHD == ctau.MAHD && CTAU.MAMH == ctau.MAMH
                                select CTAU).FirstOrDefault();
                    if (ctau2 == null)
                    {
                        ctau.TONGTIENAU = ctau.SOLUONG * mh.GIAMH;
                        data.CHITIETAUs.InsertOnSubmit(ctau);
                        data.SubmitChanges();
                    }
                    else {
                       
                        ctau2.SOLUONG = ctau2.SOLUONG + 1;
                        ctau2.TONGTIENAU = ctau2.SOLUONG * mh.GIAMH;
                        data.SubmitChanges();
                    }
                    
                    var pdc1 = (from PDC in data.CHITIETPHONGs
                               where PDC.SOPHONG == comboBox1.SelectedText
                               select PDC).FirstOrDefault();
                    var ctau1 = (from CTAU in data.CHITIETAUs
                                where CTAU.MAHD == pdc.MAHD
                                select CTAU).ToList();
                    listView2.Items.Clear();
                    for (int i = 0; i < ctau1.Count; i++)
                    {
                        var mh2 = (from MH in data.DICHVUAUs
                                   where MH.MAMH == ctau1[i].MAMH
                                   select MH).FirstOrDefault();
                        ListViewItem item = new ListViewItem(mh2.TENMH);
                        item.SubItems.Add(ctau1[i].SOLUONG.ToString());
                        item.SubItems.Add(mh2.GIAMH.ToString());
                        listView2.Items.Add(item);
                    }
                    var tt = (from TT in data.CHITIETAUs
                             where TT.MAHD == pdc.MAHD
                             select TT).ToList();
                    
                     for (int i = 0; i < tt.Count; i++)
                     {

                            tongtien = tongtien + tt[i].TONGTIENAU;
                     }
                     textBox2.Text = tongtien.ToString();
                    
                    
                }
               
            }
            
            
            
            
        }
  

        private void button1_Click(object sender, EventArgs e)
        {
            


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems != null&&comboBox1.SelectedItem != null)
            {
                var pdc = (from PDC in data.CHITIETPHONGs
                           where PDC.SOPHONG == comboBox1.SelectedItem.ToString() && PDC.NGAYTRAP == null
                           select PDC).FirstOrDefault();
                ListViewItem selectedItem = listView2.SelectedItems[0];

                string column1Value = selectedItem.SubItems[0].Text;
                var mh = (from MH in data.DICHVUAUs
                          where MH.TENMH == column1Value
                          select MH).FirstOrDefault();
                var ctau = (from CTAU in data.CHITIETAUs
                            where CTAU.MAMH == mh.MAMH && CTAU.MAHD == pdc.MAHD
                            select CTAU).FirstOrDefault();
                ctau.SOLUONG = ctau.SOLUONG - 1;
                data.SubmitChanges();
                var ctau1 = (from CTAU in data.CHITIETAUs
                             where CTAU.MAHD == pdc.MAHD
                             select CTAU).ToList();
                listView2.Items.Clear();
                for (int i = 0; i < ctau1.Count; i++)
                {
                    var mh2 = (from MH in data.DICHVUAUs
                               where MH.MAMH == ctau1[i].MAMH
                               select MH).FirstOrDefault();
                    ListViewItem item = new ListViewItem(mh2.TENMH);
                    item.SubItems.Add(ctau1[i].SOLUONG.ToString());
                    item.SubItems.Add(mh2.GIAMH.ToString());
                    listView2.Items.Add(item);
                }
            }
            else {
                MessageBox.Show("Vui lòng chọn mặt hàng cần giảm số lượng", "Thông Báo");
            }
            
        }

        
    }
}
