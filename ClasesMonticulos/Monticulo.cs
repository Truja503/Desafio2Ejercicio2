using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace Desafio2Ejercicio2.ClasesMonticulos
{
    class Monticulo
    {
        private int totnodos; //conteo de nodos existentes en Heap

        //parametros del contenido del monticulo
        Tarea[] matriz; //para almacenar los valores de los nodos

        //matriz con valores ordenados del heap
        private int[] valoresHeapSort;



        private void InicializarCampos()
        {
            totnodos = 0;
            //se ignora la posicion 0 de la matriz de nodos
            matriz = new Tarea[1000];

        }

        public Monticulo() //metodo constructor
        {
            InicializarCampos();
        }
        // Metodos para definir aspectos del diagrama del Arbol de Monticulo


        //propiedad solo lectura para retornar cantidad de nodos del Heap
        public int TotNodos()
        {
            return totnodos;
        }
        public void Vaciar()
        {
            //vacia contenido del Heap
            totnodos = 0;
        }

        //Operaciones para ordenar valores dentro de un Heap

        public void IntercmabiarNodos(int id1, int id2)
        {
           
            Tarea temp = new Tarea();

            //intercambia posiciones de nodos
            temp= matriz[id1];
            matriz[id1] = matriz[id2];
            matriz[id2] = temp;

            //restaura apariencia estandar de nodos

        }

        private void SubirNodo(int id, bool continuar = true)
        {
           
            int idpadre;
     
            if (id > 1)
            {
                idpadre = matriz[id].Padre();
                //resalta nodo padre

                //determina si intercambiar nodos (hijo-padre)
                if (matriz[id].tipodetarea < matriz[idpadre].tipodetarea)
                {
                    IntercmabiarNodos(id, idpadre);

                    //proc. de ordenamiento sigue con nuevo nodo padre
                    id = idpadre;

                    //continua ordenando al nuevo nodo padre en montículo
                    if (continuar)
                        SubirNodo(id);
                }else if (matriz[id].fecha < matriz[idpadre].fecha)
                {
                    IntercmabiarNodos(id, idpadre);
                }


            }


        }


        private void Descender(int idpadre, bool continuar = true)
        {
            //determina si debe descender nodo padre, intercambiando su valor
            //con el mayor de sus hijos
            int idhijo; //indice de hijo izquierdo
            int idhijomayor = 0; //indice de hijo con supuesto valor mayor que nodo padre

            if (idpadre > 0 && idpadre <= totnodos)
            {
                idhijo = 2 * idpadre;
                if (idhijo <= totnodos)
                {
                    //resalta a nodo padre

                    //determina el idhijo con valor mayor
                    idhijomayor = idhijo;


                    //prueba si existe hijo derecho
                    if (idhijo + 1 <= totnodos)
                    {

                        //prueba si valor hijo derecho es menor que de hijo izquierdo
                        if (matriz[idhijo + 1].tipodetarea > matriz[idhijo].tipodetarea)
                            idhijomayor = idhijo + 1;
                        //hijo con menor valor es nodo derecho
                    }

                    if (matriz[idhijomayor].tipodetarea > matriz[idpadre].tipodetarea)
                    {
                        IntercmabiarNodos(idpadre, idhijomayor);
                    }
                    //restaura formato a nodo padre


                    if (continuar)
                        Descender(idhijomayor);

                }
            }
        }

        //Insercion de valores en un Heap
        public bool InsertarValor(int Tipotarea, DateTime Fecha, string responsbale, string Descripcion, bool ordenarnodo = true)
        {
            bool res = false;

                totnodos++;

                //Crea nuevo nodo en ultima posicion de matriz
                matriz[totnodos] = new Tarea(Tipotarea, Fecha, responsbale, Descripcion, totnodos);


                if (ordenarnodo)
                    SubirNodo(totnodos);
                res = true;
            
            return res;
        }
        //Operaciones para remover valor maximo de un Heap

        private int BorrarRaiz(bool ActualizarVista = true)
        {
            //Remueve y retorna valor de nodo raiz (clave minima) del Heap
            int valorraiz = -1; //asume que monticulo esta vacio
            if (totnodos > 0)
            {
                //extrae copia de valor del nodo a borrar
                valorraiz = matriz[1].tipodetarea;

                totnodos--; //reduce cantidad nodos en Heap 


                matriz[totnodos + 1].tipodetarea = valorraiz;

            }
            return valorraiz;
        }
        public int borrarPrimero()
        {
            return BorrarRaiz();
        }
        public int BorrarMaximo()
        {
            int numborrar = BorrarRaiz(false);
            if (totnodos > 0)
                Descender(1);
            //retorna valor del raiz inicial
            return numborrar;
        }
        //Operaciones para obtener vector ordenado de un Heap 

        public int[] EjecutarHeapSort()
        {
            int i; //contador de indice de posiciones del vector ya ordenado 
            int valorRaiz; //valor de nodo raiz de monticulo 
            if (totnodos > 0)
            {
                //metodo de ordenamiento rapido de prioridad (HeapSort) 
                //crea vector y longitud igual a cantidad nodos del Heap 
                valoresHeapSort = new int[0];
                i = 0; //contador de elementos extraidos de Heap while (totnodos > 0) 
                while (totnodos > 0)
                { //extrae raiz (clave menor, heap minimizante). 
                    valorRaiz = BorrarRaiz(false);
                    //agrega una posicion mas a la longitud de vector
                    Array.Resize(ref valoresHeapSort, i + 1);
                    valoresHeapSort[i] = valorRaiz;
                    Descender(1);
                    i++;
                }
            }
            else return null;

            return valoresHeapSort;
        }

        //Ordena contenido de un vector usando Monticulo y algoritmo HeapSort 
        public int[] OrdenarVector()
        {
            //utiliza metodo HeapSort para ordenar vector ingresado por usuario 
            //contador de indice de posic. dentro de vector ya ordenado 
            int c = 0;
            if (totnodos == 0)
                return null; // Heap esta vacio 
                             //crea vector, que tendra valores del Heap ya ordenados 
            valoresHeapSort = new int[0]; //vector vacio 
            while (totnodos > 0)
            {
                //id del ultimo nodo padre a ordenar del Heap actual 
                int id = totnodos / 2;
                //ordena a ultimo nodo padre y sus nodos hijos 
                while (id > 0)
                {
                    Descender(id, false); //no aplicara recursividad 
                                          //determina el padre anterior al actual para ordenarlo
                    id = id - 1;
                }
                //agrega una posicion mas a la longitud de vector
                Array.Resize(ref valoresHeapSort, c + 1);
                valoresHeapSort[c] = BorrarRaiz(false);
                c++;
            }

            return valoresHeapSort;
        }


        //Encapsulamos los metodos para poder utilizarlos en el formulario


        private void mostrarMonticuloEnGrid(DataGridView dataGridView)
        {
            // Limpiar el DataGridView antes de agregar nuevos datos
            dataGridView.Rows.Clear();

            // Recorrer todos los nodos del montículo y agregarlos al DataGridView
            for (int i = 1; i <= totnodos; i++)
            {
                dataGridView.Rows.Add(
                    matriz[i].correlativo,
                    matriz[i].descripcion,
                    matriz[i].fecha,
                    Prioridad(matriz[i].tipodetarea),
                    matriz[i].responsable
                );
            }

        }
        public void MostrarMonticuloEnGrid(DataGridView dataGridView)
        {

            mostrarMonticuloEnGrid(dataGridView);
        }
        public String Prioridad(int prioridadn)
        {
            switch (prioridadn)
            {
                case 0:
                    return "Salud";


                case 1:
                    return "Familia";


                case 2:
                    return "Trabajo";

                case 3:
                    return "Personal";

                default:
                    return "";
            }
        }
    }
}


 

