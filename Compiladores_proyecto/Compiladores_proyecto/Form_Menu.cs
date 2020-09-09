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
		string ruta_archivo; //Ruta del archivo actual
		bool band_guardado; //Bandera para determinar si los datos están guardados al cerrar el archivo

		public Form_Menu()
		{
			InitializeComponent();
		}

		// Se inicializan todas las variables para usar desde 0
        private void Form1_Load(object sender, EventArgs e)
        {
			Text_Box.Enabled = false;					// Desactivados porque 
			guardarToolStripMenuItem.Enabled = false;   // no hay ningun archivo 
			cerrarToolStripMenuItem.Enabled = false;    // abierto.
			ruta_archivo = ""; // Cadena vacia preparada para usarse en abrir o crear nuevo archivo.
			band_guardado = true; // En true porque no se ha modificado nada, lo que significa que no hay cambios.
			openFileDialog = new OpenFileDialog();
			saveFileDialog = new SaveFileDialog();
        }

		// LISTA DE ACCIONES DE LA PESTAÑA "ARCHIVO" -------------------------------------------------------- |
        private void Opciones_Archivo(object sender, ToolStripItemClickedEventArgs e)
        {
			switch (e.ClickedItem.AccessibleName)//ON_COMMAND_RANGE
            {
				// Crea un nuevo archivo vacio y lo abre automaticamente.
				case "nuevo":
					if (ruta_archivo != "") // Si ya hay un archivo abierto
                    {
						// Pregunta si quiere confirmar porque si ya hay un archivo abierto, se cerrará
						DialogResult resultado = MessageBox.Show("Un archivo está actualmente abierto.\nSi creas uno nuevo, se cerrará el actual perdiendo los datos no guardados.\n\n¿Continuar?", "Advertencia", MessageBoxButtons.OKCancel);
						if(resultado == DialogResult.OK) // Si se seleccionó "OK"
                        {
							cierra_archivo();
							crea_archivo();
						}
                    }
                    else // No hay un archivo abierto, simplemente crea uno nuevo
                    {
						crea_archivo();
					}
					break;
				// Abre un archivo ya existente borrando y descartando todo lo que esté en ese momento.
				case "abrir":
					if (ruta_archivo != "") // Si ya hay un archivo abierto
					{
						// Pregunta si quiere confirmar porque si ya hay un archivo abierto, se cerrará
						DialogResult resultado = MessageBox.Show("Un archivo está actualmente abierto.\nSi abres otro, se cerrará el actual perdiendo los datos no guardados.\n\n¿Continuar?", "Advertencia", MessageBoxButtons.OKCancel);
						if (resultado == DialogResult.OK) // Si se seleccionó "OK"
						{
							cierra_archivo();
							abre_archivo();
						}
					}
					else // No hay un archivo abierto, simplemente abre otro
					{
						abre_archivo();
					}
					break;
				// Sobreescribe el archivo actual con lo que haya en el Text_Box.
				case "guardar":
					guarda_archivo();
					break;
				// Cierra un archivo actual, si se hicieron cambios y no se guardaron, se perderán, desactiva controles necesarios
				case "cerrar":
					if(band_guardado == false) // Si no se han guardado cambios
                    {
						// Pregunta si quiere confirmar porque si lo cierra no se quedarán guardados los cambios recientes
						DialogResult resultado = MessageBox.Show("Hay cambios sin guardar.\nSi cierras el archivo, se perderán los cambios.\n\n¿Continuar?", "Advertencia", MessageBoxButtons.OKCancel);
						if (resultado == DialogResult.OK) // Si se seleccionó "OK".
                        {
							cierra_archivo();
                        }
                    }
                    else // No hay cambios sin guardar, todo bien
                    {
						cierra_archivo();
					}
					
					break;
			}
		}

		// -<>--<>--<>--<>--<>--<>--<>--<>--<>--<>--<>- FUNCIONES -<>--<>--<>--<>--<>--<>--<>--<>--<>--<>--<>-


		// Funciones de ARCHIVOS -|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|
		void crea_archivo()
        {
			// Activa controles correspondientes.
			Text_Box.Enabled = true;
			guardarToolStripMenuItem.Enabled = true;
			cerrarToolStripMenuItem.Enabled = true;


			// Abre la ventana para crear un archivo.
			saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Text Files | *.txt"; //Especifica que solo se buscan archivos de texto
			if(saveFileDialog.ShowDialog() == DialogResult.OK) //Si ya se presionó "OK"
            {
				ruta_archivo = saveFileDialog.FileName; //Setea el string de la ruta
				File.Create(saveFileDialog.FileName); //Crea el archivo
            }

			// Muestra el nombre del archivo actual en la ventana nomas porque se ve chido
			this.Text = obten_nombre_archivo_actual(ruta_archivo);
		}
		void abre_archivo() 
		{
			// Activa controles correspondientes.
			Text_Box.Enabled = true;
			guardarToolStripMenuItem.Enabled = true;
			cerrarToolStripMenuItem.Enabled = true;

			// Abre la ventana para abrir un archivo.
			openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Text Files | *.txt"; //Especifica que solo se buscan archivos de texto
			if (openFileDialog.ShowDialog() == DialogResult.OK) //Si ya se presionó "OK"
			{
				ruta_archivo = openFileDialog.FileName; //Setea el string de la ruta
				Text_Box.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText); //Carga el texto del archivo en el Text_Box
			}

			// Muestra el nombre del archivo actual en la ventana nomas porque se ve chido
			this.Text = obten_nombre_archivo_actual(ruta_archivo);
		}
		void guarda_archivo()
        {
			File.WriteAllText(ruta_archivo, Text_Box.Text);
			band_guardado = true;
		}
		void cierra_archivo()
        {
			ruta_archivo = ""; // Resetea la ruta del archivo
			band_guardado = true; // Resetea la bandera
			Text_Box.Clear(); // Vacia el Text_Box
			this.Text = "Analizador Léxico-Sintáctico";

			// Desabilita controles correspondientes.
			Text_Box.Enabled = false;
			guardarToolStripMenuItem.Enabled = false;
			cerrarToolStripMenuItem.Enabled = false;
		}
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


		
	}
}
