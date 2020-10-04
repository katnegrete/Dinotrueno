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
			this.kkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.Boton_genera_posfija = new System.Windows.Forms.Button();
			this.Text_Box_posfija = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Text_Box
			// 
			this.Text_Box.BackColor = System.Drawing.Color.Moccasin;
			this.Text_Box.Location = new System.Drawing.Point(12, 27);
			this.Text_Box.Name = "Text_Box";
			this.Text_Box.Size = new System.Drawing.Size(692, 175);
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
            this.archivoToolStripMenuItem,
            this.kkToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(716, 25);
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
			this.archivoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("archivoToolStripMenuItem.Image")));
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
			this.nuevoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nuevoToolStripMenuItem.Image")));
			this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
			this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.nuevoToolStripMenuItem.Text = "Nuevo";
			// 
			// abrirToolStripMenuItem
			// 
			this.abrirToolStripMenuItem.AccessibleName = "abrir";
			this.abrirToolStripMenuItem.BackColor = System.Drawing.Color.GreenYellow;
			this.abrirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("abrirToolStripMenuItem.Image")));
			this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
			this.abrirToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.abrirToolStripMenuItem.Text = "Abrir";
			// 
			// guardarToolStripMenuItem
			// 
			this.guardarToolStripMenuItem.AccessibleName = "guardar";
			this.guardarToolStripMenuItem.BackColor = System.Drawing.Color.GreenYellow;
			this.guardarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("guardarToolStripMenuItem.Image")));
			this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
			this.guardarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.guardarToolStripMenuItem.Text = "Guardar";
			// 
			// cerrarToolStripMenuItem
			// 
			this.cerrarToolStripMenuItem.AccessibleName = "cerrar";
			this.cerrarToolStripMenuItem.BackColor = System.Drawing.Color.GreenYellow;
			this.cerrarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cerrarToolStripMenuItem.Image")));
			this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
			this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.cerrarToolStripMenuItem.Text = "Cerrar";
			// 
			// kkToolStripMenuItem
			// 
			this.kkToolStripMenuItem.Name = "kkToolStripMenuItem";
			this.kkToolStripMenuItem.Size = new System.Drawing.Size(12, 21);
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
			this.Text_Box_posfija.Location = new System.Drawing.Point(177, 226);
			this.Text_Box_posfija.Name = "Text_Box_posfija";
			this.Text_Box_posfija.Size = new System.Drawing.Size(527, 20);
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
			// Form_Menu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(210)))), ((int)(((byte)(0)))));
			this.ClientSize = new System.Drawing.Size(716, 268);
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
		private System.Windows.Forms.ToolStripMenuItem kkToolStripMenuItem;
	}
}

