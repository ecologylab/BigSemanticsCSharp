using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ecologylab.Semantics.MetadataNS;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Ecologylab.Semantics.PlatformSpecifics
{
    // semantics platform specifics windows store app implementation
    class SemanticsPlatformSpecificsImpl : ISemanticsPlatformSpecifics
    {
        public object CreateNewBitmapImageFromUri(Uri uri)
        {
            return new BitmapImage(uri);
        }

        public bool FileIsADictionary(object file)
        {
            throw new NotImplementedException();
        }

        public FieldInfo GetFieldFromTypeWithName(Type type, string fieldName)
        {
            return type.GetTypeInfo().GetDeclaredField(fieldName);
        }
    }
}
