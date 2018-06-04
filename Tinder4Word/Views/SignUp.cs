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

namespace Tinder4Word.Views
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();

            this.MinimumSize = new Size(500,600);

            if (textBox1.Text != String.Empty && textBox2.Text != String.Empty && textBox3.Text != String.Empty && textBox4.Text != String.Empty &&
                textBox5.Text != String.Empty && textBox6.Text != String.Empty && textBox7.Text != String.Empty)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    label9.Visible = false;
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "[spCrearUsuarioSinTarjeta]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Matricula", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Nombre", textBox2.Text);
                    cmd.Parameters.AddWithValue("@ApellidoP", textBox3.Text);
                    cmd.Parameters.AddWithValue("@ApellidoM", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Correo", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Contraseña", textBox6.Text);
                    cmd.Parameters.AddWithValue("@Telefono", textBox7.Text);

                    cmd.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("New user created!");
                    this.Close();
                }
                catch (Exception)
                {
                    label9.Visible = true;
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text != String.Empty && textBox2.Text != String.Empty && textBox3.Text != String.Empty && textBox4.Text != String.Empty &&
                textBox5.Text != String.Empty && textBox6.Text != String.Empty && textBox7.Text != String.Empty)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
