using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.collections;
using ecologylab.semantics.actions;

namespace ecologylab.semantics.actions.exceptions
{
    public class IfOperationException : SemanticOperationExecutionException
    {

        public IfOperationException(Exception e, NestedSemanticOperation operation, Scope<Object> semanticActionReturnValueMap)
            : base(operation)
        {
            Console.WriteLine(":::All the nested semantic actions might not execute properly:::");
            List<SemanticOperation> nestedOperations= operation.NestedSemanticActionList;
            if (nestedOperations != null && nestedOperations.Count > 0)
            {
                for(int i = 0; i < nestedOperations.Count; i++)
                {
                    Console.WriteLine("\t\t\t[" + nestedOperations[i].Name.ToUpper() + "] skipped");
                }
            }
            StackTrace(semanticActionReturnValueMap);
        }
    }
}
