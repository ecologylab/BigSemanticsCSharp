using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Context;
using Simpl.Serialization.Types;
using Simpl.Serialization.Types.Scalar;

namespace ecologylab.semantics.metadata.scalar.types
{
    public class MetadataScalarTypeType : ReferenceType

    {
        public MetadataScalarTypeType() 
            : base(typeof(MetadataScalarType), null, null, null)
        {

        }

        public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext unmarshallingContext)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            MetadataScalarType result = null;
            String simpleName;

            if (value != null && value.StartsWith("Metadata"))
                simpleName = value;
            else
            {
                if ("int".Equals(value) || "Int".Equals(value))
                    value = "Integer";	// be flexible about integer types

                int length = value.Length;

                StringBuilder buffy = new StringBuilder(length + 18);	// includes room for "Metadata" & "Type"
                buffy.Append("Metadata");
                char firstChar = value[0];
                if (char.IsLower(firstChar))
                {
                    buffy.Append(char.ToUpper(firstChar));
                    if (length > 1)
                        buffy.Append(value, 1, length - 1);
                }
                else
                {
                    buffy.Append(value);
                }
                simpleName = buffy.ToString();
            }
            return (MetadataScalarType)TypeRegistry.GetScalarTypeBySimpleName(simpleName);			
        }
    }
}
