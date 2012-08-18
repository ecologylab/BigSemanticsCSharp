//
//  SetFieldSemanticAction.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using System.Reflection;
using Simpl.Fundamental.Generic;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.actions.exceptions;
using ecologylabSemantics.ecologylab.semantics.actions;

namespace ecologylab.semantics.actions 
{
    /// <summary>
    /// missing java doc comments or could not find the source file.
    /// </summary>
    [SimplInherit]
    [SimplTag("set_field")]
    public class SetFieldSemanticOperation : SemanticOperation
    {
        /// <summary>
        /// missing java doc comments or could not find the source file.
        /// </summary>
        [SimplScalar]
        [SimplTag("value")]
        [SimplHints(new Hint[] { Hint.XmlAttribute })]
        private String valueName;

        public SetFieldSemanticOperation()
        {
        }

        public String ValueName
        {
            get { return valueName; }
            set { valueName = value; }
        }

        public static readonly String Value	= "value";

        public override String GetOperationName()
        {
            return SemanticOperationStandardMethods.SetFieldAction;
        }

        public override void HandleError()
        {
        }

        public override Object Perform(Object obj)
        {
            String setterName = XmlTools.CamelCaseFromXMLElementName(GetReturnObjectName(), true);
            Object value = null;
            if (valueName != null)
                value = semanticOperationHandler.SemanticOperationVariableMap.Get(valueName);
            if (value == null)
            {
                return null;
                /*String errorMessage = valueName == null ? 
                    "Can't set_field name=\"" + GetReturnObjectName() + "\" in " + obj + " because value=\"null\"" : 
                    "Can't set_field name=\"" + GetReturnObjectName() + " in " + obj + "\" because there's no value bound to " + valueName;
                throw new SemanticOperationExecutionException(this, errorMessage);*/
            }

            Type valueClass = value.GetType();
            PropertyInfo method = ReflectionTools.GetProperty(obj.GetType(), setterName);
            if (method != null)
            try
            {
                method.SetValue(obj, value, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(System.Environment.StackTrace);
                Console.WriteLine("set_field failed: object={0}, setter={1}, value={2}", obj, setterName, value);
            }
            else
            {
                throw new SemanticOperationExecutionException(this, "set_field name=\"" + Name + "\"\tCan't find set method in " + obj + "  for " + valueName + " of type " + valueClass + "");
            }
            return null;
        }
    }
}
