namespace etut_merkezi_uygulamasi
{
    partial class frm_secim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_secim));
            this.btn_yoklama = new System.Windows.Forms.Button();
            this.btn_ogrenci_bilgi = new System.Windows.Forms.Button();
            this.btn_ogrenci_kayıt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_yoklama
            // 
            this.btn_yoklama.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_yoklama.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_yoklama.Location = new System.Drawing.Point(151, 77);
            this.btn_yoklama.Name = "btn_yoklama";
            this.btn_yoklama.Size = new System.Drawing.Size(146, 57);
            this.btn_yoklama.TabIndex = 0;
            this.btn_yoklama.Text = "Yoklama Giriş Ekranı";
            this.btn_yoklama.UseVisualStyleBackColor = false;
            this.btn_yoklama.Click += new System.EventHandler(this.btn_yoklama_Click);
            // 
            // btn_ogrenci_bilgi
            // 
            this.btn_ogrenci_bilgi.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_ogrenci_bilgi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ogrenci_bilgi.Location = new System.Drawing.Point(151, 277);
            this.btn_ogrenci_bilgi.Name = "btn_ogrenci_bilgi";
            this.btn_ogrenci_bilgi.Size = new System.Drawing.Size(146, 57);
            this.btn_ogrenci_bilgi.TabIndex = 2;
            this.btn_ogrenci_bilgi.Text = "Öğrenci Bilgi Ekranı";
            this.btn_ogrenci_bilgi.UseVisualStyleBackColor = false;
            this.btn_ogrenci_bilgi.Click += new System.EventHandler(this.btn_ogrenci_bilgi_Click);
            // 
            // btn_ogrenci_kayıt
            // 
            this.btn_ogrenci_kayıt.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_ogrenci_kayıt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ogrenci_kayıt.Location = new System.Drawing.Point(151, 177);
            this.btn_ogrenci_kayıt.Name = "btn_ogrenci_kayıt";
            this.btn_ogrenci_kayıt.Size = new System.Drawing.Size(146, 57);
            this.btn_ogrenci_kayıt.TabIndex = 3;
            this.btn_ogrenci_kayıt.Text = "Öğrenci Kayıt Ekranı";
            this.btn_ogrenci_kayıt.UseVisualStyleBackColor = false;
            this.btn_ogrenci_kayıt.Click += new System.EventHandler(this.btn_ogrenci_kayıt_Click);
            // 
            // frm_secim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(214)))), ((int)(((byte)(210)))));
            this.ClientSize = new System.Drawing.Size(443, 435);
            this.Controls.Add(this.btn_ogrenci_kayıt);
            this.Controls.Add(this.btn_ogrenci_bilgi);
            this.Controls.Add(this.btn_yoklama);
            this.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.Name = "frm_secim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yönetim ekranı";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_secim_FormClosing);
            this.Load += new System.EventHandler(this.frm_secim_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_yoklama;
        private System.Windows.Forms.Button btn_ogrenci_bilgi;
        private System.Windows.Forms.Button btn_ogrenci_kayıt;
    }
}