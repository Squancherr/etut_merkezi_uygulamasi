using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace etut_merkezi_uygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        cls_sqlconn bgl = new cls_sqlconn();
        frm_dashboard dashboard = new frm_dashboard();
        public string kullanici_adi;
        private class sifrebutton : Button
        {
           
        }
        private void sifre_buttonu_ekleme()
        {
            sifrebutton sb = new sifrebutton();
            sb.Size = new Size(20, 20);
            sb.Location = new Point(533, 130);
            sb.BackColor = Color.FromArgb(234, 237, 237);
            sb.FlatStyle = FlatStyle.Flat;
            sb.FlatAppearance.BorderSize = 0;
            sb.FlatAppearance.BorderColor = Color.FromArgb(234, 237, 237);
            Image sifre = cls_sqlconn.yuksekkalite(Properties.Resources.eye, new Size(20, 20));
            sb.Image = sifre;
            this.Controls.Add(sb);
            sb.BringToFront();
            sb.Click += (s, ev) =>
            {
                if (txt_sifre.UseSystemPasswordChar == true)
                {
                    txt_sifre.UseSystemPasswordChar = false;
                }
                else
                {
                    txt_sifre.UseSystemPasswordChar = true;
                }
            };

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sifre_buttonu_ekleme();
            Image kullanici = cls_sqlconn.yuksekkalite(Properties.Resources.user_interface, new Size(20, 20));
            button1.Image = kullanici;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = bgl.baglan())
            {
                SqlCommand cmd = new SqlCommand("select * from tbl_user where kullanici_adi=@p1 and sifre=@p2", conn);
                cmd.Parameters.AddWithValue("@p1", txt_eposta.Text);
                cmd.Parameters.AddWithValue("@p2", txt_sifre.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    kullanici_adi = dr["isim"].ToString() + " " + dr["soyisim"].ToString();
                    dashboard.lbl_isim.Text = kullanici_adi;
                    dashboard.yonetici = Convert.ToBoolean(dr["yonetici"]);
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı");

                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Uygulamadan çıkış yapmak istediğinizden emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.ExitThread();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
