using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecologylab.Semantics.MetadataNS
{
    /// <summary>
    /// Analog to @semantics_mixin in java.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SemanticsMixin : Attribute
    {
    }
}
