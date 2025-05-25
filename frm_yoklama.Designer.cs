namespace etut_merkezi_uygulamasi
{
    partial class frm_yoklama
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_yoklama));
            this.label1 = new System.Windows.Forms.Label();
            this.btn_kaydet = new System.Windows.Forms.Button();
            this.txt_ogrenci_id = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_gec = new System.Windows.Forms.CheckBox();
            this.cb_gelmedi = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Öğrenci Numarası:";
            // 
            // btn_kaydet
            // 
            this.btn_kaydet.Location = new System.Drawing.Point(176, 334);
            this.btn_kaydet.Name = "btn_kaydet";
            this.btn_kaydet.Size = new System.Drawing.Size(110, 40);
            this.btn_kaydet.TabIndex = 3;
            this.btn_kaydet.Text = "Kaydet";
            this.btn_kaydet.UseVisualStyleBackColor = true;
            this.btn_kaydet.Click += new System.EventHandler(this.btn_kaydet_Click);
            // 
            // txt_ogrenci_id
            // 
            this.txt_ogrenci_id.Location = new System.Drawing.Point(225, 53);
            this.txt_ogrenci_id.Name = "txt_ogrenci_id";
            this.txt_ogrenci_id.Size = new System.Drawing.Size(200, 29);
            this.txt_ogrenci_id.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(225, 138);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 29);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tarih:";
            // 
            // cb_gec
            // 
            this.cb_gec.AutoSize = true;
            this.cb_gec.Location = new System.Drawing.Point(225, 203);
            this.cb_gec.Name = "cb_gec";
            this.cb_gec.Size = new System.Drawing.Size(65, 26);
            this.cb_gec.TabIndex = 7;
            this.cb_gec.Text = "Geç";
            this.cb_gec.UseVisualStyleBackColor = true;
            this.cb_gec.CheckedChanged += new System.EventHandler(this.cb_gec_CheckedChanged);
            // 
            // cb_gelmedi
            // 
            this.cb_gelmedi.AutoSize = true;
            this.cb_gelmedi.Location = new System.Drawing.Point(225, 248);
            this.cb_gelmedi.Name = "cb_gelmedi";
            this.cb_gelmedi.Size = new System.Drawing.Size(101, 26);
            this.cb_gelmedi.TabIndex = 8;
            this.cb_gelmedi.Text = "Gelmedi";
            this.cb_gelmedi.UseVisualStyleBackColor = true;
            this.cb_gelmedi.CheckedChanged += new System.EventHandler(this.cb_gelmedi_CheckedChanged);
            // 
            // frm_yoklama
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(452, 435);
            this.Controls.Add(this.cb_gelmedi);
            this.Controls.Add(this.cb_gec);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txt_ogrenci_id);
            this.Controls.Add(this.btn_kaydet);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frm_yoklama";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yoklama Ekranı";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm_yoklama_FormClosed);
            this.Load += new System.EventHandler(this.frm_yoklama_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_kaydet;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_gec;
        private System.Windows.Forms.CheckBox cb_gelmedi;
        public System.Windows.Forms.TextBox txt_ogrenci_id;
    }
}