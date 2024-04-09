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
using static DataBaseApp.MDIParent1;
using static System.Windows.Forms.LinkLabel;

namespace DataBaseApp
{
    public partial class Kunden : Form
    {
        private string connectionString = @"Server = MAMOREZGAR\SQLEXPRESS; Database = musikinstrumenteverleih; Trusted_Connection = True;";
        public int aktuelleID = 0;


        private DataTable dataTable1;
        public Kunden()
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

        
        public void Kunden_Load(object sender, EventArgs e)
        {

            //sql connection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    // vor dem Anfangen clear
                    dataTable1.Clear();
                    dataTable1.Rows.Clear();
                    dataTable1.Columns.Clear();

                    string sqlQuery = "SELECT ID ,Trim(Vorname) As Vorname, Trim(Nachname) As Nachname,FORMAT(Geburtstag, 'dd.MM.yyyy') AS Geburtsdatum,Geschlecht FROM id_kunden";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //  "Spalte"ID,Vorname,Nachname,Geburtstag,Geschlecht sind Spalten in deiner Tabelle
                        DataColumn column = dataTable1.Columns.Add("ID", typeof(int));
                        column.ReadOnly = true;
                        dataTable1.Columns.Add("Vorname", typeof(string));
                        dataTable1.Columns.Add("Nachname", typeof(string));
                        dataTable1.Columns.Add("Geburtsdatum", typeof(string));
                        dataTable1.Columns.Add("Geschlecht", typeof(string));

                        while (reader.Read())
                        {

                            // Hier fügst du eine neue Zeile zur DataTable hinzu
                            DataRow newRow = dataTable1.NewRow();

                            newRow["ID"] = reader["ID"];
                            newRow["Vorname"] = reader["Vorname"];
                            newRow["Nachname"] = reader["Nachname"];
                            newRow["Geburtsdatum"] = reader["Geburtsdatum"];
                            newRow["Geschlecht"] = reader["Geschlecht"];
                            dataTable1.Rows.Add(newRow);

                        }
                        //reader.Close();
                        //command.Dispose();

                        //Martin
                        //
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

        /*
        static void RetrieveData(SqlConnection connection, int id)
        {
            string retrieveQuery = "SELECT * FROM Kunden WHERE id = @id";

            using (SqlCommand command = new SqlCommand(retrieveQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}");
                        Console.WriteLine($"Vorname: {reader["vorname"]}");
                        Console.WriteLine($"Nachname: {reader["nachname"]}");
                        Console.WriteLine($"Geburtstag: {reader["geburtstag"]}");
                        Console.WriteLine($"Geschlecht: {reader["geschlecht"]}");
                    }
                    else
                    {
                        Console.WriteLine("Keine Daten gefunden.");
                    }
                }
            }
        }
        */

        public void save()
        {
            //MessageBox.Show("Hallo Rezgar bin im Speicher vom KUNDEN");


            //tabele abhole
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
                        if (row.RowState == DataRowState.Added)
                        {
                            int id = 0;
                            string selectQuery = @"Select Max(id) +1 as ID  from id_kunden  ";
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

                            //
                            string insertQuery = @"INSERT INTO id_kunden (ID,Vorname,Nachname,Geburtstag,Geschlecht) VALUES (@id,@Vorname, @Nachname, @Geburtsdatum, upper(@Geschlecht))";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {

                                // Add parameters to the SQL command
                                command.Parameters.AddWithValue("@id", id);
                                command.Parameters.AddWithValue("@Vorname", row["Vorname"]);
                                command.Parameters.AddWithValue("@Nachname", row["Nachname"]);
                                command.Parameters.AddWithValue("@Geburtsdatum", row["Geburtsdatum"]);
                                command.Parameters.AddWithValue("@Geschlecht", row["Geschlecht"]);
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
                            string updateQuery = "UPDATE id_kunden SET Vorname = @Vorname, Nachname = @Nachname, Geburtstag = @Geburtsdatum, Geschlecht = @Geschlecht WHERE id = @id";
                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                // Add parameters to the SQL command
                                command.Parameters.AddWithValue("@id", row["ID"]);
                                command.Parameters.AddWithValue("@Vorname", row["Vorname"]);
                                command.Parameters.AddWithValue("@Nachname", row["Nachname"]);
                                command.Parameters.AddWithValue("@Geburtsdatum", row["Geburtsdatum"]);
                                command.Parameters.AddWithValue("@Geschlecht", row["Geschlecht"]);
                                try
                                {
                                    // Execute the SQL command
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
                        //
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            string deletedQuery = "DELETE  from id_kunden WHERE id = @id ";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Überprüfen Sie, ob der Klick auf eine Zelle im Zeilenheader erfolgt ist
            // Löschen Sie die Zeile mit der angeklickten ID aus der DataTabl
            aktuelleID = (int)dataGridView1.Rows[e.RowIndex].Cells["ID"].Value;
        }

        private void Kunden_Enter(object sender, EventArgs e)
        {
            Kunden_Load(sender, e);
        }

        public int getAktuelleID()
        {
            int id = 0;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Abrufen des ausgewählten Index der ersten ausgewählten Zeile sonst bleibt 0 
                id = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            }
            
            
            
            return id;
            
         
        }
    }

    //Console.WriteLine($"Customer ID: {customerId}, Name: {firstName} {lastName}, Birthdate: {birthdate}, Gender: {gender}");

}
