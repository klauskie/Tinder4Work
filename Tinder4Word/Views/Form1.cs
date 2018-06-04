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
using Tinder4Word.Classes1;
using Tinder4Word.Views;

namespace Tinder4Word
{
    public partial class Form1 : Form
    {
        Usuario master;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checarSiExisteUsuario();
        }

        private void checarSiExisteUsuario()
        {
            int bandera = 0;
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spChecarSiExiste";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Matricula", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Contra", textBox2.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        if (reader["Contador"].ToString() == "1")
                        {
                            bandera = 1;
                        }
                        else
                        {
                            label2.Visible = true;
                        }


                    }
                    reader.Close();

                    if (bandera == 1)
                    {
                        cmd.CommandText = "spMiInformacion";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Matricula", textBox1.Text);
                        cmd.ExecuteNonQuery();
                        SqlDataReader reader2 = cmd.ExecuteReader();
                        while (reader2.Read())
                        {
                            Usuario principal = new Usuario(reader2["Matricula"].ToString(), reader2["Nombre"].ToString(),
                                reader2["ApellidoP"].ToString(), reader2["ApellidoM"].ToString(), reader2["Correo"].ToString(),
                                reader2["Contraseña"].ToString(), reader2["Tarjeta"].ToString(), reader2["Telefono"].ToString());
                                master = principal;
                        }
                        reader2.Close();
                    }

                    con.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (bandera == 1)
                {
                    label2.Visible = false;
                    Main main = new Main(master);
                    this.Hide();
                    main.ShowDialog();
                    this.Show();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp sign = new SignUp();
            this.Hide();
            sign.ShowDialog();
            this.Show();
        }
    }
}
