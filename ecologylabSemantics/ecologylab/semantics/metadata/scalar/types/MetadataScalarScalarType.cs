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
			this.valueScalarType = TypeRegistry.GetType(valueClass);
			GetValueField();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		FieldInfo GetValueField()
		{
			FieldInfo result = valueField;
			if (result == null)
			{
				Type typeClass = typeof(MetadataScalarBase);
				result = typeClass.GetField(MetadataScalarBase.VALUE_FIELD_NAME);
				//result.setAccessible(true);
				valueField = result;
			}
			return result;
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
				if (metadataScalarContext != null)
				{
					metadataScalarContext = Activator.CreateInstance(field.GetType());
					field.SetValue(context, metadataScalarContext);
				}
				valueField.SetValue(context, valueObject);
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
