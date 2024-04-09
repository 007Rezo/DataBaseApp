using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseApp
{
    public partial class Instrumenten : Form
    {
        //
        private string connectionString = @"Server = MAMOREZGAR\SQLEXPRESS; Database = musikinstrumenteverleih; Trusted_Connection = True;";


        private DataTable dataTable1;
        private int aktuelleID;
        public Instrumenten()
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
                            int id = 0;

                            string selectQuery = @"Select Max(id) +1 as ID  from id_instrumenten ";
                            using (SqlCommand command = new SqlCommand(selectQuery, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        id = (int)reader["ID"];
                                    }
                                }

                            }

                            string insertQuery = "INSERT INTO id_instrumenten (id,Instrumenten,Ausgeliehen) VALUES (@id,@Instrumenten, @Ausgeliehen)";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Add parameters to the SQL command

                                command.Parameters.AddWithValue("@id", id);
                                command.Parameters.AddWithValue("@Instrumenten", row["Instrumenten"]);
                                command.Parameters.AddWithValue("@Ausgeliehen", 0);

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
                            // UPDATE SQL command
                            string updateQuery = "UPDATE id_instrumenten SET Instrumenten = @Instrumenten, Ausgeliehen = @Ausgeliehen WHERE id = @id";
                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                // Add parameters to the SQL command
                                command.Parameters.AddWithValue("@id", row["ID"]);
                                command.Parameters.AddWithValue("@Instrumenten", row["Instrumenten"]);
                                command.Parameters.AddWithValue("@Ausgeliehen", row["Ausgeliehen"]);
                                try
                                {
                                    // Execute the SQL command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine("Data updated successfully.");
                                        //Instrumenten_Load();
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
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            string deletedQuery = "DELETE  from id_instrumenten WHERE id = @id ";
                            using (SqlCommand command = new SqlCommand(deletedQuery, connection))
                            {
                                //DELETE 
                                command.Parameters.AddWithValue("@id", aktuelleID);
                                try
                                {
                                    // Execute the SQL command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine("Data deleted successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No rows were deleted.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error deleting data: {ex.Message}");
                                }
                            }
                        }
                    }
                    //
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


        public void Instrumenten_Load(object sender, EventArgs e)
        {
            //sql connection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    // vor dem Anfangen
                    dataTable1.Clear();
                    dataTable1.Rows.Clear();
                    dataTable1.Columns.Clear();

                    //  "Spalte1,2,3,4,5"ID,Vorname,Nachname,Geburtstag,Geschlecht sind Spalten in Tabelle
                    //ID darf man nicht ändern
                    DataColumn column = dataTable1.Columns.Add("ID", typeof(int));
                    column.ReadOnly = true;
                    DataColumn column1 = dataTable1.Columns.Add("Instrumenten", typeof(string));
                    column1.ReadOnly = false;
                    // Set the "Ausleihen" column as readonly
                    DataColumn column2 = dataTable1.Columns.Add("Ausgeliehen", typeof(bool));
                    column2.ReadOnly = true;





                    string sqlQuery = "SELECT ID, Trim(Instrumenten) AS Instrumenten, Ausgeliehen FROM id_instrumenten";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {



                        while (reader.Read())
                        {

                            // Hier fügst du eine neue Zeile zur DataTable hinzu
                            DataRow newRow = dataTable1.NewRow();

                            newRow["ID"] = reader["ID"];
                            newRow["Instrumenten"] = reader["Instrumenten"];
                            newRow["Ausgeliehen"] = reader["Ausgeliehen"];

                            dataTable1.Rows.Add(newRow);

                        }
                        //reader.Close();
                        //command.Dispose();

                        dataTable1.AcceptChanges();
                        //dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler bei der Abfrage: {ex.Message}");
                }

                connection.Close();
            }
        }

        private void Instrumenten_Activated(object sender, EventArgs e)
        {
            Instrumenten_Load(sender, e);
        }

        private void instrumenten_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Überprüfen Sie, ob der Klick auf eine Zelle im Zeilenheader erfolgt ist
            // Löschen Sie die Zeile mit der angeklickten ID aus der DataTabl
            aktuelleID = (int)dataGridView1.Rows[e.RowIndex].Cells["ID"].Value;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // DataGridView datagrid = (DataGridView)sender;
            if (e.ColumnIndex == 2) { e.Cancel = true; }
        }

        private void Instrumenten_Enter(object sender, EventArgs e)
        {
            Instrumenten_Load(sender, e);
        }
    }
}
