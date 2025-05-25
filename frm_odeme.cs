using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace etut_merkezi_uygulamasi
{
    public partial class frm_odeme : Form
    {
        public frm_odeme()
        {
            InitializeComponent();
        }
        cls_sqlconn bgl = new cls_sqlconn();
        public string kullanici_adi;
        public int ogrenci_id, taksit;
        public decimal toplam;
        decimal kalan, tutar, toplamtaksit, toplamtutar;
        bool guncelleme;
        int odemeid;
        private void bilgileri_temizle()
        {
            txt_tutar.Text = "";
            dtp_tarih.Value = DateTime.Now;
            odemeid = 0;
            guncelleme = false;
            lblkalan.Visible = true;
            tablo();
        }
        private void ogrenci_bilgileri()
        {
            using (SqlConnection conn = bgl.baglan())
            {
                //ogrenci bilgilerini alma
                SqlDataAdapter da = new SqlDataAdapter("select * from tbl_ogrenci_bilgi where id=" + ogrenci_id + "", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cmb_ogrenciad.Text = dt.Rows[0]["ogrenci_adi"].ToString() + " " + dt.Rows[0]["ogrenci_soyadi"].ToString();
                    cmb_ogrencino.Text = dt.Rows[0]["id"].ToString();
                    cmb_telno.Text = dt.Rows[0]["veli_telno"].ToString();
                }

            }
        }

        private void tablo()
        {
            try
            {
                using (SqlConnection conn = bgl.baglan())
                {
                    SqlDataAdapter da = new SqlDataAdapter("select id, ogrenci_id as [Öğrenci Numarası],format(tutar,'N0')+' TL' as [Ödeme Tutarı],islem_tarihi as [İslem Tarihi],tip  from tbl_odeme where ogrenci_id=@p1", conn);
                    da.SelectCommand.Parameters.AddWithValue("@p1", ogrenci_id);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    dt.Columns.Add("Durum", typeof(string));

                    foreach (DataRow roww in dt.Rows)
                    {
                        bool odendi = Convert.ToBoolean(roww["tip"]);
                        DateTime dateTime = Convert.ToDateTime(roww["İslem Tarihi"]);
                        string durum = "";
                        if (odendi == true)
                        {
                            durum = "Ödendi";

                        }
                        else
                        {
                            if (dateTime < DateTime.Now)
                            {
                                durum = "Gecikti";
                            }
                            else
                            {
                                durum = "Ödenmedi";
                            }
                        }
                        roww["Durum"] = durum;
                    }
                    dataGridView1.DataSource = dt;
                    dt.Columns.Remove("tip");
                    if (!dataGridView1.Columns.Contains("Durumimg"))
                    {
                        DataGridViewImageColumn img = new DataGridViewImageColumn();
                        img.Name = "Durumimg";
                        img.HeaderText = "";
                        img.ImageLayout = DataGridViewImageCellLayout.Normal;
                        dataGridView1.Columns.Add(img);
                    }
                    dataGridView1.Sort(dataGridView1.Columns["İslem Tarihi"], ListSortDirection.Ascending);
                    dataGridView1.Columns["id"].Visible = false;
                    //toplamtaksit tutarı alma
                    SqlCommand cmd = new SqlCommand("select sum(tutar) from tbl_odeme where ogrenci_id=@p1 and tip=1", conn);
                    cmd.Parameters.AddWithValue("@p1", ogrenci_id);
                    object result = cmd.ExecuteScalar();
                    toplamtaksit = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    //toplamtutaralma 
                    SqlCommand cmd2 = new SqlCommand("select toplam_tutar from tbl_ogrenci_bilgi where id=@p1", conn);
                    cmd2.Parameters.AddWithValue("@p1", ogrenci_id);
                    object result2 = cmd2.ExecuteScalar();
                    toplamtutar = result2 != DBNull.Value ? Convert.ToDecimal(result2) : 0;
                    kalan = toplamtutar - toplamtaksit;
                    lbltoplam.Text = toplamtutar.ToString("N0") + " TL";
                    lblkalan.Text = kalan.ToString("N0") + " TL";
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata");
            }
            finally
            {
                lblkalan.Visible = true;
            }
        }

        private void btn_borc_Click(object sender, EventArgs e)
        {
            try
            {
                toplamtutar = Convert.ToDecimal(lbltoplam.Text.Replace(" TL", "").Replace(".", "").Replace(",", ""));
                //txt_tutar boş mu kontrolü 
                if (!string.IsNullOrWhiteSpace(txt_tutar.Text))
                {
                    tutar = Convert.ToDecimal(txt_tutar.Text.Trim().ToUpper().Replace(",", "").Replace(".", "").Replace(" TL", ""));
                }
                else
                {
                    MessageBox.Show("Lütfen bir tutar giriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection conn = bgl.baglan())
                {

                    //borçlu toplamtaksit tutarını alma
                    SqlCommand cmd = new SqlCommand("select sum(tutar) from tbl_odeme where ogrenci_id=@p1", conn);
                    cmd.Parameters.AddWithValue("@p1", ogrenci_id);
                    object result = cmd.ExecuteScalar();
                    toplamtaksit = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    //seçilen taksidin tutarını alma
                    SqlCommand cmd1 = new SqlCommand("select tutar from tbl_odeme where ogrenci_id=@p1 and id=@p2", conn);
                    cmd1.Parameters.AddWithValue("@p1", ogrenci_id);
                    cmd1.Parameters.AddWithValue("@p2", odemeid);
                    object result1 = cmd1.ExecuteScalar();
                    decimal secilentutar = result1 != DBNull.Value ? Convert.ToDecimal(result1) : 0;
                    //sadece tarih güncelleme 
                    if (guncelleme)
                    {
                        if (secilentutar == tutar)
                        {
                            SqlCommand cmd2 = new SqlCommand("update tbl_odeme set tip=0 , islem_tarihi=@p1 where ogrenci_id=@p2 and id=@p3", conn);
                            cmd2.Parameters.AddWithValue("@p1", dtp_tarih.Value.Date);
                            cmd2.Parameters.AddWithValue("@p2", ogrenci_id);
                            cmd2.Parameters.AddWithValue("@p3", odemeid);
                            cmd2.ExecuteNonQuery();
                            guncelleme = false;
                            return;
                        }
                    }
                    //toplamtaksit toplamtutardan fazla mı kontrolü
                    if (toplamtaksit + tutar > toplamtutar)
                    {
                        decimal yenitutar = toplamtaksit + tutar;
                        DialogResult dr = MessageBox.Show("Taksitlendirilen toplam tutar toplam tutarı geçiyor devam etmek istediğinize emin misiniz?", "Hata", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            //güncelleniyorsa toplam tutar alma
                            if (guncelleme)
                            {

                                SqlCommand cmd2 = new SqlCommand("update tbl_odeme set tip=0,islem_tarihi=@p1,tutar=@p2 where ogrenci_id=@p3 and id=@p4", conn);
                                cmd2.Parameters.AddWithValue("@p1", dtp_tarih.Value.Date);
                                cmd2.Parameters.AddWithValue("@p2", tutar);
                                cmd2.Parameters.AddWithValue("@p3", ogrenci_id);
                                cmd2.Parameters.AddWithValue("@p4", odemeid);
                                cmd2.ExecuteNonQuery();
                                guncelleme = false;

                            }
                            else
                            {
                                SqlCommand cmd3 = new SqlCommand("insert into tbl_odeme (ogrenci_id,tutar,tip,islem_tarihi) values (@p1,@p2,@p3,@p4)", conn);
                                cmd3.Parameters.AddWithValue("@p1", ogrenci_id);
                                cmd3.Parameters.AddWithValue("@p2", tutar);
                                cmd3.Parameters.AddWithValue("@p3", false);
                                cmd3.Parameters.AddWithValue("@p4", dtp_tarih.Value.Date);
                                cmd3.ExecuteNonQuery();
                            }

                            //ogrencibilgideki toplam tutarı güncelleme 
                            SqlCommand cmd4 = new SqlCommand("update tbl_ogrenci_bilgi set toplam_tutar=@p1 where id=@p2", conn);
                            cmd4.Parameters.AddWithValue("@p1", yenitutar);
                            cmd4.Parameters.AddWithValue("@p2", ogrenci_id);
                            cmd4.ExecuteNonQuery();
                            //kalan borç alma
                            SqlCommand cmd5 = new SqlCommand("select sum(tutar) from tbl_odeme where ogrenci_id=@p1 and tip=1", conn);
                            cmd5.Parameters.AddWithValue("@p1", ogrenci_id);
                            object result2 = cmd5.ExecuteScalar();
                            toplamtaksit = result2 != DBNull.Value ? Convert.ToDecimal(result2) : 0;
                            kalan = yenitutar - toplamtaksit;
                            lbltoplam.Text = yenitutar.ToString("N0") + " TL";
                            lblkalan.Text = kalan.ToString("N0") + " TL";
                        }
                        else
                        {
                            MessageBox.Show("İşlem iptal edildi", "İşlem İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                    else
                    {
                        if (guncelleme)
                        {
                            SqlCommand cmd2 = new SqlCommand("update tbl_odeme set tip=0,islem_tarihi=@p1,tutar=@p2 where ogrenci_id=@p3 and id=@p4", conn);
                            cmd2.Parameters.AddWithValue("@p1", dtp_tarih.Value.Date);
                            cmd2.Parameters.AddWithValue("@p2", tutar);
                            cmd2.Parameters.AddWithValue("@p3", ogrenci_id);
                            cmd2.Parameters.AddWithValue("@p4", odemeid);
                            cmd2.ExecuteNonQuery();
                            guncelleme = false;
                        }
                        else
                        {
                            SqlCommand cmd3 = new SqlCommand("insert into tbl_odeme (ogrenci_id,tutar,tip,islem_tarihi) values (@p1,@p2,@p3,@p4)", conn);
                            cmd3.Parameters.AddWithValue("@p1", ogrenci_id);
                            cmd3.Parameters.AddWithValue("@p2", tutar);
                            cmd3.Parameters.AddWithValue("@p3", false);
                            cmd3.Parameters.AddWithValue("@p4", dtp_tarih.Value.Date);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata");
            }
            finally
            {
                bilgileri_temizle();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Durum")
            {
                string durum = e.Value?.ToString();
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                switch (durum)
                {
                    case "Ödendi":
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#32CD32");
                        break;
                    case "Gecikti":
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#8B0000");
                        break;
                    case "Ödenmedi":
                        row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        break;
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Durumimg")
            {
                string durum = dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value.ToString();
                switch (durum)
                {
                    case "Ödendi":
                        Image imgcheck = cls_sqlconn.yuksekkalite(Properties.Resources.check, new Size(20, 20));
                        e.Value = imgcheck;
                        break;
                    case "Gecikti":
                        Image imgwarning = cls_sqlconn.yuksekkalite(Properties.Resources.exclamation, new Size(20, 20));
                        e.Value = imgwarning;
                        break;
                    case "Ödenmedi":
                        Image imgclockwise = cls_sqlconn.yuksekkalite(Properties.Resources.clockwise, new Size(20, 20));
                        e.Value = imgclockwise;
                        break;
                }
            }
        }
        private void dataGridView1_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {

                    e.PaintBackground(e.ClipBounds, true);
                    Color buttoncolor = Color.FromArgb(244, 67, 54);
                    Color textcolor = Color.Black;

                    using (SolidBrush brush = new SolidBrush(buttoncolor))
                    {
                        e.Graphics.FillRectangle(brush, e.CellBounds);
                    }
                    e.Graphics.DrawRectangle(Pens.Black, e.CellBounds);
                    TextRenderer.DrawText(e.Graphics, e.FormattedValue.ToString(), e.CellStyle.Font, e.CellBounds, textcolor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal silinenborc = 0;
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Sil"].Index)
            {
                DialogResult dr = MessageBox.Show("Silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    using (SqlConnection conn = bgl.baglan())
                    {
                        SqlCommand cmd = new SqlCommand("delete from tbl_odeme where id=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
                        cmd.ExecuteNonQuery();
                        //Borcu güncelleme
                        silinenborc = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString().Replace(" TL", "").Replace(".", "").Replace(",", ""));
                        SqlCommand cmd2 = new SqlCommand("update tbl_ogrenci_bilgi set toplam_tutar=toplam_tutar-@p1 where id=@p2", conn);
                        cmd2.Parameters.AddWithValue("@p1", silinenborc);
                        cmd2.Parameters.AddWithValue("@p2", ogrenci_id);
                        cmd2.ExecuteNonQuery();
                    }
                    tablo();
                }
                else
                {
                    MessageBox.Show("İşlem iptal edildi", "İşlem İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                dtp_tarih.Text = dataGridView1.Rows[e.RowIndex].Cells["İslem Tarihi"].Value.ToString();
                guncelleme = true;
                taksit = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString().Replace(" TL", "").Replace(".", "").Replace(",", ""));
                txt_tutar.Text = dataGridView1.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString();
                odemeid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
            }
        }

        private void cmb_ogrenciad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {


                using (SqlConnection conn = bgl.baglan())
                {
                    SqlCommand cmd = new SqlCommand("Select * from tbl_ogrenci_bilgi where id=@p1", conn);
                    cmd.Parameters.AddWithValue("@p1", cmb_ogrenciad.SelectedValue);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ogrenci_id = Convert.ToInt32(dr["id"].ToString());
                        cmb_ogrencino.Text = dr["id"].ToString();
                        cmb_telno.Text = dr["veli_telno"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata");
            }
            finally
            {
                tablo();
            }
        }

        private void cmb_telno_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (/*cmb_telno.SelectedIndex == null ||*/ cmb_telno.SelectedValue is DataRowView)
                {
                    return;
                }
                else
                {
                    using (SqlConnection conn = bgl.baglan())
                    {
                        SqlCommand cmd = new SqlCommand("Select * from tbl_ogrenci_bilgi where id=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", cmb_telno.SelectedValue);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            ogrenci_id = Convert.ToInt32(dr["id"].ToString());
                            cmb_ogrencino.Text = dr["id"].ToString();
                            cmb_ogrenciad.Text = dr["ogrenci_adi"].ToString() + " " + dr["ogrenci_soyadi"].ToString();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata");
            }
            finally
            {
                tablo();
            }
        }

        private void cmb_ogrencino_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection conn = bgl.baglan())
                {
                    SqlCommand cmd = new SqlCommand("Select * from tbl_ogrenci_bilgi where id=@p1", conn);
                    cmd.Parameters.AddWithValue("@p1", cmb_ogrencino.SelectedValue);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ogrenci_id = Convert.ToInt32(dr["id"].ToString());
                        cmb_ogrenciad.Text = dr["ogrenci_adi"].ToString() + " " + dr["ogrenci_soyadi"].ToString();
                        cmb_telno.Text = dr["veli_telno"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata");
            }
            finally
            {
                tablo();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 12, FontStyle.Bold);
            float y = 10;
            float x = 10;
            e.Graphics.DrawString("Etüt Merkezi Tahsilat Makbuzu", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString("--------------------------------------------------", font, Brushes.Black, new PointF(100, y)); y += 25;
            e.Graphics.DrawString($"Öğrenci Numarası: {ogrenci_id}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"Öğrenci Adı: {cmb_ogrenciad.Text}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"İşlem Tarihi: {dtp_tarih.Value.ToShortDateString()}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"Ödeme Tutarı: {txt_tutar.Text} TL", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"Kalan Borç: {lblkalan.Text}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString("--------------------------------------------------", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"Ödemeyi Alan: {kullanici_adi}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString("İmza: ", font, Brushes.Black, new PointF(x, y)); y += 25;

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dtp_tarih.Text = dataGridView1.Rows[e.RowIndex].Cells["İslem Tarihi"].Value.ToString();
            guncelleme = true;
            taksit = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString().Replace(" TL", "").Replace(".", "").Replace(",", ""));
            txt_tutar.Text = dataGridView1.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString();
            odemeid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
        }

        private void frm_odeme_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_dashboard dashboard = new frm_dashboard();
            dashboard.lbl_isim.Text = kullanici_adi;
            dashboard.Show();
        }

        private void btn_tahsilat_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = bgl.baglan())
                {
                    if (guncelleme)
                    {
                        //toplamtaksit tutarını alma
                        SqlCommand cmd = new SqlCommand("select tutar from tbl_odeme where ogrenci_id=@p1 and id=@p2", conn);
                        cmd.Parameters.AddWithValue("@p1", ogrenci_id);
                        cmd.Parameters.AddWithValue("@p2", odemeid);
                        object result = cmd.ExecuteScalar();
                        decimal guncellenendeger = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                        tutar = decimal.Parse(txt_tutar.Text.Replace(" TL", "").Replace(".", "").Replace(",", ""));
                        //toplamtaksit toplamtutardan fazla mı kontrolü
                        if (tutar != guncellenendeger)
                        {
                            MessageBox.Show("Hatalı giriş yapıldı lütfen değeri kontrol ediniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            SqlCommand cmd2 = new SqlCommand("update tbl_odeme set tip=1,islem_tarihi=@p1,tutar=@p2 where ogrenci_id=@p3 and id=@p4", conn);
                            cmd2.Parameters.AddWithValue("@p1", dtp_tarih.Value.Date);
                            cmd2.Parameters.AddWithValue("@p2", tutar);
                            cmd2.Parameters.AddWithValue("@p3", ogrenci_id);
                            cmd2.Parameters.AddWithValue("@p4", odemeid);
                            cmd2.ExecuteNonQuery();
                            guncelleme = false;
                            toplamtutar = decimal.Parse(lbltoplam.Text.Replace(" TL", "").Replace(".", "").Replace(",", ""));
                            tutar = decimal.Parse(txt_tutar.Text.Replace(" TL", "").Replace(".", "").Replace(",", ""));
                            kalan = toplamtutar - tutar;
                            lblkalan.Text = kalan.ToString("N0") + " TL";
                            //makbuz yazdırma
                            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Makuz", 280, 500);
                            printPreviewDialog1.Document = printDocument1;
                            printPreviewDialog1.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tahsilat için yeni değer eklenemez sadece güncelleme yapılabilir", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata");
            }
            finally
            {

                bilgileri_temizle();
            }
        }

        private void frm_odeme_Load(object sender, EventArgs e)
        {
            //datagridview renk 
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 90, 90);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.ClearSelection();
            //datagride silme buttonu ekleme 
            if (!dataGridView1.Columns.Contains("Sil"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Sil";
                btn.HeaderText = "Sil";
                btn.Text = "Sil";
                btn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn);
                btn.FlatStyle = FlatStyle.Flat;
            }
            //buttonlara resim ekleme
            Image borc = cls_sqlconn.yuksekkalite(Properties.Resources.database, new Size(45, 45));
            btn_borc.Image = borc;
            btn_borc.ImageAlign = ContentAlignment.MiddleLeft;
            btn_tahsilat.Image = borc;
            btn_tahsilat.ImageAlign = ContentAlignment.MiddleLeft;
            //comboboxa veritabanından veri çekme
            using (SqlConnection conn = bgl.baglan())
            {
                SqlCommand cmd = new SqlCommand("select ogrenci_adi +' '+ ogrenci_soyadi as adsoyad, id ,veli_telno from tbl_ogrenci_bilgi", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cmb_ogrenciad.DataSource = dt;
                    cmb_ogrenciad.DisplayMember = "adsoyad";
                    cmb_ogrenciad.ValueMember = "id";
                    cmb_ogrencino.DataSource = dt;
                    cmb_ogrencino.DisplayMember = "id";
                    cmb_ogrencino.ValueMember = "id";
                    cmb_telno.DataSource = dt;
                    cmb_telno.DisplayMember = "veli_telno";
                    cmb_telno.ValueMember = "id";
                }
            }
            //yazdırılan kağıdın boyutunu ayarlama
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A5", 583, 827);
            //Diğer işlemler
            lblkalan.Visible = false;
            dtp_tarih.Value = DateTime.Now;
            this.WindowState = FormWindowState.Maximized;
            ogrenci_bilgileri();
            tablo();
        }
    }
}
