using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinder4Word.Classes1
{
    public class Usuario
    {
        string matricula;
        string nombre;
        string apellidoP;
        string apellidoM;
        string correo;
        string contraseña;
        string telefono;
        bool estado;
        string tarjeta;
        List<Proyecto> misPro;
        public List<Habilidad> misHab;

        public Usuario(string matricula, string nombre, string apellidoP, string apellidoM, string correo, string contraseña, string tarjeta, string telefono)
        {
            this.matricula = matricula;
            this.nombre = nombre;
            this.apellidoP = apellidoP;
            this.apellidoM = apellidoM;
            this.correo = correo;
            this.contraseña = contraseña;
            this.tarjeta = tarjeta;
            this.telefono = telefono;
            this.MisPro = new List<Proyecto>();
        }

        public Usuario(string matricula, string nombre, string apellidoP, string apellidoM)
        {

            this.matricula = matricula;
            this.nombre = nombre;
            this.apellidoP = apellidoP;
            this.apellidoM = apellidoM;
            this.misHab = new List<Habilidad>();
            this.estado = true;
        }

        public Usuario(string nombre, string ApellidoP, string correo, string telefono, string mat)
        {
            this.nombre = nombre;
            this.apellidoP = ApellidoP;
            this.correo = correo;
            this.telefono = telefono;
            this.matricula = mat;
        }

        public string Matricula { get => matricula; set => matricula = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApellidoP { get => apellidoP; set => apellidoP = value; }
        public string ApellidoM { get => apellidoM; set => apellidoM = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public bool Estado { get => estado; set => estado = value; }
        public string Tarjeta { get => tarjeta; set => tarjeta = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        internal List<Proyecto> MisPro { get => misPro; set => misPro = value; }


       
    }
}
