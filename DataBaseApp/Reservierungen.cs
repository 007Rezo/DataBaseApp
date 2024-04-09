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
    public partial class Reservierungen : Form
    {
        private string connectionString = @"Server = MAMOREZGAR\SQLEXPRESS; Database = musikinstrumenteverleih; Trusted_Connection = True;";

        private DataTable dataTable1;
        private int wer;
        private int was;
        private DateTime wann;

        public Reservierungen()
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
                            string insertQuery = "INSERT INTO Reservierungen (wer,was,wann_reserviert) VALUES (@wer,@was,@wann_reserviert)";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                // Add parameters to the SQL command
                                command.Parameters.AddWithValue("@wer", row["wer"]);
                                command.Parameters.AddWithValue("@was", row["was"]);
                                //command.Parameters.AddWithValue("@wann_reserviert", row["wann_reserviert"]);

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
                                        //throw new Exception("Beim Insert kam es zu enem unerwarteten Ergebnis.");
                                        Console.WriteLine("No rows were inserted.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error inserting : {ex.Message}");
                                }

                            }
                        }

                        else if (row.RowState == DataRowState.Modified)
                        {
                            // UPDATE SQL command wer kann man nicht ändern
                            string updateQuery = "UPDATE Reservierungen SET was = @was  , wann_reserviert = @wann_reserviert WHERE wer = @wer";
                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                // Add parameters to the SQL command
                                command.Parameters.AddWithValue("@wer", row["wer"]);
                                command.Parameters.AddWithValue("@was", row["was"]);
                                command.Parameters.AddWithValue("@wann_reserviert", row["wann_reserviert"]);
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
                                        //throw new Exception("Beim Update kam es zu enem unerwarteten Ergebnis.");
                                        Console.WriteLine("No rows were updated.");
                                    }

                                }
                                catch
                                (Exception ex)
                                {
                                    Console.WriteLine($"Error updating data: {ex.Message}");
                                }
                            }
                        }
                        else if (row.RowState == DataRowState.Deleted)
                        {
                            string deletedQuery = "DELETE  from reservierungen WHERE wer = @wer and was = @was and wann_reserviert = @wann ";
                            using (SqlCommand command = new SqlCommand(deletedQuery, connection))
                            {
                                //DELETE 
                                command.Parameters.AddWithValue("@wer", wer);
                                command.Parameters.AddWithValue("@was", was);
                                command.Parameters.AddWithValue("@wann", wann);


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
                Console.WriteLine($"Error : {ex.Message}");
            }
        }

        public void Reservierungen_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    // vor dem Anfangen
                    dataTable1.Clear();
                    dataTable1.Rows.Clear();
                    dataTable1.Columns.Clear();

                    string sqlQuery = @"SELECT wer,trim (k.Vorname) as Vorname , trim (k.Nachname) as Nachname ,was,trim(i.Instrumenten) as Instrument ,FORMAT(wann_reserviert, 'dd.MM.yyyy') AS wann_reserviert   
                            FROM  reservierungen as r 
                            inner join id_instrumenten as i on i.ID = r.was 
                            inner join id_kunden as k on k.ID = r.wer";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Annahme: "Spalte1,2,3,4,5"ID,Vorname,Nachname,Geburtstag,Geschlecht sind Spalten in deiner Tabelle
                        dataTable1.Columns.Add("Wer", typeof(int));
                        dataTable1.Columns.Add("Vorname", typeof(string));
                        dataTable1.Columns.Add("Nachname", typeof(string));

                        dataTable1.Columns.Add("Was", typeof(int));
                        dataTable1.Columns.Add("Instrument", typeof(string));

                        dataTable1.Columns.Add("Wann", typeof(string));


                        while (reader.Read())
                        {

                            // Hier fügst du eine neue Zeile zur DataTable hinzu
                            DataRow newRow = dataTable1.NewRow();

                            newRow["Wer"] = reader["wer"];
                            newRow["Vorname"] = reader["Vorname"];
                            newRow["Nachname"] = reader["Nachname"];

                            newRow["Was"] = reader["was"];
                            newRow["Instrument"] = reader["Instrument"];
                            newRow["Wann"] = reader["wann_reserviert"];

                            dataTable1.Rows.Add(newRow);

                        }
                        //reader.Close();
                        //command.Dispose();


                    }
                    // without adding it just show it 3+1 
                    dataTable1.AcceptChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler bei der Abfrage: {ex.Message}");
                }

                connection.Close();
            }

        }

        private void reservierungen_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Überprüfen Sie, ob der Klick auf eine Zelle im Zeilenheader erfolgt ist
            // Löschen Sie die Zeile mit der angeklickten ID aus der DataTabl
            wer = (int)dataGridView1.Rows[e.RowIndex].Cells["Wer"].Value;
            was = (int)dataGridView1.Rows[e.RowIndex].Cells["Was"].Value;
            wann = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Wann"].Value);

        }

        private void reservierungen_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataTable1.Rows.Count > 0)
            {
                // überprüfen ob Bis spalte Leer ist oder nicht nur einmal zurückgeben 

                if (dataTable1.Rows[e.RowIndex]["Vorname"].ToString() != "")
                {

                    if (MessageBox.Show("Wollen Sie es wirklich Ausleihen? ", "Ausleihen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string insertSQLQuery = @"INSERT INTO Ausleihen (wer,was,von_wann) VALUES (@wer,@was,@von_wann)";
                            using (SqlCommand command = new SqlCommand(insertSQLQuery, connection))
                            {
                                command.Parameters.AddWithValue("@wer", dataTable1.Rows[e.RowIndex]["Wer"]);
                                command.Parameters.AddWithValue("@was", dataTable1.Rows[e.RowIndex]["Was"]);
                                command.Parameters.AddWithValue("@von_wann", dataTable1.Rows[e.RowIndex]["Wann"]);



                                connection.Open();
                                try
                                {
                                    // Execute the SQL command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine("Data inserted successfully.");

                                        //Instrumenten_Load(sender, e);
                                        string updateQuery = "UPDATE id_instrumenten SET  Ausgeliehen = 1 WHERE id = @id";

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

                                                    string deletedQuery = "DELETE  from Reservierungen WHERE wer = @wer and was = @was ";

                                                    using (SqlCommand command1 = new SqlCommand(deletedQuery, connection))
                                                    {
                                                        //DELETE 
                                                        command1.Parameters.AddWithValue("@wer", dataTable1.Rows[e.RowIndex]["Wer"]);
                                                        command1.Parameters.AddWithValue("@was", dataTable1.Rows[e.RowIndex]["Was"]);

                                                        try
                                                        {
                                                            // Execute the SQL command
                                                            rowsAffected = command1.ExecuteNonQuery();

                                                            if (rowsAffected > 0)
                                                            {
                                                                Console.WriteLine("Data deleted successfully.");
                                                                MessageBox.Show("Instrument wurde Ausgeliehen. ", "Ausleihen", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                                Reservierungen_Load(sender, e);

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

        private void Reservierungen_Enter(object sender, EventArgs e)
        {
            Reservierungen_Load(sender, e);
        }
    }
}