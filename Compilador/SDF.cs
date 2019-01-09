using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador
{
    public class SDF : IDisposable
    {
        FormCompiler FormCompiler;
        string[] TextCompiler;

        private Lexical Lexical;
        private Syntactic Syntactic;

        public SDF(FormCompiler formCompiler, string[] textCompiler)
        {
            FormCompiler = formCompiler;
            TextCompiler = textCompiler;
        }

        /// <summary>
        /// Ejecuta el proceso para analizar un código SDF.
        /// </summary>
        /// <returns>El booleano de retorno específica si la operación se ejecutó correctamente.</returns>
        public bool Execute()
        {
            Lexical = new Lexical(FormCompiler, TextCompiler);
            bool LexicalFinished = Lexical.Execute();

            //Rompe la ejecución si falla el léxico
            if (!LexicalFinished)
                return false;



            return true;
        }

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

    public class Token
    {
        public string Value { get; set; }
        public Type Type { get; set; }

        public Token(string value)
        {
            Value = value;
            Type = null;
        }

        public Token(string value, Type type)
        {
            Value = value;
            Type = type;
        }
    }

    public class ListTokens : List<Token>
    {
        public bool Empty { get; set; }
        public Type Type { get; set; }

        public ListTokens()
        {
            Type = typeof(Rules.Indefinido);
        }
    }

    public class ListOfListTokens : List<ListTokens>
    {
        public bool BloqueInicio1 { get; set; }
        public bool BloqueInicio2 { get; set; }
        public bool BloqueInicio3 { get; set; }
        public bool BloqueFin1 { get; set; }
        public bool BloqueFin2 { get; set; }
        public bool BloqueFin3 { get; set; }

        public List<Error> Errors;

        public ListOfListTokens()
        {
            Errors = new List<Error>();
        }
    }

    public class Error
    {
        public int LineNumber { get; set; }
        public string ErrorString { get; set; }

        public Error(int lineNumber, string error)
        {
            LineNumber = lineNumber;
            ErrorString = error;
        }
    }

    public static class Rules
    {
        public static class EstructuraGeneral
        {
            public static class Linea { }
            public static class Comentario { }
            public static class BloqueComentario { }

            public static class Bloques
            {
                public static string inicio = "INICIO";
                public static string fin = "FIN";

                public static class BloqueVariables
                {
                    public static class Inicio
                    {
                        public static string[] r = new string[3]
                        {
                        "INICIO",
                        "DE",
                        "VARIABLES"
                        };
                    }
                    public static class Fin
                    {
                        public static string[] r = new string[3]
                        {
                        "FIN",
                        "DE",
                        "VARIABLES"
                        };
                    }
                }
                public static class BloqueFunciones
                {
                    public static class Inicio
                    {
                        public static string[] r = new string[3]
                        {
                        "INICIO",
                        "DE",
                        "FUNCIONES"
                        };
                    }
                    public static class Fin
                    {
                        public static string[] r = new string[3]
                        {
                        "FIN",
                        "DE",
                        "FUNCIONES"
                        };
                    }
                }
                public static class BloqueCuerpo
                {
                    public static class Inicio
                    {
                        public static string[] r = new string[3]
                        {
                        "INICIO",
                        "DE",
                        "CUERPO"
                        };
                    }
                    public static class Fin
                    {
                        public static string[] r = new string[3]
                        {
                        "FIN",
                        "DE",
                        "CUERPO"
                        };
                    }
                }
            }

            public static class Variables
            {
                public static class Booleano
                {
                    public static string r = "Bool";
                }
                public static class Caracter
                {
                    public static string r = "Carct";
                }
                public static class Decimal
                {
                    public static string r = "Dnum";
                }
                public static class Entero
                {
                    public static string r = "Num";
                }
                public static class CadenaCaracteres
                {
                    public static string r = "Plb";
                }
            }

            public static class Estructuras
            {
                public static class EstructuraIf
                {
                    public static string r = "ELEGIR";
                }
                public static class EstructuraHacer
                {
                    public static string r = "ENTONCES";
                }
                public static class EstructuraElse
                {
                    public static string r = "CONTRARIO";
                }
                public static class OpcionSwitch
                {
                    public static string r = "FN";
                }
                public static class EstructuraFor
                {
                    public static string r = "CIC";
                }
                public static class EstructuraWhile
                {
                    public static string r = "REP";
                }
                public static class EstructuraArray
                {
                    public static string r = "MTX";
                }
                public static class EstructuraFuncion
                {
                    public static string r = "PROC";
                }
                public static class Retorno
                {
                    public static string r = "RET";
                }
                public static class Imprimir
                {
                    public static string r = "PR";
                }
                public static class RecibirInput
                {
                    public static string r = "EX";
                }
            }

            public static class Operacion { }
        }

        public static class FinDeLinea
        {
            public static string r = ":<";
        }
        public static class CorcheteAbre
        {
            public static string r = "[";
        }
        public static class CorcheteCierra
        {
            public static string r = "]";
        }
        public static class ComentarioAbre
        {
            public static string r = "*";
        }
        public static class ComentarioCierra
        {
            public static string r = "*";
        }
        public static class BloqueComentarioAbre
        {
            public static string r = "*^";
        }
        public static class BloqueComentarioCierra
        {
            public static string r = "^*";
        }

        public static class Indefinido { }
        public static class Vacio { }
    }
}
