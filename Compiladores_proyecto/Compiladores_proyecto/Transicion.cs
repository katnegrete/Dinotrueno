﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_proyecto
{
    public class Transicion
    {
        public char simbolo = new char(); // Pues el simbolo que mas
        public Estado estado_origen = new Estado(); // Estado del que sale
        public Estado estado_destino = new Estado(); // Estado al que llega
    }
}