using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace Compilador
{
    /// <summary>
    /// Permite manegar archivos de texto.
    /// </summary>
    public class Manager : IDisposable
    {
        string Path;

        public Manager()
        {

        }

        /// <summary>
        /// Inicializa una nueva isntancia de la clase <see cref="Manager"/>.
        /// </summary>
        /// <param name="path">La dirección del archivo.</param>
        public Manager(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Lee el archivo usando un OpenFileDialog.
        /// </summary>
        /// <returns>Retorna un Array de las lineas del archivo, retorna null si el método falla.</returns>
        public string[] ReadFile()
        {
            ArrayList Lines = new ArrayList();
            string[] ArrayLines;

            //Configuraciones del OpenFileDialog
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt|All Files (*.*)|*.*";
            theDialog.InitialDirectory = @"C:\";

            try
            {
                if (theDialog.ShowDialog() == DialogResult.OK)
                    if ((myStream = theDialog.OpenFile()) != null)
                    {
                        string Path = theDialog.FileName;
                        // Stream para leer el txt
                        using (var reader = new StreamReader(Path))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                                Lines.Add(line);
                        }
                    }
            }
            catch (Exception e)
            {
                Lines.Add("Error en Manager.cs, Class Manager, ReadFile Function, Excepción: " + e.ToString());
            }

            // revisión final
            if (Lines != null && Lines.Count != 0)
            {
                ArrayLines = (string[])Lines.ToArray(typeof(string));
                return ArrayLines;
            }
            else
                return null;
        }

        /// <summary>
        /// Escribe un archivo
        /// </summary>
        /// <param name="Path">La ubicación del archivo.</param>
        /// <param name="ArrayLines">Arreglo de strings.</param>
        public void WriteFile(string Path, string[] ArrayLines) => System.IO.File.WriteAllLines(Path, ArrayLines);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}
