using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinder4Word.Classes1
{
    public class Proyecto
    {
        int id;
        string nombre;
        string matriculaDueño;
        bool estado;
        string descripcion;
        public List<Habilidad> habRequeridas = new List<Habilidad>();
        public Usuario dueño;

        public Proyecto(int id, string nombre, string matricula, bool estado)
        {
            this.id = id;
            this.nombre = nombre;
            this.matriculaDueño = matricula;
            this.estado = estado;
        }

        public Proyecto(int id, string nombre, string matricula, Usuario dueño)
        {
            this.id = id;
            this.nombre = nombre;
            this.matriculaDueño = matricula;
            this.dueño = dueño;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string MatriculaDueño { get => matriculaDueño; set => matriculaDueño = value; }
        public bool Estado { get => estado; set => estado = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
