using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace etut_merkezi_uygulamasi
{
    public partial class frm_dashboard : Form
    {
        public frm_dashboard()
        {
            InitializeComponent();
        }
        string kullanici_adi;
        public int ogrenci_id = 1, taksit;
        public decimal toplam;
        public bool yonetici;
        decimal kalan, tutar, toplamtaksit, toplamtutar;
        int odemeid;
        bool cikis = true, guncelleme;
        cls_sqlconn bgl = new cls_sqlconn();
        private void frm_dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cikis)
            {
                DialogResult dr = MessageBox.Show("Uygulamadan çıkış yapmak istediğinizden emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    Application.ExitThread();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        private void kullanici_ekleme_tablo()
        {
            using (SqlConnection conn = bgl.baglan())
            {
                SqlDataAdapter da = new SqlDataAdapter("select id as [Kullanıcı Numarası],isim as [İsim],soyisim as [Soyisim],kullanici_adi as [Kullanıcı Adı],sifre as [Şifre],yonetici as [Yönetici] ,telefon_no as [Kullanıcı Telefon Numarası] from tbl_user", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtv_kullanici.DataSource = dt;
                dtv_kullanici.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtv_kullanici.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                //renk ayarları
                dtv_kullanici.DefaultCellStyle.BackColor = Color.White;
                dtv_kullanici.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);
                dtv_kullanici.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dtv_kullanici.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
                dtv_kullanici.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dtv_kullanici.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#BBDEFB");
                dtv_kullanici.DefaultCellStyle.SelectionForeColor = Color.White;
                dtv_kullanici.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //button imageleri
                Image ekle = cls_sqlconn.yuksekkalite(Properties.Resources.add, new Size(20, 20));
                Image sil = cls_sqlconn.yuksekkalite(Properties.Resources.delete, new Size(20, 20));
                Image avatar = cls_sqlconn.yuksekkalite(Properties.Resources.user_avatar, new Size(20, 20));
                Image temizle = cls_sqlconn.yuksekkalite(Properties.Resources.data_cleaning, new Size(20, 20));
                btn_ekle_kullanici.Image = ekle;
                btn_ekle_kullanici.ImageAlign = ContentAlignment.MiddleLeft;
                btn_sil_kullanici.Image = sil;
                btn_sil_kullanici.ImageAlign = ContentAlignment.MiddleLeft;
                btn_duzenle_kullanici.Image = avatar;
                btn_duzenle_kullanici.ImageAlign = ContentAlignment.MiddleLeft;
                btn_temizle_kullanici.Image = temizle;
            }
        }
        private void ilkharfbuyuk(TextBox txt)
        {
            txt.Text = txt.Text.Substring(0, 1).ToUpper() + txt.Text.Substring(1).ToLower();
        }
        private void bilgileri_temizle()
        {
            txt_tutar.Text = "";
            dtp_tarih.Value = DateTime.Now;
            odemeid = 0;
            guncelleme = false;
            lblkalan.Visible = true;
            odeme_tablo();
        }
        private void odeme_ogrenci_bilgi()
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
                    cmb_ogrencino.SelectedValue = dt.Rows[0]["id"];
                    cmb_telno.Text = dt.Rows[0]["veli_telno"].ToString();
                }
            }
        }
        private void odeme_tablo()
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
                    dtv_odeme.DataSource = dt;
                    dt.Columns.Remove("tip");
                    if (!dtv_odeme.Columns.Contains("Durumimg"))
                    {
                        DataGridViewImageColumn img = new DataGridViewImageColumn();
                        img.Name = "Durumimg";
                        img.HeaderText = "";
                        img.ImageLayout = DataGridViewImageCellLayout.Normal;
                        dtv_odeme.Columns.Add(img);
                    }
                    dtv_odeme.Sort(dtv_odeme.Columns["İslem Tarihi"], ListSortDirection.Ascending);
                    dtv_odeme.Columns["id"].Visible = false;
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
                    cmb_ogrencino.SelectedValue = ogrenci_id;
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
        private void frm_odeme_kodlar()
        {
            //datagridview renk 
            dtv_odeme.EnableHeadersVisualStyles = false;
            dtv_odeme.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dtv_odeme.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtv_odeme.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            dtv_odeme.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtv_odeme.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dtv_odeme.DefaultCellStyle.BackColor = Color.White;
            dtv_odeme.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);
            dtv_odeme.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dtv_odeme.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dtv_odeme.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtv_odeme.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 90, 90);
            dtv_odeme.DefaultCellStyle.SelectionForeColor = Color.White;
            dtv_odeme.ClearSelection();
            //datagride silme buttonu ekleme 
            if (!dtv_odeme.Columns.Contains("Sil"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Sil";
                btn.HeaderText = "Sil";
                btn.Text = "Sil";
                btn.UseColumnTextForButtonValue = true;
                dtv_odeme.Columns.Add(btn);
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
            cmb_ogrencino.SelectedValue = ogrenci_id;
            odeme_ogrenci_bilgi();
            odeme_tablo();
        }

        private void ogrenci_bilgi_tablo()
        {
            using (SqlConnection conn = bgl.baglan())
            {

                SqlDataAdapter da = new SqlDataAdapter("select id as [Öğrenci Numarası],ogrenci_adi as [Öğrenci İsim], ogrenci_soyadi as [Öğrenci Soyisim],ogrenci_sinif as [Öğrenci Sınıf] ,ogrenci_telno as [Öğrenci Telefon Numarası],veli_telno as [Veli Telefon Numarası],format(toplam_tutar,'N0')+' TL' as [Toplam Tutar],devamsizlik_durumu as [Toplam Devamsızlık] from tbl_ogrenci_bilgi", conn);
                DataTable dt = new DataTable();
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                da.Fill(dt);
                dtv_ogrenci_bilgi.DataSource = bs;
            }
        }
        private void frm_ogrenci_bilgi_kodlar()
        {
            //renk
            dtv_ogrenci_bilgi.EnableHeadersVisualStyles = false;
            dtv_ogrenci_bilgi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dtv_ogrenci_bilgi.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtv_ogrenci_bilgi.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            dtv_ogrenci_bilgi.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtv_ogrenci_bilgi.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dtv_ogrenci_bilgi.DefaultCellStyle.BackColor = Color.White;
            dtv_ogrenci_bilgi.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);
            dtv_ogrenci_bilgi.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dtv_ogrenci_bilgi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dtv_ogrenci_bilgi.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtv_ogrenci_bilgi.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 90, 90);
            dtv_ogrenci_bilgi.DefaultCellStyle.SelectionForeColor = Color.White;
            dtv_ogrenci_bilgi.ClearSelection();
            this.WindowState = FormWindowState.Maximized;
            ogrenci_bilgi_tablo();

            if (!dtv_ogrenci_bilgi.Columns.Contains("Ödeme"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Text = "Ödeme";
                btn.Name = "Ödeme";
                btn.UseColumnTextForButtonValue = true;
                btn.HeaderText = "Ödeme";
                btn.UseColumnTextForButtonValue = true;
                dtv_ogrenci_bilgi.Columns.Add(btn);
                btn.FlatStyle = FlatStyle.Flat;

            }
            if (!dtv_ogrenci_bilgi.Columns.Contains("Düzenle"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Text = "Düzenle";
                btn.Name = "Düzenle";
                btn.UseColumnTextForButtonValue = true;
                btn.HeaderText = "Düzenle";
                btn.UseColumnTextForButtonValue = true;
                dtv_ogrenci_bilgi.Columns.Add(btn);
                btn.FlatStyle = FlatStyle.Flat;
            }
            if (!dtv_ogrenci_bilgi.Columns.Contains("Devamsızlık"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Text = "Devamsızlık";
                btn.Name = "Devamsızlık";
                btn.UseColumnTextForButtonValue = true;
                btn.HeaderText = "Devamsızlık";
                btn.UseColumnTextForButtonValue = true;
                dtv_ogrenci_bilgi.Columns.Add(btn);
                btn.FlatStyle = FlatStyle.Flat;
            }
            if (!dtv_ogrenci_bilgi.Columns.Contains("Yoklama"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Text = "Yoklama";
                btn.Name = "Yoklama";
                btn.UseColumnTextForButtonValue = true;
                btn.HeaderText = "Yoklama";
                btn.UseColumnTextForButtonValue = true;
                dtv_ogrenci_bilgi.Columns.Add(btn);
                btn.FlatStyle = FlatStyle.Flat;
            }

            //buttonlara ikon ekleme
            Image buyutec = cls_sqlconn.yuksekkalite(Properties.Resources.magnifier, new Size(20, 20));
            Image liste = cls_sqlconn.yuksekkalite(Properties.Resources.list, new Size(20, 20));
            btn_ara.Image = buyutec;
            btn_ara.ImageAlign = ContentAlignment.MiddleLeft;
            btn_listele.Image = liste;
            btn_listele.ImageAlign = ContentAlignment.MiddleLeft;
        }
        private void ogrenci_kayit_tablo_yenile()
        {
            using (SqlConnection conn = bgl.baglan())
            {
                SqlDataAdapter da = new SqlDataAdapter("select id as [Öğrenci Numarası],ogrenci_adi as [Öğrenci İsim], ogrenci_soyadi as [Öğrenci Soyisim],ogrenci_sinif as [Öğrenci Sınıf] ,ogrenci_telno as [Öğrenci Telefon Numarası],veli_telno as [Veli Telefon Numarası],Format(toplam_tutar,'N0')+' TL' as [Toplam Ücret] from tbl_ogrenci_bilgi", conn);
                DataTable dt = new DataTable();
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                da.Fill(dt);
                dtv_ogrenci_kayit.DataSource = bs;
            }
        }
        private void frm_ogrenci_kayit_kodlar()
        {
            ogrenci_kayit_tablo_yenile();
            // datagrid renk ayarları
            dtv_ogrenci_kayit.DefaultCellStyle.BackColor = Color.White;
            dtv_ogrenci_kayit.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);
            dtv_ogrenci_kayit.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dtv_ogrenci_kayit.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dtv_ogrenci_kayit.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtv_ogrenci_kayit.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#BBDEFB");
            dtv_ogrenci_kayit.DefaultCellStyle.SelectionForeColor = Color.White;
            dtv_ogrenci_kayit.RowHeadersVisible = false;
            dtv_ogrenci_kayit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            button2.Image = buyutec;
            btn_temizle.Image = temizleme;
            button1.Image = temizleme;
            btn_listele.Image = liste;
            //diğer işlemler
            lblid.Visible = false;
        }
        private void ilk_harf_buyuk()
        {
            string[] kelimeler = lbl_isim.Text.Split(' ');
            for (int i = 0; i < kelimeler.Length; i++)
            {
                if (kelimeler[i].Length > 0)
                {
                    kelimeler[i] = char.ToUpper(kelimeler[i][0]) + kelimeler[i].Substring(1);
                }
            }
            lbl_isim.Text = string.Join(" ", kelimeler);
            lbl_isim_yoklama.Text = lbl_isim.Text;
        }

        double gelen, gelmeyen;
        int odeme_yapan_ogrenciler, odeme_yapmayan_ogrenciler;


        private void chartdoldurma()
        {
            DateTime aybasi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime aysonu = aybasi.AddMonths(1).AddTicks(-1);
            //devamsızlık
            chrt_devamsizlik.Series.Clear();
            chrt_devamsizlik.Series.Add("Devamsızlık");
            chrt_devamsizlik.Series["Devamsızlık"].ChartType = SeriesChartType.Doughnut;
            chrt_devamsizlik.Series["Devamsızlık"]["DoughnutRadius"] = "35";
            chrt_devamsizlik.Series["Devamsızlık"]["StartAngle"] = "360";
            chrt_devamsizlik.Series["Devamsızlık"]["PieLabelStyle"] = "Disabled";

            using (SqlConnection conn = bgl.baglan())
            {
                SqlCommand cmd = new SqlCommand("select count(distinct ogrenci_id) from tbl_devamsizlik where devamsizlik_tarihi between @p1 and @p2", conn);
                cmd.Parameters.AddWithValue("@p1", aybasi);
                cmd.Parameters.AddWithValue("@p2", aysonu);
                object result = cmd.ExecuteScalar();
                int devamsizlik_yapan_ogrenciler = Convert.ToInt32(result);
                SqlCommand cmd2 = new SqlCommand("select count(*) from tbl_ogrenci_bilgi ob where ob.id not in ( select distinct d.ogrenci_id from tbl_devamsizlik d where d.devamsizlik_tarihi between @p1 and @p2)", conn);
                cmd2.Parameters.AddWithValue("@p1", aybasi);
                cmd2.Parameters.AddWithValue("@p2", aysonu);
                object result2 = cmd2.ExecuteScalar();
                int devamsizlik_yapmayan_ogrenciler = Convert.ToInt32(result2);
                chrt_devamsizlik.Series["Devamsızlık"].Points.AddXY("Devamsızlık Yapan Öğrenciler", devamsizlik_yapan_ogrenciler);
                chrt_devamsizlik.Series["Devamsızlık"].Points.AddXY("Devamsızlık Yapmayan Öğrenciler", devamsizlik_yapmayan_ogrenciler);
                gelen = Convert.ToDouble(devamsizlik_yapmayan_ogrenciler);
                gelmeyen = Convert.ToDouble(devamsizlik_yapan_ogrenciler);
            }

            //odeme
            chrt_odeme.Series.Clear();
            chrt_odeme.Series.Add("odeme durumu");
            chrt_odeme.Series["odeme durumu"].ChartType = SeriesChartType.Doughnut;
            chrt_odeme.Series["odeme durumu"]["DoughnutRadius"] = "35";
            chrt_odeme.Series["odeme durumu"]["StartAngle"] = "360";
            chrt_odeme.Series["odeme durumu"]["PieLabelStyle"] = "Disabled";
            using (SqlConnection conn = bgl.baglan())
            {
                SqlCommand cmd = new SqlCommand("select count(*) as [odeme yapan ogrenciler] from tbl_odeme where tip=1 and islem_tarihi between @p1 and @p2", conn);
                cmd.Parameters.AddWithValue("@p1", aybasi);
                cmd.Parameters.AddWithValue("@p2", aysonu);
                object result = cmd.ExecuteScalar();
                odeme_yapan_ogrenciler = Convert.ToInt32(result);
                SqlCommand cmd2 = new SqlCommand("select count(*) as [odeme yapmayan ogrenciler]from tbl_odeme where tip=0 and islem_tarihi between @p1 and @p2", conn);
                cmd2.Parameters.AddWithValue("@p1", aybasi);
                cmd2.Parameters.AddWithValue("@p2", aysonu);
                object result2 = cmd2.ExecuteScalar();
                odeme_yapmayan_ogrenciler = Convert.ToInt32(result2);
                chrt_odeme.Series["odeme durumu"].Points.AddXY("Ödeme Yapmayan Öğrenciler", odeme_yapmayan_ogrenciler);
                chrt_odeme.Series["odeme durumu"].Points.AddXY("Ödeme Yapan Öğrenciler", odeme_yapan_ogrenciler);
            }
            //sınıf dağılımı
            chrt_sinif.Series.Clear();
            chrt_sinif.Series.Add("Sınıf Dağılımı");
            chrt_sinif.Series["Sınıf Dağılımı"].ChartType = SeriesChartType.Bar;
            using (SqlConnection conn = bgl.baglan())
            {
                SqlCommand cmd = new SqlCommand("SELECT ogrenci_sinif, COUNT(*) AS sinif_ogrenci_sayisi FROM tbl_ogrenci_bilgi GROUP BY ogrenci_sinif ORDER BY  CASE  WHEN ogrenci_sinif like 'M' THEN 0 ELSE 1 END, CASE  WHEN ISNUMERIC(LEFT(ogrenci_sinif, CHARINDEX('/', ogrenci_sinif) - 1)) = 1  THEN CAST(LEFT(ogrenci_sinif, CHARINDEX('/', ogrenci_sinif) - 1) AS INT) ELSE 999 END desc, ogrenci_sinif desc", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    chrt_sinif.Series["Sınıf Dağılımı"].Points.AddXY(dr["ogrenci_sinif"].ToString(), Convert.ToInt32(dr["sinif_ogrenci_sayisi"]));
                }
            }
            //Alacak Verecek
            decimal alinan, alacak;
            DateTime tarihbaslangic = DateTime.Now.AddMonths(-3);
            DateTime tarihbitis = DateTime.Now.AddMonths(3);
            Dictionary<string, decimal> alacaklar = new Dictionary<string, decimal>();
            Dictionary<string, decimal> alinanlar = new Dictionary<string, decimal>();
            chrt_alacak.Series.Clear();
            chrt_alacak.Series.Add("Alınan").ChartType = SeriesChartType.Column;
            chrt_alacak.Series.Add("Alacak").ChartType = SeriesChartType.Column;
            using (SqlConnection conn = bgl.baglan())
            {
                SqlCommand cmd = new SqlCommand("select format (islem_tarihi ,'yyyy-MM') as ay,sum(tutar) as Tutar from tbl_odeme where islem_tarihi between @p1 and @p2 group by format(islem_tarihi,'yyyy-MM')", conn);
                cmd.Parameters.AddWithValue("@p1", tarihbaslangic);
                cmd.Parameters.AddWithValue("@p2", tarihbitis);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    alacaklar.Add(dr["ay"].ToString(), Convert.ToDecimal(dr["Tutar"]));
                }
                dr.Close();
                SqlCommand cmd2 = new SqlCommand("select format (islem_tarihi ,'yyyy-MM') as ay,sum(tutar) as Tutar from tbl_odeme where tip=1 and islem_tarihi between @p1 and @p2 group by format(islem_tarihi,'yyyy-MM')", conn);
                cmd2.Parameters.AddWithValue("@p1", tarihbaslangic);
                cmd2.Parameters.AddWithValue("@p2", tarihbitis);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    alinanlar.Add(dr2["ay"].ToString(), Convert.ToDecimal(dr2["Tutar"]));
                }
                dr2.Close();
                for (int i = -3; i <= 3; i++)
                {
                    DateTime tarih = DateTime.Now.AddMonths(i);
                    string ay = tarih.ToString("yyyy-MM");
                    alacak = alacaklar.ContainsKey(ay) ? alacaklar[ay] : 0;
                    alinan = alinanlar.ContainsKey(ay) ? alinanlar[ay] : 0;

                    string gosterimay = tarih.ToString("MMM", new CultureInfo("tr-TR"));
                    chrt_alacak.Series["Alınan"].Points.AddXY(gosterimay, alinan);
                    chrt_alacak.Series["Alacak"].Points.AddXY(gosterimay, alacak);
                }
            }
        }
        private void chartlaringorselleri()
        {
            string ay = DateTime.Now.ToString("MMMM", new CultureInfo("tr-TR"));
            //sınıf dağılımı
            chrt_sinif.Titles.Clear();
            chrt_sinif.Titles.Add("Sınıflara Göre Öğrenci Dağılımı");
            chrt_sinif.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);
            chrt_sinif.BackColor = this.BackColor;
            chrt_sinif.ChartAreas[0].BackColor = this.BackColor;
            chrt_sinif.Legends[0].BackColor = this.BackColor;
            chrt_sinif.Legends[0].Font = new Font("Arial", 11, FontStyle.Regular);
            chrt_sinif.ChartAreas[0].AxisX.Title = "Sınıf";
            chrt_sinif.ChartAreas[0].AxisY.Title = "Öğrenci Sayısı";
            chrt_sinif.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 11, FontStyle.Regular);
            chrt_sinif.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Arial", 11, FontStyle.Regular);
            chrt_sinif.ChartAreas[0].AxisX.Interval = 1;
            chrt_sinif.Series["Sınıf Dağılımı"].Color = ColorTranslator.FromHtml("#94b6d6");
            //devamsızlık
            chrt_devamsizlik.Titles.Clear();
            chrt_devamsizlik.Titles.Add($"{ay} Ayı Devamsızlık Durumu");
            chrt_devamsizlik.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);
            chrt_devamsizlik.BackColor = this.BackColor;
            chrt_devamsizlik.ChartAreas[0].BackColor = this.BackColor;
            chrt_devamsizlik.Legends[0].Font = new Font("Arial", 10, FontStyle.Regular);
            chrt_devamsizlik.Legends[0].BackColor = this.BackColor;
            chrt_devamsizlik.Series["Devamsızlık"].Points[0].Color = ColorTranslator.FromHtml("#fc786f");
            chrt_devamsizlik.Series["Devamsızlık"].Points[1].Color = ColorTranslator.FromHtml("#c7fab1");

            // ödeme
            chrt_odeme.Titles.Clear();
            chrt_odeme.Titles.Add($"{ay} Ayı Ödeme Durumu");
            chrt_odeme.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);
            chrt_odeme.BackColor = this.BackColor;
            chrt_odeme.ChartAreas[0].BackColor = this.BackColor;
            chrt_odeme.Legends[0].BackColor = this.BackColor;
            chrt_odeme.Legends[0].Font = new Font("Arial", 11, FontStyle.Regular);
            chrt_odeme.Series["odeme durumu"].Points[0].Color = ColorTranslator.FromHtml("#fc786f");
            chrt_odeme.Series["odeme durumu"].Points[1].Color = ColorTranslator.FromHtml("#c7fab1");

            //alacak
            chrt_alacak.Titles.Clear();
            chrt_alacak.Titles.Add("Alacak ve Alınan Durumu");
            chrt_alacak.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);
            chrt_alacak.BackColor = this.BackColor;
            chrt_alacak.ChartAreas[0].BackColor = this.BackColor;
            chrt_alacak.Legends[0].BackColor = this.BackColor;
            chrt_alacak.Legends[0].Font = new Font("Arial", 11, FontStyle.Regular);
            chrt_alacak.Series["Alacak"].Color = ColorTranslator.FromHtml("#fc786f");
            chrt_alacak.Series["Alınan"].Color = ColorTranslator.FromHtml("#c7fab1");
            chrt_alacak.ChartAreas[0].AxisX.Title = "Aylar";
            chrt_alacak.ChartAreas[0].AxisY.Title = "Tutar";
            chrt_alacak.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 11, FontStyle.Regular);
            chrt_alacak.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Arial", 11, FontStyle.Regular);
            chrt_alacak.ChartAreas[0].AxisX.Interval = 1;
            chrt_alacak.ChartAreas[0].AxisY.Interval = 50000;
        }
        private void labellaridoldurma()
        {
            using (SqlConnection conn = bgl.baglan())
            {
                SqlCommand cmd = new SqlCommand("select count(distinct ogrenci_sinif) from tbl_ogrenci_bilgi", conn);
                object result = cmd.ExecuteScalar();
                lbl_sinif.Text = result.ToString();
                SqlCommand cmd2 = new SqlCommand("select count(id) from tbl_ogrenci_bilgi", conn);
                object result2 = cmd2.ExecuteScalar();
                lbl_toplam_ogrenci.Text = result2.ToString();
                SqlCommand cmd3 = new SqlCommand("select kasadaki_para from tbl_kasa", conn);
                object result3 = cmd3.ExecuteScalar();
                decimal kasa = Convert.ToDecimal(result3);
                lbl_kasa.Text = kasa.ToString("N0") + " TL";
            }
        }
        private void frm_dashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            lbl_toplam_devamsizlik_yoklama.Visible = false;
            chartdoldurma();
            chartlaringorselleri();
            ilk_harf_buyuk();
            frm_ogrenci_bilgi_kodlar();
            frm_ogrenci_kayit_kodlar();
            frm_odeme_kodlar();
            labellaridoldurma();
            if (yonetici != true)
            {
                tabControl1.TabPages.Remove(tp_kullanici);
            }
            kullanici_ekleme_tablo();
        }

        private void dtv_ogrenci_bilgi_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = dtv_ogrenci_bilgi.Columns[e.ColumnIndex];

                if (column is DataGridViewButtonColumn)
                {
                    e.PaintBackground(e.ClipBounds, true);
                    Color buttonColor = Color.White;
                    Color textcolor = Color.Black;
                    if (column.Name == "Ödeme")
                    {
                        buttonColor = Color.FromArgb(76, 175, 80);
                        textcolor = Color.White;
                    }
                    else if (column.Name == "Düzenle")
                    {
                        buttonColor = Color.FromArgb(33, 150, 243);
                        textcolor = Color.White;
                    }
                    else if (column.Name == "Devamsızlık")
                    {
                        buttonColor = Color.FromArgb(255, 193, 7);
                        textcolor = Color.Black;
                    }
                    else if (column.Name == "Yoklama")
                    {
                        buttonColor = ColorTranslator.FromHtml("#BDBDBD");
                        textcolor = Color.Black;
                    }
                    using (SolidBrush brush = new SolidBrush(buttonColor))
                    {
                        e.Graphics.FillRectangle(brush, e.CellBounds);
                    }
                    e.Graphics.DrawRectangle(Pens.Black, e.CellBounds);
                    TextRenderer.DrawText(e.Graphics, e.FormattedValue.ToString(), e.CellStyle.Font, e.CellBounds, textcolor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    e.Handled = true;
                }
            }
        }

        private void dtv_ogrenci_bilgi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtv_ogrenci_bilgi.Columns["Ödeme"].Index && e.RowIndex >= 0)
            {
                ogrenci_id = Convert.ToInt32(dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value);
                tabControl1.SelectedTab = tp_odeme;
                frm_odeme_kodlar();
                string toplamTutar = Convert.ToString(dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Toplam Tutar"].Value);
            }
            else if (e.ColumnIndex == dtv_ogrenci_bilgi.Columns["Düzenle"].Index && e.RowIndex >= 0)
            {

                ogrenci_id = Convert.ToInt32(dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value);
                lblid.Text = ogrenci_id.ToString();
                txt_isim.Text = dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Öğrenci İsim"].Value.ToString();
                txt_soyisim.Text = dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Öğrenci Soyisim"].Value.ToString();
                cmb_sinif.Text = dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Öğrenci Sınıf"].Value.ToString();
                msk_ogrtel.Text = dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Öğrenci Telefon Numarası"].Value.ToString();
                msk_velitel.Text = dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Veli Telefon Numarası"].Value.ToString();
                txt_tutar.Text = dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Toplam Tutar"].Value.ToString();
                tabControl1.SelectedTab = tp_ogrenci_kayit;
            }
            else if (e.ColumnIndex == dtv_ogrenci_bilgi.Columns["Devamsızlık"].Index && e.RowIndex >= 0)
            {
                ogrenci_id = Convert.ToInt32(dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value);
                frm_devamsizlik_bilgi frm_devamsizlik_bilgi = new frm_devamsizlik_bilgi();
                frm_devamsizlik_bilgi.ogrenci_id = ogrenci_id;
                frm_devamsizlik_bilgi.Show();
            }
            else if (e.ColumnIndex == dtv_ogrenci_bilgi.Columns["Yoklama"].Index && e.RowIndex >= 0)
            {
                ogrenci_id = Convert.ToInt32(dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value);
                lbl_toplam_devamsizlik_yoklama.Visible = true;
                txt_ogrenci_id_yoklama.Text = ogrenci_id.ToString();
                lbl_toplam_devamsizlik_yoklama.Text = dtv_ogrenci_bilgi.Rows[e.RowIndex].Cells["Toplam Devamsızlık"].Value.ToString();
                tabControl1.SelectedTab = tp_yoklama;
            }
        }

        private void btn_ara_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)dtv_ogrenci_bilgi.DataSource;
            string filter = txt_ara.Text.Trim().ToLower();
            if (int.TryParse(filter, out _))
            {
                bs.Filter = $"Convert([Öğrenci Numarası] , 'System.String') like '%{filter}%'";
            }
            else
            {
                bs.Filter = $"[Öğrenci İsim] like '%{txt_ara.Text}%' or [Öğrenci Soyisim] like '%{txt_ara.Text}%' or [Öğrenci Sınıf] like '%{txt_ara.Text}%' or [Öğrenci Telefon Numarası] like '%{txt_ara.Text}%' or [Veli Telefon Numarası] like '%{txt_ara.Text}%'";

            }
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            ogrenci_bilgi_tablo();
            txt_ara.Clear();
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
                    txt_tutar.Text = "";
                    ogrenci_kayit_tablo_yenile();
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
                    ogrenci_kayit_tablo_yenile();
                }
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
            lblid.Text = "0";
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
                            ogrenci_kayit_tablo_yenile();
                            MessageBox.Show("Öğrenci başarıyla Silindi", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtv_odeme.Columns[e.ColumnIndex].Name == "Durum")
            {
                string durum = e.Value?.ToString();
                DataGridViewRow row = dtv_odeme.Rows[e.RowIndex];
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
            else if (dtv_odeme.Columns[e.ColumnIndex].Name == "Durumimg")
            {
                string durum = dtv_odeme.Rows[e.RowIndex].Cells["Durum"].Value.ToString();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal silinenborc = 0;
            if (e.RowIndex >= 0 && e.ColumnIndex == dtv_odeme.Columns["Sil"].Index)
            {
                DialogResult dr = MessageBox.Show("Taksidi silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    using (SqlConnection conn = bgl.baglan())
                    {
                        SqlCommand cmd = new SqlCommand("delete from tbl_odeme where id=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", dtv_odeme.Rows[e.RowIndex].Cells["id"].Value.ToString());
                        cmd.ExecuteNonQuery();
                        //Borcu güncelleme
                        silinenborc = Convert.ToDecimal(dtv_odeme.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString().Replace(" TL", "").Replace(".", "").Replace(",", ""));
                        SqlCommand cmd2 = new SqlCommand("update tbl_ogrenci_bilgi set toplam_tutar=toplam_tutar-@p1 where id=@p2", conn);
                        cmd2.Parameters.AddWithValue("@p1", silinenborc);
                        cmd2.Parameters.AddWithValue("@p2", ogrenci_id);
                        cmd2.ExecuteNonQuery();
                    }
                    odeme_tablo();
                }
                else
                {
                    MessageBox.Show("İşlem iptal edildi", "İşlem İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                dtp_tarih.Text = dtv_odeme.Rows[e.RowIndex].Cells["İslem Tarihi"].Value.ToString();
                guncelleme = true;
                taksit = Convert.ToInt32(dtv_odeme.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString().Replace(" TL", "").Replace(".", "").Replace(",", ""));
                txt_tutar.Text = dtv_odeme.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString();
                odemeid = Convert.ToInt32(dtv_odeme.Rows[e.RowIndex].Cells["id"].Value.ToString());
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = dtv_odeme.Columns[e.ColumnIndex];
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dtp_tarih.Text = dtv_odeme.Rows[e.RowIndex].Cells["İslem Tarihi"].Value.ToString();
            guncelleme = true;
            taksit = Convert.ToInt32(dtv_odeme.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString().Replace(" TL", "").Replace(".", "").Replace(",", ""));
            txt_tutar_odeme.Text = dtv_odeme.Rows[e.RowIndex].Cells["Ödeme Tutarı"].Value.ToString();
            odemeid = Convert.ToInt32(dtv_odeme.Rows[e.RowIndex].Cells["id"].Value.ToString());
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
                odeme_tablo();
            }
        }

        private void btn_borc_Click(object sender, EventArgs e)
        {
            try
            {
                toplamtutar = Convert.ToDecimal(lbltoplam.Text.Replace(" TL", "").Replace(".", "").Replace(",", ""));
                //txt_tutar boş mu kontrolü 
                if (!string.IsNullOrWhiteSpace(txt_tutar_odeme.Text))
                {
                    tutar = Convert.ToDecimal(txt_tutar_odeme.Text.Trim().ToUpper().Replace(",", "").Replace(".", "").Replace(" TL", ""));
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
                            //kasayı güncelleme
                            SqlCommand cmd3 = new SqlCommand("update tbl_kasa set kasadaki_para=kasadaki_para-@p1", conn);
                            cmd3.Parameters.AddWithValue("@p1", tutar);
                            cmd3.ExecuteNonQuery();
                            labellaridoldurma();
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
                chartdoldurma();
                chartlaringorselleri();
                bilgileri_temizle();
            }
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
                        tutar = decimal.Parse(txt_tutar_odeme.Text.Replace(" TL", "").Replace(".", "").Replace(",", ""));
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
                            tutar = decimal.Parse(txt_tutar_odeme.Text.Replace(" TL", "").Replace(".", "").Replace(",", ""));
                            kalan = toplamtutar - tutar;
                            lblkalan.Text = kalan.ToString("N0") + " TL";


                            //kasadaki parayı güncelleme 
                            SqlCommand cmd3 = new SqlCommand("update tbl_kasa set kasadaki_para=kasadaki_para+@p1", conn);
                            cmd3.Parameters.AddWithValue("@p1", tutar);
                            cmd3.ExecuteNonQuery();
                            SqlCommand cmd4 = new SqlCommand("select kasadaki_para from tbl_kasa", conn);
                            object result2 = cmd4.ExecuteScalar();
                            decimal kasa = result2 != DBNull.Value ? Convert.ToDecimal(result2) : 0;
                            lbl_kasa.Text = kasa.ToString("N0") + " TL";
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
                chartdoldurma();
                chartlaringorselleri();
                bilgileri_temizle();
                //makbuz yazdırma
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Makbuz", 280, 500);
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
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
                odeme_tablo();
            }
        }

        private void cmb_telno_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
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
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata");
            }
            finally
            {
                odeme_tablo();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 12, FontStyle.Bold);
            float y = 10;
            float x = 10;
            e.Graphics.DrawString("Etüt Merkezi Tahsilat Makbuzu", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString("--------------------------------------------------", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"Öğrenci Numarası: {ogrenci_id}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"Öğrenci Adı: {cmb_ogrenciad.Text}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"İşlem Tarihi: {DateTime.Now.ToShortDateString()}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"Ödeme Tutarı: {tutar} TL", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"Kalan Borç: {lblkalan.Text}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString("--------------------------------------------------", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString($"Ödemeyi Alan: {lbl_isim.Text}", font, Brushes.Black, new PointF(x, y)); y += 25;
            e.Graphics.DrawString("İmza: ", font, Brushes.Black, new PointF(x, y)); y += 25;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = bgl.baglan())
                {
                    if (txt_ogrenci_id_yoklama.Text == "")
                    {
                        return;
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Select sum(devamsizlik_toplam) as toplam_devamsizlik from tbl_devamsizlik where ogrenci_id=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", txt_ogrenci_id_yoklama.Text);
                        object result = cmd.ExecuteScalar();
                        decimal toplamdevamsizlik = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                        lbl_toplam_devamsizlik_yoklama.Text = toplamdevamsizlik.ToString().Replace(",", ".");
                        lbl_toplam_devamsizlik_yoklama.Visible = true;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata");
            }
            finally
            {

            }
        }

        private void btn_ekle_kullanici_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                yonetici = true;
            }
            else if (radioButton2.Checked)
            {
                yonetici = false;
            }
            else
            {
                MessageBox.Show("Lütfen yönetici türünü seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ilkharfbuyuk(txt_isim_kullanici);
            ilkharfbuyuk(txt_soyisim_kullanici);
            using (SqlConnection conn = bgl.baglan())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into tbl_user (yonetici,isim,soyisim,kullanici_adi,telefon_no,sifre) values (@p1,@p2,@p3,@p4,@p5,@p6)", conn);
                    cmd.Parameters.AddWithValue("@p1", yonetici);
                    cmd.Parameters.AddWithValue("@p2", txt_isim_kullanici.Text);
                    cmd.Parameters.AddWithValue("@p3", txt_soyisim_kullanici.Text);
                    cmd.Parameters.AddWithValue("@p4", txt_kullanici_adi_kullanici.Text);
                    cmd.Parameters.AddWithValue("@p5", mtb_tel_no_kullanici.Text);
                    cmd.Parameters.AddWithValue("@p6", txt_sifre_kullanici.Text);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString(), "Hata oluştu");
                }
                finally
                {
                    kullanici_ekleme_tablo();
                    txt_isim_kullanici.Text = "";
                    txt_soyisim_kullanici.Text = "";
                    txt_kullanici_adi_kullanici.Text = "";
                    mtb_tel_no_kullanici.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    txt_sifre_kullanici.Text = "";
                }
            }
        }
        int kullanici_id = 0;
        private void btn_sil_kullanici_Click(object sender, EventArgs e)
        {
            if (kullanici_id == 0)
            {
                MessageBox.Show("Silmek istediğiniz kullanıcıyı seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DialogResult sil = MessageBox.Show("Silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (sil == DialogResult.Yes)
                {
                    using (SqlConnection conn = bgl.baglan())
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand("delete from tbl_user where id=@p1", conn);
                            cmd.Parameters.AddWithValue("@p1", kullanici_id);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString(), "Hata oluştu");
                        }
                        finally
                        {
                            kullanici_ekleme_tablo();
                            kullanici_id = 0;
                        }
                    }
                }
            }
        }

        private void btn_duzenle_kullanici_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                yonetici = true;
            }
            else if (radioButton2.Checked)
            {
                yonetici = false;
            }
            else
            {
                MessageBox.Show("Lütfen yönetici türünü seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                using (SqlConnection conn = bgl.baglan())
                {
                    SqlCommand cmd = new SqlCommand("update tbl_user set isim=@p1,soyisim=@p2,kullanici_adi=@p3,telefon_no=@p4,sifre=@p5,yonetici=@p6 where id=@p7", conn);
                    cmd.Parameters.AddWithValue("@p1", txt_isim_kullanici.Text);
                    cmd.Parameters.AddWithValue("@p2", txt_soyisim_kullanici.Text);
                    cmd.Parameters.AddWithValue("@p3", txt_kullanici_adi_kullanici.Text);
                    cmd.Parameters.AddWithValue("@p4", mtb_tel_no_kullanici.Text);
                    cmd.Parameters.AddWithValue("@p5", txt_sifre_kullanici.Text);
                    cmd.Parameters.AddWithValue("@p6", yonetici);
                    cmd.Parameters.AddWithValue("@p7", kullanici_id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata oluştu");
            }
            finally
            {
                kullanici_ekleme_tablo();
                txt_isim_kullanici.Text = "";
                txt_soyisim_kullanici.Text = "";
                txt_kullanici_adi_kullanici.Text = "";
                mtb_tel_no_kullanici.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                txt_sifre_kullanici.Text = "";
            }
        }

        private void btn_temizle_kullanici_Click(object sender, EventArgs e)
        {
            kullanici_id = 0;
            txt_isim_kullanici.Text = "";
            txt_soyisim_kullanici.Text = "";
            txt_kullanici_adi_kullanici.Text = "";
            mtb_tel_no_kullanici.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txt_sifre_kullanici.Text = "";
        }

        private void dtv_kullanici_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            kullanici_id = Convert.ToInt32(dtv_kullanici.Rows[e.RowIndex].Cells[0].Value.ToString());
            txt_isim_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_soyisim_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_kullanici_adi_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_sifre_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[4].Value.ToString();
            if (dtv_kullanici.Rows[e.RowIndex].Cells[5].Value.ToString() == "True")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            mtb_tel_no_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void dtv_kullanici_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            kullanici_id = Convert.ToInt32(dtv_kullanici.Rows[e.RowIndex].Cells[0].Value.ToString());
            txt_isim_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_soyisim_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_kullanici_adi_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_sifre_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[4].Value.ToString();
            if (dtv_kullanici.Rows[e.RowIndex].Cells[5].Value.ToString() == "True")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            mtb_tel_no_kullanici.Text = dtv_kullanici.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void btn_cek_Click(object sender, EventArgs e)
        {
            try
            {
                int kasadaki_para = Convert.ToInt32(lbl_kasa.Text.Replace(" TL", "").Replace(".", "").Replace(",", ""));
                int cekilen_para = Convert.ToInt32(txt_cek.Text.Trim().ToUpper().Replace(",", "").Replace(".", "").Replace(" TL", ""));
                int kalan_para = kasadaki_para - cekilen_para;
                using (SqlConnection conn = bgl.baglan())
                {
                    SqlCommand cmd = new SqlCommand("update tbl_kasa set kasadaki_para=@p1", conn);
                    cmd.Parameters.AddWithValue("@p1", kalan_para);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("select kasadaki_para from tbl_kasa", conn);
                    object result = cmd2.ExecuteScalar();
                    lbl_kasa.Text = result != DBNull.Value ? Convert.ToInt32(result).ToString("N0") + " TL" : "0 TL";
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Hata");
            }
            finally
            {
                printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Makbuz", 280, 500);
                printPreviewDialog2.Document = printDocument2;
                printPreviewDialog2.ShowDialog();
                txt_cek.Text = "";
            }
        }

        private void chrt_alacak_Click(object sender, EventArgs e)
        {
            frm_odeme_yapmayanlar odemeyapmayan = new frm_odeme_yapmayanlar();
            odemeyapmayan.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tp_anasayfa)
            {
                this.Text = "Uygulama Ana Sayfası";
            }
            else if (tabControl1.SelectedTab == tp_odeme)
            {
                this.Text = "Ödeme Ekranı";
            }
            else if (tabControl1.SelectedTab == tp_ogrenci_bilgi)
            {
                this.Text = "Öğrenci Bilgi Ekranı";
            }
            else if (tabControl1.SelectedTab == tp_kullanici)
            {
                this.Text = "Kullanıcı Düzenleme Ekranı";
            }
            else if (tabControl1.SelectedTab == tp_yoklama)
            {
                this.Text = "Yoklama Giriş Ekranı";
            }
            else if (tabControl1.SelectedTab == tp_ogrenci_kayit)
            {
                this.Text = "Öğrenci Kayıt Ekranı";
            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Kasadan Alınan Paranın Makbuzu", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 10));
            e.Graphics.DrawString("--------------------------------------------------", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 40));
            e.Graphics.DrawString($"Çekilen Tutar: {txt_cek.Text.Trim().ToUpper().Replace(",", "").Replace(".", "").Replace(" TL", "")} TL", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 70));
            e.Graphics.DrawString($"Kasada Kalan Para: {lbl_kasa.Text}", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 100));
            e.Graphics.DrawString($"Paranın çekildiği tarih {DateTime.Now.ToShortDateString()}", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 130));
            e.Graphics.DrawString("--------------------------------------------------", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(10, 160));
        }

        private void yenileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            chartdoldurma();
            chartlaringorselleri();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            decimal devamsizlik_turu;
            if (rb_gec.Checked == true)
            {
                devamsizlik_turu = 0.5m;
            }
            else if (rb_gelmedi.Checked == true)
            {
                devamsizlik_turu = 1m;
            }
            else
            {
                devamsizlik_turu = 0;
            }
            using (SqlConnection conn = bgl.baglan())
            {
                try
                {
                    if (rb_gec.Checked == false && rb_gelmedi.Checked == false)
                    {
                        MessageBox.Show("Lütfen devamsızlık türünü seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("insert into tbl_devamsizlik (ogrenci_id,devamsizlik_tarihi, devamsizlik_toplam) values (@p1,@p2,@p3)", conn);
                        cmd.Parameters.AddWithValue("@p1", txt_ogrenci_id_yoklama.Text);
                        cmd.Parameters.AddWithValue("@p2", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@p3", devamsizlik_turu);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("select sum(devamsizlik_toplam) from tbl_devamsizlik where ogrenci_id=@p1", conn);
                        cmd2.Parameters.AddWithValue("@p1", txt_ogrenci_id_yoklama.Text);
                        object result = cmd2.ExecuteScalar();
                        SqlCommand cmd3 = new SqlCommand("update tbl_ogrenci_bilgi set devamsizlik_durumu=@p1 where id=@p2", conn);
                        cmd3.Parameters.AddWithValue("@p1", result);
                        cmd3.Parameters.AddWithValue("@p2", txt_ogrenci_id_yoklama.Text);
                        cmd3.ExecuteNonQuery();
                        lbl_toplam_devamsizlik_yoklama.Text = result.ToString();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    rb_gec.Checked = false;
                    rb_gelmedi.Checked = false;
                    frm_ogrenci_bilgi_kodlar();
                    txt_ogrenci_id_yoklama.Clear();
                    chartdoldurma();
                    chartlaringorselleri();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)dtv_ogrenci_kayit.DataSource;
            bs.Filter = $"[Öğrenci İsim] LIKE '%{textBox2.Text}%' OR [Öğrenci Soyisim] LIKE '%{textBox2.Text}%' OR [Öğrenci Sınıf] LIKE '%{textBox2.Text}%' OR [Öğrenci Telefon Numarası] LIKE '%{textBox2.Text}%' OR [Veli Telefon Numarası] LIKE '%{textBox2.Text}%'";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            ogrenci_kayit_tablo_yenile();
        }

        private void dgv_ogrenci_kayit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblid.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value.ToString();
            txt_isim.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Öğrenci İsim"].Value.ToString();
            txt_soyisim.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["ÖĞrenci Soyisim"].Value.ToString();
            cmb_sinif.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Öğrenci Sınıf"].Value.ToString();
            msk_ogrtel.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Öğrenci Telefon Numarası"].Value.ToString();
            msk_velitel.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Veli Telefon Numarası"].Value.ToString();
            txt_tutar.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Toplam Ücret"].Value.ToString();
        }

        private void dtv_ogrenci_kayit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_isim.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Öğrenci İsim"].Value.ToString();
            txt_soyisim.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["ÖĞrenci Soyisim"].Value.ToString();
            cmb_sinif.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Öğrenci Sınıf"].Value.ToString();
            msk_ogrtel.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Öğrenci Telefon Numarası"].Value.ToString();
            msk_velitel.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Veli Telefon Numarası"].Value.ToString();
            txt_tutar.Text = dtv_ogrenci_kayit.Rows[e.RowIndex].Cells["Toplam Ücret"].Value.ToString();
        }

        private void chrt_odeme_PostPaint(object sender, ChartPaintEventArgs e)
        {
            Chart chart = (Chart)sender;

            float centerx = chart.ChartAreas[0].Position.X + (chart.ChartAreas[0].Position.Width / 2);
            float centery = chart.ChartAreas[0].Position.Y + (chart.ChartAreas[0].Position.Width / 2);

            float pixelx = chart.Width * centerx / 100;
            float pixely = chart.Height * centery / 100 + 50;

            string yazi;
            double toplam = odeme_yapan_ogrenciler + odeme_yapmayan_ogrenciler;
            double oran = ((odeme_yapmayan_ogrenciler / toplam) * 100);
            oran = Math.Floor(oran);
            yazi = string.Format($"%{oran} \n Ödeme yapmayan ");
            // Yazı tipi ve hizalama
            e.ChartGraphics.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.ChartGraphics.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            using (Font font = new Font("Arial", 11, FontStyle.Bold))

            using (Brush brush = new SolidBrush(SystemColors.ControlText))
            using (StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                e.ChartGraphics.Graphics.DrawString(yazi, font, brush, new PointF(pixelx, pixely), format);
            }
        }

        private void chrt_devamsizlik_PostPaint(object sender, ChartPaintEventArgs e)
        {
            Chart chart = (Chart)sender;

            float centerx = chart.ChartAreas[0].Position.X + (chart.ChartAreas[0].Position.Width / 2);
            float centery = chart.ChartAreas[0].Position.Y + (chart.ChartAreas[0].Position.Width / 2);

            float pixelx = chart.Width * centerx / 100;
            float pixely = chart.Height * centery / 100 + 50;

            string yazi;
            double toplam = gelen + gelmeyen;
            double oran = ((gelmeyen / toplam) * 100);
            oran = Math.Floor(oran);
            yazi = string.Format($"%{oran} \n Devamsızlık Yapan");
            using (Font font = new Font("Arial", 11, FontStyle.Bold))
            using (Brush brush = new SolidBrush(SystemColors.ControlText))
            using (StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                e.ChartGraphics.Graphics.DrawString(yazi, font, brush, new PointF(pixelx, pixely), format);
            }
        }
    }
}
