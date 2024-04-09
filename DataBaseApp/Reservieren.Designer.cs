namespace DataBaseApp
{
    partial class Reservieren
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
            comboBoxKunde = new ComboBox();
            comboBoxInstrument = new ComboBox();
            dateTimePickerWann = new DateTimePicker();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // comboBoxKunde
            // 
            comboBoxKunde.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxKunde.FormattingEnabled = true;
            comboBoxKunde.Location = new Point(33, 37);
            comboBoxKunde.Name = "comboBoxKunde";
            comboBoxKunde.Size = new Size(216, 23);
            comboBoxKunde.TabIndex = 0;
            // 
            // comboBoxInstrument
            // 
            comboBoxInstrument.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxInstrument.FormattingEnabled = true;
            comboBoxInstrument.Location = new Point(33, 100);
            comboBoxInstrument.Name = "comboBoxInstrument";
            comboBoxInstrument.Size = new Size(216, 23);
            comboBoxInstrument.TabIndex = 1;
            // 
            // dateTimePickerWann
            // 
            dateTimePickerWann.Location = new Point(33, 171);
            dateTimePickerWann.Name = "dateTimePickerWann";
            dateTimePickerWann.Size = new Size(216, 23);
            dateTimePickerWann.TabIndex = 2;
            dateTimePickerWann.Value = new DateTime(2024, 3, 21, 0, 0, 0, 0);
            dateTimePickerWann.ValueChanged += dateTimePickerWann_ValueChanged;
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.Cancel;
            button1.Location = new Point(172, 243);
            button1.Name = "button1";
            button1.Size = new Size(83, 23);
            button1.TabIndex = 3;
            button1.Text = "Abbrechen";
            button1.UseVisualStyleBackColor = true;
            button1.Click += abrechen_Click;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.OK;
            button2.Location = new Point(91, 243);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "Speichern";
            button2.UseVisualStyleBackColor = true;
            button2.Click += speichern_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 17);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 5;
            label1.Text = "Kunde";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 82);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 6;
            label2.Text = "Instrument";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 153);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 7;
            label3.Text = "Wann";
            // 
            // Reservieren
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 276);
            ControlBox = false;
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dateTimePickerWann);
            Controls.Add(comboBoxInstrument);
            Controls.Add(comboBoxKunde);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Reservieren";
            Text = "Reservierungen";
            FormClosing += Reservieren_FormClosing;
            Load += reservieren_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxKunde;
        private ComboBox comboBoxInstrument;
        private DateTimePicker dateTimePickerWann;
        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}