using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_proyecto
{
	public class Lenguaje_Tiny
	{
		public AFN afn_id;
		public AFD afd_id;
		public AFN afn_num;
		public AFD afd_num;
		public string posfija_id, posfija_num;
		public List<Token> tokens = new List<Token>();
		public List<string> palabras_reservadas = new List<string>();

		public void genera_afds(string exp_id, string exp_num)
        {
			posfija_id = "";
			posfija_num = "";
			Expresion exp = new Expresion(); // Inicializa la expresion.
			List<int> cero = new List<int>();

			// Genera la posfija de ID
			exp.expresion = exp_id;
			exp.expresion = exp.expresion.Replace(" ", "");
			exp.expresion = exp.convierte_a_explicita(exp.expresion);
			posfija_id = exp.convierte_a_posfija();

			// Genera la posfija de NUM
			exp.expresion = exp_num;
			exp.expresion = exp.expresion.Replace(" ", "");
			exp.expresion = exp.convierte_a_explicita(exp.expresion);
			posfija_num = exp.convierte_a_posfija();

			// Genera los AFN
			afn_id = new AFN();
			afn_id.genera_automata_AFN(posfija_id);
			afn_id.genera_alfabeto(posfija_id);
			afn_id.genera_matriz();

			afn_num = new AFN();
			afn_num.genera_automata_AFN(posfija_num);
			afn_num.genera_alfabeto(posfija_num);
			afn_num.genera_matriz();

			// Genera los AFD
			afd_id = new AFD();
			afd_id.automata_afn = afn_id;
			afd_id.genera_alfabeto(posfija_id);
			cero.Add(0);
			afd_id.genera_automata_afd(afd_id.cerradura_epsilon(cero));
			afd_id.genera_matriz();
			cero.Clear();

			afd_num = new AFD();
			afd_num.automata_afn = afn_num;
			afd_num.genera_alfabeto(posfija_num);
			cero.Add(0);
			afd_num.genera_automata_afd(afd_num.cerradura_epsilon(cero));
			afd_num.genera_matriz();
			cero.Clear();
		}

		public void setea_palabras_reservadas()
        {
			palabras_reservadas.Add("if");
			palabras_reservadas.Add("then");
			palabras_reservadas.Add("else");
			palabras_reservadas.Add("end");
			palabras_reservadas.Add("repeat");
			palabras_reservadas.Add("until");
			palabras_reservadas.Add("read");
			palabras_reservadas.Add("write");
			palabras_reservadas.Add("+");
			palabras_reservadas.Add("-");
			palabras_reservadas.Add("*");
			palabras_reservadas.Add("/");
			palabras_reservadas.Add("=");
			palabras_reservadas.Add("<");
			palabras_reservadas.Add(">");
			palabras_reservadas.Add("(");
			palabras_reservadas.Add(")");
			palabras_reservadas.Add(";");
			palabras_reservadas.Add(":=");
		}

		public void verifica_tokens()
        {
			bool band_checado = false; // Bandera que se usa para ya no repetir validaciones

			for (int i = 0; i < tokens.Count; i++)
			{
				band_checado = false;

				// Checa si es una palabra reservada
				foreach (string palabra in palabras_reservadas)
					if (tokens[i].lexema == palabra)
					{
						tokens[i].nombre = palabra;
						band_checado = true;
					}

				// Si no fue palabra reservada, entonces se tiene que checar si es id o es num
				if (!band_checado)
                {
					// Checa si es id
					if(afd_id.verifica_lexema(afd_id.Destados[0], tokens[i].lexema, 0))
                    {
						tokens[i].nombre = "Identificador";
						band_checado = true;
                    }
					else // Checa si es num
					{
						if (afd_num.verifica_lexema(afd_num.Destados[0], tokens[i].lexema, 0))
						{
							tokens[i].nombre = "Número";
							band_checado = true;
						}
					}
                }

                // Si no es nada de lo anterior, entonces es error
                if (!band_checado)
					tokens[i].nombre = "Error léxico";
            }
        }

		public void genera_tokens(string codigo_tiny)
        {
			// Le quita todos los espacios repetidos y deja 1 solo espacio
			codigo_tiny = codigo_tiny.Replace("\n", " ");
			while (codigo_tiny.Contains("  "))
			{
				codigo_tiny = codigo_tiny.Replace("  ", " ");
			}

			Token tkn = new Token();
			string palabra = ""; // String usado para separar las palabras

			// Se recorre toda la linea de codigo y si es espacio, corta la palabra y la guarda
			foreach(char c in codigo_tiny)
            {
				if(c != ' ')
					palabra += c.ToString();
				else
                {
					tkn = new Token();
					tkn.lexema = palabra;
					tokens.Add(tkn);
					palabra = "";
                }
            }
        }
	}
}
