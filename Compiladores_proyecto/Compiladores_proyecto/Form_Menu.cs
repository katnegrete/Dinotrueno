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

			// Limpia el grid
			limpia_grid_AFN();
			limpia_grid_AFD();
			limpia_grid_TOKENS();
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
    }
}
