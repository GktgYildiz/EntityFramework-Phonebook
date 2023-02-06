using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TelRehberiDBEntities db = new TelRehberiDBEntities();
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void KisileriGetir()
        {
            try
            {
                dataGridView1.DataSource = db.Kisilers.Select(k => new
                {
                  
                    KisiId = k.KisiID,
                    KisiAdi = k.KisiAdi,
                    KisiSoyadi = k.KisiSoyadi,
                    TelNo = k.TelNo
                }).ToList();
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception)
            {

                MessageBox.Show("Kişiler gösterilemiyor");
            }
           
        }
        private void btnGetir_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.Kisilers.Select(k => new { k.KisiAdi, k.KisiSoyadi, k.TelNo });

            //---------bu şekilde yazdığımzıda kolonlara istediğimiz ismide vermiş oluyoruz. aşşağıda kolan isimler KisiAdi,soaydi ... oto geldi//
            KisileriGetir();

        }

        private void KisileriEkle()
        {
            try
            {
                Kisiler k = new Kisiler();
               
                k.KisiAdi = txtAd.Text;
                k.KisiSoyadi = txtSoyad.Text;
                k.TelNo = txtTel.Text;
               
                    db.Kisilers.Add(k); //veriyi modele aktardık ama daha database'e yollamadık
                    db.SaveChanges();//burada artık veriyi veritabanına gönderiyoruz
                
               

            }
            catch (Exception)
            {

                MessageBox.Show("eklenemedi");
            }
            
        }
        private void btnekle_Click(object sender, EventArgs e)
        {
            KisileriEkle();
            KisileriGetir();
            Temizle();
        }

        private void Temizle()
        {
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = (TextBox)item;
                    txt.Clear();
                }
            }
        }
        Kisiler guncellenecek;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                guncellenecek = db.Kisilers.Find(id);

                txtAd.Text = guncellenecek.KisiAdi;
                txtSoyad.Text = guncellenecek.KisiSoyadi;
                txtTel.Text = guncellenecek.TelNo;
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            guncellenecek.KisiAdi = txtAd.Text;
            guncellenecek.KisiSoyadi = txtSoyad.Text;
            guncellenecek.TelNo = txtTel.Text;

            db.SaveChanges();
            KisileriGetir();

        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            db.Kisilers.Remove(db.Kisilers.Find(id));
            db.SaveChanges();
            KisileriGetir();
        }
    }
}
