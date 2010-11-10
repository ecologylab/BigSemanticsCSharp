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
            Object value = Value;
            return value == null ? "null" : value.ToString();
        }
    }

    public class MetadataString
    {

    }
    public class MetadataInteger
    {

    }
    public class MetadataParsedURL
    {

    }
    public class MetadataDate
    {

    }
    public class MetadataStringBuilder
    {

    }
}
