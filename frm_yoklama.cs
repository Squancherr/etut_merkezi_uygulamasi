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
    public partial class frm_yoklama : Form
    {
        public frm_yoklama()
        {
            InitializeComponent();
        }
        cls_sqlconn bgl = new cls_sqlconn();
        public string kullanici_adi;
        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            decimal devamsizlik_turu;
            if (cb_gec.Checked == true)
            {
                devamsizlik_turu = 0.5m;
            }
            else if (cb_gelmedi.Checked == true)
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
                    if (cb_gec.Checked == false && cb_gelmedi.Checked == false)
                    {
                        MessageBox.Show("Lütfen devamsızlık türünü seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("insert into tbl_devamsizlik (ogrenci_id,devamsizlik_tarihi, devamsizlik_toplam) values (@p1,@p2,@p3)", conn);
                        cmd.Parameters.AddWithValue("@p1", txt_ogrenci_id.Text);
                        cmd.Parameters.AddWithValue("@p2", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@p3", devamsizlik_turu);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("select sum(devamsizlik_toplam) from tbl_devamsizlik where ogrenci_id=@p1", conn);
                        cmd2.Parameters.AddWithValue("@p1", txt_ogrenci_id.Text);
                        object result = cmd2.ExecuteScalar();
                        SqlCommand cmd3 = new SqlCommand("update tbl_ogrenci_bilgi set devamsizlik_durumu=@p1 where id=@p2", conn);
                        cmd3.Parameters.AddWithValue("@p1", result);
                        cmd3.Parameters.AddWithValue("@p2", txt_ogrenci_id.Text);
                        cmd3.ExecuteNonQuery();

                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cb_gec.Checked = false;
                    cb_gelmedi.Checked = false;
                }
            }
        }

        private void frm_yoklama_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm_dashboard dashboard = new frm_dashboard();
            dashboard.lbl_isim.Text = kullanici_adi;
            dashboard.Show();
        }

        private void cb_gec_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_gec.Checked)
            {
                cb_gelmedi.Checked = false;
            }
        }

        private void cb_gelmedi_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_gelmedi.Checked)
            {
                cb_gec.Checked = false;
            }
        }

        private void frm_yoklama_Load(object sender, EventArgs e)
        {

        }
    }
}
