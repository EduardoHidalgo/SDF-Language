using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
In the Lexical Analysis phase: You identify each word/token and assign a meaning to it. 
In the code above, you start by identifying that i followed by n followed by t and then 
a space is the word int, and that it is a language keyword;1 followed by 0 and a space 
is a number 10 and so on.
*/

// debo realizar los siguientes pasos:
//      1) leer una línea
//      2) identificar cada token de la linea
//      3) asignar significado
//          3.1 revisar SOLO el primer token, pues este podría ayudar a determinar los siguientes tokens.
//          3.2 en caso de que el primer token no valide, iterar todos los tipos

namespace Compilador
{
    public class Lexical
    {
        FormCompiler FormCompiler;
        private string[] TextCompiler;

        public ListOfListTokens TokensList { get; set; }

        public Lexical(FormCompiler formCompiler, string[] textCompiler)
        {
            FormCompiler = formCompiler;
            TextCompiler = textCompiler;
            TokensList = new ListOfListTokens();
        }

        /// <summary>
        /// Ejecuta el proceso para analizar Lexicográficamente un código SDF.
        /// </summary>
        /// <returns>El booleano de retorno específica si la operación se ejecutó correctamente.</returns>
        public bool Execute()
        {
            FormCompiler.AddDebugLine("--- LEXICOGRÁFICO ---", Color.Green);

            try
            {
                DebugFunction(GenerateTokens, "Tokens Generados");
                DebugFunction(IdentifyTokens, "Tokens Identificados");

                PrintResults();
            }
            catch (Exception e)
            {
                FormCompiler.AddDebugLine("Lexical.cs, Lexical Class, Execute Method, Exception: " + e.Message, Color.Red);
                FormCompiler.AddDebugLine("Exception StackTrace: " + e.ToString(), Color.Red);
                FormCompiler.AddDebugLine("Lexicográfico ejecutado incorrectamente.", Color.Red);
                FormCompiler.AddDebugLine("--------------------------------", Color.Green);

                return false;
            }

            FormCompiler.AddDebugLine("Lexicográfico ejecutado correctamente.", Color.Green);
            FormCompiler.AddDebugLine("--------------------------------", Color.Green);

            return true;
        }

        #region LexicalFunctions

        private void GenerateTokens()
        {
            //Creación de un arreglo de tokens
            foreach (var line in TextCompiler)
                TokensList.Add(CreateTokens(line));
        }

        /// <summary>
        /// Itera cada lista de tokens (linea del archivo) para determinar los tipos de sus tokens
        /// </summary>
        private void IdentifyTokens()
        {
            //primer iteración. busca identificar la estructura general del código
            foreach (var ListTokens in TokensList)
            {
                if (!ListTokens.Empty && ListTokens.Count != 0)
                {
                    if (ListTokens.Count == 1)
                    {
                        if (ListTokens[0].Value == Rules.CorcheteAbre.r)
                        {
                            ListTokens[0].Type = typeof(Rules.CorcheteAbre);
                            ListTokens.Type = typeof(Rules.CorcheteAbre);
                        }
                        else if (ListTokens[0].Value == Rules.CorcheteCierra.r)
                        {
                            ListTokens[0].Type = typeof(Rules.CorcheteCierra);
                            ListTokens.Type = typeof(Rules.CorcheteCierra);
                        }
                        else
                        {
                            ListTokens[0].Type = typeof(Rules.Indefinido);
                        }
                    }
                    else if (ListTokens.Count > 2) //para buscar bloques
                    {
                        if (ListTokens[0].Value == "INICIO")
                        {
                            if (ListTokens[1].Value == Rules.EstructuraGeneral.Bloques.BloqueVariables.Inicio.r[1] &&
                                ListTokens[2].Value == Rules.EstructuraGeneral.Bloques.BloqueVariables.Inicio.r[2])
                            {
                                if (!TokensList.BloqueInicio1)
                                {
                                    TokensList.BloqueInicio1 = true;
                                    ListTokens.Type = typeof(Rules.EstructuraGeneral.Bloques.BloqueVariables.Inicio);
                                }
                                else
                                    TokensList.Errors.Add(new Error(TokensList.IndexOf(ListTokens), "Inicio de Variables ya existente."));
                            }
                            else if (ListTokens[1].Value == Rules.EstructuraGeneral.Bloques.BloqueFunciones.Inicio.r[1] &&
                                ListTokens[2].Value == Rules.EstructuraGeneral.Bloques.BloqueFunciones.Inicio.r[2])
                            {
                                if (!TokensList.BloqueInicio2)
                                {
                                    TokensList.BloqueInicio2 = true;
                                    ListTokens.Type = typeof(Rules.EstructuraGeneral.Bloques.BloqueFunciones.Inicio);
                                }
                                else
                                    TokensList.Errors.Add(new Error(TokensList.IndexOf(ListTokens), "Inicio de Funciones ya existente."));
                            }
                            else if (ListTokens[1].Value == Rules.EstructuraGeneral.Bloques.BloqueCuerpo.Inicio.r[1] &&
                                ListTokens[2].Value == Rules.EstructuraGeneral.Bloques.BloqueCuerpo.Inicio.r[2])
                            {
                                if (!TokensList.BloqueInicio3)
                                {
                                    TokensList.BloqueInicio3 = true;
                                    ListTokens.Type = typeof(Rules.EstructuraGeneral.Bloques.BloqueCuerpo.Inicio);
                                }
                                else
                                    TokensList.Errors.Add(new Error(TokensList.IndexOf(ListTokens), "Inicio de Cuerpo ya existente."));
                            }
                            else
                            {
                                TokensList.Errors.Add(new Error(TokensList.IndexOf(ListTokens), "Error en la sintaxis."));
                            }

                        } //Busca los inicios de bloque
                        else if (ListTokens[0].Value == "FIN")
                        {
                            if (ListTokens[1].Value == Rules.EstructuraGeneral.Bloques.BloqueVariables.Fin.r[1] &&
                                ListTokens[2].Value == Rules.EstructuraGeneral.Bloques.BloqueVariables.Fin.r[2])
                            {
                                if (!TokensList.BloqueFin1)
                                {
                                    TokensList.BloqueFin1 = true;
                                    ListTokens.Type = typeof(Rules.EstructuraGeneral.Bloques.BloqueVariables.Fin);
                                }
                                else
                                    TokensList.Errors.Add(new Error(TokensList.IndexOf(ListTokens), "Fin de Variables ya existente."));
                            }
                            else if (ListTokens[1].Value == Rules.EstructuraGeneral.Bloques.BloqueFunciones.Fin.r[1] &&
                                ListTokens[2].Value == Rules.EstructuraGeneral.Bloques.BloqueFunciones.Fin.r[2])
                            {
                                if (!TokensList.BloqueFin2)
                                {
                                    TokensList.BloqueFin2 = true;
                                    ListTokens.Type = typeof(Rules.EstructuraGeneral.Bloques.BloqueFunciones.Inicio);
                                }
                                else
                                    TokensList.Errors.Add(new Error(TokensList.IndexOf(ListTokens), "Fin de Funciones ya existente."));
                            }
                            else if (ListTokens[1].Value == Rules.EstructuraGeneral.Bloques.BloqueCuerpo.Fin.r[1] &&
                                ListTokens[2].Value == Rules.EstructuraGeneral.Bloques.BloqueCuerpo.Fin.r[2])
                            {
                                if (!TokensList.BloqueFin3)
                                {
                                    TokensList.BloqueFin3 = true;
                                    ListTokens.Type = typeof(Rules.EstructuraGeneral.Bloques.BloqueCuerpo.Fin);
                                }
                                else
                                    TokensList.Errors.Add(new Error(TokensList.IndexOf(ListTokens), "Fin de Cuerpo ya existente."));
                            }
                            else
                            {
                                TokensList.Errors.Add(new Error(TokensList.IndexOf(ListTokens), "Error en la sintaxis."));
                            }
                        } //Busca los finales de bloque
                        else
                        {
                            ListTokens[0].Type = typeof(Rules.Indefinido);
                        }

                        Token tempToken = ListTokens.Last();
                        //si el primer token es de tipo comentar
                        if (ListTokens[0].Value == Rules.ComentarioAbre.r)
                        {
                            ListTokens[0].Type = typeof(Rules.ComentarioAbre);
                            if (tempToken.Value == Rules.ComentarioCierra.r) //si el ultimo token cierra
                            {
                                tempToken.Type = typeof(Rules.ComentarioAbre);
                                ListTokens.Type = typeof(Rules.EstructuraGeneral.Comentario);
                                foreach (var token in ListTokens) //el resto de tokens conforman el comentario
                                    if (token.Type == null)
                                        token.Type = typeof(Rules.EstructuraGeneral.Comentario);
                            }
                            else
                                TokensList.Errors.Add(new Error(TokensList.IndexOf(ListTokens), "Falta cierre de comentario"));
                        }
                        else if (tempToken.Value == Rules.FinDeLinea.r)
                        {
                            ListTokens[0].Type = typeof(Rules.FinDeLinea);
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Linea);
                        } //si es una linea
                        else
                        {
                            ListTokens[0].Type = typeof(Rules.Indefinido);
                        }

                        if (ListTokens.Type == typeof(Rules.Indefinido))
                        {

                        }
                    }
                    else
                    {
                        ListTokens[0].Type = typeof(Rules.Indefinido);
                    }
                }
                else
                {
                    ListTokens.Type = typeof(Rules.Vacio);
                }
            }

            //revisa variables y estructuras
            foreach (var ListTokens in TokensList)
            {
                if (!ListTokens.Empty && ListTokens.Count != 0)
                {
                    Token tempToken = ListTokens.First();

                    //encuentra variables
                    if (ListTokens.Type == typeof(Rules.EstructuraGeneral.Linea))
                    {
                        if (tempToken.Value == Rules.EstructuraGeneral.Variables.Booleano.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Variables.Booleano);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Variables.Booleano);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Variables.CadenaCaracteres.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Variables.CadenaCaracteres);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Variables.CadenaCaracteres);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Variables.Caracter.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Variables.Caracter);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Variables.Caracter);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Variables.Decimal.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Variables.Decimal);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Variables.Decimal);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Variables.Entero.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Variables.Entero);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Variables.Entero);
                        }
                        else
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Operacion);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Operacion);
                        }
                    }
                    else
                    {
                        if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.EstructuraArray.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraArray);
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraArray);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.EstructuraElse.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraElse);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraElse);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.EstructuraFor.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraFor);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraFor);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.EstructuraFuncion.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraFuncion);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraFuncion);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.EstructuraHacer.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraHacer);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraHacer);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.EstructuraIf.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraIf);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraIf);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.EstructuraWhile.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraWhile);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.EstructuraWhile);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.Imprimir.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.Imprimir);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.Imprimir);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.OpcionSwitch.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.OpcionSwitch);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.OpcionSwitch);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.RecibirInput.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.RecibirInput);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.RecibirInput);
                        }
                        else if (tempToken.Value == Rules.EstructuraGeneral.Estructuras.Retorno.r)
                        {
                            ListTokens.Type = typeof(Rules.EstructuraGeneral.Estructuras.Retorno);
                            tempToken.Type = typeof(Rules.EstructuraGeneral.Estructuras.Retorno);
                        }
                    }
                }
            }
        }

        private void PrintResults()
        {
            //limpia el RichTextboxCompiler para resaltar el código con colores y
            //mostrar el tipo de cada linea de código
            FormCompiler.ClearRichTextboxCompiler();
            int ErrorCount = 0;
            for (int i = 0; i < TextCompiler.Length; i++)
            {
                var result = i.ToString().PadLeft(4, '0'); // contador de las lineas

                if (!TokensList[i].Empty)
                {
                    if (TokensList[i].Type != typeof(Rules.Indefinido))
                    {
                        if (TokensList.Errors.Count != 0)
                        {
                            if (TokensList.Errors[ErrorCount].LineNumber == i)
                            {
                                FormCompiler.AddCompilerLine(result + " | " + TextCompiler[i], Color.Red);
                                ErrorCount++;
                            }
                            else
                            {
                                FormCompiler.AddCompilerLineWithoutBreakLine(TextCompiler[i]);
                                FormCompiler.AddCompilerLine(" <- " + TokensList[i].Type.FullName.ToString(), Color.ForestGreen);
                            }
                        }
                        else
                        {
                            FormCompiler.AddCompilerLineWithoutBreakLine(TextCompiler[i]);
                            FormCompiler.AddCompilerLine(" <- " + TokensList[i].Type.FullName.ToString(), Color.ForestGreen);
                        }
                    }
                    else
                    {
                        FormCompiler.AddCompilerLine(TextCompiler[i]);
                    }
                }
                else
                {
                    FormCompiler.AddCompilerLineWithoutBreakLine("");
                    FormCompiler.AddCompilerLine(" <- " + TokensList[i].Type.FullName.ToString(), Color.ForestGreen);
                }
            }

            #region ValidaciónBloques

            if (TokensList.BloqueInicio1)
                FormCompiler.AddDebugLine("Bloque INICIO DE VARIABLES: " + TokensList.BloqueInicio1, Color.Green);
            else
                FormCompiler.AddDebugLine("Bloque INICIO DE VARIABLES: " + TokensList.BloqueInicio1, Color.Red);

            if (TokensList.BloqueFin1)
                FormCompiler.AddDebugLine("Bloque FIN DE VARIABLES: " + TokensList.BloqueFin1, Color.Green);
            else
                FormCompiler.AddDebugLine("Bloque FIN DE VARIABLES: " + TokensList.BloqueFin1, Color.Red);

            if (TokensList.BloqueInicio2)
                FormCompiler.AddDebugLine("Bloque INICIO DE FUNCIONES: " + TokensList.BloqueInicio2, Color.Green);
            else
                FormCompiler.AddDebugLine("Bloque INICIO DE FUNCIONES: " + TokensList.BloqueInicio2, Color.Red);

            if (TokensList.BloqueFin2)
                FormCompiler.AddDebugLine("Bloque FIN DE FUNCIONES: " + TokensList.BloqueFin2, Color.Green);
            else
                FormCompiler.AddDebugLine("Bloque FIN DE FUNCIONES: " + TokensList.BloqueFin2, Color.Red);

            if (TokensList.BloqueInicio3)
                FormCompiler.AddDebugLine("Bloque INICIO DE CUERPO: " + TokensList.BloqueInicio3, Color.Green);
            else
                FormCompiler.AddDebugLine("Bloque INICIO DE CUERPO: " + TokensList.BloqueInicio3, Color.Red);

            if (TokensList.BloqueFin3)
                FormCompiler.AddDebugLine("Bloque FIN DE CUERPO: " + TokensList.BloqueFin3, Color.Green);
            else
                FormCompiler.AddDebugLine("Bloque FIN DE CUERPO: " + TokensList.BloqueFin3, Color.Red);

            #endregion

            //imprime los posibles errores en la ventana de debug
            foreach (var error in TokensList.Errors)
            {
                var result = error.LineNumber.ToString().PadLeft(4, '0'); // contador de las lineas
                FormCompiler.AddDebugLine("Error en linea: " + result + "-> " + error.ErrorString, Color.Red);
            }
        }

        #endregion

        #region LexicalInternals

        /// <summary>
        /// Ejecuta una función privada de la clase y la debugea, para mostrar el tiempo de ejecución de esta.
        /// </summary>
        /// <param name="Method">Método que se va a debugear</param>
        /// <param name="debugMessage">Mensaje que va a arrojar al finalizar correctamente el debug</param>
        private void DebugFunction(Action Method, string debugMessage)
        {
            var Watch = System.Diagnostics.Stopwatch.StartNew();

            Method();

            Watch.Stop();
            var ElapsedTime = Watch.ElapsedMilliseconds;

            //Linea de debug 1
            FormCompiler.AddDebugLine("\t" + debugMessage + ", tiempo de ejecución: " + ElapsedTime + "ms", Color.Green);
        }

        /// <summary>
        /// Convierte una línea de texto del archivo de compilación en un arreglo de tokens
        /// </summary>
        /// <param name="line">linea de texto del archivo de compilación</param>
        private ListTokens CreateTokens(string line)
        {
            //Arreglo de Tokens
            ListTokens Tokens = new ListTokens();
            //Tokens Crudos
            string[] TokensRaw = line.Split(' ');

            if (TokensRaw != null)
            {
                //Por cada token crudo, generar un objeto Token
                foreach (var tokenRaw in TokensRaw)
                {
                    if (!string.IsNullOrEmpty(tokenRaw))
                    {
                        Tokens.Add(new Token(tokenRaw));
                    }
                }

                return Tokens;
            }
            else
            {
                Tokens.Clear();
                Tokens.Empty = true;
                return Tokens;
            }
        }

        #endregion
    }
}
