using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_proyecto
{
    public class Simbolo_Gramatical
    {
        public string simbolo = "";
        public string tipo = "T"; // T=terminal, NT=no terminal, $=$
        public List<string> primeros = new List<string>();
        public List<string> siguientes = new List<string>();

        public Simbolo_Gramatical(string s, string t)
        {
            simbolo = s;
            tipo = t;
        }

        public Simbolo_Gramatical(){}
    }
}
