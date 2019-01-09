using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
In the Syntactic Analysis phase: You verify whether the code follows the 
language syntax(grammar rules). For example, you check whether there is 
only one variable on the LHS of an operator(considering language C), that 
each statement is terminated by a ;, that if is followed by a conditional/Boolean 
statement etc.
*/

namespace Compilador
{
    
    public class Syntactic
    {
        FormCompiler FormCompiler;
        SDF SDFObject;

        public Syntactic(FormCompiler formCompiler, SDF SDFobject)
        {
            FormCompiler = formCompiler;
            SDFObject = SDFobject;
        }

        /// <summary>
        /// Ejecuta el proceso para analizar Sintácticamente un código SDF.
        /// </summary>
        /// <returns>El booleano de retorno específica si la operación se ejecutó correctamente.</returns>
        public bool Execute()
        {
            return true;
        }
    }
}
