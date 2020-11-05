namespace Compiladores_proyecto
{
	partial class Form_Menu
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Menu));
			this.Text_Box = new System.Windows.Forms.RichTextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.Boton_genera_posfija = new System.Windows.Forms.Button();
			this.Text_Box_posfija = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Boton_AFN = new System.Windows.Forms.Button();
			this.Tabla_transiciones_AFN = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Tabla_transiciones_AFD = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Boton_AFD = new System.Windows.Forms.Button();
			this.Text_Box_Lexema = new System.Windows.Forms.TextBox();
			this.Lexema = new System.Windows.Forms.Label();
			this.Boton_verifica_lexema = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tabla_transiciones_AFN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Tabla_transiciones_AFD)).BeginInit();
			this.SuspendLayout();
			// 
			// Text_Box
			// 
			this.Text_Box.BackColor = System.Drawing.Color.Moccasin;
			this.Text_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Text_Box.Location = new System.Drawing.Point(12, 27);
			this.Text_Box.Name = "Text_Box";
			this.Text_Box.Size = new System.Drawing.Size(427, 175);
			this.Text_Box.TabIndex = 0;
			this.Text_Box.Text = "";
			this.Text_Box.TextChanged += new System.EventHandler(this.cambios_realizados);
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(210)))), ((int)(((byte)(0)))));
			this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Historic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(909, 25);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// archivoToolStripMenuItem
			// 
			this.archivoToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(210)))), ((int)(((byte)(0)))));
			this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.cerrarToolStripMenuItem});
			this.archivoToolStripMenuItem.Image = global::Compiladores_proyecto.Properties.Resources.Dinosaur_icon;
			this.archivoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.GreenYellow;
			this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
			this.archivoToolStripMenuItem.Size = new System.Drawing.Size(79, 21);
			this.archivoToolStripMenuItem.Text = "Archivo";
			this.archivoToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Opciones_Archivo);
			// 
			// nuevoToolStripMenuItem
			// 
			this.nuevoToolStripMenuItem.AccessibleName = "nuevo";
			this.nuevoToolStripMenuItem.BackColor = System.Drawing.Color.GreenYellow;
			this.nuevoToolStripMenuItem.Image = global::Compiladores_proyecto.Properties.Resources.Nuevo;
			this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
			this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.nuevoToolStripMenuItem.Text = "Nuevo";
			// 
			// abrirToolStripMenuItem
			// 
			this.abrirToolStripMenuItem.AccessibleName = "abrir";
			this.abrirToolStripMenuItem.BackColor = System.Drawing.Color.GreenYellow;
			this.abrirToolStripMenuItem.Image = global::Compiladores_proyecto.Properties.Resources.Abrir;
			this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
			this.abrirToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.abrirToolStripMenuItem.Text = "Abrir";
			// 
			// guardarToolStripMenuItem
			// 
			this.guardarToolStripMenuItem.AccessibleName = "guardar";
			this.guardarToolStripMenuItem.BackColor = System.Drawing.Color.GreenYellow;
			this.guardarToolStripMenuItem.Image = global::Compiladores_proyecto.Properties.Resources.Guardar;
			this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
			this.guardarToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.guardarToolStripMenuItem.Text = "Guardar";
			// 
			// cerrarToolStripMenuItem
			// 
			this.cerrarToolStripMenuItem.AccessibleName = "cerrar";
			this.cerrarToolStripMenuItem.BackColor = System.Drawing.Color.GreenYellow;
			this.cerrarToolStripMenuItem.Image = global::Compiladores_proyecto.Properties.Resources.Cerrar;
			this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
			this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.cerrarToolStripMenuItem.Text = "Cerrar";
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.FileName = "saveFileDialog";
			// 
			// Boton_genera_posfija
			// 
			this.Boton_genera_posfija.BackColor = System.Drawing.Color.YellowGreen;
			this.Boton_genera_posfija.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.Boton_genera_posfija.Location = new System.Drawing.Point(12, 224);
			this.Boton_genera_posfija.Name = "Boton_genera_posfija";
			this.Boton_genera_posfija.Size = new System.Drawing.Size(122, 23);
			this.Boton_genera_posfija.TabIndex = 2;
			this.Boton_genera_posfija.Text = "Posfija";
			this.Boton_genera_posfija.UseVisualStyleBackColor = false;
			this.Boton_genera_posfija.Click += new System.EventHandler(this.Boton_genera_posfija_Click);
			// 
			// Text_Box_posfija
			// 
			this.Text_Box_posfija.BackColor = System.Drawing.Color.Moccasin;
			this.Text_Box_posfija.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Text_Box_posfija.Location = new System.Drawing.Point(168, 224);
			this.Text_Box_posfija.Name = "Text_Box_posfija";
			this.Text_Box_posfija.Size = new System.Drawing.Size(271, 26);
			this.Text_Box_posfija.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(146, 229);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "->";
			// 
			// Boton_AFN
			// 
			this.Boton_AFN.BackColor = System.Drawing.Color.YellowGreen;
			this.Boton_AFN.Location = new System.Drawing.Point(13, 265);
			this.Boton_AFN.Name = "Boton_AFN";
			this.Boton_AFN.Size = new System.Drawing.Size(121, 23);
			this.Boton_AFN.TabIndex = 5;
			this.Boton_AFN.Text = "Construye AFN";
			this.Boton_AFN.UseVisualStyleBackColor = false;
			this.Boton_AFN.Click += new System.EventHandler(this.Boton_AFN_Click);
			// 
			// Tabla_transiciones_AFN
			// 
			this.Tabla_transiciones_AFN.BackgroundColor = System.Drawing.Color.NavajoWhite;
			this.Tabla_transiciones_AFN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Tabla_transiciones_AFN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
			this.Tabla_transiciones_AFN.Location = new System.Drawing.Point(13, 295);
			this.Tabla_transiciones_AFN.Name = "Tabla_transiciones_AFN";
			this.Tabla_transiciones_AFN.Size = new System.Drawing.Size(426, 205);
			this.Tabla_transiciones_AFN.TabIndex = 6;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "";
			this.Column1.Name = "Column1";
			// 
			// Tabla_transiciones_AFD
			// 
			this.Tabla_transiciones_AFD.BackgroundColor = System.Drawing.Color.NavajoWhite;
			this.Tabla_transiciones_AFD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Tabla_transiciones_AFD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
			this.Tabla_transiciones_AFD.Location = new System.Drawing.Point(461, 295);
			this.Tabla_transiciones_AFD.Name = "Tabla_transiciones_AFD";
			this.Tabla_transiciones_AFD.Size = new System.Drawing.Size(426, 205);
			this.Tabla_transiciones_AFD.TabIndex = 7;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// Boton_AFD
			// 
			this.Boton_AFD.BackColor = System.Drawing.Color.YellowGreen;
			this.Boton_AFD.Location = new System.Drawing.Point(461, 265);
			this.Boton_AFD.Name = "Boton_AFD";
			this.Boton_AFD.Size = new System.Drawing.Size(121, 23);
			this.Boton_AFD.TabIndex = 8;
			this.Boton_AFD.Text = "Construye AFD";
			this.Boton_AFD.UseVisualStyleBackColor = false;
			this.Boton_AFD.Click += new System.EventHandler(this.Boton_AFD_Click);
			// 
			// Text_Box_Lexema
			// 
			this.Text_Box_Lexema.BackColor = System.Drawing.Color.Moccasin;
			this.Text_Box_Lexema.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Text_Box_Lexema.Location = new System.Drawing.Point(600, 71);
			this.Text_Box_Lexema.Name = "Text_Box_Lexema";
			this.Text_Box_Lexema.Size = new System.Drawing.Size(271, 26);
			this.Text_Box_Lexema.TabIndex = 9;
			// 
			// Lexema
			// 
			this.Lexema.AutoSize = true;
			this.Lexema.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Lexema.Location = new System.Drawing.Point(596, 48);
			this.Lexema.Name = "Lexema";
			this.Lexema.Size = new System.Drawing.Size(79, 20);
			this.Lexema.TabIndex = 10;
			this.Lexema.Text = "Lexema:";
			// 
			// Boton_verifica_lexema
			// 
			this.Boton_verifica_lexema.BackColor = System.Drawing.Color.YellowGreen;
			this.Boton_verifica_lexema.Location = new System.Drawing.Point(750, 119);
			this.Boton_verifica_lexema.Name = "Boton_verifica_lexema";
			this.Boton_verifica_lexema.Size = new System.Drawing.Size(121, 23);
			this.Boton_verifica_lexema.TabIndex = 11;
			this.Boton_verifica_lexema.Text = "Verificar";
			this.Boton_verifica_lexema.UseVisualStyleBackColor = false;
			// 
			// Form_Menu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(210)))), ((int)(((byte)(0)))));
			this.ClientSize = new System.Drawing.Size(909, 512);
			this.Controls.Add(this.Boton_verifica_lexema);
			this.Controls.Add(this.Lexema);
			this.Controls.Add(this.Text_Box_Lexema);
			this.Controls.Add(this.Boton_AFD);
			this.Controls.Add(this.Tabla_transiciones_AFD);
			this.Controls.Add(this.Tabla_transiciones_AFN);
			this.Controls.Add(this.Boton_AFN);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Text_Box_posfija);
			this.Controls.Add(this.Boton_genera_posfija);
			this.Controls.Add(this.Text_Box);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form_Menu";
			this.Text = "Analizador Léxico-Sintáctico";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tabla_transiciones_AFN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Tabla_transiciones_AFD)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.RichTextBox Text_Box;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button Boton_genera_posfija;
        private System.Windows.Forms.TextBox Text_Box_posfija;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Boton_AFN;
        private System.Windows.Forms.DataGridView Tabla_transiciones_AFN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridView Tabla_transiciones_AFD;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.Button Boton_AFD;
		private System.Windows.Forms.TextBox Text_Box_Lexema;
		private System.Windows.Forms.Label Lexema;
		private System.Windows.Forms.Button Boton_verifica_lexema;
	}
}

