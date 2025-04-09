using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio2Ejercicio2
{
    public class NodoCola
    {
        
            //campos 
            public int info;
            public NodoCola sig;
            //metodos 
            public NodoCola(int Valor)
            {
                info =Valor;
                sig = null;
            }
        
    }
}
