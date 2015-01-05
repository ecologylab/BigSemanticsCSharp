using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ecologylab.BigSemantics.MetadataNS;

namespace Ecologylab.BigSemantics.PlatformSpecifics
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

        public FieldInfo GetFieldFromTypeWithName(Type type, string fieldName)
        {
            return type.GetField(fieldName);
        }
    }
}
