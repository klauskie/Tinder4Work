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
    public partial class Main : Form
    {
        Usuario master;
        Proyecto proyectoMaster;
        int index = 0;
        List<Proyecto> pro;

        public Main(Usuario master)
        {
            InitializeComponent();
            //panelMatch1.Visible = true;

            this.MinimumSize = new Size(1140, 612);
            this.master = master;
            if (TxtBPName.Text != String.Empty)
            {
                CreateProj.Enabled = true;
            }
            else
            {
                CreateProj.Enabled = false;
            }
            pro = mostrarPosiblesProyectos();
            if (pro.Count != 0)
            {
                lblNameHome.Text = pro[index].Nombre;
                textBox1.Text = pro[index].Descripcion;
            }
            else
            {
                panelHome.Visible = true;

                pictureBox1.Visible = false;
                textBox1.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                lblSkill1.Visible = false;
                lblSkill2.Visible = false;
                lblSkill3.Visible = false;
                label5.Visible = false;
                lblnomatch.Visible = true;



                like.Enabled = false;
                dislike.Enabled = false;
                superlike.Enabled = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelRight.Visible = true;
            panelRight.Height = User.Height;
            panelRight.Top = User.Top;
            Title.Text = "Account";

            PName.Visible = false;
            TxtBPName.Visible = false;
            DescriptionP.Visible = false;
            TxtbDescrip.Visible = false;
            CreateProj.Visible = false;

            panelMatch1.Visible = false;

            PanelAcc.Visible = true;
            PanelCreate.Visible = false;
            panelHome.Visible = false;


            like.Visible = false;
            dislike.Visible = false;
            superlike.Visible = false;
            ProjList.Visible = false;

            lblID.Text = master.Matricula;
            lblName.Text = master.Nombre + " " + master.ApellidoP + " " + master.ApellidoM;
            lblMail.Text = master.Correo;
            lblPhone.Text = master.Telefono;

        }

        // Boton match
        private void Match_Click(object sender, EventArgs e)
        {
            panelHome.Visible = true;
            panelMatch1.Visible = true;

            panelRight.Visible = true;
            panelRight.Height = Match.Height;
            panelRight.Top = Match.Top;
            Title.Text = "Matches";

            PName.Visible = false;
            TxtBPName.Visible = false;
            DescriptionP.Visible = false;
            TxtbDescrip.Visible = false;
            CreateProj.Visible = false;



            PanelAcc.Visible = false;
            PanelCreate.Visible = false;

            like.Visible = false;
            dislike.Visible = false;
            superlike.Visible = false;
            ProjList.Visible = false;
             matchesProyectoFinal = mostrarMatchesUsuario();

        }

        // Boton My projects
        private void Project_Click(object sender, EventArgs e)
        {
            panelRight.Visible = true;
            panelRight.Height = Project.Height;
            panelRight.Top = Project.Top;

            Title.Text = "My Projects";

            ProjList.Visible = true;

            panelMatch1.Visible = true;

            PanelAcc.Visible = false;
            PanelCreate.Visible = false;

            like.Visible = false;
            dislike.Visible = false;
            superlike.Visible = false;

            panelHome.Visible = false;


            listBox1.Items.Clear();
            mostrarMisProyectos();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pro = mostrarPosiblesProyectos();
            like.Visible = true;
            dislike.Visible = true;
            superlike.Visible = true;
            panelMatch1.Visible = false;

            Title.Text = "Home";
            panelRight.Visible = false;
            PanelAcc.Visible = false;
            ProjList.Visible = false;
            if (pro.Any())
            {
                panelHome.Visible = true;
            }
            else {
                panelHome.Visible = true;

                pictureBox1.Visible = false;
                textBox1.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                lblSkill1.Visible = false;
                lblSkill2.Visible = false;
                lblSkill3.Visible = false;
                label5.Visible = false;
                lblnomatch.Visible = true;
            }


            PName.Visible = false;
            TxtBPName.Visible = false;
            DescriptionP.Visible = false;
            TxtbDescrip.Visible = false;
            CreateProj.Visible = false;
            PanelCreate.Visible = false;

        }

        private void CreateProj_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(TxtBPName.Text))
            {
                PName.Visible = false;
                TxtBPName.Visible = false;
                DescriptionP.Visible = false;
                TxtbDescrip.Visible = false;
                CreateProj.Visible = false;

                PanelCreate.Visible = true;
            }

            

            crearProyecto();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PName.Visible = true;
            TxtBPName.Visible = true;
            DescriptionP.Visible = true;
            TxtbDescrip.Visible = true;
            CreateProj.Visible = true;

            ProjList.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PName.Visible = false;
            TxtBPName.Visible = false;
            DescriptionP.Visible = false;
            TxtbDescrip.Visible = false;
            CreateProj.Visible = false;

            ProjList.Visible = true;


        }


        void mostrarMisProyectos()
        {
            master.MisPro.Clear();
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spMostrarMisProyectos";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Matricula", master.Matricula);
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    { 
                        master.MisPro.Add(new Proyecto(Convert.ToInt32(reader["ID"]), reader["Nombre"].ToString(), master.Matricula, Convert.ToBoolean(reader["Estado"])));
                        listBox1.Items.Add(reader["Nombre"].ToString());
                    }
                    reader.Close();

                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (listBox1.Items.Count == 0) {
                    listBox1.Items.Add("*Currently you dont have any projects*");
                }
            }
        }

        void crearProyecto() {
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spCrearProyecto";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", TxtBPName.Text);
                    cmd.Parameters.AddWithValue("@MatDueño", master.Matricula);
                    cmd.Parameters.AddWithValue("@Descripcion", TxtbDescrip.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("ProyectoCreado");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void TxtBPName_TextChanged(object sender, EventArgs e)
        {
            if (TxtBPName.Text != String.Empty)
            {
                CreateProj.Enabled = true;
            }
            else
            {
                CreateProj.Enabled = false;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        List<Proyecto> mostrarPosiblesProyectos() {
            List<Proyecto> proyectosPosibles = new List<Proyecto>();
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spMostrarTodosLosProyectos";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Matricula", master.Matricula);
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        Proyecto pro = new Proyecto(Convert.ToInt32(reader["ID"]), reader["Nombre"].ToString(), reader["MatriculaDueño"].ToString(), Convert.ToBoolean(reader["Estado"]));
                        pro.Descripcion = reader["Descripcion"].ToString();
                        proyectosPosibles.Add(pro);
                    }

                    con.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return proyectosPosibles;
        }

        private void like_Click(object sender, EventArgs e)
        {
            usuarioMatch();
            index++;
            if (index >= pro.Count) {
                pro = mostrarPosiblesProyectos();
                index = 0;
            }
            if (pro.Count == 0)
            {
                panelHome.Visible = true;
                pictureBox1.Visible = false;
                textBox1.Visible = false;
                TxtbDescrip.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                lblSkill1.Visible = false;
                lblSkill2.Visible = false;
                lblSkill3.Visible = false;
                label5.Visible = false;
                lblnomatch.Visible = true;
                like.Enabled = false;
                dislike.Enabled = false;
                superlike.Enabled = false;
                lblNameHome.Visible = false;
            }
            else {
                muestraProyecto();
            }
        }

        void usuarioMatch()
        {
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spUsuarioDaMatch";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Matricula", master.Matricula);
                    cmd.Parameters.AddWithValue("@IDProyecto", pro[index].Id);
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void muestraProyecto() {
            lblNameHome.Text = pro[index].Nombre;
            textBox1.Text = pro[index].Descripcion;
        }

        private void dislike_Click(object sender, EventArgs e)
        {
            index++;
            if (index >= pro.Count)
            {
                index = 0;
            }
            pro = mostrarPosiblesProyectos();
            muestraProyecto();
            
        }

        private void BttnUseProj_Click(object sender, EventArgs e)
        {
            proyectoMaster = master.MisPro[listBox1.SelectedIndex];
            ProjectView project = new ProjectView(master, proyectoMaster);
            this.Hide();
            project.ShowDialog();
            this.Show();
        }

        private void btnselectMtch_Click(object sender, EventArgs e)
        {
            panelMatch2.Visible = true;
            label12.Visible = true;
            label11.Visible = true;
            label9.Visible = true;
            label16.Visible = true; 
            label12.Text = matchesProyectoFinal[listboxMatchbox.SelectedIndex].Nombre;
            label11.Text = matchesProyectoFinal[listboxMatchbox.SelectedIndex].dueño.Nombre + " " + matchesProyectoFinal[listboxMatchbox.SelectedIndex].dueño.ApellidoP;
            label9.Text = matchesProyectoFinal[listboxMatchbox.SelectedIndex].dueño.Correo;
            label16.Text = matchesProyectoFinal[listboxMatchbox.SelectedIndex].dueño.Telefono;
        }

        List<Proyecto> matchesProyectoFinal = new List<Proyecto>();

        List<Proyecto> mostrarMatchesUsuario() {
            listboxMatchbox.Items.Clear();
            List<Proyecto> matchesProyecto = new List<Proyecto>();
            var connection = @"Server=(local)\SQLExpress;Database=TinderDBuena;Trusted_Connection=True;ConnectRetryCount=0";
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "spMatchesQueExistenParaUsuario";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Matricula", master.Matricula);
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Proyecto pro = new Proyecto(Convert.ToInt32(reader["ID"]), reader["Nombre proyecto"].ToString(), reader["MatriculaDueño"].ToString(), new Usuario(reader["Nombre"].ToString(), reader["ApellidoP"].ToString(), reader["Correo"].ToString(), reader["Telefono"].ToString(), reader["MatriculaDueño"].ToString()));
                        matchesProyecto.Add(pro);
                        listboxMatchbox.Items.Add(pro.Nombre);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return matchesProyecto;
        }
    }
}


/* */