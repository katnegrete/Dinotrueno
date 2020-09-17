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
    public class EditorTexto
    {
        //Variables globales de la clase
        public string ruta_archivo; //Ruta del archivo actual
        public bool band_guardado; //Bandera para determinar si los datos están guardados al cerrar el archivo
		public OpenFileDialog open;
		public SaveFileDialog save;

		public void crea_archivo()
		{
			FileStream fs;
			// Abre la ventana para crear un archivo.
			save = new SaveFileDialog();
			save.Filter = "Text Files | *.txt"; //Especifica que solo se buscan archivos de texto
			if (save.ShowDialog() == DialogResult.OK) //Si ya se presionó "OK"
			{
				ruta_archivo = save.FileName; //Setea el string de la ruta
				fs = File.Create(save.FileName); //Crea el archivo
				fs.Close();
			}
		}

		public void abre_archivo()
		{
			// Abre la ventana para abrir un archivo.
			open = new OpenFileDialog();
			open.Filter = "Text Files | *.txt"; //Especifica que solo se buscan archivos de texto
			if (open.ShowDialog() == DialogResult.OK) //Si ya se presionó "OK"
			{
				ruta_archivo = open.FileName; //Setea el string de la ruta
				band_guardado = true;
			}
		}

		public void guarda_archivo(string texto_a_guardar)
		{
			File.WriteAllText(ruta_archivo, texto_a_guardar);
			band_guardado = true;
		}

		public void cierra_archivo()
		{
			ruta_archivo = ""; // Resetea la ruta del archivo
			band_guardado = true; // Resetea la bandera
		}
	}
}
