using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.serialization.types.scalar;
using ecologylab.serialization.types;
using System.Reflection;

namespace ecologylab.semantics.metadata.scalar.types
{
	public abstract class MetadataScalarScalarType : ReferenceType
	{
		ScalarType              valueScalarType;
		FieldInfo               valueField;
		private static Boolean  metadataScalarTypesRegistered = false;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="metadataScalarTypeClass"></param>
		/// <param name="valueClass"></param>
		public MetadataScalarScalarType(Type metadataScalarTypeClass, Type valueClass)
			:base(metadataScalarTypeClass)
		{
			// Get type handles for Test<String> and its field.

            this.valueScalarType = TypeRegistry.GetType(valueClass);
            valueField = metadataScalarTypeClass.GetField("value");

            /*
            
            Type baseType = metadataScalarTypeClass.BaseType;
            
            RuntimeTypeHandle rth = metadataScalarTypeClass.TypeHandle;
            RuntimeFieldHandle rfh = metadataScalarTypeClass.GetField("value").FieldHandle;

            
            Type[] genericParams = baseType.GetGenericArguments();
            Type gType = genericParams.Length == 1 ? genericParams[0] : null;
            if(gType != null)
            {
                Object t = ((MetadataScalarBase<gType>)metadataScalarTypeClass).Value;
            }
            

            valueField = FieldInfo.GetFieldFromHandle(rfh); 
            */
            if (ValueField == null)
				Console.WriteLine(metadataScalarTypeClass.Name + " does not have a valueField");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		FieldInfo ValueField
		{
			get{ return valueField; }
		}

		public override bool SetField(object context, FieldInfo field, string valueString, string[] format)
		{
			if (valueString == null)
				return true;
			bool result = false;
			Object valueObject;

			valueObject = valueScalarType.GetInstance(valueString);
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

		public Object GetValueInstance(String value, String[] formatStrings)
		{
			return valueScalarType.GetInstance(value, formatStrings);
		}

		public static Type[]	METADATA_SCALAR_TYPES	=
			{ 
				typeof(MetadataStringScalarType),
				typeof(MetadataStringBuilderScalarType),
				typeof(MetadataIntegerScalarType),
				typeof(MetadataParsedURLScalarType),
				typeof(MetadataDateScalarType) };

		public static void init()
		{
			if (!metadataScalarTypesRegistered)
			{
				TypeRegistry.Register(METADATA_SCALAR_TYPES);
				metadataScalarTypesRegistered = true;
			}
		}
	}

	public class MetadataStringScalarType : MetadataScalarScalarType
	{
		public MetadataStringScalarType()
			: base(typeof(MetadataString), typeof(String))
		{

		}
		public override object GetInstance(string value, string[] formatStrings)
		{
			return new MetadataString(GetValueInstance(value, formatStrings));
		}
	}
	public class MetadataStringBuilderScalarType : MetadataScalarScalarType
	{
		public MetadataStringBuilderScalarType()
			: base(typeof(MetadataStringBuilder), typeof(StringBuilder))
		{

		}
		public override object GetInstance(string value, string[] formatStrings)
		{
			return new MetadataStringBuilder(GetValueInstance(value, formatStrings));
		}
	}
	public class MetadataIntegerScalarType : MetadataScalarScalarType
	{
		public MetadataIntegerScalarType()
			: base(typeof(MetadataInteger), typeof(int))
		{

		}
		public override object GetInstance(string value, string[] formatStrings)
		{
			return new MetadataStringBuilder(GetValueInstance(value, formatStrings));
		}
	}
	public class MetadataParsedURLScalarType : MetadataScalarScalarType
	{
		public MetadataParsedURLScalarType()
			: base(typeof(MetadataParsedURL), typeof(Uri))
		{

		}
		public override object GetInstance(string value, string[] formatStrings)
		{
			return new MetadataParsedURL(GetValueInstance(value, formatStrings));
		}
	}
	public class MetadataDateScalarType : MetadataScalarScalarType
	{
		public MetadataDateScalarType()
			: base(typeof(MetadataDate), typeof(DateTime))
		{

		}
		public override object GetInstance(string value, string[] formatStrings)
		{
			return new MetadataDate(GetValueInstance(value, formatStrings));
		}
	}
}
