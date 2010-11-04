using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecologylab.semantics.metadata
{
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class mm_name : Attribute
    {
        private String mmName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        public mm_name(String tagName)
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
