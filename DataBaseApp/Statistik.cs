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
    public partial class Statistik : Form
    {
        private string connectionString = @"Server = MAMOREZGAR\SQLEXPRESS; Database = musikinstrumenteverleih; Trusted_Connection = True;";

        private DataTable dataTable1;
        private DataTable dataTable2;
        private DataTable dataTable3;

        private int id;

        public Statistik(int aktuelleID)
        {
            InitializeComponent();
            // gesetzt parmeter (aktuelleID) 

            id = aktuelleID;
            dataTable1 = new DataTable();
            dataTable2 = new DataTable();
            dataTable3 = new DataTable();
            dataGridView1.DataSource = dataTable1;
            dataGridView2.DataSource = dataTable2;
            dataGridView3.DataSource = dataTable3;

        }

        private void Statistik_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string sqlQuery = "SELECT  Trim(Vorname) As Vorname, Trim(Nachname) As Nachname FROM id_kunden where id = @id ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    // Add parameters to the SQL command
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            textBoxVorname.Text = reader["Vorname"].ToString();
                            textBoxNachname.Text = reader["Nachname"].ToString();
                            //textBoxNachname.Text = reader["Geschlecht"].ToString();
                        }
                    }
                }
                // vor dem Anfangen
                dataTable1.Clear();
                dataTable1.Rows.Clear();
                dataTable1.Columns.Clear();
                string reserierungsqlQuery = @"SELECT  trim(i.Instrumenten) as Instrument, FORMAT(wann_reserviert , 'dd.MM.yyyy') AS wann_reserviert 
                                                FROM reservierungen as r  
                                                inner join id_instrumenten as i on i.ID = r.was 
                                                inner join id_kunden as k on k.ID = r.wer
                                                where r.wer = @id ";

                using (SqlCommand command1 = new SqlCommand(reserierungsqlQuery, connection))
                {

                    // Add parameters to the SQL command
                    command1.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        dataTable1.Columns.Add("Instrument", typeof(string));
                        dataTable1.Columns.Add("Wann", typeof(string));

                        while (reader.Read())
                        {
                            // Hier fügst du eine neue Zeile zur DataTable hinzu
                            DataRow newRow = dataTable1.NewRow();

                            newRow["Instrument"] = reader["Instrument"];
                            newRow["Wann"] = reader["wann_reserviert"];

                            dataTable1.Rows.Add(newRow);


                        }
                        // without adding it just show it 3+1 
                        dataTable1.AcceptChanges();
                    }
                }
                // vor dem Anfangen
                dataTable2.Clear();
                dataTable2.Rows.Clear();
                dataTable2.Columns.Clear();

                string ausleihensqlQuery = @"SELECT  trim(i.Instrumenten) as Instrument,FORMAT(von_wann, 'dd.MM.yyyy') AS von_wann 
                                                FROM ausleihen as a  
                                                inner join id_instrumenten as i on i.ID = a.was 
                                                inner join id_kunden as k on k.ID = a.wer
                                                where a.wer = @id and a.bis_wann IS NULL ";

                using (SqlCommand command1 = new SqlCommand(ausleihensqlQuery, connection))
                {

                    // Add parameters to the SQL command
                    command1.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        dataTable2.Columns.Add("Instrument", typeof(string));
                        dataTable2.Columns.Add("Von Wann", typeof(string));

                        while (reader.Read())
                        {
                            // Hier fügst du eine neue Zeile zur DataTable hinzu
                            DataRow newRow = dataTable2.NewRow();

                            newRow["Instrument"] = reader["Instrument"];
                            newRow["Von Wann"] = reader["von_wann"];

                            dataTable2.Rows.Add(newRow);
                        }
                        // without adding it just show it 3+1 
                        dataTable2.AcceptChanges();
                    }
                }

                // vor dem Anfangen
                dataTable3.Clear();
                dataTable3.Rows.Clear();
                dataTable3.Columns.Clear();
                string ausgeliehensqlQuery = @"SELECT  trim(i.Instrumenten) as Instrument, FORMAT(von_wann, 'dd.MM.yyyy') AS von_wann,FORMAT(bis_wann, 'dd.MM.yyyy') AS bis_wann, DATEDIFF(day, Von_Wann, bis_wann)  as Tage
                                                FROM ausleihen as a  
                                                inner join id_instrumenten as i on i.ID = a.was 
                                                inner join id_kunden as k on k.ID = a.wer
                                                
                                                where a.wer = @id and  a.bis_wann IS NOT NULL";

                using (SqlCommand command1 = new SqlCommand(ausgeliehensqlQuery, connection))
                {

                    // Add parameters to the SQL command
                    command1.Parameters.AddWithValue("@id", id);


                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        dataTable3.Columns.Add("Instrument", typeof(string));
                        dataTable3.Columns.Add("Von Wann", typeof(string));
                        dataTable3.Columns.Add("Bis Wann", typeof(string));
                        dataTable3.Columns.Add("Tage", typeof(string));

                        while (reader.Read())
                        {
                            // Hier fügst du eine neue Zeile zur DataTable hinzu
                            DataRow newRow = dataTable3.NewRow();

                            newRow["Instrument"] = reader["Instrument"];
                            newRow["Von Wann"] = reader["von_wann"];
                            newRow["Bis Wann"] = reader["bis_wann"];
                            newRow["Tage"] = reader["Tage"];

                            dataTable3.Rows.Add(newRow);
                        }
                        // without adding it just show it 3+1 
                        dataTable3.AcceptChanges();
                    }
                }
                connection.Close();


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
