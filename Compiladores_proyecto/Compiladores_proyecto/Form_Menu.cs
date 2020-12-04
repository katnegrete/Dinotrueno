using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Compiladores_proyecto
{
	public partial class Form_Menu : Form
	{
		// Variables globales
		EditorTexto editor;
		AFN automata_afn = new AFN();
		AFD automata_afd = new AFD();

		public Form_Menu()
		{
			InitializeComponent();
		}

		// Se inicializan todas las variables para usar desde 0
        private void Form1_Load(object sender, EventArgs e)
        {
			editor = new EditorTexto(); // Inicializa el editor de texto (lo basico)
			editor.open = new OpenFileDialog();
			editor.save = new SaveFileDialog();

			editor.ruta_archivo = "";  // Cadena vacia preparada para usarse en abrir o crear nuevo archivo.
			editor.band_guardado = true; // En true porque no se ha modificado nada, lo que significa que no hay cambios.

			resetea_controles(); // Inicializa los controles
		}

		// LISTA DE ACCIONES DE LA PESTAÑA "ARCHIVO" -------------------------------------------------------- |
        private void Opciones_Archivo(object sender, ToolStripItemClickedEventArgs e)
        {
			switch (e.ClickedItem.AccessibleName)//ON_COMMAND_RANGE
            {
				// Crea un nuevo archivo vacio y lo abre automaticamente.
				case "nuevo":
					if (!editor.band_guardado) // Si ya hay un archivo abierto con cambios sin guardar
                    {
						// Pregunta si quiere confirmar porque si ya hay un archivo abierto, se cerrará
						DialogResult resultado = MessageBox.Show("El archivo actual tiene cambios sin guardar.\nSi creas uno nuevo, se cerrará el actual perdiendo los datos no guardados.\n\n¿Continuar?", "Advertencia", MessageBoxButtons.OKCancel);
						if(resultado == DialogResult.OK) // Si se seleccionó "OK"
                        {
							// Primero cierra el archivo
							editor.cierra_archivo();
							resetea_controles();

							// Despues crea
							editor.crea_archivo();
							activa_controles();
						}
                    }
                    else if(editor.ruta_archivo != "") // Si ya hay un archivo abierto pero sin cambios sin guardar
                    {
						// Primero cierra el archivo
						editor.cierra_archivo();
						resetea_controles();

						// Despues crea
						editor.crea_archivo();
						activa_controles();
					}
                    else // No hay nada abierto
                    {
						editor.crea_archivo();
						activa_controles();
					}
					break;
				// Abre un archivo ya existente borrando y descartando todo lo que esté en ese momento.
				case "abrir":
					if (!editor.band_guardado) // Si ya hay un archivo abierto con cambios sin guardar
					{
						// Pregunta si quiere confirmar porque si ya hay un archivo abierto, se cerrará
						DialogResult resultado = MessageBox.Show("El archivo actual tiene cambios sin guardar.\nSi abres otro, se cerrará el actual perdiendo los datos no guardados.\n\n¿Continuar?", "Advertencia", MessageBoxButtons.OKCancel);
						if (resultado == DialogResult.OK) // Si se seleccionó "OK"
						{
							// Primero cierra
							editor.cierra_archivo();
							resetea_controles();

							// Despues abre
							editor.abre_archivo();
							Text_Box.LoadFile(editor.open.FileName, RichTextBoxStreamType.PlainText); //Carga el texto del archivo en el Text_Box
							activa_controles();
						}
					}
					else if(editor.ruta_archivo != "") // Si ya hay archivo abierto pero sin cambios sin guardar.
					{
						// Primero cierra
						editor.cierra_archivo();
						resetea_controles();

						// Despues abre
						editor.abre_archivo();
						Text_Box.LoadFile(editor.open.FileName, RichTextBoxStreamType.PlainText); //Carga el texto del archivo en el Text_Box
						activa_controles();
					}
                    else // No hay nada abierto
                    {
						editor.abre_archivo();
						Text_Box.LoadFile(editor.open.FileName, RichTextBoxStreamType.PlainText); //Carga el texto del archivo en el Text_Box
						activa_controles();
					}
					break;
				// Sobreescribe el archivo actual con lo que haya en el Text_Box.
				case "guardar":
					editor.guarda_archivo(Text_Box.Text); // Le envia el texto actual para guardarlo.
					break;
				// Cierra un archivo actual, si se hicieron cambios y no se guardaron, se perderán, desactiva controles necesarios
				case "cerrar":
					if(!editor.band_guardado) // Si no se han guardado cambios
                    {
						// Pregunta si quiere confirmar porque si lo cierra no se quedarán guardados los cambios recientes
						DialogResult resultado = MessageBox.Show("El archivo actual tiene cambios sin guardar.\nSi cierras el archivo, se perderán los cambios.\n\n¿Continuar?", "Advertencia", MessageBoxButtons.OKCancel);
						if (resultado == DialogResult.OK) // Si se seleccionó "OK".
                        {
							editor.cierra_archivo();
							resetea_controles();
						}
                    }
                    else // No hay cambios sin guardar, todo bien
                    {
						editor.cierra_archivo();
						resetea_controles();
					}
					break;
			}
		}

		// -<>--<>--<>--<>--<>--<>--<>--<>--<>--<>--<>- FUNCIONES -<>--<>--<>--<>--<>--<>--<>--<>--<>--<>--<>-

		string obten_nombre_archivo_actual(string s)
		{
			string actual = "";
			string nombre_actual_revertido = ""; //Se tiene que usar porque al momento de obtener el nombre del archivo, se va escribiendo en reversa

			for (int i = s.Length - 1; i >= 0; i--) //Recorre la cadena del final al inicio
			{
				if (s[i] == '\\') //En cuanto encuentre el "\" que va antes de la carpeta, se rompe el ciclo
					break;
				else
					nombre_actual_revertido += s[i];
			}
			//Se tiene el nombre del archivo pero al reves, se tiene que invertir para tenerlo bien escrito
			for (int i = nombre_actual_revertido.Length - 1; i >= 0; i--)
				actual += nombre_actual_revertido[i]; //Ya se tiene el nombre actual del archivo bien escrito

			return actual;
		}

		public void resetea_controles()
		{
			Text_Box.Clear(); // Vacia el Text_Box
			this.Text = "Analizador Léxico-Sintáctico";

			Text_Box.Enabled = false;
			guardarToolStripMenuItem.Enabled = false;
			cerrarToolStripMenuItem.Enabled = false;

			Boton_AFN.Enabled = false;
			Boton_genera_posfija.Enabled = false;
			Text_Box_posfija.Text = "";
			Text_Box_posfija.Enabled = false;
			Text_Box_Lexema.Text = "";
			Text_Box_Lexema.Enabled = false;
			TextBox_id.Enabled = false;
			TextBox_Num.Enabled = false;
			Boton_Tokens.Enabled = false;
			label_estadosC.Text = "Estados en C:";
			Text_EstadosC.Text = "";

			// Limpia el grid
			limpia_grid_AFN();
			limpia_grid_AFD();
			limpia_grid_TOKENS();
			limpia_grid_LR0();
		}

		public void activa_controles()
		{
			// Muestra el nombre del archivo actual en la ventana nomas porque se ve chido
			this.Text = obten_nombre_archivo_actual(editor.ruta_archivo);
			editor.band_guardado = true;

			Text_Box.Enabled = true;
			guardarToolStripMenuItem.Enabled = true;
			cerrarToolStripMenuItem.Enabled = true;

			Boton_AFN.Enabled = true;
			Boton_genera_posfija.Enabled = true;
			Text_Box_posfija.Enabled = true;
			Text_Box_Lexema.Enabled = true;
			TextBox_id.Enabled = true;
			TextBox_Num.Enabled = true;
			Boton_Tokens.Enabled = true;
		}
		

		// -<>--<>--<>--<>--<>--<>--<>--<>--<>--<>--<>-  EVENTOS  -<>--<>--<>--<>--<>--<>--<>--<>--<>--<>--<>-

		private void cambios_realizados(object sender, EventArgs e)
		{
			// Si se realizó un cambio, entonces se baja la bandera.
			editor.band_guardado = false;
		}

		private void Boton_genera_posfija_Click(object sender, EventArgs e)
        {
			Expresion exp = new Expresion(); // Inicializa la expresion.
			
			exp.expresion = Text_Box.Text;
			exp.expresion = exp.expresion.Replace(" ", "");

			// Limpia el grid
			limpia_grid_AFN();
			limpia_grid_AFD();
			Text_Box_posfija.Clear(); // Resetea el textbox

			exp.expresion = exp.convierte_a_explicita(exp.expresion);
			Text_Box_posfija.Text = exp.convierte_a_posfija();
			
		}

        private void Boton_AFN_Click(object sender, EventArgs e)
        {
			string[] row;
			int cont;
			int cont2;
			if(Text_Box_posfija.Text != "") // Si ya se generó la posfija
            {
				// Limpia el grid antes de usarlo
				limpia_grid_AFN();

				// Generar AFN
				automata_afn = new AFN();
				automata_afn.genera_automata_AFN(Text_Box_posfija.Text);
				automata_afn.genera_alfabeto(Text_Box_posfija.Text);
				automata_afn.genera_matriz();

				// Hay que rellenar el grid
				// Primero se setean las columnas y las filas
				foreach (char c in automata_afn.alfabeto)
					Tabla_transiciones_AFN.Columns.Add(c.ToString(), c.ToString());

				cont = 0;
				foreach (Estado es in automata_afn.estados)
                {
					cont2 = 0;
					row = new string[automata_afn.alfabeto.Length+1];
					row[cont2] = es.id.ToString();
					cont2++;
					for(int j = 0; j < automata_afn.alfabeto.Length; j++)
                    {
						row[cont2] = automata_afn.matriz[cont, j];
						cont2++;
                    }
					cont++;
					Tabla_transiciones_AFN.Rows.Add(row);
				}
			}
        }

		private void Boton_AFD_Click(object sender, EventArgs e)
		{
			string[] row;
			int cont;
			int cont2;
			List<int> cero = new List<int>();

			if (Text_Box_posfija.Text != "" && automata_afn.estados.Count > 0) // Si ya se generó la posfija y el AFN
            {
				// Limpia el grid antes de usarlo
				limpia_grid_AFD();

				automata_afd = new AFD();
				automata_afd.automata_afn = automata_afn;
				automata_afd.genera_alfabeto(Text_Box_posfija.Text);
				cero.Add(0);
				automata_afd.genera_automata_afd(automata_afd.cerradura_epsilon(cero));
				automata_afd.genera_matriz();

				// Hay que rellenar el grid
				// Primero se setean las columnas y las filas
				foreach (char c in automata_afd.alfabeto)
					Tabla_transiciones_AFD.Columns.Add(c.ToString(), c.ToString());

				cont = 0;
				foreach (Destado es in automata_afd.Destados)
				{
					cont2 = 0;
					row = new string[automata_afd.alfabeto.Length + 1];
					row[cont2] = es.id.ToString();
					cont2++;
					for (int j = 0; j < automata_afd.alfabeto.Length; j++)
					{
						row[cont2] = automata_afd.matriz[cont, j];
						cont2++;
					}
					cont++;
					Tabla_transiciones_AFD.Rows.Add(row);
				}
			}
		}

		private void Boton_verifica_lexema_Click(object sender, EventArgs e)
		{
			if(automata_afd.Destados.Count > 0) // Si tiene al menos un destado, significa que ya se generó
            {
				if(automata_afd.verifica_lexema(automata_afd.Destados[0], Text_Box_Lexema.Text, 0))
					MessageBox.Show("El lexema SI es parte de la gramatica.");
                else
					MessageBox.Show("El lexema NO es parte de la gramatica.");
            }
            else
				MessageBox.Show("Primero se tiene que genrar el AFD de la gramatica.");
		}

		private void Boton_Tokens_Click(object sender, EventArgs e)
		{
			Lenguaje_Tiny tny = new Lenguaje_Tiny();
			string[] row = new string[2];

			if (TextBox_id.Text != "" && TextBox_Num.Text != "") // Solo si id y num tienen gramatica
            {
				if(Text_Box.Text != "") // Solo si si hay código
                {
					// Si ya habia algo escrito, lo sobrescribe
					limpia_grid_TOKENS();

					tny.setea_palabras_reservadas();
					tny.genera_afds(TextBox_id.Text, TextBox_Num.Text);

					// Le quita todos los espacios sobrantes dejando solo 1 espacio

					tny.genera_tokens(Text_Box.Text);
					tny.verifica_tokens();

					// Se tiene que rellenar el grid
					foreach (Token t in tny.tokens)
					{
						row[0] = t.nombre;
						row[1] = t.lexema;
						Tabla_Tokens.Rows.Add(row);
						row = new string[2];
					}
                }
                else
                {
					MessageBox.Show("No existe ningún código abierto.");
				}
            }
            else
            {
				MessageBox.Show("Identificar y Número tienen que contener una gramatica");
			}
		}

		private void Boton_AFD_LR_Click(object sender, EventArgs e)
		{
			List<List<Simbolo_Gramatical>> res_ir_a = new List<List<Simbolo_Gramatical>>();
			List<List<Simbolo_Gramatical>> lista_aux_aumentada = new List<List<Simbolo_Gramatical>>();
			bool band_agregado = false; // Bandera para saber si se agregó algo a C en alguna iteracion
			bool band_repetido = false; // Bandera para saber si el resultado de IR_A ya existe en C
			int id_destado = 0;
			Destado I_aux = new Destado();
			Destado I_origen = new Destado();
			List<Transicion> transiciones = new List<Transicion>(); // Lista de transiciones para el afd
			LR0 lr0 = new LR0();
			lr0.set_gramatica(); // Setea toda la gramatica del lenguaje tiny

			// Crea el primer estado, le aplica cerradura y lo añade al conjunto C
			I_aux.id = "I" + id_destado.ToString();
			id_destado++;
			lista_aux_aumentada.Add(lr0.G.producciones[0]); // Unicamente es para poder llamar a la funcion de cerradura
			I_aux.conjunto_lr0 = lr0.cerradura(lista_aux_aumentada);
			lr0.c.Add(I_aux);
			
			band_agregado = false;

			for(int i = 0; i < lr0.c.Count; i++)
            {
				foreach(Simbolo_Gramatical x in lr0.G.noterminales)
                {
					band_repetido = false;
					res_ir_a = lr0.ir_a(lr0.c[i], x);

					if (res_ir_a != null) // Si no está vacío
                    {
						// Busca si ya existe en C
						foreach(Destado estado in lr0.c)
							if (compara_ListaProducciones(res_ir_a, estado.conjunto_lr0)) // Si está repetido
								band_repetido = true;

						if (!band_repetido) // Si no se repite, entonces lo agrega
						{
							I_aux = new Destado();
							I_aux.id = "I" + id_destado.ToString();
							id_destado++;
							I_aux.conjunto_lr0 = res_ir_a;
							lr0.c.Add(I_aux);
							band_agregado = true;

							// Va y busca cual Destado es el que tiene los elementos ´que se están recorriendo
							I_origen = new Destado();
							foreach(Destado dst in lr0.c)
								if(compara_ListaProducciones(dst.conjunto_lr0, lr0.c[i].conjunto_lr0))
									I_origen = dst;

							Transicion tr = new Transicion();
							tr.simbolo_lr0 = x.simbolo;
							tr.destado_origen = I_origen;
							tr.destado_destino = I_aux;
							transiciones.Add(tr);
						}
                    }
                }
				foreach(Simbolo_Gramatical x in lr0.G.terminales)
				{
					band_repetido = false;
					res_ir_a = lr0.ir_a(lr0.c[i], x);

					if (res_ir_a != null) // Si no está vacío
					{
						// Busca si ya existe en C
						foreach (Destado estado in lr0.c)
							if (compara_ListaProducciones(res_ir_a, estado.conjunto_lr0)) // Si está repetido
								band_repetido = true;

						if (!band_repetido) // Si no se repite, entonces lo agrega
						{
							I_aux = new Destado();
							I_aux.id = "I" + id_destado.ToString();
							id_destado++;
							I_aux.conjunto_lr0 = res_ir_a;
							lr0.c.Add(I_aux);
							band_agregado = true;

							// Va y busca cual Destado es el que tiene los elementos ´que se están recorriendo
							I_origen = new Destado();
							foreach (Destado dst in lr0.c)
								if (compara_ListaProducciones(dst.conjunto_lr0, lr0.c[i].conjunto_lr0))
									I_origen = dst;

							Transicion tr = new Transicion();
							tr.simbolo_lr0 = x.simbolo;
							tr.destado_origen = I_origen;
							tr.destado_destino = I_aux;
							transiciones.Add(tr);
						}
					}
				}
			}

			// Imprimir c final
			label_estadosC.Text += "  (" + lr0.c.Count.ToString() + ")";
			string sc_prod = "";
			string sc_estado = "";
			string sc_todo = "";
			foreach (Destado estado in lr0.c)
			{
				sc_estado = estado.id += "\n";
				foreach (List<Simbolo_Gramatical> prod in estado.conjunto_lr0)
                {
					sc_prod = prod[0].simbolo + " -> ";
					for(int i = 1; i < prod.Count; i++)
						sc_prod += prod[i].simbolo + " ";
					sc_estado += sc_prod + "\n";
				}
				sc_todo += sc_estado += "\n\n";
			}
			Text_EstadosC.Text = sc_todo;

			// Llenar el grid de las transiciones de los Destados
			// Primero se setean las columnas y las filas
			int cont = 0, cont2 = 0;
			string[] row;
			string[,] matriz;
			List<string> alfabeto = new List<string>();
			foreach (Simbolo_Gramatical x in lr0.G.noterminales)
            {
				tabla_transiciones_LR.Columns.Add(x.simbolo, x.simbolo);
				alfabeto.Add(x.simbolo);
			}
				
			foreach (Simbolo_Gramatical x in lr0.G.terminales)
            {
				tabla_transiciones_LR.Columns.Add(x.simbolo, x.simbolo);
				alfabeto.Add(x.simbolo);
			}


			matriz = genera_matriz_lr0(lr0.c, alfabeto, transiciones);

			cont = 0;
			foreach (Destado es in lr0.c)
			{
				cont2 = 0;
				row = new string[alfabeto.Count + 1];
				row[cont2] = es.id.ToString();
				cont2++;
				for (int j = 0; j < alfabeto.Count; j++)
				{
					row[cont2] = matriz[cont, j];
					cont2++;
				}
				cont++;
				tabla_transiciones_LR.Rows.Add(row);
			}
		}

		public string[,] genera_matriz_lr0(List<Destado> c, List<string> alfabeto, List<Transicion> transiciones)
		{
			string[,] matriz;
			matriz = new string[c.Count, alfabeto.Count];
			string vacio = "";

			for (int i = 0; i < c.Count; i++)
			{
				for (int j = 0; j < alfabeto.Count; j++)
				{
					matriz[i, j] = vacio;
					foreach (Transicion t in transiciones)
					{
						if (t.destado_origen.id == c[i].id && t.simbolo_lr0 == alfabeto[j])
							matriz[i, j] = t.destado_destino.id;
					}
				}
			}
			return matriz;
		}

		bool compara_ListaProducciones(List<List<Simbolo_Gramatical>> a, List<List<Simbolo_Gramatical>> b)
        {
			bool iguales = true;
			List<string> a_s = new List<string>();
			string b_s = "";
			int contador = 0; // Contador con el que se va a checar que los elementos de a sean iguales a los de b

			// Si nisiquiera tienen el mismo tamaño, pues desde ahi no son ifuales
			if (a.Count != b.Count)
				iguales = false;

			// Si al menos el tamaño es el mismo, entonecs PUEDEN ser iguales
            if (iguales)
            {
				// Para evitar checar lista de listas, porque es demasiado por el ausnto de que pueden estár en desorden, 
				// se "castean" las listas de listas de simbolos gramaticales, a lista de strings para hacer directo el chequeo
				// a se hace una lista de strings y b se hace una string completa
				a_s = transoforma_a_strings(a);
				b_s = transoforma_a_string(b);

				// Se checa string por string de a, que exista en b, si si existe, se incremente el contador y
				// si al final el contador es igual al count de a, significa que todas y cada una de las strings
				// de a se encontraron en b, lo que significa que a y b son iguales.
				foreach(string s in a_s)
					if (b_s.Contains(s))
						contador++;

				// Significa que no se encontraron todas las producciones de a en b, por lo tanto, no son iguales
				if (contador != a_s.Count)
					iguales = false;
			}

			return iguales;
        }

		public List<string> transoforma_a_strings(List<List<Simbolo_Gramatical>> a)
        {
			List<string> l = new List<string>();
			string produccion = "";

			// Recorre una produccion de la lista de producciones
			foreach(List<Simbolo_Gramatical> prod in a)
            {
				produccion = "";
				// Recorre todos los elementos de esa produccion y los concatena
				foreach(Simbolo_Gramatical sim in prod)
					produccion += sim.simbolo;

				l.Add(produccion);
            }

			return l;
		}

		public string transoforma_a_string(List<List<Simbolo_Gramatical>> a)
		{
			string produccion = "";

			// Recorre una produccion de la lista de producciones
			foreach (List<Simbolo_Gramatical> prod in a)
			{
				// Recorre todos los elementos de esa produccion y los concatena
				foreach (Simbolo_Gramatical sim in prod)
					produccion += sim.simbolo;
			}

			return produccion;
		}

		public void limpia_grid_AFN()
        {
			Tabla_transiciones_AFN.Columns.Clear();
			Tabla_transiciones_AFN.Rows.Clear();

			Tabla_transiciones_AFN.Columns.Add("", "");
		}

		public void limpia_grid_AFD()
		{
			Tabla_transiciones_AFD.Columns.Clear();
			Tabla_transiciones_AFD.Rows.Clear();

			Tabla_transiciones_AFD.Columns.Add("", "");
		}

		public void limpia_grid_TOKENS()
        {
			Tabla_Tokens.Columns.Clear();
			Tabla_Tokens.Rows.Clear();

			Tabla_Tokens.Columns.Add("Nombre", "Nombre");
			Tabla_Tokens.Columns.Add("Lexema", "Lexema");
		}

		public void limpia_grid_LR0()
		{
			tabla_transiciones_LR.Columns.Clear();
			tabla_transiciones_LR.Rows.Clear();

			tabla_transiciones_LR.Columns.Add("", "");
		}
	}
}
