using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ecologylab.Semantics.MetadataNS;

namespace Ecologylab.Semantics.PlatformSpecifics
{
    // semantics platformSpecifics .NET implementation
    class SemanticsPlatformSpecificsImpl : ISemanticsPlatformSpecifics
    {
        /// <summary>
        /// return System.Windows.Media.ImageSource
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public object CreateNewBitmapImageFromUri(Uri uri)
        {
            return (ImageSource) new BitmapImage(uri);
        }

        public bool FileIsADictionary(object file)
        {
            var fileInfo = file as FileInfo;
            if (fileInfo != null)
                return fileInfo.Attributes.HasFlag(FileAttributes.Directory);
            throw new FileNotFoundException();
        }

        public string DeriveMmNameFromField(FieldInfo thatField)
        {
            String result = null;
            foreach (CustomAttributeData cad in thatField.CustomAttributes)
            {
                if (cad.Constructor.DeclaringType == typeof(MmName))
                {
                    result = (String)cad.ConstructorArguments[0].Value;
                    break;
                }
            }
            return result;
        }

        public object InvokeInstance(Type metadataClass, Type[] argClasses, object[] argObjects)
        {
            var constructorInfo = metadataClass.GetConstructor(argClasses);
            if (constructorInfo != null)
                return constructorInfo.Invoke(argObjects);
            return null;
        }

        public FieldInfo GetFieldFromTypeWithName(Type type, string fieldName)
        {
            return type.GetField(fieldName);
        }
    }
}
