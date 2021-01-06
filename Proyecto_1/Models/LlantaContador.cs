using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1.Models
{
    public class LlantaContador
    {
        public int idLlanta;
        public int contador;

        public LlantaContador(int i, int c)
        {
            idLlanta = i; contador = c;
        }
    }
}