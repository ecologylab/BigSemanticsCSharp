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
				//Type typeClass = typeof(MetadataScalarBase);
				//result = typeClass.GetField(MetadataScalarBase.VALUE_FIELD_NAME);
				//result.setAccessible(true);
				//valueField = result;
			}
			return result;
		}

		public override bool SetField(object context, FieldInfo field, string valueString, string[] format)
		{
			if (valueString == null)
				return true;
			bool result = false;
			//T valueObject;

			//valueObject = valueScalarType.GetInstance(valueString);
			//if (valueObject != null)
			{
				//M metadataScalarContext = (M)field.GetValue(context);
				//if (metadataScalarContext != null)
				//{
					//field.GetType().;
				//}
			}
			return base.SetField(context, field, valueString, format);
		}

		public override object GetInstance(string value, string[] formatStrings)
		{
			throw new NotImplementedException();
		}
	}
}
