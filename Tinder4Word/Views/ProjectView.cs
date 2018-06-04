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


namespace Tinder4Word.Views
{
    public partial class ProjectView : Form
    {
        Usuario master;
        Proyecto proMaster;
        List<Usuario> posibleUsuarios;
        int index = 0;
        public ProjectView(Usuario master, Proyecto proMaster)
        {
            InitializeComponent();
            this.MinimumSize = new Size(1140, 612);
            this.master = master;
            this.proMaster = proMaster;
            this.posibleUsuarios = mostrarPosiblesUsuarios();
            if (posibleUsuarios.Count == 0)
            {
                label1.Text = "";
                lblSkill1.Text = "- ";
                lblSkill2.Text = "- ";
                lblSkill3.Text = "- ";
                like.Enabled = false;
                dislike.Enabled = false;
                superlike.Enabled = false;
                lblnomatch.Visible = true;
                label5.Visible = false;
                label8.Visible = false;
                pictureBox1.Visible = false;
                lblSkill1.Visible = false;
                lblSkill2.Visible = false;
                lblSkill3.Visible = false;
            }
            else
            {
                imprimirUsu();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<Usuario> mostrarPosiblesUsuarios()
        {
            List<Usuario> UsuariosPosibles = new List<Usuario>();
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spMostrarPosiblesUsuarios";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Matricula", master.Matricula);
                    cmd.Parameters.AddWithValue("@ID", proMaster.Id);
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario usus = new Usuario(reader["Matricula"].ToString(), reader["Nombre"].ToString(), reader["ApellidoP"].ToString(), reader["ApellidoM"].ToString());
                        UsuariosPosibles.Add(usus);
                        insertaHabilidades(usus);
                    }

                    con.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return UsuariosPosibles;
        }

        void insertaHabilidades(Usuario usu)
        {
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spMostrarHabilidad";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Matricula", usu.Matricula);
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        usu.misHab.Add(new Habilidad(Convert.ToInt32(reader["ID"]), reader["Nombre"].ToString()));
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            
        }
        void imprimirUsu()
        {
            label1.Text = posibleUsuarios[index].Nombre + " " + posibleUsuarios[index].ApellidoP + " " + posibleUsuarios[index].ApellidoM;
            if (posibleUsuarios[index].misHab.Count == 3)
            {
                lblSkill1.Text = "- " + posibleUsuarios[index].misHab[0].nombre;
                lblSkill2.Text = "- " + posibleUsuarios[index].misHab[1].nombre;
                lblSkill3.Text = "- " + posibleUsuarios[index].misHab[2].nombre;
            }
            else if (posibleUsuarios[index].misHab.Count == 2)
            {
                lblSkill1.Text = "- " + posibleUsuarios[index].misHab[0].nombre;
                lblSkill2.Text = "- " + posibleUsuarios[index].misHab[1].nombre;
            }
            else if (posibleUsuarios[index].misHab.Count == 1)
            {
                lblSkill1.Text = "- " + posibleUsuarios[index].misHab[0].nombre;

            }
            else
            {
                lblSkill1.Text = "No Tiene habilidades :v";
                lblSkill2.Text = "- ";
                lblSkill3.Text = "- ";
            }
        }

        private void like_Click(object sender, EventArgs e)
        {
            projectoMatch();
            index++;
            if (index >= posibleUsuarios.Count) {
                posibleUsuarios = mostrarPosiblesUsuarios();
                index = 0;
            }if (posibleUsuarios.Count == 0)
            {
                label1.Text ="";
                lblSkill1.Text = "- ";
                lblSkill2.Text = "- ";
                lblSkill3.Text = "- ";
                like.Enabled = false;
                dislike.Enabled = false;
                superlike.Enabled = false;
                lblnomatch.Visible = true;
                label5.Visible = false;
                label8.Visible = false;
                pictureBox1.Visible = false;
                lblSkill1.Visible = false;
                lblSkill2.Visible = false;
                lblSkill3.Visible = false;
            }
            else {
                imprimirUsu();
            }
            
        }

        void projectoMatch() {
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spProyectoDaMatch";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDProyecto", proMaster.Id);
                    cmd.Parameters.AddWithValue("@Matricula", posibleUsuarios[index].Matricula);
                    cmd.ExecuteNonQuery();
                    
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dislike_Click(object sender, EventArgs e)
        {
            index++;
            if (index >= posibleUsuarios.Count)
            {
                posibleUsuarios = mostrarPosiblesUsuarios();
                index = 0;
            }
            imprimirUsu();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Match_Click(object sender, EventArgs e)
        {
            panelMatch.Visible = true;
            panel4.Visible = false;
            projectoMatchesFinal();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelMatch2.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label9.Visible = true;
            label6.Text = UsuariosMatches[listBox1.SelectedIndex].Nombre + " " + UsuariosMatches[listBox1.SelectedIndex].ApellidoP;
            label7.Text = UsuariosMatches[listBox1.SelectedIndex].Telefono;
            label9.Text = UsuariosMatches[listBox1.SelectedIndex].Correo;
        }
        List<Usuario> UsuariosMatches = new List<Usuario>(); 

        void projectoMatchesFinal() {
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spMatchesQueExisten";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDProyecto", proMaster.Id);
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Usuario matc = new Usuario(reader["Matricula"].ToString(), reader["Nombre"].ToString(),
                                reader["ApellidoP"].ToString(), reader["ApellidoM"].ToString(), reader["Correo"].ToString(),
                                reader["Contraseña"].ToString(), reader["Tarjeta"].ToString(), reader["Telefono"].ToString());
                        UsuariosMatches.Add(matc);
                        listBox1.Items.Add(matc.Nombre + " " + matc.ApellidoP);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        } 
    }
}


