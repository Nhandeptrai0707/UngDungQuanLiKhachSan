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
    public partial class XtraUserControlDVGU : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlDVGU()
        {
            InitializeComponent();
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void XtraUserControlDVGU_Load(object sender, EventArgs e)
        {
            var phong = (from P in data.PHONGs
                         where P.TRANGTHAI == "đang thuê"
                         select P).ToList();
            for (int i = 0; i < phong.Count; i++)
            {
                comboBox1.Items.Add(phong[i].SOPHONG);
            }
            var gu = (from GU in data.DICHVUGUs
                      select GU).ToList();
            for (int i = 0; i < gu.Count; i++)
            {
                ListViewItem item = new ListViewItem(gu[i].LOAIGU);
                item.SubItems.Add(gu[i].GIAGU.ToString());
                listView1.Items.Add(item);
            }
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) {
                if (comboBox1.SelectedItem == null) {
                    MessageBox.Show("Vui lòng chọn phòng", "Thông báo");
                }else{
                   
                    if (textBox1.Text == "") {
                        MessageBox.Show("Vui lòng Nhập Số Kí", "Thông Báo");
                    }else{
                        var pdc = (from PDC in data.CHITIETPHONGs
                                   where PDC.SOPHONG == comboBox1.SelectedItem.ToString() && PDC.NGAYTRAP == null
                                   select PDC).FirstOrDefault();

                        ListViewItem selectedItem = listView1.SelectedItems[0];

                        string column1Value = selectedItem.SubItems[0].Text;
                        var dvgu = (from DVGU in data.DICHVUGUs
                                    where DVGU.LOAIGU == column1Value
                                    select DVGU).FirstOrDefault();
                        var ctgu2 = (from CTGU2 in data.CHITIETGUs
                                     where CTGU2.MAHD == pdc.MAHD && CTGU2.MAGU == dvgu.MAGU
                                     select CTGU2).FirstOrDefault();
                        if (ctgu2 == null) {
                            CHITIETGU ctgu = new CHITIETGU();
                            ctgu.MAHD = pdc.MAHD;
                            ctgu.MAGU = dvgu.MAGU;
                            ctgu.SOKG = int.Parse(textBox1.Text);
                            ctgu.TONGTIENGU = dvgu.GIAGU * ctgu.SOKG;
                            data.CHITIETGUs.InsertOnSubmit(ctgu);
                            data.SubmitChanges();
                        }else{
                            ctgu2.SOKG = ctgu2.SOKG + 1;
                            data.SubmitChanges();

                        }
                        
                        var ctgu1 = (from CTGU1 in data.CHITIETGUs
                                     where CTGU1.MAHD == pdc.MAHD
                                     select CTGU1).ToList();
                        listView2.Items.Clear();
                        for (int i = 0; i < ctgu1.Count; i++)
                        {
                            var gu = (from GU in data.DICHVUGUs
                                      where GU.MAGU == ctgu1[i].MAGU
                                      select GU).FirstOrDefault();
                            ListViewItem item = new ListViewItem(gu.LOAIGU);
                            item.SubItems.Add(ctgu1[i].SOKG.ToString());
                            item.SubItems.Add(ctgu1[i].TONGTIENGU.ToString());
                            listView2.Items.Add(item);
                        }
                    }
                    

                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ctp = (from CTP in data.CHITIETPHONGs
                      where CTP.SOPHONG == comboBox1.SelectedItem.ToString() && CTP.NGAYTRAP == null
                      select CTP).FirstOrDefault();
            var ctgu = (from CTGU in data.CHITIETGUs
                        where CTGU.MAHD == ctp.MAHD
                        select CTGU).ToList();
            for (int i = 0; i < ctgu.Count; i++) { 
                var dvgu = (from DVGU in data.DICHVUGUs
                           where DVGU.MAGU == ctgu[i].MAGU
                           select DVGU).FirstOrDefault();
                ctgu[i].TONGTIENGU = ctgu[i].SOKG * dvgu.GIAGU;
            }
        }
    }
}
