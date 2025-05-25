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
    public partial class frm_odeme_yapmayanlar : Form
    {
        public frm_odeme_yapmayanlar()
        {
            InitializeComponent();
        }
        cls_sqlconn bgl = new cls_sqlconn();


        private void frm_odeme_yapmayanlar_Load(object sender, EventArgs e)
        {
            this.Text = "Ödeme Yapmayan Öğrenciler - " + DateTime.Now.ToString("MMMM yyyy");
            //datagridi doldurma
            DateTime aybasi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime aysonu = aybasi.AddMonths(1).AddTicks(-1);
            try
            {
                using (SqlConnection conn = bgl.baglan())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT ogrenci_id as [Öğrencinin Numarası],format(tutar,'N0')+ ' TL' as [Ödenmemiş Tutar], islem_Tarihi as [Son Ödeme Tarihi] FROM tbl_odeme WHERE tip=0 and islem_tarihi between @p1 and @p2 order by islem_tarihi asc", conn);
                    da.SelectCommand.Parameters.AddWithValue("@p1", aybasi);
                    da.SelectCommand.Parameters.AddWithValue("@p2", aysonu);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //datagrid tasarım
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 51);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 90, 90);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.ClearSelection();
        }
    }
}
