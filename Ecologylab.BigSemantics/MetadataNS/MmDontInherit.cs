using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecologylab.BigSemantics.MetadataNS
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class MmDontInherit : Attribute
    {
    }
}
