namespace etut_merkezi_uygulamasi
{
    partial class frm_odeme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_odeme));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbltoplam = new System.Windows.Forms.Label();
            this.btn_tahsilat = new System.Windows.Forms.Button();
            this.btn_borc = new System.Windows.Forms.Button();
            this.lblkalan = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_tutar = new System.Windows.Forms.TextBox();
            this.dtp_tarih = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_telno = new System.Windows.Forms.ComboBox();
            this.cmb_ogrencino = new System.Windows.Forms.ComboBox();
            this.cmb_ogrenciad = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(214)))), ((int)(((byte)(210)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 479);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1924, 582);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting_1);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Öğrenci Adı Soyadı:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "Öğrenci Numarası:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 22);
            this.label5.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(209, 22);
            this.label6.TabIndex = 6;
            this.label6.Text = "Anlaşılan Toplam Ücret:";
            // 
            // lbltoplam
            // 
            this.lbltoplam.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbltoplam.AutoSize = true;
            this.lbltoplam.Location = new System.Drawing.Point(239, 90);
            this.lbltoplam.Name = "lbltoplam";
            this.lbltoplam.Size = new System.Drawing.Size(86, 22);
            this.lbltoplam.TabIndex = 7;
            this.lbltoplam.Text = "lbltoplam";
            // 
            // btn_tahsilat
            // 
            this.btn_tahsilat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btn_tahsilat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tahsilat.Location = new System.Drawing.Point(72, 192);
            this.btn_tahsilat.Name = "btn_tahsilat";
            this.btn_tahsilat.Size = new System.Drawing.Size(230, 53);
            this.btn_tahsilat.TabIndex = 8;
            this.btn_tahsilat.Text = "Tahsilat";
            this.btn_tahsilat.UseVisualStyleBackColor = false;
            this.btn_tahsilat.Click += new System.EventHandler(this.btn_tahsilat_Click);
            // 
            // btn_borc
            // 
            this.btn_borc.BackColor = System.Drawing.Color.IndianRed;
            this.btn_borc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_borc.Location = new System.Drawing.Point(72, 47);
            this.btn_borc.Name = "btn_borc";
            this.btn_borc.Size = new System.Drawing.Size(230, 53);
            this.btn_borc.TabIndex = 9;
            this.btn_borc.Text = "Borçlandır";
            this.btn_borc.UseVisualStyleBackColor = false;
            this.btn_borc.Click += new System.EventHandler(this.btn_borc_Click);
            // 
            // lblkalan
            // 
            this.lblkalan.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblkalan.AutoSize = true;
            this.lblkalan.Location = new System.Drawing.Point(239, 173);
            this.lblkalan.Name = "lblkalan";
            this.lblkalan.Size = new System.Drawing.Size(72, 22);
            this.lblkalan.TabIndex = 11;
            this.lblkalan.Text = "lblkalan";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 22);
            this.label4.TabIndex = 10;
            this.label4.Text = "Kalan Toplam Ücret:";
            // 
            // txt_tutar
            // 
            this.txt_tutar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.txt_tutar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tutar.Location = new System.Drawing.Point(184, 153);
            this.txt_tutar.Name = "txt_tutar";
            this.txt_tutar.Size = new System.Drawing.Size(186, 29);
            this.txt_tutar.TabIndex = 12;
            // 
            // dtp_tarih
            // 
            this.dtp_tarih.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.dtp_tarih.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_tarih.Location = new System.Drawing.Point(184, 46);
            this.dtp_tarih.Name = "dtp_tarih";
            this.dtp_tarih.Size = new System.Drawing.Size(186, 29);
            this.dtp_tarih.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 22);
            this.label2.TabIndex = 14;
            this.label2.Text = "Ödeme Tutarı:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 22);
            this.label7.TabIndex = 15;
            this.label7.Text = "Ödeme Tarihi:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_telno);
            this.groupBox1.Controls.Add(this.cmb_ogrencino);
            this.groupBox1.Controls.Add(this.cmb_ogrenciad);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 285);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Öğrenci Bilgileri";
            // 
            // cmb_telno
            // 
            this.cmb_telno.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_telno.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_telno.FormattingEnabled = true;
            this.cmb_telno.Location = new System.Drawing.Point(193, 189);
            this.cmb_telno.Name = "cmb_telno";
            this.cmb_telno.Size = new System.Drawing.Size(221, 30);
            this.cmb_telno.TabIndex = 11;
            this.cmb_telno.SelectionChangeCommitted += new System.EventHandler(this.cmb_telno_SelectionChangeCommitted);
            // 
            // cmb_ogrencino
            // 
            this.cmb_ogrencino.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_ogrencino.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_ogrencino.FormattingEnabled = true;
            this.cmb_ogrencino.Location = new System.Drawing.Point(193, 75);
            this.cmb_ogrencino.Name = "cmb_ogrencino";
            this.cmb_ogrencino.Size = new System.Drawing.Size(221, 30);
            this.cmb_ogrencino.TabIndex = 10;
            this.cmb_ogrencino.SelectionChangeCommitted += new System.EventHandler(this.cmb_ogrencino_SelectionChangeCommitted);
            // 
            // cmb_ogrenciad
            // 
            this.cmb_ogrenciad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_ogrenciad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_ogrenciad.FormattingEnabled = true;
            this.cmb_ogrenciad.Location = new System.Drawing.Point(193, 130);
            this.cmb_ogrenciad.Name = "cmb_ogrenciad";
            this.cmb_ogrenciad.Size = new System.Drawing.Size(221, 30);
            this.cmb_ogrenciad.TabIndex = 9;
            this.cmb_ogrenciad.SelectionChangeCommitted += new System.EventHandler(this.cmb_ogrenciad_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(82, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 22);
            this.label8.TabIndex = 6;
            this.label8.Text = "Veli Tel No:";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(245, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 22);
            this.label10.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbltoplam);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblkalan);
            this.groupBox2.Location = new System.Drawing.Point(580, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 285);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ücret bilgileri";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtp_tarih);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txt_tutar);
            this.groupBox3.Location = new System.Drawing.Point(1000, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(420, 285);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "İşlemler";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_borc);
            this.groupBox4.Controls.Add(this.btn_tahsilat);
            this.groupBox4.Location = new System.Drawing.Point(1520, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(390, 285);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ekleme İşlemleri";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // frm_odeme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(214)))), ((int)(((byte)(210)))));
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.Name = "frm_odeme";
            this.Text = "Ödeme Ekranı";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_odeme_FormClosed);
            this.Load += new System.EventHandler(this.frm_odeme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_tahsilat;
        private System.Windows.Forms.Button btn_borc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_tutar;
        private System.Windows.Forms.DateTimePicker dtp_tarih;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbltoplam;
        private System.Windows.Forms.Label lblkalan;
        private System.Windows.Forms.ComboBox cmb_ogrenciad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_telno;
        private System.Windows.Forms.ComboBox cmb_ogrencino;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
    }
}