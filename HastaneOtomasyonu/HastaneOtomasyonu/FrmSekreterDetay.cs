using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;

namespace HastaneOtomasyonu
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        public string tc;
        
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            
            //Ad Soyad
            SqlCommand komut1 = new SqlCommand("select sekreterAdSoyad from TblSekreter where sekreterTC=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut1.ExecuteReader();
            while(dr.Read())
            {
                lblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
            //Branşları çekme
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TblBranslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource= dt1;
            

            //Doktorları Çekme
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (doktorAd + ' ' + doktorSoyad) as 'Doktorlar',doktorBrans as 'Doktor Branşları' from TblDoktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşları comboboxa Getirme
            SqlCommand komut2 = new SqlCommand("select bransAd from TblBranslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0].ToString());
            }
            bgl.baglanti().Close();
        }
        
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutKaydet = new SqlCommand("insert into Randevular (randevuTarih,randevuSaat,randevuBrans,randevuDoktor) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komutKaydet.Parameters.AddWithValue("@p1", mskTarih.Text);
            komutKaydet.Parameters.AddWithValue("@p2", mskSaat.Text);
            komutKaydet.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komutKaydet.Parameters.AddWithValue("@p4", cmbDoktor.Text);
            komutKaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Kaydedilmiştir");
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Doktorları ComboBoxa Çekme
            cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select doktorAd,doktorSoyad from TblDoktorlar where doktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblDuyuru (duyuru) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",rchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frm = new FrmDoktorPaneli();
            frm.Show();
        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBrans frm = new FrmBrans();
            frm.Show();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            FrmRandevuListe frm = new FrmRandevuListe();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }
    }
}
