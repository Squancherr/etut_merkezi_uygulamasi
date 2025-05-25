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

namespace etut_merkezi_uygulamasi
{
    public partial class frm_devamsizlik_bilgi : Form
    {
        public frm_devamsizlik_bilgi()
        {
            InitializeComponent();
        }
        cls_sqlconn bgl = new cls_sqlconn();
        public int ogrenci_id;
        private void tablo()
        {
            using (SqlConnection conn = bgl.baglan())
            {

                SqlDataAdapter da = new SqlDataAdapter("Select id,ogrenci_id as [Öğrenci Numarası],devamsizlik_tarihi as [Devamsızlık Tarihi],devamsizlik_toplam as [Devamsızlık Türü] from tbl_devamsizlik where ogrenci_id=" + ogrenci_id, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }
        int devamsizlik_id;
        private void frm_devamsizlik_bilgi_Load(object sender, EventArgs e)
        {
            tablo();
            if (!dataGridView1.Columns.Contains("Sil"))
            {
                DataGridViewButtonColumn sil = new DataGridViewButtonColumn();
                sil.Name = "Sil";
                sil.HeaderText = "Sil";
                sil.Text = "Sil";
                sil.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(sil);
            }
            dataGridView1.Columns["id"].Visible = false;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                devamsizlik_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                if (e.ColumnIndex == dataGridView1.Columns["Sil"].Index && e.RowIndex >= 0)
                {
                    DialogResult dr = MessageBox.Show("Devamsızlık kaydını silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        using (SqlConnection conn = bgl.baglan())
                        {

                            SqlCommand cmd = new SqlCommand("Delete from tbl_devamsizlik where ogrenci_id=@p1 and id=@p2", conn);
                            cmd.Parameters.AddWithValue("@p1", ogrenci_id);
                            cmd.Parameters.AddWithValue("@p2", devamsizlik_id);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Devamsızlık kaydı silindi.", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            SqlCommand cmd2 = new SqlCommand("select sum(devamsizlik_toplam) from tbl_devamsizlik where ogrenci_id=@p1", conn);
                            cmd2.Parameters.AddWithValue("@p1", ogrenci_id);
                            object result = cmd2.ExecuteScalar();
                            SqlCommand cmd3 = new SqlCommand("update tbl_ogrenci_bilgi set devamsizlik_durumu=@p1 where id=@p2", conn);
                            cmd3.Parameters.AddWithValue("@p1", result);
                            cmd3.Parameters.AddWithValue("@p2", ogrenci_id);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Hata: " + exp.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tablo();
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
    }
}
