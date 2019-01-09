using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class FormCompiler : Form
    {
        private string[] ArrayLines;
        private string[] TextCompiler;
        private bool LoadFlag;

        int CompilerCount;

        public FormCompiler()
        {
            InitializeComponent();
            LoadFlag = false;
            CompilerCount = 0;
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            using (Manager Manager = new Manager())
            {
                ArrayLines = Manager.ReadFile();

                if (ArrayLines == null)
                {
                    AddDebugLine("Error en Manager.cs, Class Manager, ReadFile Function, Excepción: " +
                        "Archivo de texto ha devuelto nulo.", Color.Red);
                }
                else if(ArrayLines.Length < 3)
                {
                    AddDebugLine(ArrayLines, Color.Red);
                }
                else
                {
                    AddCompilerLine(ArrayLines);
                    TextCompiler = ArrayLines;
                    LoadFlag = true;
                    AddDebugLine("Archivo de compilación cargado correctamente.", Color.ForestGreen);
                }
            }
        }

        private void ButtonRun_Click(object sender, EventArgs e)
        {
            if (LoadFlag)
            {
                using (SDF SDFObject = new SDF(this, TextCompiler))
                    SDFObject.Execute();
            }
            else
                AddDebugLine("Error: El archivo de compilación no ha sido cargado.", Color.Red);
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            RichTextboxCompiler.Clear();
            RichTextboxDebug.Clear();
            LoadFlag = false;
            ArrayLines = null;
            TextCompiler = null;
        }

        #region AddLines
        public void AddCompilerLine(string text)
        {
            RichTextboxCompiler.AppendText(text + "\n");
        }
        public void AddCompilerLine(string[] text)
        {
            RichTextboxCompiler.Clear();

            int Count = 0;
            foreach (var item in text)
            {
                Count++;
                var result = Count.ToString().PadLeft(4, '0'); // contador de las lineas
                RichTextboxCompiler.AppendText(result + " | " + item + "\n");
            }
        }
        public void AddCompilerLine(string text, Color Color)
        {
            Color TempColor = RichTextboxCompiler.ForeColor;
            RichTextboxCompiler.SelectionColor = Color;
            RichTextboxCompiler.AppendText(text + "\n");
            RichTextboxCompiler.SelectionColor = TempColor;
        }
        public void AddCompilerLineWithoutBreakLine(string text)
        {
            RichTextboxCompiler.AppendText(text);
        }
        public void AddCompilerLineWithoutBreakLine(string text, Color Color)
        {
            Color TempColor = RichTextboxCompiler.ForeColor;
            RichTextboxCompiler.SelectionColor = Color;
            RichTextboxCompiler.AppendText(text);
            RichTextboxCompiler.SelectionColor = TempColor;
        }
        public void AddCompilerLine(string[] text, Color Color)
        {
            RichTextboxCompiler.Clear();

            int Count = 0;
            Color TempColor = RichTextboxCompiler.ForeColor;

            RichTextboxCompiler.SelectionColor = Color;
            foreach (var item in text)
            {
                Count++;
                var result = Count.ToString().PadLeft(4, '0'); // contador de las lineas
                RichTextboxCompiler.AppendText(result + " | " + item + "\n");
            }
            RichTextboxCompiler.SelectionColor = TempColor;
        }

        public void AddDebugLine(string text) => RichTextboxDebug.AppendText(text + "\n");
        public void AddDebugLine(string text, Color Color)
        {
            Color TempColor = RichTextboxDebug.ForeColor;
            RichTextboxDebug.SelectionColor = Color;
            RichTextboxDebug.AppendText(text + "\n");
            RichTextboxDebug.SelectionColor = TempColor;
        }
        public void AddDebugLine(string[] text)
        {
            foreach (var item in text)
                RichTextboxDebug.AppendText(item + "\n");
        }
        public void AddDebugLine(string[] text, Color Color)
        {
            Color TempColor = RichTextboxDebug.ForeColor;
            RichTextboxDebug.SelectionColor = Color;
            foreach (var item in text)
                RichTextboxDebug.AppendText(item + "\n");
            RichTextboxDebug.SelectionColor = TempColor;
        }

        #endregion

        public void ClearRichTextboxCompiler()
        {
            RichTextboxCompiler.Clear();
        }
    }
}
