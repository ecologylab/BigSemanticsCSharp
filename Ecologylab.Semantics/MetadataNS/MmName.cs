using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecologylab.Semantics.MetadataNS
{
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class MmName : Attribute
    {
        private String mmName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        public MmName(String tagName)
        {
            this.mmName = tagName;
        }

        /// <summary>
        /// 
        /// </summary>
        public String MMName
        {
            get
            {
                return mmName;
            }
        }
    }
}
