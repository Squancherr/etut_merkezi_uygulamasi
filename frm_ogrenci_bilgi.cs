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
    public partial class frm_ogrenci_bilgi : Form
    {
        public frm_ogrenci_bilgi()
        {
            InitializeComponent();
        }
        cls_sqlconn bgl = new cls_sqlconn();
        public string kullanici_adi;
        private void tablo()
        {
            using (SqlConnection conn = bgl.baglan())
            {

                SqlDataAdapter da = new SqlDataAdapter("select id as [Öğrenci Numarası],ogrenci_adi as [Öğrenci İsim], ogrenci_soyadi as [Öğrenci Soyisim],ogrenci_sinif as [Öğrenci Sınıf] ,ogrenci_telno as [Öğrenci Telefon Numarası],veli_telno as [Veli Telefon Numarası],format(toplam_tutar,'N0')+' TL' as [Toplam Tutar],devamsizlik_durumu as [Toplam Devamsızlık] from tbl_ogrenci_bilgi", conn);
                DataTable dt = new DataTable();
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                da.Fill(dt);
                dataGridView1.DataSource = bs;
            }
        }
        private void frm_ogrenci_bilgi_Load(object sender, EventArgs e)
        {
            //renk
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
            this.WindowState = FormWindowState.Maximized;
            tablo();

            if (!dataGridView1.Columns.Contains("Ödeme"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Text = "Ödeme";
                btn.Name = "Ödeme";
                btn.UseColumnTextForButtonValue = true;
                btn.HeaderText = "Ödeme";
                btn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn);
                btn.FlatStyle = FlatStyle.Flat;

            }
            if (!dataGridView1.Columns.Contains("Düzenle"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Text = "Düzenle";
                btn.Name = "Düzenle";
                btn.UseColumnTextForButtonValue = true;
                btn.HeaderText = "Düzenle";
                btn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn);
                btn.FlatStyle = FlatStyle.Flat;
            }
            if (!dataGridView1.Columns.Contains("Devamsızlık"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Text = "Devamsızlık";
                btn.Name = "Devamsızlık";
                btn.UseColumnTextForButtonValue = true;
                btn.HeaderText = "Devamsızlık";
                btn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn);
                btn.FlatStyle = FlatStyle.Flat;
            }
            if (!dataGridView1.Columns.Contains("Yoklama"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Text = "Yoklama";
                btn.Name = "Yoklama";
                btn.UseColumnTextForButtonValue = true;
                btn.HeaderText = "Yoklama";
                btn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn);
                btn.FlatStyle = FlatStyle.Flat;
            }

            //buttonlara ikon ekleme
            Image buyutec = cls_sqlconn.yuksekkalite(Properties.Resources.magnifier, new Size(20, 20));
            Image liste = cls_sqlconn.yuksekkalite(Properties.Resources.list, new Size(20, 20));
            button1.Image = buyutec;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            btn_listele.Image = liste;
            btn_listele.ImageAlign = ContentAlignment.MiddleLeft;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)dataGridView1.DataSource;
            bs.Filter = $"[Öğrenci İsim] like '%{txt_ara.Text}%' or [Öğrenci Soyisim] like '%{txt_ara.Text}%' or [Öğrenci Sınıf] like '%{txt_ara.Text}%' or [Öğrenci Telefon Numarası] like '%{txt_ara.Text}%' or [Veli Telefon Numarası] like '%{txt_ara.Text}%'";
        }
        bool exit = true;
        public int ogrenci_id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Ödeme"].Index && e.RowIndex >= 0)
            {
                frm_odeme frm_odeme = new frm_odeme();
                ogrenci_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value);
                frm_odeme.ogrenci_id = ogrenci_id;
                string toplamTutar = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Toplam Tutar"].Value);
                frm_odeme.toplam = decimal.Parse(toplamTutar.Replace(" TL", "").Replace(".", "").Replace(",", ""));
                frm_odeme.Show();
                exit = false;
                this.Close();
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Düzenle"].Index && e.RowIndex >= 0)
            {
                frm_ogrenci_kayıt frm_ogrenci_kayıt = new frm_ogrenci_kayıt();
                ogrenci_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value);
                frm_ogrenci_kayıt.lblid.Text = ogrenci_id.ToString();
                frm_ogrenci_kayıt.txt_isim.Text = dataGridView1.Rows[e.RowIndex].Cells["Öğrenci İsim"].Value.ToString();
                frm_ogrenci_kayıt.txt_soyisim.Text = dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Soyisim"].Value.ToString();
                frm_ogrenci_kayıt.cmb_sinif.Text = dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Sınıf"].Value.ToString();
                frm_ogrenci_kayıt.msk_ogrtel.Text = dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Telefon Numarası"].Value.ToString();
                frm_ogrenci_kayıt.msk_velitel.Text = dataGridView1.Rows[e.RowIndex].Cells["Veli Telefon Numarası"].Value.ToString();
                frm_ogrenci_kayıt.txt_tutar.Text = dataGridView1.Rows[e.RowIndex].Cells["Toplam Tutar"].Value.ToString();
                frm_ogrenci_kayıt.Show();
                exit = false;
                this.Close();
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Devamsızlık"].Index && e.RowIndex >= 0)
            {
                ogrenci_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value);
                frm_devamsizlik_bilgi frm_devamsizlik_bilgi = new frm_devamsizlik_bilgi();
                frm_devamsizlik_bilgi.ogrenci_id = ogrenci_id;
                frm_devamsizlik_bilgi.Show();
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Yoklama"].Index && e.RowIndex >= 0)
            {
                ogrenci_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Öğrenci Numarası"].Value);
                frm_yoklama frm_yoklama_bilgi = new frm_yoklama();
                frm_yoklama_bilgi.txt_ogrenci_id.Text = ogrenci_id.ToString();
                frm_yoklama_bilgi.Show();
                exit = false;
                this.Close();
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

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

        private void frm_ogrenci_bilgi_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (exit)
            {
                frm_dashboard dashboard = new frm_dashboard();
                dashboard.lbl_isim.Text = kullanici_adi;
                dashboard.Show();
            }
        }

        private void frm_ogrenci_bilgi_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablo();
            txt_ara.Clear();
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            tablo();
            txt_ara.Clear();
        }
    }
}
