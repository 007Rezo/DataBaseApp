namespace DataBaseApp
{
    partial class Trend
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
            label1 = new Label();
            label2 = new Label();
            textBoxM = new TextBox();
            textBoxW = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            textBoxWenig = new TextBox();
            textBoxMeist = new TextBox();
            label3 = new Label();
            label4 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 29);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "mänlich";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 55);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 1;
            label2.Text = "weiblich";
            // 
            // textBoxM
            // 
            textBoxM.Location = new Point(104, 23);
            textBoxM.Name = "textBoxM";
            textBoxM.Size = new Size(119, 23);
            textBoxM.TabIndex = 2;
            // 
            // textBoxW
            // 
            textBoxW.Location = new Point(104, 55);
            textBoxW.Name = "textBoxW";
            textBoxW.Size = new Size(119, 23);
            textBoxW.TabIndex = 3;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBoxW);
            groupBox1.Controls.Add(textBoxM);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(38, 33);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(251, 104);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kunden";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxWenig);
            groupBox2.Controls.Add(textBoxMeist);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(37, 153);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(260, 121);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Instrumente";
            // 
            // textBoxWenig
            // 
            textBoxWenig.Location = new Point(149, 57);
            textBoxWenig.Name = "textBoxWenig";
            textBoxWenig.Size = new Size(100, 23);
            textBoxWenig.TabIndex = 3;
            // 
            // textBoxMeist
            // 
            textBoxMeist.Location = new Point(149, 30);
            textBoxMeist.Name = "textBoxMeist";
            textBoxMeist.Size = new Size(100, 23);
            textBoxMeist.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 60);
            label3.Name = "label3";
            label3.Size = new Size(129, 15);
            label3.TabIndex = 1;
            label3.Text = "Wenigsten ausgeliehen";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 33);
            label4.Name = "label4";
            label4.Size = new Size(120, 15);
            label4.TabIndex = 0;
            label4.Text = "Meistens ausgeliehen";
            // 
            // Trend
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(328, 302);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Trend";
            Text = "Trend";
            Load += Trend_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBoxM;
        private TextBox textBoxW;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox textBoxWenig;
        private TextBox textBoxMeist;
        private Label label3;
        private Label label4;
    }
}