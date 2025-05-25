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
using System.Drawing.Drawing2D;

namespace etut_merkezi_uygulamasi
{
    public partial class frm_ogrenci_kayıt : Form
    {
        public frm_ogrenci_kayıt()
        {
            InitializeComponent();
        }
        cls_sqlconn bgl = new cls_sqlconn();
        frm_odeme frm_Odeme = new frm_odeme();
        public string kullanici_adi;    
        bool exit = true;
        private void ilkharfbuyuk(TextBox txt)
        {
            txt.Text = txt.Text.Substring(0, 1).ToUpper() + txt.Text.Substring(1).ToLower();
        }
        private void tablo_yenile()
        {
            using (SqlConnection conn = bgl.baglan())
            {
                SqlDataAdapter da = new SqlDataAdapter("select id as [Öğrenci Numarası],ogrenci_adi as [Öğrenci İsim], ogrenci_soyadi as [Öğrenci Soyisim],ogrenci_sinif as [Öğrenci Sınıf] ,ogrenci_telno as [Öğrenci Telefon Numarası],veli_telno as [Veli Telefon Numarası],Format(toplam_tutar,'N0')+' TL' as [Toplam Ücret] from tbl_ogrenci_bilgi", conn);
                DataTable dt = new DataTable();
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                da.Fill(dt);
                dataGridView1.DataSource = bs;
            }
        }
        decimal tutar;

        private void frm_ogrenci_kayıt_Load(object sender, EventArgs e)
        {
            tablo_yenile();
            // datagrid renk ayarları
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#BBDEFB");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //button image boyut ayarları
            Image ekle = cls_sqlconn.yuksekkalite(Properties.Resources.add, new Size(20, 20));
            Image sil = cls_sqlconn.yuksekkalite(Properties.Resources.delete, new Size(20, 20));
            Image avatar = cls_sqlconn.yuksekkalite(Properties.Resources.user_avatar, new Size(20, 20));
            Image buyutec = cls_sqlconn.yuksekkalite(Properties.Resources.magnifier, new Size(20, 20));
            Image temizleme = cls_sqlconn.yuksekkalite(Properties.Resources.data_cleaning, new Size(20, 20));
            Image liste = cls_sqlconn.yuksekkalite(Properties.Resources.list, new Size(20, 20));
            btn_ekle.Image = ekle;
            btn_duzenle.Image = avatar;
            btn_sil.Image = sil;
            btn_ara.Image = buyutec;
            btn_temizle.Image = temizleme;
            btn_listele.Image = liste;
            //diğer işlemler
            lblid.Visible = false;
            this.WindowState = FormWindowState.Maximized;

        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            decimal.TryParse(txt_tutar.Text.Replace(",", " TL"), out tutar);
            ilkharfbuyuk(txt_isim);
            ilkharfbuyuk(txt_soyisim);
            using (SqlConnection conn = bgl.baglan())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into tbl_ogrenci_bilgi (ogrenci_adi,ogrenci_soyadi,ogrenci_sinif,ogrenci_telno,veli_telno,toplam_tutar,devamsizlik_durumu) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", conn);
                    cmd.Parameters.AddWithValue("@p1", txt_isim.Text);
                    cmd.Parameters.AddWithValue("@p2", txt_soyisim.Text);
                    cmd.Parameters.AddWithValue("@p3", cmb_sinif.Text);
                    cmd.Parameters.AddWithValue("@p4", msk_ogrtel.Text);
                    cmd.Parameters.AddWithValue("@p5", msk_velitel.Text);
                    cmd.Parameters.AddWithValue("@p6", tutar);
                    cmd.Parameters.AddWithValue("@p7", 0);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString(), "Hata oluştu");
                }
                finally
                {
                    txt_isim.Text = "";
                    txt_soyisim.Text = "";
                    cmb_sinif.Text = "";
                    msk_ogrtel.Text = "";
                    msk_velitel.Text = "";
                    tablo_yenile();
                }
            }
        }

        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = bgl.baglan())
            {

                try
                {
                    decimal.TryParse(txt_tutar.Text.ToUpper().Trim().Replace(",", "").Replace("TL", "").Replace(".", ""), out tutar);
                    SqlCommand cmd = new SqlCommand("update tbl_ogrenci_bilgi set ogrenci_adi=@p1,ogrenci_soyadi=@p2,ogrenci_telno=@p3,ogrenci_sinif=@p4,veli_telno=@p5,toplam_tutar=@p6 where id=@p7", conn);
                    cmd.Parameters.AddWithValue("@p1", txt_isim.Text);
                    cmd.Parameters.AddWithValue("@p2", txt_soyisim.Text);
                    cmd.Parameters.AddWithValue("@p3", msk_ogrtel.Text);
                    cmd.Parameters.AddWithValue("@p4", cmb_sinif.Text);
                    cmd.Parameters.AddWithValue("@p5", msk_velitel.Text);
                    cmd.Parameters.AddWithValue("@p6", tutar);
                    cmd.Parameters.AddWithValue("@p7", lblid.Text);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Hata oluştu", exp.ToString());
                }
                finally
                {
                    txt_isim.Text = "";
                    txt_soyisim.Text = "";
                    txt_tutar.Text = "";
                    cmb_sinif.Text = "";
                    msk_ogrtel.Text = "";
                    msk_velitel.Text = "";
                    lblid.Text = "0";
                    tablo_yenile();
                }
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = bgl.baglan())
            {

                if (lblid.Text == "0")
                {
                    MessageBox.Show("Silmek istediğiniz öğrenciyi seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DialogResult sil = MessageBox.Show("Silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (sil == DialogResult.Yes)
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand("delete from tbl_odeme where ogrenci_id=@p1", conn);
                            cmd.Parameters.AddWithValue("@p1", lblid.Text);
                            cmd.ExecuteNonQuery();
                            SqlCommand cmd2 = new SqlCommand("delete from tbl_devamsizlik where ogrenci_id=@p1", conn);
                            cmd2.Parameters.AddWithValue("@p1", lblid.Text);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("delete from tbl_ogrenci_bilgi where id=@p1", conn);
                            cmd3.Parameters.AddWithValue("@p1", lblid.Text);
                            cmd3.ExecuteNonQuery();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString(), "Hata oluştu");
                        }
                        finally
                        {
                            txt_isim.Text = "";
                            txt_soyisim.Text = "";
                            cmb_sinif.Text = "";
                            msk_ogrtel.Text = "";
                            msk_velitel.Text = "";
                            lblid.Text = "0";
                            frm_Odeme.ogrenci_id = 0;
                            tablo_yenile();
                            MessageBox.Show("Öğrenci başarıyla Silindi", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

        }

        private void btn_ara_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)dataGridView1.DataSource;
            bs.Filter = $"[Öğrenci İsim] LIKE '%{txt_ara.Text}%' OR [Öğrenci Soyisim] LIKE '%{txt_ara.Text}%' OR [Öğrenci Sınıf] LIKE '%{txt_ara.Text}%' OR [Öğrenci Telefon Numarası] LIKE '%{txt_ara.Text}%' OR [Veli Telefon Numarası] LIKE '%{txt_ara.Text}%'";
        }

        private void frm_ogrenci_kayıt_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (exit)
            {
                frm_dashboard dashboard = new frm_dashboard();
                dashboard.lbl_isim.Text=kullanici_adi;
                dashboard.Show();
            }
        }
        
        private void btn_odeme_Click(object sender, EventArgs e)
        {
            if (frm_Odeme.ogrenci_id == 0)
            {
                MessageBox.Show("Öğrenci seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                frm_Odeme.Show();
                exit = false;
                this.Close();
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            txt_isim.Text = "";
            txt_soyisim.Text = "";
            txt_tutar.Text = "";
            cmb_sinif.Text = "";
            msk_ogrtel.Text = "";
            msk_velitel.Text = "";
            frm_Odeme.ogrenci_id = 0;
            lblid.Text = "0";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblid.Text = dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value.ToString();
            txt_isim.Text = dataGridView1.Rows[e.RowIndex].Cells["Öğrenci İsim"].Value.ToString();
            txt_soyisim.Text = dataGridView1.Rows[e.RowIndex].Cells["ÖĞrenci Soyisim"].Value.ToString();
            cmb_sinif.Text = dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Sınıf"].Value.ToString();
            msk_ogrtel.Text = dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Telefon Numarası"].Value.ToString();
            msk_velitel.Text = dataGridView1.Rows[e.RowIndex].Cells["Veli Telefon Numarası"].Value.ToString();
            txt_tutar.Text = dataGridView1.Rows[e.RowIndex].Cells["Toplam Ücret"].Value.ToString();
            frm_Odeme.ogrenci_id = Convert.ToInt32(lblid.Text);
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            tablo_yenile();
        }
    }
}