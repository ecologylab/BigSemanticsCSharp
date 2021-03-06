﻿using System;
using System.Diagnostics;
using System.Text;
using Ecologylab.BigSemantics.PlatformSpecifics;
using Simpl.Fundamental.Net;
using Simpl.Serialization.Context;
using Simpl.Serialization.Types;
using Simpl.Serialization.Types.Scalar;
using System.Reflection;
using System.IO;

namespace Ecologylab.BigSemantics.MetadataNS.Scalar.Types
{
	public class MetadataScalarType : ReferenceType
	{
	    ScalarType              valueScalarType;
	    FieldInfo               valueField;
		private static Boolean  metadataScalarTypesRegistered = false;


        public MetadataScalarType()
            : this(typeof(MetadataScalarType), typeof(ScalarType))
        {
            
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="metadataScalarTypeClass"></param>
		/// <param name="valueClass"></param>
		protected MetadataScalarType(Type metadataScalarTypeClass, Type valueClass)
            : base(metadataScalarTypeClass, metadataScalarTypeClass.Name, null, null)
		{
			// Get type handles for Test<String> and its field.

            this.valueScalarType = TypeRegistry.ScalarTypes[valueClass];
			valueField = metadataScalarTypeClass.GetRuntimeField(MetadataScalarBase<object>.VALUE_FIELD_NAME);
            if (ValueField == null)
				Debug.WriteLine(metadataScalarTypeClass.Name + " does not have a valueField");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		FieldInfo ValueField
		{
			get{ return valueField; }
		}

	    public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext unmarshallingContext)
	    {
	        throw new NotImplementedException();
	    }

	    public override bool SetField(object context, FieldInfo field, string valueString, string[] format, IScalarUnmarshallingContext scalarUnmarshallingContext)
		{
			if (valueString == null || string.IsNullOrWhiteSpace(valueString))
				return true;
			bool result = false;

		    object valueObject = valueScalarType.GetInstance(valueString, format, scalarUnmarshallingContext);
			if (valueObject != null)
			{
				Object metadataScalarContext = field.GetValue(context);
				if (metadataScalarContext == null)
				{
					Type t = field.FieldType;
					metadataScalarContext = Activator.CreateInstance(t,new object[]{valueObject});
					field.SetValue(context, metadataScalarContext);
				}
				else
					ValueField.SetValue(metadataScalarContext, valueObject);
				result = true;
			}
			return result;
		}

	    public override string Marshall(object instance, TranslationContext context = null)
	    {
	        var metadataScalarInstance = instance as IMetadataScalar<object>;
	        return (metadataScalarInstance != null) ? OperativeScalarType.Marshall(metadataScalarInstance.Value, context) : instance.ToString();
	    }

	    public Object GetValueInstance(String value, String[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
		{
			return OperativeScalarType.GetInstance(value, formatStrings, scalarUnmarshallingContext);
		}

        public override bool NeedsJsonQuotationWrap()
        {
            return OperativeScalarType.NeedsJsonQuotationWrap();
        }

		public static void init()
		{
			if (!metadataScalarTypesRegistered)
			{
				SimplType[]	METADATA_SCALAR_TYPES	=
			    { 
				    
                    new MetadataStringScalarType(),
				    new MetadataStringBuilderScalarType(),
				    new MetadataIntegerScalarType(),
				    new MetadataParsedURLScalarType(),
				    new MetadataDateScalarType(),
                    //new MetadataFileScalarType(),
                    new MetadataFloatScalarType(),
                    new MetadataDoubleScalarType(),
                    new MetadataBooleanScalarType(),
                    new MetadataScalarTypeType(),
                };

                //TypeRegistry.RegisterTypes(METADATA_SCALAR_TYPES); //TODO FIXME
				metadataScalarTypesRegistered = true;
			}
		}

	    public override bool SimplEquals(object left, object right)
	    {
	        throw new NotImplementedException();
	    }

        public override  ScalarType OperativeScalarType
        {
            get { return valueScalarType; }
        }
	}

	public class MetadataStringScalarType : MetadataScalarType
	{
		public MetadataStringScalarType()
			: base(typeof(MetadataString), typeof(String))
		{

		}
        public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
		{
            return new MetadataString(GetValueInstance(value, formatStrings, scalarUnmarshallingContext));
		}
	}
	public class MetadataStringBuilderScalarType : MetadataScalarType
	{
		public MetadataStringBuilderScalarType()
			: base(typeof(MetadataStringBuilder), typeof(StringBuilder))
		{

		}
        public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
		{
            return new MetadataStringBuilder(GetValueInstance(value, formatStrings, scalarUnmarshallingContext));
		}
	}
	public class MetadataIntegerScalarType : MetadataScalarType
	{
		public MetadataIntegerScalarType()
			: base(typeof(MetadataInteger), typeof(int))
		{

		}
        public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
		{
            return new MetadataStringBuilder(GetValueInstance(value, formatStrings, scalarUnmarshallingContext));
		}
	}
    public class MetadataFloatScalarType : MetadataScalarType
    {
        public MetadataFloatScalarType()
            : base(typeof(MetadataFloat), typeof(float))
        {

        }
        public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
        {
            return new MetadataFloat(GetValueInstance(value, formatStrings, scalarUnmarshallingContext));
        }
    }
    
    public class MetadataParsedURLScalarType : MetadataScalarType
	{
		public MetadataParsedURLScalarType()
			: base(typeof(MetadataParsedURL), typeof(ParsedUri))
		{

		}
        public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
		{
            return new MetadataParsedURL(GetValueInstance(value, formatStrings, scalarUnmarshallingContext));
		}
	}
	public class MetadataDateScalarType : MetadataScalarType
	{
		public MetadataDateScalarType()
			: base(typeof(MetadataDate), typeof(DateTime))
		{

		}
        public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
		{
            return new MetadataDate(GetValueInstance(value, formatStrings, scalarUnmarshallingContext));
		}

        public override string Marshall(object instance, TranslationContext context = null)
        {
            var metadataScalarInstance = instance as IMetadataScalar<DateTime>;
            return (metadataScalarInstance != null) ? OperativeScalarType.Marshall(metadataScalarInstance.Value, context) : instance.ToString();
        }
	}

    //TODO: to get file type right. 
    //public class MetadataFileScalarType : MetadataScalarType
    //{
    //    public MetadataFileScalarType()
    //        : base(typeof(MetadataFile), typeof(FileTypeBase))
    //    {

    //    }
    //    public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
    //    {
    //        return new MetadataFile(GetValueInstance(value, formatStrings, scalarUnmarshallingContext));
    //    }
    //}

    public class MetadataDoubleScalarType : MetadataScalarType
    {
        public MetadataDoubleScalarType()
            : base(typeof(MetadataDouble), typeof(double))
        {

        }
        public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
        {
            return new MetadataDouble(GetValueInstance(value, formatStrings, scalarUnmarshallingContext));
        }
    }

    public class MetadataBooleanScalarType : MetadataScalarType
    {
        public MetadataBooleanScalarType()
            : base(typeof(MetadataBoolean), typeof(Boolean))
        {

        }
        public override object GetInstance(string value, string[] formatStrings, IScalarUnmarshallingContext scalarUnmarshallingContext)
        {
            return new MetadataBoolean(GetValueInstance(value, formatStrings, scalarUnmarshallingContext));
        }
    }
}
