//
//  OrCondition.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;

namespace ecologylab.semantics.actions 
{
    /// <summary>
    /// missing java doc comments or could not find the source file.
    /// </summary>
    [SimplInherit]
    [SimplTag("or")]
    public class OrCondition : Condition
    {
        /// <summary>
        /// missing java doc comments or could not find the source file.
        /// </summary>
        [SimplCollection]
        [SimplScope("condition_scope")]
        [SimplNoWrap]
        private List<Condition> checks;

        public OrCondition()
        {
        }

        public List<Condition> Checks
        {
            get { return checks; }
            set { checks = value; }
        }

        public override bool Evaluate(SemanticOperationHandler handler)
        {
            bool flag = false;
            if (checks != null)
            {
                foreach (Condition check in checks)
                {
                    flag = flag || check.Evaluate(handler);
                    if (flag)
                        break;
                }
            }
            return flag;
        }
    }
}
