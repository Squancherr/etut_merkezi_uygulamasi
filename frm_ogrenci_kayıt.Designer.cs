namespace etut_merkezi_uygulamasi
{
    partial class frm_ogrenci_kayıt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ogrenci_kayıt));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_isim = new System.Windows.Forms.TextBox();
            this.txt_soyisim = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.msk_ogrtel = new System.Windows.Forms.MaskedTextBox();
            this.msk_velitel = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_duzenle = new System.Windows.Forms.Button();
            this.btn_ara = new System.Windows.Forms.Button();
            this.txt_ara = new System.Windows.Forms.TextBox();
            this.cmb_sinif = new System.Windows.Forms.ComboBox();
            this.lblid = new System.Windows.Forms.Label();
            this.txt_tutar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_temizle = new System.Windows.Forms.Button();
            this.btn_sil = new System.Windows.Forms.Button();
            this.btn_ekle = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_listele = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "İsim:";
            // 
            // txt_isim
            // 
            this.txt_isim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.txt_isim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_isim.Location = new System.Drawing.Point(60, 37);
            this.txt_isim.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_isim.Name = "txt_isim";
            this.txt_isim.Size = new System.Drawing.Size(200, 29);
            this.txt_isim.TabIndex = 1;
            // 
            // txt_soyisim
            // 
            this.txt_soyisim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.txt_soyisim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_soyisim.Location = new System.Drawing.Point(368, 37);
            this.txt_soyisim.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_soyisim.Name = "txt_soyisim";
            this.txt_soyisim.Size = new System.Drawing.Size(200, 29);
            this.txt_soyisim.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Soyisim:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sınıf:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(284, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "Öğrenci TELNo:";
            // 
            // msk_ogrtel
            // 
            this.msk_ogrtel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.msk_ogrtel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_ogrtel.Location = new System.Drawing.Point(433, 86);
            this.msk_ogrtel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.msk_ogrtel.Mask = "(999) 000-0000";
            this.msk_ogrtel.Name = "msk_ogrtel";
            this.msk_ogrtel.Size = new System.Drawing.Size(174, 29);
            this.msk_ogrtel.TabIndex = 4;
            // 
            // msk_velitel
            // 
            this.msk_velitel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.msk_velitel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk_velitel.Location = new System.Drawing.Point(108, 135);
            this.msk_velitel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.msk_velitel.Mask = "(999) 000-0000";
            this.msk_velitel.Name = "msk_velitel";
            this.msk_velitel.Size = new System.Drawing.Size(180, 29);
            this.msk_velitel.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 135);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Veli TelNo:";
            // 
            // btn_duzenle
            // 
            this.btn_duzenle.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_duzenle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_duzenle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_duzenle.Location = new System.Drawing.Point(180, 186);
            this.btn_duzenle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_duzenle.Name = "btn_duzenle";
            this.btn_duzenle.Size = new System.Drawing.Size(130, 41);
            this.btn_duzenle.TabIndex = 11;
            this.btn_duzenle.Text = "Düzenle";
            this.btn_duzenle.UseVisualStyleBackColor = false;
            this.btn_duzenle.Click += new System.EventHandler(this.btn_duzenle_Click);
            // 
            // btn_ara
            // 
            this.btn_ara.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_ara.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ara.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ara.Location = new System.Drawing.Point(25, 169);
            this.btn_ara.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_ara.Name = "btn_ara";
            this.btn_ara.Size = new System.Drawing.Size(130, 41);
            this.btn_ara.TabIndex = 14;
            this.btn_ara.Text = "Ara";
            this.btn_ara.UseVisualStyleBackColor = false;
            this.btn_ara.Click += new System.EventHandler(this.btn_ara_Click);
            // 
            // txt_ara
            // 
            this.txt_ara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.txt_ara.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ara.Location = new System.Drawing.Point(6, 41);
            this.txt_ara.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_ara.Name = "txt_ara";
            this.txt_ara.Size = new System.Drawing.Size(389, 29);
            this.txt_ara.TabIndex = 15;
            // 
            // cmb_sinif
            // 
            this.cmb_sinif.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_sinif.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_sinif.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.cmb_sinif.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_sinif.FormattingEnabled = true;
            this.cmb_sinif.Items.AddRange(new object[] {
            "9/A",
            "9/B",
            "9/C",
            "10/A",
            "10/B",
            "10/C",
            "11/A",
            "11/B",
            "11/C",
            "12/A",
            "12/B",
            "12/C",
            "M/A",
            "M/B",
            "M/C"});
            this.cmb_sinif.Location = new System.Drawing.Point(64, 86);
            this.cmb_sinif.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_sinif.Name = "cmb_sinif";
            this.cmb_sinif.Size = new System.Drawing.Size(195, 30);
            this.cmb_sinif.TabIndex = 3;
            // 
            // lblid
            // 
            this.lblid.AutoSize = true;
            this.lblid.Location = new System.Drawing.Point(1863, 39);
            this.lblid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(25, 22);
            this.lblid.TabIndex = 16;
            this.lblid.Text = "id";
            // 
            // txt_tutar
            // 
            this.txt_tutar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.txt_tutar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tutar.Location = new System.Drawing.Point(425, 130);
            this.txt_tutar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_tutar.Name = "txt_tutar";
            this.txt_tutar.Size = new System.Drawing.Size(200, 29);
            this.txt_tutar.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(295, 135);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 22);
            this.label6.TabIndex = 19;
            this.label6.Text = "Toplam Ücret:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_temizle);
            this.groupBox1.Controls.Add(this.btn_sil);
            this.groupBox1.Controls.Add(this.btn_duzenle);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_tutar);
            this.groupBox1.Controls.Add(this.txt_isim);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_soyisim);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmb_sinif);
            this.groupBox1.Controls.Add(this.btn_ekle);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.msk_ogrtel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.msk_velitel);
            this.groupBox1.Location = new System.Drawing.Point(40, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(660, 250);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Öğrenci Ekleme";
            // 
            // btn_temizle
            // 
            this.btn_temizle.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_temizle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_temizle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_temizle.Location = new System.Drawing.Point(350, 186);
            this.btn_temizle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_temizle.Name = "btn_temizle";
            this.btn_temizle.Size = new System.Drawing.Size(130, 41);
            this.btn_temizle.TabIndex = 20;
            this.btn_temizle.Text = "temizle";
            this.btn_temizle.UseVisualStyleBackColor = false;
            this.btn_temizle.Click += new System.EventHandler(this.btn_temizle_Click);
            // 
            // btn_sil
            // 
            this.btn_sil.BackColor = System.Drawing.Color.IndianRed;
            this.btn_sil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_sil.Location = new System.Drawing.Point(521, 186);
            this.btn_sil.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_sil.Name = "btn_sil";
            this.btn_sil.Size = new System.Drawing.Size(130, 41);
            this.btn_sil.TabIndex = 12;
            this.btn_sil.Text = "Sil";
            this.btn_sil.UseVisualStyleBackColor = false;
            this.btn_sil.Click += new System.EventHandler(this.btn_sil_Click);
            // 
            // btn_ekle
            // 
            this.btn_ekle.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_ekle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ekle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ekle.Location = new System.Drawing.Point(9, 186);
            this.btn_ekle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_ekle.Name = "btn_ekle";
            this.btn_ekle.Size = new System.Drawing.Size(130, 41);
            this.btn_ekle.TabIndex = 10;
            this.btn_ekle.Text = "Ekle";
            this.btn_ekle.UseVisualStyleBackColor = false;
            this.btn_ekle.Click += new System.EventHandler(this.btn_ekle_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_listele);
            this.groupBox3.Controls.Add(this.txt_ara);
            this.groupBox3.Controls.Add(this.btn_ara);
            this.groupBox3.Location = new System.Drawing.Point(1500, 12);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(400, 250);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Arama";
            // 
            // btn_listele
            // 
            this.btn_listele.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_listele.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_listele.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_listele.Location = new System.Drawing.Point(224, 169);
            this.btn_listele.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_listele.Name = "btn_listele";
            this.btn_listele.Size = new System.Drawing.Size(130, 41);
            this.btn_listele.TabIndex = 16;
            this.btn_listele.Text = "Listele";
            this.btn_listele.UseVisualStyleBackColor = false;
            this.btn_listele.Click += new System.EventHandler(this.btn_listele_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(214)))), ((int)(((byte)(210)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(1, 433);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1923, 604);
            this.dataGridView1.TabIndex = 23;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // frm_ogrenci_kayıt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(214)))), ((int)(((byte)(210)))));
            this.ClientSize = new System.Drawing.Size(1923, 1061);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblid);
            this.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.Name = "frm_ogrenci_kayıt";
            this.Text = "Öğrenci Kayıt Ekranı";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_ogrenci_kayıt_FormClosed);
            this.Load += new System.EventHandler(this.frm_ogrenci_kayıt_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_duzenle;
        private System.Windows.Forms.Button btn_sil;
        private System.Windows.Forms.Button btn_ara;
        private System.Windows.Forms.TextBox txt_ara;
        public System.Windows.Forms.Label lblid;
        public System.Windows.Forms.TextBox txt_isim;
        public System.Windows.Forms.TextBox txt_soyisim;
        public System.Windows.Forms.MaskedTextBox msk_ogrtel;
        public System.Windows.Forms.MaskedTextBox msk_velitel;
        public System.Windows.Forms.ComboBox cmb_sinif;
        public System.Windows.Forms.TextBox txt_tutar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_ekle;
        private System.Windows.Forms.Button btn_temizle;
        private System.Windows.Forms.Button btn_listele;
        public System.Windows.Forms.DataGridView dataGridView1;
    }
}