using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq;
using System.Linq;

namespace WindowsFormsApplication2
{
    public partial class XtraUserControlTC : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlTC()
        {
            InitializeComponent();
        }
        public int trong = 0;
        public int dangthue = 0;
        public int suachua = 0;

        public Button[] button;
        public string sp;
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void hienthi()
        {
            var sophong = (from p in data.PHONGs
                           select p.SOPHONG).ToList();

            var loaiphong = (from p in data.PHONGs
                             select p.LOAIPHONG).ToList();
            var sogiuong = (from p in data.PHONGs
                            select p.SOGIUONG).ToList();
            var trangthai = (from p in data.PHONGs
                             select p.TRANGTHAI).ToList();
            int x = 64;
            int y = 75;

            button = new Button[sophong.Count];


            for (int i = 0; i < sophong.Count; i++)
            {

                button[i] = new Button();
                button[i].Text = sophong[i].ToString() + Environment.NewLine + loaiphong[i].ToString() + " " + sogiuong[i].ToString(); // Đặt văn bản cho Button
                button[i].Width = 130; // Đặt chiều rộng cho Button
                button[i].Height = 85; // Đặt chiều cao cho Button
                button[i].Left = x;
                x = x + 152;
                button[i].Top = y;
                if (trangthai[i].ToString() == "còn trống")
                {
                    button[i].BackColor = Color.Green;
                    trong++;
                }
                if (trangthai[i].ToString() == "đang thuê")
                {
                    button[i].BackColor = Color.Red;
                    dangthue++;
                }
                if (trangthai[i].ToString() == "sửa chữa")
                {
                    button[i].BackColor = Color.Purple;
                    suachua++;
                }
                if (i == 6 || i == 13 || i == 20 || i == 27 || i == 34)
                {
                    x = 64;
                    y = y + 90;
                }
                button[i].Click += new EventHandler(Button_Click);
                this.Controls.Add(button[i]);

            }
            textBox1.Text = sophong.Count.ToString();
            textBox2.Text = trong.ToString();
            textBox3.Text = dangthue.ToString();
            textBox4.Text = suachua.ToString();
            textBox5.Text = "0";
        }

        private void XtraUserControlTC_Load(object sender, EventArgs e)
        {
            hienthi();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            sp = clickedButton.Text.ToString();
            textBox5.Text = "1";
            hienthi();



        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sp == null)
            {
                MessageBox.Show("Bạn chưa chọn phòng", "thông báo");
                textBox5.Text = "0";

            }
            else
            {

                PHONG phong = new PHONG();
                phong = (from p in data.PHONGs
                         where p.SOPHONG == ((sp[0] + sp[1] + sp[2]) - 45).ToString()
                         select p).FirstOrDefault();
                if (phong.TRANGTHAI == "còn trống")
                {

                    XtraForm2 from2 = new XtraForm2(phong.SOPHONG.ToString());
                    from2.Show();
                   
                    

                }
                else if ((phong.TRANGTHAI == "đang thuê") || (phong.TRANGTHAI == "sửa chữa"))
                {
                    MessageBox.Show(String.Format("{0} {1}", phong.SOPHONG, phong.TRANGTHAI), "Thông Báo");

                }



            }

            hienthi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sp == null)
            {
                MessageBox.Show("Bạn chưa chọn phòng", "Thông Báo");
            }
            else
            {
                PHONG phong = new PHONG();
                phong = (from p in data.PHONGs
                         where p.SOPHONG == ((sp[0] + sp[1] + sp[2]) - 45).ToString()
                         select p).FirstOrDefault();
                if (phong.TRANGTHAI == "đang thuê")
                {
                    MessageBox.Show("tạo hóa đơn", "thông báo");
                    var ctphong = (from ctp in data.CHITIETPHONGs
                                   where ctp.SOPHONG == phong.SOPHONG && ctp.NGAYTRAP == null
                                   select ctp).FirstOrDefault();
                    DateTime ngaytra = DateTime.Now;
                    ctphong.NGAYTRAP = ngaytra;
                    TimeSpan songay = ctphong.NGAYTRAP.Value - ctphong.NGAYNHANP.Value;
                     int sn = (int)songay.TotalDays;
                     if (sn <= 0) {
                         sn = 1;
                     }
                    ctphong.TONGTIENP = phong.GIAP * sn ;
                    
                    var hoadon = (from hd in data.HOADONs
                                  where hd.MAHD == ctphong.MAHD
                                  select hd).FirstOrDefault();
                    var ctau = (from CTAU in data.CHITIETAUs
                                where CTAU.MAHD == ctphong.MAHD
                                select CTAU).ToList();
                    int? tongtienau = 0;
                    for (int i = 0; i < ctau.Count; i++) 
                    {
                        tongtienau = tongtienau + ctau[i].TONGTIENAU;
                    }
                    var ctdc = (from CTDC in data.CHITIETDCs
                                where CTDC.MAHD == ctphong.MAHD
                                select CTDC).ToList();
                    int? tongtiendc = 0;
                    for (int i = 0; i < ctdc.Count; i++) {
                        tongtiendc = tongtiendc + ctdc[i].TONGTIENDC;
                    }
                    var ctgu = (from CTGU in data.CHITIETGUs
                                where CTGU.MAHD == ctphong.MAHD
                                select CTGU).ToList();
                    int? tongtiengu = 0;
                    for (int i = 0; i < ctgu.Count; i++) {
                        tongtiengu = tongtiengu + ctgu[i].TONGTIENGU;
                    }
                    hoadon.TONGTIENHD = ctphong.TONGTIENP + tongtienau + tongtiendc+tongtiengu;
                    phong.TRANGTHAI = "còn trống";
                    data.GetChangeSet();
                    data.SubmitChanges();
                }
                else if ((phong.TRANGTHAI == "còn trống") || (phong.TRANGTHAI == "sửa chữa"))
                {
                    MessageBox.Show(String.Format("{0} đang {1}", phong.SOPHONG, phong.TRANGTHAI), "Thông Báo");

                }
            }
            hienthi();
        }

    }
}
