using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecologylab.semantics.metadata.scalar
{
    abstract public class MetadataScalarBase
    {
        public Object value;
        public static String VALUE_FIELD_NAME	    = "value";

        public MetadataScalarBase()
        {
        
        }

        public MetadataScalarBase(object value)
        {
            this.value = value;
        }

        public object Value
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
        public MetadataString()
        {

        }

        //The termvector stuff goes here !
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public MetadataString(object value)
            :base((String)value)
        {

        }

    }
    public class MetadataInteger : MetadataScalarBase
    {
        public MetadataInteger()
        {

        }
        public MetadataInteger(object value)
            :base((int)value)
        {

        }

    }
    public class MetadataParsedURL : MetadataScalarBase
    {
        public MetadataParsedURL()
        {

        }
        public MetadataParsedURL(object value)
            :base((Uri)value)
        {

        }

    }
    public class MetadataDate : MetadataScalarBase
    {
        public MetadataDate()
        {

        }
        public MetadataDate(object value)
            :base((DateTime)value)
        {

        }
    }
    public class MetadataStringBuilder : MetadataScalarBase
    {
        public MetadataStringBuilder()
        {

        }
        public MetadataStringBuilder(object value)
            :base((StringBuilder)value)
        {

        }
    }
}
