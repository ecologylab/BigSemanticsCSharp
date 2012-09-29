using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Ecologylab.Semantics.PlatformSpecifics
{
    // semantics platform specifics windows store app implementation
    class SemanticsPlatformSpecificsImpl : ISemanticsPlatformSpecifics
    {
        public object CreateNewBitmapImageFromUri(Uri uri)
        {
            return (ImageSource) new BitmapImage(uri);
        }

        public bool FileIsADictionary(object file)
        {
            throw new NotImplementedException();
        }

        public string DeriveMmNameFromField(FieldInfo thatField)
        {
            throw new NotImplementedException();
        }

        public object InvokeInstance(Type metadataClass, Type[] argClasses, object[] argObjects)
        {
            throw new NotImplementedException();
        }

        public FieldInfo GetFieldFromTypeWithName(Type type, string fieldName)
        {
            throw new NotImplementedException();
        }
    }
}
