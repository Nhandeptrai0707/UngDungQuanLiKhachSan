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
    public partial class XtraUserControlQLDC : DevExpress.XtraEditors.XtraUserControl
    {
        public XtraUserControlQLDC()
        {
            InitializeComponent();
        }
        public DataClasses1DataContext data = new DataClasses1DataContext();
        private void XtraUserControlQLDC_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.DICHVUDCs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dc = (from DC in data.DICHVUDCs
                      where DC.MADC == textBox1.Text
                      select DC).FirstOrDefault();
            if (dc == null)
            {
                DICHVUDC dichuyen = new DICHVUDC();
                dichuyen.MADC = textBox1.Text;
                dichuyen.TENPT = textBox2.Text;
                dichuyen.GIAPT = int.Parse(textBox3.Text);
                dichuyen.TINHTRANGXE = textBox4.Text;
                data.DICHVUDCs.InsertOnSubmit(dichuyen);
                data.SubmitChanges();
            }
            else {
                MessageBox.Show("Dịch vụ đã tồn tại", "Thông báo");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dc = (from DC in data.DICHVUDCs
                      where DC.MADC == textBox1.Text
                      select DC).FirstOrDefault();
            if (dc == null) {
                MessageBox.Show("Phương tiện không tồn tại", "Thông Báo");
            
            }else{
                DICHVUDC dichuyen = new DICHVUDC();
                dichuyen.MADC = textBox1.Text;
                dichuyen.TENPT = textBox2.Text;
                dichuyen.GIAPT = int.Parse(textBox3.Text);
                
            }
        }
       
    }
}
