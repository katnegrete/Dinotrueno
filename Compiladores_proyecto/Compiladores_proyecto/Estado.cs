using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_proyecto
{
    public class Estado
    {
        public int id = 0; // Identificador del estado
        public string tipo = ""; // Tipo del estado ("inicio", "normal", "aceptacion")
        public List<Transicion> transiciones_entrada = new List<Transicion>(); // Lista de transiciones que entran a este estado
        public List<Transicion> transiciones_salida = new List<Transicion>(); // Lista de transiciones que salen de este estado
    }
}
