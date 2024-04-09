using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataBaseApp
{
    public partial class MDIParent1 : Form
    {

        private Kunden kunden;
        private Instrumenten instrumenten;
        private Reservierungen reservierungen;
        private Ausleihen ausleihen;

        private int childFormNumber = 0;






        public MDIParent1()
        {
            InitializeComponent();
            
        }


        private void ShowNewForm(object sender, EventArgs e)
        {

            Form? activeChild = this.ActiveMdiChild;
            if (activeChild != null)
            {
                if (activeChild.Name == "Ausleihen")
                {
                    using (Leihen dlgLeihen = new Leihen())
                    {
                        dlgLeihen.StartPosition = FormStartPosition.CenterParent;
                        dlgLeihen.ShowDialog();
                        if (dlgLeihen.DialogResult == DialogResult.OK)
                        {

                            ausleihen.Ausleihen_Load(sender, e);

                        }
                    }
                }
                else if (activeChild.Name == "Reservierungen")
                {

                    using (Reservieren dlgReservieren = new Reservieren())
                    {
                        dlgReservieren.StartPosition = FormStartPosition.CenterParent;
                        dlgReservieren.ShowDialog();
                        if (dlgReservieren.DialogResult == DialogResult.OK)
                        {

                            reservierungen.Reservierungen_Load(sender, e);


                        }
                    }
                }


            }


        }

        private void OpenFile(object sender, EventArgs e)
        {

        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }


        #region Tabellen einmalig offnen
        private void kundenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kunden == null || kunden.IsDisposed)
            {
                // If the form is not open or has been disposed, create a new instance
                kunden = new Kunden();

                kunden.MdiParent = this;
                kunden.Show();

            }
            else
            {
                // If the form is already open, bring it to the front
                kunden.BringToFront();
                kunden.Kunden_Load(sender, e);
            }
            // Count 
            //toolStripStatusLabel1.Text = "Spalten: " + dataTable.Columns.Count.ToString();
            //toolStripStatusLabel2.Text = "Zeilen: " + dataTable.Rows.Count.ToString();

        }
        private void instrumentenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (instrumenten == null || instrumenten.IsDisposed)
            {
                // If the form is not open or has been disposed, create a new instance
                instrumenten = new Instrumenten();
                instrumenten.MdiParent = this;
                instrumenten.Show();

            }
            else
            {
                // If the form is already open, bring it to the front
                instrumenten.BringToFront();
                instrumenten.Instrumenten_Load(sender, e);
            }

        }

        private void ausleihenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ausleihen == null || ausleihen.IsDisposed)
            {
                // If the form is not open or has been disposed, create a new instance
                ausleihen = new Ausleihen();
                ausleihen.MdiParent = this;
                ausleihen.Show();
            }
            else
            {
                // If the form is already open, bring it to the front
                ausleihen.BringToFront();
                ausleihen.Ausleihen_Load(sender, e);
            }

        }

        private void reservierungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (reservierungen == null || reservierungen.IsDisposed)
            {
                // If the form is not open or has been disposed, create a new instance
                reservierungen = new Reservierungen();
                reservierungen.MdiParent = this;
                reservierungen.Show();

            }
            else
            {
                // If the form is already open, bring it to the front
                reservierungen.BringToFront();
                reservierungen.Reservierungen_Load(sender, e);
            }



        }

        #endregion

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
        //Koordinat of table
        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
            //toolStripStatusLabel3.Text = "Spalten: " + e.ColumnIndex.ToString();
            //toolStripStatusLabel4.Text = "Zeilen: " + e.RowIndex.ToString();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form? activeChild = this.ActiveMdiChild;
            if (activeChild != null)
            {
                Type tpe = activeChild.GetType();
                if (activeChild.Name == "Kunden")
                {
                    Kunden kunden = (Kunden)activeChild;
                    kunden.save();
                    kunden.Kunden_Load(sender, e);
                }
                else if (activeChild.Name == "Instrumenten")
                {
                    Instrumenten instrumenten = (Instrumenten)activeChild;
                    instrumenten.save();
                    instrumenten.Instrumenten_Load(sender, e);
                }
                else if (activeChild.Name == "Ausleihen")
                {
                    Ausleihen ausleihen = (Ausleihen)activeChild;
                    ausleihen.save();
                    ausleihen.Ausleihen_Load(sender, e);
                }
                else if (activeChild.Name == "Reservierungen")
                {
                    Reservierungen reservierungen = (Reservierungen)activeChild;
                    reservierungen.save();
                    reservierungen.Reservierungen_Load(sender, e);
                }
            }

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ausleihenToolStripMenuItem_Click(sender, e);
            instrumentenToolStripMenuItem_Click(sender, e);
            reservierungenToolStripMenuItem_Click(sender, e);
            kundenToolStripMenuItem_Click(sender, e);
            LayoutMdi(MdiLayout.TileHorizontal);




        }


        private void infoToolStrip_Click(object sender, EventArgs e)
        {

            Form? activeChild = this.ActiveMdiChild;
            if (activeChild != null)
            {
                if (activeChild.Name == "Kunden")
                {
                    int id = kunden.getAktuelleID();

                    if (id > 0)
                    {
                        using (Statistik dlgStatistik = new Statistik(id))
                        {

                            dlgStatistik.StartPosition = FormStartPosition.CenterParent;
                            dlgStatistik.ShowDialog();

                        }

                    }
                    else
                    {
                        MessageBox.Show("Kein Kunde Ausgewählt!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void toolStripButtonTrend_Click(object sender, EventArgs e)
        {

            using (Trend dlgTrend = new Trend())
            {
                dlgTrend.StartPosition = FormStartPosition.CenterParent;
                dlgTrend.ShowDialog();

            }
        }
    }
}
