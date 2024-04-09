using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseApp
{

    public partial class Reservieren : Form
    {
        private string connectionString = @"Server = MAMOREZGAR\SQLEXPRESS; Database = musikinstrumenteverleih; Trusted_Connection = True;";


        public Reservieren()
        {
            InitializeComponent();

            // Setzen Sie das aktuelle Datum
            dateTimePickerWann.Value = DateTime.Now;

            // Setzen Sie das MinDate auf das aktuelle Datum
            //dateTimePickerWann.MinDate = DateTime.Today;
        }

        private void abrechen_Click(object sender, EventArgs e)
        {
            Close();
        }

        // speichern 
        private void speichern_Click(object sender, EventArgs e)
        {

            Close();
        }
        private void dateTimePickerWann_ValueChanged(object sender, EventArgs e)
        {
            DateTime ausgewähltesDatum = dateTimePickerWann.Value;

            if (ausgewähltesDatum < DateTime.Today)
            {
                // Wenn das ausgewählte Datum in der Vergangenheit liegt, zeigen Sie eine Nachricht an
                MessageBox.Show("Das ausgewählte Datum darf nicht in der Vergangenheit liegen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Setzen Sie das Datum auf das aktuelle Datum
                dateTimePickerWann.Value = DateTime.Today;

            }
        }

        // SELECT ( zeigen )
        private void reservieren_Load(object sender, EventArgs e)
        {



            // sql connection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string sqlQuery = "SELECT Trim(Nachname) as Nachname, Trim(Vorname) as Vorname from id_kunden order by Nachname ";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBoxKunde.Items.Add(reader["Nachname"].ToString() + ", " + reader["Vorname"].ToString());

                        }
                    }
                    string sqlQuery1 = "SELECT Trim(Instrumenten) as Instrumenten from id_instrumenten order by Instrumenten ";
                    SqlCommand command1 = new SqlCommand(sqlQuery1, connection);

                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBoxInstrument.Items.Add(reader["Instrumenten"].ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler bei der Abfrage: {ex.Message}");
                }
                connection.Close();
            }

        }
        //
        private void Reservieren_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (DialogResult == DialogResult.OK)
            {

                if (comboBoxKunde.Text == "" || comboBoxKunde.Text == " ")
                {
                    MessageBox.Show("Muss ein Kunde ausgewählt sein", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }

                else if (comboBoxInstrument.Text == "" || comboBoxInstrument.Text == " ")
                {
                    MessageBox.Show("Muss ein Instrument ausgewählt sein", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }

                else if (dateTimePickerWann.Text == "" || dateTimePickerWann.Text == " ")
                {
                    MessageBox.Show("Muss ein Datum ausgewählt sein", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }

                // Abrufen des aktuellen Datums ohne Uhrzeit DateTime.Now.Date

                else if (dateTimePickerWann.Value < DateTime.Now.Date)
                {
                    MessageBox.Show("Darf es nicht in der Vergangenheit sein ", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }

                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        int wer = 0;
                        int was = 0;
                        int anzahl = 0;

                        connection.Open();
                        string selectQueryKunde = "SELECT ID from id_kunden where Vorname = @Vorname and Nachname = @Nachname ";

                        using (SqlCommand command = new SqlCommand(selectQueryKunde, connection))
                        {

                            //
                            command.Parameters.AddWithValue("@Vorname", comboBoxKunde.Text.Split(",")[1].Trim());
                            command.Parameters.AddWithValue("@Nachname", comboBoxKunde.Text.Split(",")[0].Trim());



                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    wer = (int)reader["ID"];
                                    // was = (int)reader["ID"];
                                }
                            }

                        }

                        string selectQueryInstrumenten = "SELECT ID from id_instrumenten where Instrumenten = @Instrumenten ";

                        using (SqlCommand command = new SqlCommand(selectQueryInstrumenten, connection))
                        {

                            command.Parameters.AddWithValue("@Instrumenten", comboBoxInstrument.Text.Trim());

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                    was = (int)reader["ID"];
                                }

                            }

                        }
                        // darf nicht sich wieder geben 
                        string sqlCheck = "SELECT Count(*) as Anzahl from Reservierungen where  was = @was and wann_reserviert = @wann_reserviert  ";
                        using (SqlCommand command = new SqlCommand(sqlCheck, connection))
                        {
                            // überpruft nur bei wer (Rezgar)  und kann nicht bei alle Kunden 
                            command.Parameters.AddWithValue("@was", was);
                            command.Parameters.AddWithValue("@wann_reserviert", dateTimePickerWann.Value.Date);
                            //
                            //command.Parameters.AddWithValue("@wann_reserviert_bis", dateTimePickerWann.Value.Date.AddDays(7));

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    anzahl = (int)reader["Anzahl"];
                                    // was = (int)reader["ID"];

                                }

                            }
                        }

                        string insertQuery = "INSERT INTO Reservierungen (wer,was,wann_reserviert) VALUES (@wer,@was, @wann_reserviert)";

                        if (anzahl == 0)
                        {
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Add parameters to the SQL command

                                command.Parameters.AddWithValue("@wer", wer);
                                command.Parameters.AddWithValue("@was", was);
                                //
                                command.Parameters.AddWithValue("@wann_reserviert", dateTimePickerWann.Value);

                                try
                                {
                                    // Execute the SQL command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine("Data inserted successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No rows were inserted.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error inserting data: {ex.Message}");
                                }
                                connection.Close();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Das Instrument ist bereits reserviert! ", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // bleibt 
                            e.Cancel = true;
                        }

                    }
                }

            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
