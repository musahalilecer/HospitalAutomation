using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneOtomasyonu
{
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        SqlBaglanti bgl = new SqlBaglanti();
        public string tc;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            label3.Text = tc;
            //Doktor Ad Soyad
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad from TblDoktorlar where DoktorTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", label3.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                label4.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Randevular where randevuDoktor='" + label4.Text + "'" , bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnBilgi_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle frm = new FrmDoktorBilgiDuzenle();
            frm.tc = label3.Text;
            frm.Show();
        }

        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

        private void LblCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
