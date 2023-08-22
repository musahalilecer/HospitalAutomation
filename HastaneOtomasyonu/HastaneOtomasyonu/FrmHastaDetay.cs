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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        SqlBaglanti bgl = new SqlBaglanti();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;
            SqlCommand komut1 = new SqlCommand("Select hastaAd,hastaSoyad from TblHastalar where hastaTC=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while(dr1.Read())
            {
                lblAd.Text = dr1[0] + " " + dr1[1];
            }
            // Randevu Geçmiş
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Randevular where hastaTc= "+tc,bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource= dt;

            //Branşları Çekme
            SqlCommand komut2 = new SqlCommand("select BransAd from TblBranslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Doktorları Çekme
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

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Randevular where randevuBrans='"+cmbBrans.Text + "'" + "and randevuDoktor='"+cmbDoktor.Text+"' and randevuDurum=0",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource= dt;
        }

        private void LinkBilgi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgiDuzenle frm = new FrmHastaBilgiDuzenle();
            frm.tc = lblTc.Text;
            frm.Show();
            
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Randevular set randevuDurum=1,hastaTc=@p1 where randevuid=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTc.Text);
            komut.Parameters.AddWithValue("@p2",txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}
