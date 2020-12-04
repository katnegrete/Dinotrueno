using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_proyecto
{
    public class Destado
    {
        public string id = "";
        public List<int> conjunto = new List<int>();
        public bool marcado = false;
        public string tipo = "normal"; // Normal o de Aceptacion

        // para LR()
        public List<List<Simbolo_Gramatical>> conjunto_lr0 = new List<List<Simbolo_Gramatical>>();
    }
}
