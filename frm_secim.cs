using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace etut_merkezi_uygulamasi
{
    public partial class frm_secim : Form
    {
        public frm_secim()
        {
            InitializeComponent();
        }
        bool exit = true;
        private void frm_secim_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
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
        }

        private void btn_ogrenci_kayıt_Click(object sender, EventArgs e)
        {
            frm_ogrenci_kayıt ogrenci_kayıt = new frm_ogrenci_kayıt();
            ogrenci_kayıt.Show();
            exit = false;
            this.Close();
        }

        private void btn_ogrenci_bilgi_Click(object sender, EventArgs e)
        {
            frm_ogrenci_bilgi ogrenci_bilgi = new frm_ogrenci_bilgi();
            ogrenci_bilgi.Show();
            exit = false;
            this.Close();
        }

        private void btn_yoklama_Click(object sender, EventArgs e)
        {
            frm_yoklama yoklama = new frm_yoklama();
            yoklama.Show();
            exit = false;
            this.Close();
        }

        private void frm_secim_Load(object sender, EventArgs e)
        {
            exit=true;
        }
    }
}
