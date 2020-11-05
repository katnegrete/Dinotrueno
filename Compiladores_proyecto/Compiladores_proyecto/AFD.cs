using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiladores_proyecto
{
	public class AFD
	{
		public AFN automata_afn = new AFN();
		public List<Destado> Destados = new List<Destado>(); // Destados 
		public List<Transicion> transiciones = new List<Transicion>(); // Lista de transiciones
		public string[,] matriz;
		public string alfabeto;

		public void genera_automata_afd(List<int> cerradura_cero)
		{
			Destado destado;
			Transicion transicion;
			int cont_destado = 0;
			List<int> destado_aux = new List<int>();
			char id_destado = 'A';
			bool band_repetido = false;
			

			// Se genera el primer destado (A)
			destado = new Destado();
			destado.conjunto = cerradura_cero;
			destado.id = id_destado.ToString(); // Siempre va a ser A por sel el primer destado
			id_destado++;
			Destados.Add(destado);

			while (checa_destados_marcados())
			{
				Destados[cont_destado].marcado = true; // Marca el destado

				foreach (char a in alfabeto) // Recorre el alfabeto
				{
					destado_aux = cerradura_epsilon(mover(Destados[cont_destado], a));

					if(destado_aux.Count > 0)
                    {
						destado = new Destado();
						band_repetido = false;

						// Buscar si existe ese conjunto en Destados para agregarlo
						for (int i = 0; i < Destados.Count; i++)
						{
							if (compara_conjuntos(Destados[i].conjunto, destado_aux)) // Si se repite, se asigna para saber cual es
							{
								band_repetido = true;
								destado = Destados[i]; 
								break;
							}
						}
						
						if (!band_repetido) // Si no se repite, crea un destado nuevo
						{
							destado.conjunto = destado_aux;
							destado.id = id_destado.ToString();
							id_destado++;
							Destados.Add(destado);
						}

						// Se crea la tansicion correspondiente
						transicion = new Transicion();
						transicion.simbolo = a;
						transicion.destado_origen = Destados[cont_destado];
						transicion.destado_destino = destado;
						transiciones.Add(transicion);
					}

                }

				cont_destado++; // Mueve el indice
            }

			// Checa destados de aceptacion
			verifica_destados_aceptacion();
        }

		public void verifica_destados_aceptacion()
        {
			foreach(Destado d in Destados) // Recorre los destados
				foreach(int i in d.conjunto) // Recorre el conjunto del destado
					if(i == automata_afn.estados[automata_afn.estados.Count - 1].id) // Si el estado de aceptacion del afn se encuentra en el conjunto entonces es de aceptacion
						d.tipo = "aceptacion";
        }

		public bool compara_conjuntos(List<int> c1, List<int> c2)
        {
			bool band = true;

			// Se ordenan de menor a mayor para comprar las cadenas
			c1.Sort();
			c2.Sort();

			if (c1.Count == c2.Count) 
			{ 
				for(int i = 0; i < c1.Count; i++)
					if(c1[i] != c2[i])
						band = false;
            }
            else
				band = false;

			return band;
        }

		public List<int> cerradura_epsilon(List<int> estados)
        {
			List<int> cerradura = new List<int>();
			List<int> cerradura_aux = new List<int>();
			bool band_repetido;

			foreach(int e in estados) // Rcorre los estados a sacar la cerradura
            {
				cerradura.Add(e); // Se agrega a si mismo

				for(int i = 0; i < cerradura.Count; i++)
                {
					foreach(Estado estado in automata_afn.estados) // Recorre todos los estados del afn
					{
						if(estado.id == cerradura[i]) // Encuentra un estado del afn que sea el estado que se está aplicando la cerradura
						{
							cerradura_aux = checa_transiciones_epsilon(estado);

							// Se tiene que checar cuales ya están repetidos para no agregarlos
							foreach(int c1 in cerradura_aux) // Recorre los nuevos
                            {
								band_repetido = false;

								foreach(int c2 in cerradura) // Recorre los actuales
									if(c1 == c2) // Si se repide
										band_repetido = true;

								if (!band_repetido) // Si no se repite, agregar
									cerradura.Add(c1);
                            }
                        }
                    }
                }
            }

			return cerradura;
        }

		public List<int> checa_transiciones_epsilon(Estado e)
        {
			List<int> cad = new List<int>();

			foreach(Transicion t in automata_afn.transiciones)
				if(t.estado_origen == e && t.simbolo == 'ε')
					cad.Add(t.estado_destino.id);

			return cad;
        }

		public List<int> mover(Destado destado, char letra)
        {
			List<int> conjunto_mover = new List<int>();

			foreach (int c in destado.conjunto) // Recorre el conjunto del destado
			{
				// Tiene que buscar una transicion que tenga la etiqueta a y que tenga un estado origen c
				foreach (Transicion t in automata_afn.transiciones)
					if (t.simbolo == letra && t.estado_origen.id == c)
						conjunto_mover.Add(t.estado_destino.id);
			}

			return conjunto_mover;
		}

		public bool checa_destados_marcados()
        {
			bool band = false;

			foreach(Destado d in Destados)
            {
				if (d.marcado == false)
					band = true;
            }

			return band;
        }

		public void genera_alfabeto(string posfija)
		{
			string a = ""; // Es el alfabeto chiquito (solo las letras que se van a usar)
			bool band_repetido;

			// Recorre la posfija y extrae todos los operandos y ese es el alfabeto.
			foreach (char c in posfija)
				if (c != '?' && c != '+' && c != '*' && c != '|' && c != '&' ) // Si es diferente de los operadores, entonces es operando.
                {
					// Se tiene que checar que c no se repita ya en a
					band_repetido = false;
					foreach(char ca in a)
                    {
						if(ca == c)
							band_repetido = true;
                    }
					// Si no se repite, se añade
					if(!band_repetido)
						a += c; // Añade operando al alfabeto
				}

			alfabeto = a;
		}

		public void genera_matriz()
        {
			matriz = new string[Destados.Count, alfabeto.Length];
			string vacio = "Φ";

			for (int i = 0; i < Destados.Count; i++)
            {
				for(int j = 0; j < alfabeto.Length; j++)
                {
					matriz[i, j] = vacio;
					foreach (Transicion t in transiciones)
                    {
						if(t.destado_origen.id == Destados[i].id && t.simbolo == alfabeto[j])
							matriz[i, j] = t.destado_destino.id;
                    }
                }
            }
		}

		public bool verifica_lexema(Destado usando, string lexema, int cont)
        {
			bool band = false; // Bandera para saber si encontró camino

			if(lexema == "") // Si se puso epsilon
				if (usando.tipo == "aceptacion") // Si el destado en el que terminó es de aceptacion
					return true;

			if (cont >= lexema.Length) // Si ya terminó de recorrer el lexema
				if(usando.tipo == "aceptacion") // Si el destado en el que terminó es de aceptacion
					return true;

			// Se recorren todas las transiciones para bsucar los caminos
			foreach(Transicion t in transiciones)
            {
				if(t.destado_origen == usando) // Si encuentra una transicion que empiece con el destado usando
                {
					if(t.simbolo == lexema[cont]) // Si la transicion va con el simblo en el que se está iterando el lexema
                    {
						band = true;
						cont++;
						return verifica_lexema(t.destado_destino, lexema, cont);
                    }
                }
            }
            if (!band) // Si no encontró por donde irse, el lexema no poertenece a la gramatica.
				return false;

			return false;
        }
	}
}
