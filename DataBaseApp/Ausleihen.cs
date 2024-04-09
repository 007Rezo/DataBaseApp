using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseApp
{
    public partial class Ausleihen : Form
    {
        private string connectionString = @"Server = MAMOREZGAR\SQLEXPRESS; Database = musikinstrumenteverleih; Trusted_Connection = True;";


        private DataTable dataTable1;
        public Ausleihen()
        {
            InitializeComponent();
            InitializeDataTable();
        }
        private void InitializeDataTable()
        {
            // Erstelle eine DataTable und binden an die DataGridView
            dataTable1 = new DataTable();
            dataGridView1.DataSource = dataTable1;
        }
        public void save()
        {


            // Funktion aufrufen
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // INSERT SQL command
                    //Add data 
                    foreach (DataRow row in dataTable1.Rows)
                    {

                        //
                        if (row.RowState == DataRowState.Added)
                        {
                            string insertQuery = "INSERT INTO Ausleihen (wer,was,von_wann) VALUES (@wer,@was,@von_wann)";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Add parameters to the SQL command

                                command.Parameters.AddWithValue("@wer", row["wer"]);
                                command.Parameters.AddWithValue("@was", row["was"]);

                                command.Parameters.AddWithValue("@von_wann", row["von_wann"]);

                                //ich weiss noch nicht bis  wie lange man Ausleiht 
                                //command.Parameters.AddWithValue("@bis_wann", row["bis_wann"]);
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

                            }
                        }

                        else if (row.RowState == DataRowState.Modified)
                        {
                            // UPDATE SQL command wenn das zurückgibt 
                            string updateQuery = "UPDATE Ausleihen SET bis_wann = @bis_wann where wer = @wer AND was = @was AND von_wann = @von_wann";
                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                // Add parameters to the SQL command
                                command.Parameters.AddWithValue("@wer", row["wer"]);
                                command.Parameters.AddWithValue("@was", row["was"]);
                                command.Parameters.AddWithValue("@von_wann", row["von_wann"]);
                                command.Parameters.AddWithValue("@bis_wann", row["bis_wann"]);

                                // 
                                try
                                {
                                    // Execute the SQL command, gibt mir wie viel spalten hinzufügt worden 
                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine("Data updated successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No rows were updated.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error updating data: {ex.Message}");
                                }

                            }
                        }

                    }
                    dataTable1.AcceptChanges();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            //COunting spalten zeilen 
            //toolStripStatusLabel2.Text = "Spalten: " + dataTable.Columns.Count.ToString();
            //toolStripStatusLabel3.Text = "Zeilen: " + dataTable.Rows.Count.ToString();
        }



        public void Ausleihen_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    // vor dem Anfangen
                    dataTable1.Clear();
                    dataTable1.Rows.Clear();
                    dataTable1.Columns.Clear();
                    //FORMAT(von_wann, 'dd.MM.yyyy') AS von_wann + id nicht gleich sein 10 10 
                    string sqlQuery = @"SELECT Wer,Trim(k.Vorname)as Vorname, Trim(k.Nachname) as Nachname ,was,Trim(i.Instrumenten) as Instrument,FORMAT(von_wann, 'dd.MM.yyyy') AS von_wann,FORMAT(bis_wann, 'dd.MM.yyyy') AS bis_wann 
                                       FROM Ausleihen As a
                                      inner join id_instrumenten as i on i.ID = a.was
                                      inner join id_kunden as k on k.ID = a.wer 
                                      Where bis_wann is null 
                                            ";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Annahme: "Spalte1,2,3,4,5"ID,Vorname,Nachname,Geburtstag,Geschlecht sind Spalten in deiner Tabelle
                        dataTable1.Columns.Add("Wer", typeof(int));
                        dataTable1.Columns.Add("Vorname", typeof(string));
                        dataTable1.Columns.Add("Nachname", typeof(string));

                        dataTable1.Columns.Add("Was", typeof(string));
                        dataTable1.Columns.Add("Instrument", typeof(string));
                        dataTable1.Columns.Add("Von", typeof(string));
                        dataTable1.Columns.Add("Bis", typeof(string));


                        while (reader.Read())
                        {

                            // Hier fügst du eine neue Zeile zur DataTable hinzu
                            DataRow newRow = dataTable1.NewRow();

                            newRow["Wer"] = reader["Wer"];
                            newRow["Vorname"] = reader["Vorname"];
                            newRow["Nachname"] = reader["Nachname"];

                            newRow["Was"] = reader["was"];
                            newRow["Instrument"] = reader["Instrument"];

                            newRow["Von"] = reader["von_wann"];
                            newRow["Bis"] = reader["bis_wann"];

                            dataTable1.Rows.Add(newRow);

                        }
                        //reader.Close();
                        //command.Dispose();

                        dataTable1.AcceptChanges();

                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler bei der Abfrage: {ex.Message}");
                }

                connection.Close();
            }
        }
        // COUNTING 
        private void statusLabel12_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //toolStripStatusLabel1.Text = "Spalten: " + e.ColumnIndex.ToString();
            //toolStripStatusLabel2.Text = "Zeilen: " + e.RowIndex.ToString();
        }
        //
        private void Ausleihen_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataTable1.Rows.Count > 0)
            {
                // überprüfen ob Bis spalte Leer ist oder nicht nur einmal zurückgeben 

                if (dataTable1.Rows[e.RowIndex]["Bis"].ToString() == "")
                {

                    if (MessageBox.Show("Wollen Sie es wirklich zurückgeben? ", "Zurückgeben", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                            string updateSQLQuery = @"UPDATE Ausleihen SET bis_wann = SYSDATETIME() WHERE  
                                                    wer = @wer 
                                                    and was = @was 
                                                    and von_wann = @von_wann";

                            using (SqlCommand command = new SqlCommand(updateSQLQuery, connection))
                            {

                                // Add parameters to the SQL command
                                command.Parameters.AddWithValue("@wer", dataTable1.Rows[e.RowIndex]["Wer"]);
                                command.Parameters.AddWithValue("@was", dataTable1.Rows[e.RowIndex]["Was"]);
                                command.Parameters.AddWithValue("@von_wann", dataTable1.Rows[e.RowIndex]["Von"]);

                                connection.Open();
                                try
                                {
                                    // Execute the SQL command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine("Data updated successfully.");

                                        //Instrumenten_Load(sender, e);
                                        string updateQuery = "UPDATE id_instrumenten SET  Ausgeliehen = 0 WHERE id = @id";

                                        using (SqlCommand command0 = new SqlCommand(updateQuery, connection))
                                        {

                                            //Ausgeliehen = zurück ersetzen 
                                            command0.Parameters.AddWithValue("@id", dataTable1.Rows[e.RowIndex]["Was"]);
                                            try
                                            {
                                                // Execute the SQL command
                                                rowsAffected = command0.ExecuteNonQuery();

                                                if (rowsAffected > 0)
                                                {
                                                    Console.WriteLine("Data updated successfully.");
                                                    Ausleihen_Load(sender, e);
                                                    //Instrumenten_Load(sender, e);

                                                }
                                                else
                                                {
                                                    Console.WriteLine("No rows were updated.");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"Error updating data: {ex.Message}");
                                            }
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("No rows were updated.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error updating data: {ex.Message}");
                                }

                            }

                        }

                    }
                }

            }

        }
        //
        private void Ausleihen_Enter(object sender, EventArgs e)
        {
            Ausleihen_Load(sender, e);
        }
    }
}
