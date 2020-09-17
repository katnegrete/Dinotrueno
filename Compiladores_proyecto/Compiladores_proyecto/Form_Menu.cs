using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiladores_proyecto
{
	public partial class Form_Menu : Form
	{
		// Variables globales
		EditorTexto editor;

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
                    else // No hay un archivo abierto, simplemente crea uno nuevo
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
					else // No hay un archivo abierto, simplemente abre otro
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

		private void cambios_realizados(object sender, EventArgs e)
		{
			editor.band_guardado = false;
		}

		public void resetea_controles()
        {
			Text_Box.Clear(); // Vacia el Text_Box
			this.Text = "Analizador Léxico-Sintáctico";

			Text_Box.Enabled = false;
			guardarToolStripMenuItem.Enabled = false;
			cerrarToolStripMenuItem.Enabled = false;
		}

		public void activa_controles()
        {
			// Muestra el nombre del archivo actual en la ventana nomas porque se ve chido
			this.Text = obten_nombre_archivo_actual(editor.ruta_archivo);
			editor.band_guardado = true;

			Text_Box.Enabled = true;
			guardarToolStripMenuItem.Enabled = true;
			cerrarToolStripMenuItem.Enabled = true;
		}
	}
}
