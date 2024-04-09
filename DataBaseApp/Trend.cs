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
    public partial class Trend : Form
    {
        private string connectionString = @"Server = MAMOREZGAR\SQLEXPRESS; Database = musikinstrumenteverleih; Trusted_Connection = True;";

        public Trend()
        {
            InitializeComponent();
        }

        private void Trend_Load(object sender, EventArgs e)
        {
            //sql connection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQueryM = "SELECT Count (*) AS ANZAHL  FROM id_kunden Where Geschlecht = 'M' ";
                SqlCommand command = new SqlCommand(sqlQueryM, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        textBoxM.Text = reader["ANZAHL"].ToString();

                    }

                }
                string sqlQueryW = "SELECT Count (*) AS ANZAHL  FROM id_kunden Where Geschlecht = 'W' ";
                SqlCommand command1 = new SqlCommand(sqlQueryW, connection);

                using (SqlDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        textBoxW.Text = reader["ANZAHL"].ToString();

                    }

                }
                string sqlQueryMeist = @" SELECT TOP 1 Instrumenten, COUNT(a.was) AS AnzahlAusleihen
                                            FROM id_instrumenten AS i
                                        LEFT JOIN Ausleihen AS a ON a.was = i.id
                                            GROUP BY i.Instrumenten
                                        ORDER BY COUNT(a.was) DESC;                           
                                         ";
                SqlCommand command2 = new SqlCommand(sqlQueryMeist, connection);

                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        textBoxMeist.Text = reader["AnzahlAusleihen"].ToString()+ " " + reader["Instrumenten"].ToString();

                    }

                }
                string sqlQueryWenig = @" SELECT TOP 1 Instrumenten, COUNT(a.was) AS AnzahlAusleihen
                                            FROM id_instrumenten AS i
                                        LEFT JOIN Ausleihen AS a ON a.was = i.id
                                            GROUP BY i.Instrumenten
                                        ORDER BY COUNT(a.was);                           
                                         ";
                SqlCommand command3 = new SqlCommand(sqlQueryWenig, connection);

                using (SqlDataReader reader = command3.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        textBoxWenig.Text = reader["AnzahlAusleihen"].ToString() + " " + reader["Instrumenten"].ToString();

                    }

                }

                connection.Close();
        
      
            } 
        }
    }
}
