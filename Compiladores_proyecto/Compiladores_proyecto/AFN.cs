using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiladores_proyecto
{
    public class AFN
    {
		public List<Estado> estados = new List<Estado>(); // Lista de estados
		public List<Transicion> transiciones =  new List<Transicion>(); // Lista de transiciones
		public string[,] matriz;
		public string alfabeto;

		public void genera_automata_AFN(string posfija)
		{
			Stack<AFN> pila = new Stack<AFN>(); // Pila de los automatas
			AFN r1, r2, r3;
			

			// Recorre la posfija para el algoritmo
			foreach (char c in posfija)
			{
				if(c == '?' || c == '*' || c == '+') // Encuentra operador unario
                {
					// Se inicializan las variables
					r1 = new AFN();
					r2 = new AFN();

					r1 = pila.Pop();
					r2 = genera_miniautomata_operador_unario(r1, c);
					pila.Push(r2);
                }
				else if(c == '&' || c == '|'){ // Encuentra operador binario
					// Se inicializan las variables
					r1 = new AFN();
					r2 = new AFN();
					r3 = new AFN();

					r2 = pila.Pop();
					r1 = pila.Pop();
					r3 = genera_miniautomata_operador_binario(r1, r2, c);
					pila.Push(r3);
				}
				else // Encuentra operando
                {
					// Se inicializan las variables
					r1 = new AFN();

					r1 = genera_miniautomata_operando(c);
					pila.Push(r1);
                }
			}

			// Se rellenan la lista de estados, la lista de tranciciones y se genera la matriz de relaciones con el tope de la pila
			r1 = new AFN();
			r1 = pila.Peek();

			foreach (Estado e in r1.estados)
				estados.Add(e);
			foreach (Transicion t in r1.transiciones)
				transiciones.Add(t);
			genera_identificadores();
		}

		AFN genera_miniautomata_operando(char operando)
		{
			// Genera automata simple de una sola transicion
			AFN mini = new AFN();

			Estado e1 = new Estado(); // Estado de inicio
			Estado e2 = new Estado(); // Estado de aceptacion
			Transicion t = new Transicion(); // Transicion

			// Se crea manualmente el automata de una sola transicion
			t.simbolo = operando;
			e1.tipo = "inicio";
			e2.tipo = "aceptacion";
			t.estado_origen = e1;
			t.estado_destino = e2;
			e1.transiciones_salida.Add(t);
			e2.transiciones_entrada.Add(t);

			mini.estados.Add(e1);
			mini.estados.Add(e2);
			mini.transiciones.Add(t);

			return mini;
		}

		AFN genera_miniautomata_operador_unario(AFN mini_a, char operador)
		{
			// Genera automata a partir de otro automata aplicandole un operador unario
			List<Estado> lista_aux = new List<Estado>();
			AFN mini = new AFN();
			mini = mini_a;
			Estado e1 = new Estado(); // Nuevo estado de inicio
			Estado e2 = new Estado(); // Nuevo estado de aceptacion
			Transicion t1 = new Transicion();
			Transicion t2 = new Transicion();
			Transicion t3 = new Transicion();
			Transicion t4 = new Transicion();
			

			e1.tipo = "inicio";
			e2.tipo = "aceptacion";

			switch (operador)
            {
				case '?':
					t1.simbolo = 'ε';
					t2.simbolo = 'ε';
					t3.simbolo = 'ε';

					t1.estado_origen = e1;
					e1.transiciones_salida.Add(t1);

					t3.estado_destino = e2;
					e2.transiciones_entrada.Add(t3);

					// Se busca el estado de inicio actual, se ancla al nuevo estado de inicio y se le quita que es de inicio
					// De igual forma se aplica con el estado de aceptacion actual y el nuevo
					for(int i = 0; i < mini.estados.Count; i++)
                    {
						if(mini.estados[i].tipo == "inicio")
                        {
							t1.estado_destino = mini.estados[i];
							mini.estados[i].transiciones_entrada.Add(t1);
							mini.estados[i].tipo = "normal";
						}
						if(mini.estados[i].tipo == "aceptacion")
                        {
							t3.estado_origen = mini.estados[i];
							mini.estados[i].transiciones_salida.Add(t3);
							mini.estados[i].tipo = "normal";
                        }
                    }

					// Se asigna la transicion del nuevo inicio al nuevo final
					t2.estado_origen = e1;
					t2.estado_destino = e2;
					e1.transiciones_salida.Add(t2);
					e2.transiciones_entrada.Add(t2);

					// Se respaldan los estados
					foreach (Estado e in mini.estados)
						lista_aux.Add(e);
					// Se borran losactuales
					mini.estados.Clear();
					// Se añade el primer estado
					mini.estados.Add(e1);
					// Se añaden el resto de los estados del automata
					foreach (Estado e in lista_aux)
						mini.estados.Add(e);
					// Se añade el ultimo estado
					mini.estados.Add(e2);


					mini.transiciones.Add(t1);
					mini.transiciones.Add(t2);
					mini.transiciones.Add(t3);

					break;
				case '+':
					t1.simbolo = 'ε';
					t2.simbolo = 'ε';
					t3.simbolo = 'ε';

					t1.estado_origen = e1;
					e1.transiciones_salida.Add(t1);

					t3.estado_destino = e2;
					e2.transiciones_entrada.Add(t3);

					// Asignar la transicion de "regreso"
					for (int i = 0; i < mini.estados.Count; i++)
                    {
						if (mini.estados[i].tipo == "inicio")
						{
							t2.estado_destino = mini.estados[i];
							mini.estados[i].transiciones_entrada.Add(t2);
						}
						if (mini.estados[i].tipo == "aceptacion")
						{
							t2.estado_origen = mini.estados[i];
							mini.estados[i].transiciones_salida.Add(t2);
						}
					}

					// Se busca el estado de inicio actual, se ancla al nuevo estado de inicio y se le quita que es de inicio
					// De igual forma se aplica con el estado de aceptacion actual y el nuevo
					for (int i = 0; i < mini.estados.Count; i++)
					{
						if (mini.estados[i].tipo == "inicio")
						{
							t1.estado_destino = mini.estados[i];
							mini.estados[i].transiciones_entrada.Add(t1);
							mini.estados[i].tipo = "normal";
						}
						if (mini.estados[i].tipo == "aceptacion")
						{
							t3.estado_origen = mini.estados[i];
							mini.estados[i].transiciones_salida.Add(t3);
							mini.estados[i].tipo = "normal";
						}
					}

					// Se respaldan los estados
					foreach (Estado e in mini.estados)
						lista_aux.Add(e);
					// Se borran losactuales
					mini.estados.Clear();
					// Se añade el primer estado
					mini.estados.Add(e1);
					// Se añaden el resto de los estados del automata
					foreach (Estado e in lista_aux)
						mini.estados.Add(e);
					// Se añade el ultimo estado
					mini.estados.Add(e2);

					mini.transiciones.Add(t1);
					mini.transiciones.Add(t2);
					mini.transiciones.Add(t3);

					break;
				case '*':
					t1.simbolo = 'ε'; // Transicion de inicio
					t2.simbolo = 'ε'; // Transicion de nuevo inicio a nuevo final
					t3.simbolo = 'ε'; // Transicion de regreso
					t4.simbolo = 'ε'; // Transicion de final

					t1.estado_origen = e1;
					e1.transiciones_salida.Add(t1);

					t4.estado_destino = e2;
					e2.transiciones_entrada.Add(t4);

					// Asignar la transicion de "regreso"
					for (int i = 0; i < mini.estados.Count; i++)
					{
						if (mini.estados[i].tipo == "inicio")
						{
							t3.estado_destino = mini.estados[i];
							mini.estados[i].transiciones_entrada.Add(t3);
						}
						if (mini.estados[i].tipo == "aceptacion")
						{
							t3.estado_origen = mini.estados[i];
							mini.estados[i].transiciones_salida.Add(t3);
						}
					}

					// Se asigna la transicion del nuevo inicio al nuevo final
					t2.estado_origen = e1;
					t2.estado_destino = e2;
					e1.transiciones_salida.Add(t2);
					e2.transiciones_entrada.Add(t2);

					// Se busca el estado de inicio actual, se ancla al nuevo estado de inicio y se le quita que es de inicio
					// De igual forma se aplica con el estado de aceptacion actual y el nuevo
					for (int i = 0; i < mini.estados.Count; i++)
					{
						if (mini.estados[i].tipo == "inicio")
						{
							t1.estado_destino = mini.estados[i];
							mini.estados[i].transiciones_entrada.Add(t1);
							mini.estados[i].tipo = "normal";
						}
						if (mini.estados[i].tipo == "aceptacion")
						{
							t4.estado_origen = mini.estados[i];
							mini.estados[i].transiciones_salida.Add(t4);
							mini.estados[i].tipo = "normal";
						}
					}

					// Se respaldan los estados
					foreach (Estado e in mini.estados)
						lista_aux.Add(e);
					// Se borran losactuales
					mini.estados.Clear();
					// Se añade el primer estado
					mini.estados.Add(e1);
					// Se añaden el resto de los estados del automata
					foreach (Estado e in lista_aux)
						mini.estados.Add(e);
					// Se añade el ultimo estado
					mini.estados.Add(e2);

					mini.transiciones.Add(t1);
					mini.transiciones.Add(t2);
					mini.transiciones.Add(t3);
					mini.transiciones.Add(t4);

					break;
            }

			return mini;
		}

		AFN genera_miniautomata_operador_binario(AFN mini_a1, AFN mini_a2, char operador)
		{
			// Genera automata a partir de otros 2 automatas aplicandoles un operador binario
			List<Estado> lista_aux = new List<Estado>();
			AFN mini = new AFN();
			mini = mini_a1;

			Estado e1 = new Estado(); // Nuevo estado de inicio
			Estado e2 = new Estado(); // Nuevo estado de aceptacion
			Transicion t1 = new Transicion();
			Transicion t2 = new Transicion();
			Transicion t3 = new Transicion();
			Transicion t4 = new Transicion();

			e1.tipo = "inicio";
			e2.tipo = "aceptacion";

			switch (operador)
            {
				case '&':
					// Primero saca el estado de inicio del segundo automata
					for (int i = 0; i < mini_a2.estados.Count; i++)
						if (mini_a2.estados[i].tipo == "inicio")
							e1 = mini_a2.estados[i];

					// Busca el de aceptacion del primero
					for (int i = 0; i < mini.estados.Count; i++)
                    {
						if(mini.estados[i].tipo == "aceptacion")
                        {
							// Lo encuentra y tiene que reemplazar al de inicio del segundo
							for(int j = 0; j < e1.transiciones_salida.Count; j++)
                            {
								e1.transiciones_salida[j].estado_origen = mini.estados[i];
								mini.estados[i].transiciones_salida.Add(e1.transiciones_salida[j]);
                            }
                        }
                    }

					// Solo se le deberian de añadir todos los nodos y las transiciones del automada de la derecha al de la izquierda
					// Empieza en 1 porque tiene que evitar agregar el primer estado (el que ya se le asigno al estado final de a1
					for (int i = 1; i < mini_a2.estados.Count; i++)
						mini.estados.Add(mini_a2.estados[i]);
					for (int i = 0; i < mini_a2.transiciones.Count; i++)
						mini.transiciones.Add(mini_a2.transiciones[i]);

					break;
				case '|':
					t1.simbolo = 'ε'; // Transicion de inicio a arriba
					t2.simbolo = 'ε'; // Transicion de arriba a final

					t3.simbolo = 'ε'; // Transicion de inicio a abajo
					t4.simbolo = 'ε'; // Transicion de abajo a final

					e1.tipo = "inicio";
					e2.tipo = "aceptacion";
					e1.transiciones_salida.Add(t1);
					e1.transiciones_salida.Add(t3);
					e2.transiciones_entrada.Add(t2);
					e2.transiciones_entrada.Add(t4);
					t1.estado_origen = e1;
					t3.estado_origen = e1;
					t2.estado_destino = e2;
					t4.estado_destino = e2;

					//Asigna y modifica ambos estados de inicio antiguos
					for(int i = 0; i < mini.estados.Count; i++)
                    {
						if(mini.estados[i].tipo == "inicio")
                        {
							mini.estados[i].tipo = "normal";
							t1.estado_destino = mini.estados[i];
							mini.estados[i].transiciones_entrada.Add(t1);
                        }
                    }
					for (int i = 0; i < mini_a2.estados.Count; i++)
					{
						if (mini_a2.estados[i].tipo == "inicio")
						{
							mini_a2.estados[i].tipo = "normal";
							t3.estado_destino = mini_a2.estados[i];
							mini_a2.estados[i].transiciones_entrada.Add(t3);
						}
					}

					// Asigna y modifica ambos estados de aceptacion antiguos
					for (int i = 0; i < mini.estados.Count; i++)
					{
						if (mini.estados[i].tipo == "aceptacion")
						{
							mini.estados[i].tipo = "normal";
							t2.estado_origen = mini.estados[i];
							mini.estados[i].transiciones_salida.Add(t2);
						}
					}
					for (int i = 0; i < mini_a2.estados.Count; i++)
					{
						if (mini_a2.estados[i].tipo == "aceptacion")
						{
							mini_a2.estados[i].tipo = "normal";
							t4.estado_origen = mini_a2.estados[i];
							mini_a2.estados[i].transiciones_salida.Add(t4);
						}
					}

					// Se respaldan los estados
					foreach (Estado e in mini.estados)
						lista_aux.Add(e);
					// Se borran losactuales
					mini.estados.Clear();
					// Se añade el primer estado
					mini.estados.Add(e1);
					// Se añaden el resto de los estados del automata
					foreach (Estado e in lista_aux)
						mini.estados.Add(e);
					// Se añaden los estados del otro automata
					for (int i = 0; i < mini_a2.estados.Count; i++)
						mini.estados.Add(mini_a2.estados[i]);
					for (int i = 0; i < mini_a2.transiciones.Count; i++)
						mini.transiciones.Add(mini_a2.transiciones[i]);
					// Se añade el ultimo estado
					mini.estados.Add(e2);

					mini.transiciones.Add(t1);
					mini.transiciones.Add(t2);
					mini.transiciones.Add(t3);
					mini.transiciones.Add(t4);

					

					break;
            }

			return mini;
		}

		public void genera_identificadores()
        {
			int id = 0;

			for(int i = 0; i < estados.Count; i++)
            {
				estados[i].id = id;
				id++;
            }
				
        }

		public void genera_alfabeto(string posfija)
		{
			string a = "ε"; // Es el alfabeto chiquito (solo las letras que se van a usar)

			// Recorre la posfija y extrae todos los operandos y ese es el alfabeto.
			foreach (char c in posfija)
				if (c != '?' && c != '+' && c != '*' && c != '|' && c != '&') // Si es diferente de los operadores, entonces es operando.
					a += c; // Añade operando al alfabeto

			alfabeto = a;
		}

		public void genera_matriz()
        {
			matriz = new string[estados.Count,alfabeto.Length];  
			string conjunto;
			string vacio = "Φ";
			bool band;


			for(int i = 0; i < estados.Count; i++)
            {
				for(int j = 0; j < alfabeto.Length; j++) 
                {
					band = false;
					conjunto = "{";

					// Se tiene que buscar el estado destino de las tranciciones que tengan origen en el estado actual
					// que tengan el simbolo del caracter actual del alfabeto que salgan del estado actual
					foreach (Transicion t1 in transiciones)
                    {
						if(t1.estado_origen.id == estados[i].id) // Si esa transicion sale del estado de la iteracion
                        {
							if(t1.simbolo == alfabeto[j]) // Si esa misma transicion tiene el simbolo que corresponde
                            {
								conjunto += t1.estado_destino.id.ToString() + ",";
								band = true;
                            }
                        }
                    }

                    if (band)
                    {
						// Como siempre va a temrinar con una "," se le tiene que quitar y reemplazar con "}"
						conjunto = conjunto.Remove(conjunto.Length-1); 
						conjunto += "}";

						matriz[i, j] = conjunto;
                    }
                    else
                    {
						matriz[i, j] = vacio;
                    }
					
                }
            }

			/*
			//////////////
			string asd="";
			for (int i = 0; i < estados.Count; i++)
			{
				for (int j = 0; j < alfabeto.Length; j++)
				{
					asd += matriz[i, j];
				}
				asd += "\n";
			}
			MessageBox.Show(asd);
			*/

		}
	}
}
