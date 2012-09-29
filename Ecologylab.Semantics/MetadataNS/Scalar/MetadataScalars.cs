using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Net;
using Ecologylab.Semantics.MetaMetadataNS.Net;
using System.IO;

namespace Ecologylab.Semantics.MetadataNS.Scalar
{

    public interface IMetadataScalar<T>
    {
        T Value
        {
            get;
            set;
        }
    }

    abstract public class MetadataScalarBase<T> : IMetadataScalar<T>
    {
        public T value;
        public static String VALUE_FIELD_NAME	    = "value";

        public MetadataScalarBase()
        {
        
        }

        public MetadataScalarBase(object value)
        {
            this.value = (T) value;
        }

        public T Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public override String ToString()
        {
            return value == null ? "null" : value.ToString();
        }
    }

    public class MetadataString : MetadataScalarBase<String>
    {
        public MetadataString(){}

        //The termvector stuff goes here !


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public MetadataString(object value):base(value)
        {}

        /**
	     * Check if a string is not null and not equal to {@code MetadataFieldAccessor.NULL}
	     * @param valueString - string to check
	     * @return True if not null and not equal to MetadataFieldAccessor.NULL, false otherwise.
	     */
	    public static bool IsNotNullValue(String valueString)
	    {
		    return (valueString != null && !valueString.Equals(MetadataFieldDescriptor.Null) );
	    }
	
	    public static bool IsNotNullAndEmptyValue(String valueString)
	    {
		    return IsNotNullValue(valueString) && !"".Equals(valueString.Trim());
	    }

    }
    public class MetadataInteger : MetadataScalarBase<int>
    {
        public MetadataInteger(){}
        public MetadataInteger(object value):base(value)
        {}

    }
    public class MetadataFloat : MetadataScalarBase<float>
    {
        public MetadataFloat(){}
        public MetadataFloat(object value):base(value)
        {}

    }
    public class MetadataDouble: MetadataScalarBase<double>
    {
        public MetadataDouble(){}
        public MetadataDouble(object value):base(value)
        {}

    }
    public class MetadataParsedURL : MetadataScalarBase<ParsedUri>
    {
        public MetadataParsedURL(){}
        public MetadataParsedURL(object value):base(value)
        {}

    }
    public class MetadataDate : MetadataScalarBase<DateTime>
    {
        public MetadataDate(){}
        public MetadataDate(object value):base(value)
        {}
    }
    public class MetadataStringBuilder : MetadataScalarBase<StringBuilder>
    {
        public MetadataStringBuilder(){}
        public MetadataStringBuilder(object value):base(value)
        {}
    }

    public class MetadataFile : MetadataScalarBase<Stream>
    {
        public MetadataFile() { }
        public MetadataFile(object value)
            : base(value)
        { }
    }

    public class MetadataBoolean : MetadataScalarBase<Boolean>
    {
        public MetadataBoolean() {}
        public MetadataBoolean(object value)
            : base(value)
        { }
    }

}
