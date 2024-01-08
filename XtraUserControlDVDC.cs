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
    public partial class XtraUserControlDVDC : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlDVDC()
        {
            InitializeComponent();
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void XtraUserControlDVDC_Load(object sender, EventArgs e)
        {
            var pdt = (from PDT in data.PHONGs
                       where PDT.TRANGTHAI == "đang thuê"
                       select PDT).ToList();
            for (int i = 0; i < pdt.Count; i++)
            {
                comboBox1.Items.Add(pdt[i].SOPHONG);
            }
            var xe = (from XE in data.DICHVUDCs
                      select XE).ToList();
            for (int i = 0; i < xe.Count; i++)
            {
                ListViewItem item = new ListViewItem(xe[i].MADC);
                item.SubItems.Add(xe[i].TENPT.ToString());
                item.SubItems.Add(xe[i].TINHTRANGXE.ToString());
                item.SubItems.Add(xe[i].GIAPT.ToString());
                listView1.Items.Add(item);
            }
               
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ctphong = (from CTP in data.CHITIETPHONGs
                           where CTP.SOPHONG == comboBox1.SelectedItem.ToString() && CTP.NGAYTRAP ==null
                           select CTP).FirstOrDefault();
            var xedt = (from XEDT in data.CHITIETDCs
                        where XEDT.MAHD == ctphong.MAHD
                        select XEDT).ToList();
            listView2.Items.Clear();
            for (int i = 0; i < xedt.Count; i++)
            {
                ListViewItem item = new ListViewItem(xedt[i].MADC);
                var tenxe = (from TENXE in data.DICHVUDCs
                             where TENXE.MADC == xedt[i].MADC
                             select TENXE).FirstOrDefault();
                item.SubItems.Add(tenxe.TENPT);
                item.SubItems.Add(xedt[i].NGAYTHUE.Value.ToString());
                if (xedt[i].NGAYTRA == null)
                {
                    item.SubItems.Add("Chưa trả");
                }
                else
                {
                    item.SubItems.Add(xedt[i].NGAYTRA.Value.ToString());
                }
                item.SubItems.Add(xedt[i].TONGTIENDC.ToString());
                listView2.Items.Add(item);
            }
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) {
                if (comboBox1.SelectedItem == null) {
                    MessageBox.Show("Vui lòng Chọn Phòng", "Thông Báo");
                }else{
                    ListViewItem selectedItem = listView1.SelectedItems[0];
                    string column1Value = selectedItem.SubItems[0].Text;
                    var pt = (from PT in data.DICHVUDCs
                              where PT.MADC == column1Value
                              select PT).FirstOrDefault();
                    var ctphong = (from CTP in data.CHITIETPHONGs
                           where CTP.SOPHONG == comboBox1.SelectedItem.ToString() && CTP.NGAYTRAP == null
                           select CTP).FirstOrDefault();
                    if (pt.TINHTRANGXE == "còn trống")
                    {
                        pt.TINHTRANGXE = "đang thuê";
                        CHITIETDC ctdc = new CHITIETDC();
                        ctdc.MADC = pt.MADC;
                        ctdc.MAHD = ctphong.MAHD;
                        DateTime ngaythue = DateTime.Now;
                        ctdc.NGAYTHUE = ngaythue;
                        data.CHITIETDCs.InsertOnSubmit(ctdc);
                        data.SubmitChanges();
                        var xedt = (from XEDT in data.CHITIETDCs
                                    where XEDT.MAHD == ctphong.MAHD
                                    select XEDT).ToList();
                        listView2.Items.Clear();
                        for (int i = 0; i < xedt.Count; i++)
                        {
                            ListViewItem item = new ListViewItem(xedt[i].MADC);
                            var tenxe = (from TENXE in data.DICHVUDCs
                                         where TENXE.MADC == xedt[i].MADC
                                         select TENXE).FirstOrDefault();
                            item.SubItems.Add(tenxe.TENPT);
                            item.SubItems.Add(xedt[i].NGAYTHUE.Value.ToString());
                            if (xedt[i].NGAYTRA == null)
                            {
                                item.SubItems.Add("Chưa trả");
                            }
                            else {
                                item.SubItems.Add(xedt[i].NGAYTRA.Value.ToString());
                            }
                            item.SubItems.Add(xedt[i].TONGTIENDC.ToString());
                            listView2.Items.Add(item);
                        }
                    }
                    else {
                        MessageBox.Show("Xe đã được thuê", "Thông Báo");
                    }
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phòng");
            }
            else
            {
                var ctphong = (from CTP in data.CHITIETPHONGs
                               where CTP.SOPHONG == comboBox1.SelectedItem.ToString() && CTP.NGAYTRAP == null
                               select CTP).FirstOrDefault();
                ListViewItem selectedItem = new ListViewItem();
                selectedItem =  listView2.SelectedItems[0];
                string column1Value = selectedItem.SubItems[0].Text;
                var xe = (from XE in data.DICHVUDCs
                          where XE.MADC == column1Value
                          select XE).FirstOrDefault();
                var xedt = (from XEDT in data.CHITIETDCs
                            where XEDT.MAHD == ctphong.MAHD && XEDT.MADC == xe.MADC
                            select XEDT).FirstOrDefault();
                if (xedt.NGAYTRA == null)
                {
                    xedt.NGAYTRA = DateTime.Now;
                    TimeSpan songay = xedt.NGAYTRA.Value - xedt.NGAYTHUE.Value;
                    int sn = (int)songay.TotalDays;
                    if (sn == 0)
                    {
                        sn = 1;
                    }
                    xe.TINHTRANGXE = "còn trống";
                    xedt.TONGTIENDC = xe.GIAPT * sn;
                    data.SubmitChanges();
                    textBox2.Text = xedt.TONGTIENDC.ToString();
                }
                else {
                    MessageBox.Show("Xe đã được trả", "Thông Báo");
                }
                
            }
        }
    }
}
