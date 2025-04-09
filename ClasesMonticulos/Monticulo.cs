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
            int valortemp;


            //intercambia posiciones de nodos
            valortemp = matriz[id1].tipodetarea;
            matriz[id1].tipodetarea = matriz[id2].tipodetarea;
            matriz[id2].tipodetarea = valortemp;

            //restaura apariencia estandar de nodos

        }

        private void SubirNodo(int id, bool continuar = true)
        {
            //si nodo actual es mayor que su nodo padre, lo intercambia
            int idpadre;
            //evalua si nodo hijo debe ser intercambiado (ordenado) con su nodo padre
            //y si se requiere, se continua recursivamente hasta ordenar nodo id en Heap
            //Este proc. de ordenamiento se repetira hasta que se llegue al nodo Raiz
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
            //verifica que valornodo sea aceptado en el Monticulo
            if (Tipotarea >= 0 && Tipotarea < 5)
            {
                totnodos++;


                //Crea nuevo nodo en ultima posicion de matriz
                matriz[totnodos] = new Tarea(Tipotarea, Fecha, responsbale, Descripcion);


                if (ordenarnodo)
                    SubirNodo(totnodos);
                res = true;
            }
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

        public int[] Recorrer(int tipoRecorrido)
        {
            Cola cola = new Cola();
            switch (tipoRecorrido)
            {
                case 0:
                    RecorridoInOrden(1, cola);
                    break;
                case 1:
                    RecorridoPreOrden(1, cola);
                    break;
                case 2:
                    RecorridoPostOrden(1, cola);
                    break;
                default:
                    throw new ArgumentException("Tipo de recorrido no válido.");
            }
            int tot = cola.TotNodosCola();
            int[] array = new int[tot];
            NodoCola actual = cola.Primero();
            int index = 0;

            while (actual != null)
            {
                array[index++] = actual.info;
                actual = actual.sig;
            }

            return array;

        }

        private void RecorridoInOrden(int idnodo, Cola cola)
        {
            if (idnodo > totnodos) return;

            // Recorrer hijo izquierdo
            RecorridoInOrden(idnodo * 2, cola);
            // Visitar nodo actual

            NodoCola nodo = new NodoCola(matriz[idnodo].tipodetarea);
            cola.Encolar(nodo);
            // Recorrer hijo derecho
            RecorridoInOrden(idnodo * 2 + 1, cola);
        }

        private void RecorridoPreOrden(int idnodo, Cola cola)
        {
            if (idnodo > totnodos) return;

            // Visitar nodo actual

            NodoCola nodo = new NodoCola(matriz[idnodo].tipodetarea);
            cola.Encolar(nodo);
            // Recorrer hijo izquierdo
            RecorridoPreOrden(idnodo * 2, cola);
            // Recorrer hijo derecho
            RecorridoPreOrden(idnodo * 2 + 1, cola);
        }

        private void RecorridoPostOrden(int idnodo, Cola cola)
        {
            if (idnodo > totnodos) return;

            // Recorrer hijo izquierdo
            RecorridoPostOrden(idnodo * 2, cola);
            // Recorrer hijo derecho
            RecorridoPostOrden(idnodo * 2 + 1, cola);
            // Visitar nodo actual

            NodoCola nodo = new NodoCola(matriz[idnodo].tipodetarea);
            cola.Encolar(nodo);
        }

        //Encapsulamos los metodos para poder utilizarlos en el formulario


        private void mostrarMonticuloEnGrid(DataGridView dataGridView)
        {
            dataGridView.Rows.Add(matriz[totnodos].correlativo, matriz[totnodos].descripcion, matriz[totnodos].fecha, Prioridad(matriz[totnodos].tipodetarea), matriz[totnodos].responsable);
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


 

