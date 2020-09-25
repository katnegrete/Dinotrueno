using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiladores_proyecto
{
    public class Expresion
    {
        public Stack<char> pila; // La pila para el procedimiento
        public string expresion; // La expresion en infija

        public string convierte_a_posfija()
        {
            pila = new Stack<char>();
            string posfija = ""; // Inicializa la expresion posfija
            bool band_parentesis; // Bandera para encontrar parentesis izquierdo
            bool band_operador; // Bandera que se usa en el caso de los operadores

            foreach(char c in expresion) // Recorre caracter por caracter la expresion regular
            {
                switch (c)
                {
                    // Parentesis
                    case '(':
                        // Insertar en la pila
                        pila.Push(c);
                        break;
                    case ')':
                        // Extraer de la pila y desplegar en posfija hasta encontrar '(' (no desplegarlo)
                        band_parentesis = false;
                        while (!band_parentesis) // Mientras no encuentre un parenetsis izquierdo
                        {
                            if(pila.Peek() == '(') // Si ya encuentra un parentesis izquierdo lo saca pero no lo despliega
                            {
                                band_parentesis = true;
                                pila.Pop();
                            }
                            else // Si no es parentesis, continua
                            {
                                posfija += pila.Pop();
                            }
                        }
                        break;
                    // Operadores
                    case '*':
                    case '?':
                    case '+':
                    case '|':
                    case '&':
                        band_operador = true;
                        while (band_operador)
                        {
                            // Si la pila está vacia, o el tope es ( o el operador tiene mayor prioridad que el tope de la pila
                            if(pila.Count == 0 || pila.Peek() == '(' || checa_prioridad(c))
                            {
                                pila.Push(c);
                                band_operador = false;
                            }
                            else
                            {
                                posfija += pila.Pop();
                            }
                        }
                        break;
                    // Operandos
                    case '.':
                    default:
                        // Desplegar en posfija
                        posfija += c;
                        break;
                }
            }
            // Extraer y desplegar todos los elementos restantes de la pila
            while(pila.Count > 0)
                posfija += pila.Pop();

            return posfija;
        }

        public bool checa_prioridad(char operador)
        {
            bool prioridad = false; // Se asume que no tiene prioridad.

            switch (operador)
            {
                case '*':
                case '?':
                case '+':
                    // Si el tope de la pila tiene menor prioridad, entonces si tiene mayor prioridad
                    if (pila.Peek() == '&' || pila.Peek() == '|') 
                        prioridad = true;
                    break;
                case '&':
                    // Si el tope de la pila tiene menor prioridad, entonces si tiene mayor prioridad
                    if (pila.Peek() == '|')
                        prioridad = true;
                    break;
                case '|':
                    // Nunca va a tener una mayor prioridad.
                    prioridad = false;
                    break;
            }

            return prioridad; // Regresa TRUE si si tiene mayor prioridad
        }

        public string convierte_a_explicita(string exp)
        {
            string explicita = ""; // Expresion ya explicita

            string expresion_sin_corchetes = ""; // Cadena que va a quedar con la expresion con los [] cambiados por ()
            string aux_corchetes = ""; // Cadena que solo contiene [...]
            bool band_corchetes = false; // Bandera para poder capturar el contenido de los corchetes
            string corchetes_explicitos; // Es la cadena en la que va quedar transformado [a-b] -> (a|b)
            char interior_corchetes = new char(); // Usado para recorrer un "a-d"

            List<string> lista_corchetes = new List<string>(); // Se usa para almacenar todos los pares de corchetes que se van a transformar
            List<string> lista_parentesis = new List<string>(); // Lista en la que se van a almacenar todos los corchetes ya transformados en () y |
            int cont_lista_corchetes; // Contador para recorrer la lista de los parentesis 

            // Primero se tiene que desglosar los corchetes
            foreach(char c in exp)
            {
                if (c == '[') // Cuando lee un [ empieza a capturar
                    band_corchetes = true;

                if (band_corchetes)
                    aux_corchetes += c;

                if (c == ']') // Cuando lee un ] termina de capturar y almacena en la lista de corchetes
                {
                    band_corchetes = false;
                    lista_corchetes.Add(aux_corchetes);
                    aux_corchetes = "";
                } 
            }

            // Ya se tienen las cadenas con los puros corchetes.
            // Ahora a cada caedna, se le tiene que transformar a su version de () y |
            foreach(string corchetes in lista_corchetes)
            {
                // Se checa el tipo, (si es [abcd] o [a-d])
                if (corchetes.Length - 1 > 0)
                {
                    corchetes_explicitos = "(";
                    if (corchetes[2] == '-') // Se verifica a secas ese indice porque es en donde deberia de estar si es que está en esa forma
                    {
                        interior_corchetes = corchetes[1];
                        while (interior_corchetes <= corchetes[3])
                        {
                            corchetes_explicitos += interior_corchetes + "|";
                            interior_corchetes++;
                        }
                    }
                    else // Tiene forma de [abcd]
                    {
                        // Se recorren los corchetes sin pasar por los mismo corchetes, poniendo caracter | caracter | etc, (queda con un | al final
                        for (int i = 1; i <= corchetes.Length - 2; i++)
                            corchetes_explicitos += corchetes[i] + "|";
                    }
                    //Se quita el  que sobra al final, se cierra el parentesis y se agrega a la lista de los ya transformados
                    corchetes_explicitos = corchetes_explicitos.Remove(corchetes_explicitos.Length - 1);
                    corchetes_explicitos += ")";
                    lista_parentesis.Add(corchetes_explicitos);
                }
            }

            //Ahora se tienen que reemplazar los corchetes por los parentesis desglosados
            //Se recorre la cadena y se va capturando caracter por caracter hasta encontrar un [, en donde deja de capturar para reemplazar por los () y vuelve a capturar hasta que pasa un ]
            band_corchetes = true;
            cont_lista_corchetes = 0;
            foreach(char c in exp)
            {
                if (c == '[')
                {
                    band_corchetes = false;
                    expresion_sin_corchetes += lista_parentesis[cont_lista_corchetes];
                    cont_lista_corchetes++;
                }

                if (band_corchetes)
                    expresion_sin_corchetes += c;

                if (c == ']')
                    band_corchetes = true;
            }

            // Tiene que recorrer cada valor de la cadena
            for (int i = 0; i <= expresion_sin_corchetes.Length-1; i++)
            {
                explicita += expresion_sin_corchetes[i]; // Tiene que ir rellenando la cadena.

                // Si es un operador unario, se le tiene que poner a la derecha un & para que concatene con lo siguiente.
                if (i < expresion_sin_corchetes.Length - 1) // Si al menos es el penultimo, no debe de entrar cuando sea el ultimo
                {
                    if ((expresion_sin_corchetes[i] == '+' || expresion_sin_corchetes[i] == '?' || expresion_sin_corchetes[i] == '*') && expresion_sin_corchetes[i+1] != '|' && expresion_sin_corchetes[i + 1] != ')')
                        explicita += "&";
                }

                // Si el valor actual es un operando y el siguiente tambien lo es o es un (, se debe de poner un & en medio
                if (i < expresion_sin_corchetes.Length-1) // Si al menos es el penultimo, no debe de entrar cuando sea el ultimo
                {
                    if (expresion_sin_corchetes[i] != '[' && expresion_sin_corchetes[i] != ']' && expresion_sin_corchetes[i] != '(' && expresion_sin_corchetes[i] != '+' && expresion_sin_corchetes[i] != '*' && expresion_sin_corchetes[i] != '?' && expresion_sin_corchetes[i] != '-' && expresion_sin_corchetes[i] != '|')
                    {
                        if (expresion_sin_corchetes[i + 1] != '[' && expresion_sin_corchetes[i + 1] != ']' && expresion_sin_corchetes[i + 1] != ')' && expresion_sin_corchetes[i + 1] != '+' && expresion_sin_corchetes[i + 1] != '*' && expresion_sin_corchetes[i + 1] != '?' && expresion_sin_corchetes[i + 1] != '-' && expresion_sin_corchetes[i + 1] != '|')
                        {
                            explicita += "&";
                        }
                    }
                }
            }

            // Al final checa si se quedó con un & al mero final de la cadena, si si, lo tiene que quitar.
            if (explicita[explicita.Length-1] == '&')
                explicita = explicita.Remove(explicita.Length-1);

            return explicita;
        }
    }
}
