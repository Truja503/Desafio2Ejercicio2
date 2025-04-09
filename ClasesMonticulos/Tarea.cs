using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Desafio2Ejercicio2.ClasesMonticulos
{
    

    class Tarea
    {
        public int tipodetarea;
        public int correlativo; //indice de posicion dentro de la matriz del contenido
        public int nivel;
        public DateTime fecha;
        public string responsable;
        public string descripcion;

        public Point posic;
        public Point posicM;

        public int CalcularNivel()
        {
            // A partir de indice de posic. de nodo en matriz, actualiza el
            // nivel dentro de vista del arbol y tambien lo retorna
            int idnivel = -1; //asume que id (0) aun no esta definido

            if (correlativo > 0)
                nivel = Convert.ToInt32(Math.Truncate(Math.Log(correlativo) / Math.Log(2)));
            idnivel = nivel;
            return idnivel;
        }

        public Tarea() //constructor
        {
            tipodetarea = 0;
            nivel = 0;
            correlativo = 0;
            fecha = DateTime.Now;
            responsable = string.Empty;
        }
        //sobrecarga de constructor
        public Tarea(int TipodeTarea, DateTime Fecha, string Responsble, string Descripcion, int idposic = 0)
        {
            tipodetarea = TipodeTarea;
            correlativo = idposic;
            nivel = CalcularNivel();
     
            fecha = Fecha;
            responsable =Responsble;
            descripcion = Descripcion;  


        }
      

        public int Padre()
        {
            if (correlativo == 0)
                return -1;
            else
                return correlativo / 2;
        }

        public int Hijo1()
        {
            //retorna indice de matriz de nodo izquierdo
            return 2 * correlativo;
        }

    }
}
