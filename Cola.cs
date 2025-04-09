using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio2Ejercicio2
{
    public class Cola
    {
        // Atributos 
        //punteros que señalan el inicio y el final de nodos en cola 
        NodoCola primero;
        NodoCola ultimo;
        int totnodoscola; // almacena total de nodos existentes en cola 
                      // Metodos 
        public Cola() //metodo constructor 
        {
            primero = ultimo = null;
            totnodoscola = 0;
        }

       
        public int TotNodosCola()
        {
            //retorna valor de campo totnodos 
            return this.totnodoscola;
        }

        public NodoCola Primero()
        {
            return primero;
        }
        //Retorna true si cola esta vacia. Para ello, evalua a puntero primero 
        public bool EstaVacia()
        {
            if (primero == null) return true; //cola esta vacia 
            else return false; //cola tiene al menos un nodo 
        }
        public void VerContenido()
        {
            //recorre los nodos de la cola y los muestra en pantalla 
            NodoCola aux; //permitira señalar a cada nodo dentro de cola 
            if (EstaVacia())
                Console.WriteLine("\nCola esta vacia, no tiene nodos");
            else
            {
                //aplicacion de algoritmo FIFO (First In, First Out) 
                Console.Write("\n PRIMERO ");
                aux = primero; //fija puntero aux al inicio de cola 
                do
                {
                    Console.Write(" <- {0}", aux.info);
                    aux= aux.sig; //puntero se desplaza a prox. nodo 
                } while (aux != null);
                Console.WriteLine(" <- ULTIMO");
            }
        }
        public void Encolar(NodoCola nodo)
        {
            //agrega un nodo al final de la cola (entrada) 
            if (EstaVacia())
                primero= ultimo= nodo;
            else
            {
                ultimo.sig= nodo;
                ultimo= nodo;
            }
            totnodoscola++; //incrementa conteo nodos existentes 
        }

        public NodoCola Desencolar() { 
        NodoCola aux = null; //nodo a remover del inicio de cola if (!EstaVacia())
            if (!EstaVacia()) //procede a extraer nodo ubicado al inicio de cola (salida)
            {
                aux = primero;
                primero = primero.sig;
                totnodoscola--; //reduce conteo de nodos existentes
            }
    return aux; // en caso que pila este vacia, retornara null
}
}
}
