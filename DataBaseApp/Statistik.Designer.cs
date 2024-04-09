namespace DataBaseApp
{
    partial class Statistik
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statistik));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBoxVorname = new TextBox();
            textBoxNachname = new TextBox();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            dataGridView3 = new DataGridView();
            tabControl1 = new TabControl();
            Reservierungen = new TabPage();
            Ausgeliehen = new TabPage();
            Zurückgegeben = new TabPage();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            tabControl1.SuspendLayout();
            Reservierungen.SuspendLayout();
            Ausgeliehen.SuspendLayout();
            Zurückgegeben.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(27, 9);
            label1.Name = "label1";
            label1.Size = new Size(54, 25);
            label1.TabIndex = 0;
            label1.Text = "Info.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 52);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 1;
            label2.Text = "Vorname";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 88);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 2;
            label3.Text = "Nachname";
            // 
            // textBoxVorname
            // 
            textBoxVorname.Enabled = false;
            textBoxVorname.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxVorname.Location = new Point(108, 44);
            textBoxVorname.Name = "textBoxVorname";
            textBoxVorname.ReadOnly = true;
            textBoxVorname.Size = new Size(148, 33);
            textBoxVorname.TabIndex = 3;
            // 
            // textBoxNachname
            // 
            textBoxNachname.Enabled = false;
            textBoxNachname.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxNachname.Location = new Point(108, 84);
            textBoxNachname.Name = "textBoxNachname";
            textBoxNachname.ReadOnly = true;
            textBoxNachname.Size = new Size(148, 33);
            textBoxNachname.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(425, 417);
            dataGridView1.TabIndex = 8;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(3, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(429, 421);
            dataGridView2.TabIndex = 9;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            // 
            // dataGridView3
            // 
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.AllowUserToDeleteRows = false;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Dock = DockStyle.Fill;
            dataGridView3.Location = new Point(3, 3);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.ReadOnly = true;
            dataGridView3.RowTemplate.Height = 25;
            dataGridView3.Size = new Size(429, 421);
            dataGridView3.TabIndex = 10;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Reservierungen);
            tabControl1.Controls.Add(Ausgeliehen);
            tabControl1.Controls.Add(Zurückgegeben);
            tabControl1.Location = new Point(35, 124);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(443, 455);
            tabControl1.TabIndex = 11;
            // 
            // Reservierungen
            // 
            Reservierungen.BorderStyle = BorderStyle.Fixed3D;
            Reservierungen.Controls.Add(dataGridView1);
            Reservierungen.Location = new Point(4, 24);
            Reservierungen.Name = "Reservierungen";
            Reservierungen.Padding = new Padding(3);
            Reservierungen.Size = new Size(435, 427);
            Reservierungen.TabIndex = 0;
            Reservierungen.Text = "Reservierungen";
            Reservierungen.UseVisualStyleBackColor = true;
            // 
            // Ausgeliehen
            // 
            Ausgeliehen.Controls.Add(dataGridView2);
            Ausgeliehen.Location = new Point(4, 24);
            Ausgeliehen.Name = "Ausgeliehen";
            Ausgeliehen.Padding = new Padding(3);
            Ausgeliehen.Size = new Size(435, 427);
            Ausgeliehen.TabIndex = 1;
            Ausgeliehen.Text = "Ausgeliehen";
            Ausgeliehen.UseVisualStyleBackColor = true;
            // 
            // Zurückgegeben
            // 
            Zurückgegeben.Controls.Add(dataGridView3);
            Zurückgegeben.Location = new Point(4, 24);
            Zurückgegeben.Name = "Zurückgegeben";
            Zurückgegeben.Padding = new Padding(3);
            Zurückgegeben.Size = new Size(435, 427);
            Zurückgegeben.TabIndex = 2;
            Zurückgegeben.Text = "Zurückgegeben";
            Zurückgegeben.UseVisualStyleBackColor = true;
            // 
            // Statistik
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 602);
            Controls.Add(tabControl1);
            Controls.Add(textBoxNachname);
            Controls.Add(textBoxVorname);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Statistik";
            Text = "Statistik";
            Load += Statistik_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            tabControl1.ResumeLayout(false);
            Reservierungen.ResumeLayout(false);
            Ausgeliehen.ResumeLayout(false);
            Zurückgegeben.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBoxVorname;
        private TextBox textBoxNachname;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridView dataGridView3;
        private TabControl tabControl1;
        private TabPage Reservierungen;
        private TabPage Ausgeliehen;
        private TabPage Zurückgegeben;
    }
}