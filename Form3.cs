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

namespace MoleShooterFinal
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            SqlConnection sqlConnection;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\Database.mdf;Integrated Security=True;Connect Timeout=30";

            sqlConnection = new SqlConnection(connectionString);

            //открываем соединение с бд
            try
            {
                await sqlConnection.OpenAsync();

                string query = "SELECT * FROM Raiting ORDER BY id";

                SqlCommand command = new SqlCommand(query, sqlConnection);

                SqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();

                while (reader.Read())
                {
                    data.Add(new string[4]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();

                }

                reader.Close();

                sqlConnection.Close();

                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);

               /* for (int i = 0; i < 11; i++)
                {
                    dataGridView1.Rows.Add();
                }*/
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
