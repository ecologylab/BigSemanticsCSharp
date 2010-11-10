using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecologylab.semantics.metadata.scalar
{
    abstract public class MetadataScalarBase
    {
        Object value;
        public static String VALUE_FIELD_NAME	    = "value";



        public MetadataScalarBase(Object value)
        {
            this.value = value;
        }

        public Object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public override String ToString()
        {
            return value == null ? "null" : value.ToString();
        }
    }

    public class MetadataString : MetadataScalarBase
    {

        //The termvector stuff goes here !

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public MetadataString(object value)
            :base(value)
        {

        }

    }
    public class MetadataInteger : MetadataScalarBase
    {
        public MetadataInteger(object value)
            :base(value)
        {

        }

    }
    public class MetadataParsedURL : MetadataScalarBase
    {
        public MetadataParsedURL(object value)
            :base(value)
        {

        }

    }
    public class MetadataDate : MetadataScalarBase
    {
        public MetadataDate(object value)
            :base(value)
        {

        }
    }
    public class MetadataStringBuilder : MetadataScalarBase
    {
        public MetadataStringBuilder(object value)
            :base(value)
        {

        }
    }
}
