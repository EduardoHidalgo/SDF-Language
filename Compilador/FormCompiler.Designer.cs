namespace Compilador
{
    partial class FormCompiler
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
            this.ButtonLoad = new System.Windows.Forms.Button();
            this.ButtonRun = new System.Windows.Forms.Button();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.RichTextboxDebug = new System.Windows.Forms.RichTextBox();
            this.RichTextboxCompiler = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ButtonLoad
            // 
            this.ButtonLoad.Location = new System.Drawing.Point(859, 12);
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(109, 23);
            this.ButtonLoad.TabIndex = 2;
            this.ButtonLoad.Text = "Cargar Código";
            this.ButtonLoad.UseVisualStyleBackColor = true;
            this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // ButtonRun
            // 
            this.ButtonRun.Location = new System.Drawing.Point(859, 41);
            this.ButtonRun.Name = "ButtonRun";
            this.ButtonRun.Size = new System.Drawing.Size(109, 23);
            this.ButtonRun.TabIndex = 3;
            this.ButtonRun.Text = "Iniciar Compilador";
            this.ButtonRun.UseVisualStyleBackColor = true;
            this.ButtonRun.Click += new System.EventHandler(this.ButtonRun_Click);
            // 
            // ButtonClear
            // 
            this.ButtonClear.Location = new System.Drawing.Point(859, 70);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(109, 23);
            this.ButtonClear.TabIndex = 4;
            this.ButtonClear.Text = "Limpiar";
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // RichTextboxDebug
            // 
            this.RichTextboxDebug.BackColor = System.Drawing.Color.SeaShell;
            this.RichTextboxDebug.Location = new System.Drawing.Point(12, 415);
            this.RichTextboxDebug.Name = "RichTextboxDebug";
            this.RichTextboxDebug.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RichTextboxDebug.Size = new System.Drawing.Size(841, 141);
            this.RichTextboxDebug.TabIndex = 5;
            this.RichTextboxDebug.Text = "";
            // 
            // RichTextboxCompiler
            // 
            this.RichTextboxCompiler.BackColor = System.Drawing.Color.LightGray;
            this.RichTextboxCompiler.Location = new System.Drawing.Point(12, 12);
            this.RichTextboxCompiler.Name = "RichTextboxCompiler";
            this.RichTextboxCompiler.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RichTextboxCompiler.Size = new System.Drawing.Size(841, 397);
            this.RichTextboxCompiler.TabIndex = 6;
            this.RichTextboxCompiler.Text = "";
            // 
            // FormCompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(980, 568);
            this.Controls.Add(this.RichTextboxCompiler);
            this.Controls.Add(this.RichTextboxDebug);
            this.Controls.Add(this.ButtonClear);
            this.Controls.Add(this.ButtonRun);
            this.Controls.Add(this.ButtonLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormCompiler";
            this.Text = "SDF Compiler";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ButtonLoad;
        private System.Windows.Forms.Button ButtonRun;
        private System.Windows.Forms.Button ButtonClear;
        private System.Windows.Forms.RichTextBox RichTextboxDebug;
        private System.Windows.Forms.RichTextBox RichTextboxCompiler;
    }
}

