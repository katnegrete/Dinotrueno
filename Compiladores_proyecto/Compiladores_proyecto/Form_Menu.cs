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
		string[,] ACCION;			// Matriz de accion del analisis completo
		string[,] IRA;              // Matriz de ir_a del analisis completo
		LR0 lr0;

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
			Text_Box_Console_Analisis.Text = "";
			

			// Limpia el grid
			limpia_grid_AFN();
			limpia_grid_AFD();
			limpia_grid_TOKENS();
			limpia_grid_LR0();
			limpia_grid_TablaAnalisis_LR0();
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
			analisis_sintactico();
		}

		public void analisis_sintactico()
        {
			List<List<Simbolo_Gramatical>> res_ir_a = new List<List<Simbolo_Gramatical>>();
			List<List<Simbolo_Gramatical>> lista_aux_aumentada = new List<List<Simbolo_Gramatical>>();
			bool band_repetido = false; // Bandera para saber si el resultado de IR_A ya existe en C
			int id_destado = 0;
			Destado I_aux = new Destado();
			Destado I_origen = new Destado();
			List<Transicion> transiciones = new List<Transicion>(); // Lista de transiciones para el afd
			lr0 = new LR0();
			lr0.set_gramatica(); // Setea toda la gramatica del lenguaje tiny

			// Crea el primer estado, le aplica cerradura y lo añade al conjunto C
			I_aux.id = "I" + id_destado.ToString();
			id_destado++;
			lista_aux_aumentada.Add(lr0.G.producciones[0]); // Unicamente es para poder llamar a la funcion de cerradura
			I_aux.conjunto_lr0 = lr0.cerradura(lista_aux_aumentada);
			lr0.c.Add(I_aux);


			// Recorre la lista de estados (c)
			for (int i = 0; i < lr0.c.Count; i++)
			{
				foreach (Simbolo_Gramatical x in lr0.G.terminales)
				{
					band_repetido = false;
					res_ir_a = lr0.ir_a(lr0.c[i], x);

					if (res_ir_a != null) // Si no está vacío
					{
						// Busca si ya existe en C
						foreach (Destado estado in lr0.c)
							if (lr0.compara_ListaProducciones(res_ir_a, estado.conjunto_lr0))
							{ // Si está repetido
								band_repetido = true;
								I_aux = estado;
							}

						if (!band_repetido) // Si no se repite, entonces lo agrega
						{
							I_aux = new Destado();
							I_aux.id = "I" + id_destado.ToString();
							id_destado++;
							I_aux.conjunto_lr0 = res_ir_a;
							lr0.c.Add(I_aux);
						}

						// Va y busca cual Destado es el que tiene los elementos ´que se están recorriendo
						I_origen = new Destado();
						foreach (Destado dst in lr0.c)
							if (lr0.compara_ListaProducciones(dst.conjunto_lr0, lr0.c[i].conjunto_lr0))
								I_origen = dst;

						// Agrega la transicion, con el origen del que se está trabajando y el destino del que se repitió o se creó nuevo
						Transicion tr = new Transicion();
						tr.simbolo_lr0 = x.simbolo;
						tr.destado_origen = I_origen;
						tr.destado_destino = I_aux;
						transiciones.Add(tr);
					}
				}
				foreach (Simbolo_Gramatical x in lr0.G.noterminales)
				{
					band_repetido = false;
					res_ir_a = lr0.ir_a(lr0.c[i], x);

					if (res_ir_a != null) // Si no está vacío
					{
						// Busca si ya existe en C
						foreach (Destado estado in lr0.c)
							if (lr0.compara_ListaProducciones(res_ir_a, estado.conjunto_lr0))
							{ // Si está repetido
								band_repetido = true;
								I_aux = estado;
							}

						if (!band_repetido) // Si no se repite, entonces lo agrega
						{
							I_aux = new Destado();
							I_aux.id = "I" + id_destado.ToString();
							id_destado++;
							I_aux.conjunto_lr0 = res_ir_a;
							lr0.c.Add(I_aux);
						}

						// Va y busca cual Destado es el que tiene los elementos ´que se están recorriendo
						I_origen = new Destado();
						foreach (Destado dst in lr0.c)
							if (lr0.compara_ListaProducciones(dst.conjunto_lr0, lr0.c[i].conjunto_lr0))
								I_origen = dst;

						// Agrega la transicion, con el origen del que se está trabajando y el destino del que se repitió o se creó nuevo
						Transicion tr = new Transicion();
						tr.simbolo_lr0 = x.simbolo;
						tr.destado_origen = I_origen;
						tr.destado_destino = I_aux;
						transiciones.Add(tr);
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
					for (int i = 1; i < prod.Count; i++)
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


			// Genera la tabla de analisis
			limpia_grid_TablaAnalisis_LR0();
			genera_tabla_analisis_lr0(lr0, transiciones);
		}

		public void genera_tabla_analisis_lr0(LR0 lr, List<Transicion> transiciones)
        {
			// Se tienen que llenar por separado la tabla de ACCION y de ir_A
			string[,] matriz_accion = new string[lr.c.Count, lr.G.terminales.Count+1]; // El +1 es por el $
			string[,] matriz_ira = new string[lr.c.Count, lr.G.noterminales.Count];
			List<List<string>> recibidos_regla2 = new List<List<string>>();
			string aux = "";

			foreach (Simbolo_Gramatical sim in lr.G.terminales) // Setea las columnas para ACCION
				Tabla_Analisis_LR0_ACCION.Columns.Add(sim.simbolo, sim.simbolo);
			Tabla_Analisis_LR0_ACCION.Columns.Add("$", "$");
			foreach (Simbolo_Gramatical sim in lr.G.noterminales) // Setea las columnas para ir_A
				Tabla_Analisis_LR0_IRA.Columns.Add(sim.simbolo, sim.simbolo);

			// Llena matriz ACCION
			for(int i = 0; i < lr.c.Count; i++) // Recorre estado por estado
            {
				foreach (List<Simbolo_Gramatical> elemento in lr.c[i].conjunto_lr0) // Tiene que recorrer cada elemento {[],[],...}
				{
					// Checa la relga 1
					for (int j = 0; j < lr.G.terminales.Count; j++) // Tiene que recorrer cada terminal
					{ 
						if(matriz_accion[i, j] == null || matriz_accion[i, j] == "")
                        {
							aux = lr.analisis_accion_relga1(elemento, lr.c[i], lr.G.terminales[j], transiciones);
							if (aux != "")
								matriz_accion[i, j] = aux;
						}
					}

					// Checa la regla 2
					recibidos_regla2 = lr.analisis_accion_relga2(elemento); // Recibe todos los r
					if (recibidos_regla2.Count > 0) // Si si le llegó algo
					{ 
						for(int k = 0; k < recibidos_regla2.Count; k++)
                        {
							int inde = obten_indice_terminal(recibidos_regla2[k][0], lr.G.terminales);
							string simbol = recibidos_regla2[k][0];
							string simbol2 = recibidos_regla2[k][1];
							matriz_accion[i, obten_indice_terminal(recibidos_regla2[k][0], lr.G.terminales)] = recibidos_regla2[k][1];
						}
					}

					// Checa la regla 3
					if(matriz_accion[i, lr.G.terminales.Count] == null || matriz_accion[i, lr.G.terminales.Count] == "")
                    {
						aux = lr.analisis_accion_relga3(elemento);
						if (aux != "")
							matriz_accion[i, lr.G.terminales.Count] = aux;
					}
						
					// se usa directamente "lr.G.terminales.Count" porque ese es el indice en el que se encuentra $ al momento de crear la matriz
				}
			}

			// Llena matriz I_RA
			for(int i = 0; i < lr.c.Count; i++) // Recorre los estados
            {
				for(int j = 0; j < lr.G.noterminales.Count; j++) // Recorre los no terminales
                {
					matriz_ira[i, j] = ""; // La setea por si no encuentra nada
					foreach(Transicion t in transiciones) // Recorre las transiciones para llenar matriz con el destado destino
                    {
						// Si el estado origen y el simbolo embonan con el estado de c y el no terminal, entonces en esa coordenada de la matriz va el estado destino
						if(t.destado_origen == lr.c[i] && t.simbolo_lr0 == lr.G.noterminales[j].simbolo)
							matriz_ira[i, j] = t.destado_destino.id.Trim('I'); // Le quita el caracter I para solo dejar el numero
                    }
                }
			}

			ACCION = new string[lr.c.Count, lr.G.terminales.Count + 1]; // El +1 es por el $
			IRA = new string[lr.c.Count, lr.G.noterminales.Count];
			ACCION = matriz_accion;
			IRA = matriz_ira;


			int cont = 0, cont2 = 0;
			string[] row;
			List<string> alfabeto = new List<string>();

			// Llena datagrid con la matriz ACCION
			cont = 0;
			cont2 = 0;
			alfabeto = new List<string>();
			foreach (Simbolo_Gramatical x in lr.G.terminales)
				alfabeto.Add(x.simbolo);
			alfabeto.Add("$");
			cont = 0;
			foreach (Destado es in lr.c)
			{
				cont2 = 0;
				row = new string[alfabeto.Count + 1];
				row[cont2] = es.id.ToString();
				cont2++;
				for (int j = 0; j < alfabeto.Count; j++)
				{
					row[cont2] = matriz_accion[cont, j];
					cont2++;
				}
				cont++;
				Tabla_Analisis_LR0_ACCION.Rows.Add(row);
			}

			// Llena datagrid con la matriz IR_A
			cont = 0;
			cont2 = 0;
			alfabeto = new List<string>();
			foreach (Simbolo_Gramatical x in lr.G.noterminales)
				alfabeto.Add(x.simbolo);
			cont = 0;
			foreach (Destado es in lr.c)
			{
				cont2 = 0;
				row = new string[alfabeto.Count + 1];
				row[cont2] = es.id.ToString();
				cont2++;
				for (int j = 0; j < alfabeto.Count; j++)
				{
					row[cont2] = matriz_ira[cont, j];
					cont2++;
				}
				cont++;
				Tabla_Analisis_LR0_IRA.Rows.Add(row);
			}
		}

		public int obten_indice_terminal(string terminal, List<Simbolo_Gramatical> terminales)
        {
			int indice = -1;

			if(terminal == "$")
            {
				// Si es $, no lo va a encontrar en los terminales
				indice = terminales.Count;
            }
            else
            {
				// Recorre la lista de terminales para encontrar el indice de un terminal indicado
				foreach (Simbolo_Gramatical sim in terminales)
					if (sim.simbolo == terminal)
						indice = terminales.IndexOf(sim);
			}

			return indice;
        }

		public string[,] genera_matriz_lr0(List<Destado> c, List<string> alfabeto, List<Transicion> transiciones)
		{
			string[,] matriz;
			matriz = new string[c.Count, alfabeto.Count];
			string vacio = "Ø";

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

		private void Boton_Analizador_TINY_Click(object sender, EventArgs e)
		{
			Text_Box_Console_Analisis.Text = "";
			limpia_grid_TOKENS();
			limpia_grid_LR0();
			limpia_grid_TablaAnalisis_LR0();
			Arbol_sintactico.Nodes.Clear();

			Lenguaje_Tiny tny = new Lenguaje_Tiny();
			string[] row = new string[2];
			bool band_continua = true;

			if(Text_Box.Text != "")
            {
				if(TextBox_id.Text != "" && TextBox_Num.Text != "")
                {
					// Setea los datos necesarios para crear y verificar los tokens del codigo
					tny.setea_palabras_reservadas();
					tny.genera_afds(TextBox_id.Text, TextBox_Num.Text);
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

					// Verifica que hubo errores lexicos
					foreach (Token t in tny.tokens)
					{
						if (t.nombre == "Error léxico") // Si hay al menos un token que sea un error, entones ya no va a continuar
						{
							band_continua = false;
							// Se imprime en la "consola"
							Text_Box_Console_Analisis.Text += "Error léxico - Linea: " + t.linea.ToString() + ", " + t.lexema + " no se reconoce\n";
						}
					}

					if (band_continua) // Solo continua si no se encontraron errores lexicos
					{
						// Si llegó a este punto sin errores, tambien se imprime en "consola"
						Text_Box_Console_Analisis.Text += "No se encontraron errores léxicos\n";

						// Se hace el analisis sintactico
						analisis_sintactico();
						analisis_lr0(tny.tokens);
					}
				}
                else
                {
					MessageBox.Show("Los campos de Numero e Identificador no pueden estar vacios.");
				}
			}
            else
				MessageBox.Show("Por favor, primero abre un código TINY");
		}

		public void analisis_lr0(List<Token> tokens)
        {
			List<Token> w = tokens; // w
			int w_index = 0; // Indice de w
			int tree_index = -1; // Indice del arbol
			int s = -1;
			string a = "";
			int t = 0;
			string celda = "";
			int abs_prod = 0;
			int num_prod = 0;
			TreeNode trn = new TreeNode();
			TreeNode trn2 = new TreeNode();

			// Crea la base del arbol
			Arbol_sintactico.Nodes.Clear();
			foreach (Token tk in w)
				Arbol_sintactico.Nodes.Add(tk.nombre);

			// Se le añade $ a w
			Token tok = new Token();
			tok.nombre = "$";
			w.Add(tok);

			// Crea e inicializa la pila
			Stack<int> pila = new Stack<int>();
			pila.Push(0);

            while (true)
            {
                a = w[w_index].nombre;
                s = pila.Peek();
                celda = busca_celda_matriz(ACCION, s, a, "ACCION");

                if (celda != "" && celda != null) // Si no regresó un error
                {
                    switch (celda[0]) // Busca solamente el primer caracter del resultado (d, r, a)
                    {
                        case 'd':
                            t = Int32.Parse(celda.Trim('d'));
                            pila.Push(t); // Inserta a la pila el puro numero, sin la d
                            w_index++; // Avanzar el apuntador de w

							tree_index++; // Se desplaza el arbol a la derecha
                            break;
                        case 'r':
                            // Tiene que ir a buscar la produccion por el numero.
                            num_prod = Int32.Parse(celda.Trim('r')); // saca el numero de la produccion  A -> a
                            abs_prod = 0;
							trn = new TreeNode(lr0.G.producciones[num_prod][0].simbolo); // Se crea el nodo padre que va a contener a los hijos de la reduccion

							// De la lista de producciones la [0] siempre va a ser la aumentada, asi que no hay problema por buscarla directamente con producciones[]
							for (int i = 1; i < lr0.G.producciones[num_prod].Count; i++) // Empieza en 1 porque 0 es A (A -> alskdjASD)
                                abs_prod++; // Suma el absoluto del cuerpo de la produccion

							// Recorre el indice para insertar en orden los hijos
							tree_index -= (abs_prod -1); // -1 para que no se reste demás puesto que son direcciones de 0-n
							for (int c = 0; c < abs_prod; c++) // Saca ese numero de elementos de la pila
                            {
								trn2 = (TreeNode) Arbol_sintactico.Nodes[tree_index].Clone();
								trn.Nodes.Add(trn2); // Le pone los hijos al nodo padre
                                pila.Pop();
								Arbol_sintactico.Nodes.RemoveAt(tree_index);
                            }

                            t = pila.Peek(); // Setea t al tope de la pila
                            // Se tiene que insertar el resultado de IR_A[t,A]
                            pila.Push(Int32.Parse(busca_celda_matriz(IRA, t, lr0.G.producciones[num_prod][0].simbolo, "IR_A")));
							Arbol_sintactico.Nodes.Insert(tree_index, trn);

							break;
                        case 'a':
                            Text_Box_Console_Analisis.Text += "Todo correcto.";
							Arbol_sintactico.ExpandAll();
							return;
                    }
                }
                else // Regreso un error, detener todo
                {
                    Text_Box_Console_Analisis.Text += "Error sintáctico - No se puede continuar con el análisis.";
					Arbol_sintactico.ExpandAll();
					return;
                }
            }
        }

		public string busca_celda_matriz(string[,] m, int s, string a, string tbl)
        {
			// tbl sirve para ver que se va a usar, si los terminales de la tabla ACCION o los no terminales de la tabla IR_a
			string celda = "";
			List<Simbolo_Gramatical> aux = new List<Simbolo_Gramatical>();

			if(tbl == "ACCION") // Se obtienen los terminales de la tabla de accion + el $ que no lo tiene por default
			{
				Simbolo_Gramatical pesos = new Simbolo_Gramatical("$", "$");
				aux = lr0.G.terminales;
				aux.Add(pesos);
			}
			else{
				if (tbl == "IR_A") // Se obtienen los no temrinales de la tabla de ir_a
					aux = lr0.G.noterminales;
            }

			// Solamente se recorren las columnas porque para el indice del renglon es directamente s
			// Las columnas son la lista de terminales o no terminales
			for (int t = 0; t < aux.Count; t++)
            {
				if(aux[t].simbolo == a) // Encuentra a
                {
					celda = m[s, t];
					break;
                }
            }

			return celda;
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

		public void limpia_grid_TablaAnalisis_LR0()
        {
			Tabla_Analisis_LR0_ACCION.Columns.Clear();
			Tabla_Analisis_LR0_ACCION.Rows.Clear();
			Tabla_Analisis_LR0_IRA.Columns.Clear();
			Tabla_Analisis_LR0_IRA.Rows.Clear();

			Tabla_Analisis_LR0_ACCION.Columns.Add("Edo", "Edo");
			Tabla_Analisis_LR0_IRA.Columns.Add("Edo", "Edo");
		}
    }
}
