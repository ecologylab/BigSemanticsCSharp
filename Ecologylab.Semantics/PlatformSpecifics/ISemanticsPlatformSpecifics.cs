using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ecologylab.Semantics.PlatformSpecifics
{
    public interface ISemanticsPlatformSpecifics
    {
        //return System.Windows.Media.ImageSource || Windows.UI.Xaml.Media.ImageSource
        object CreateNewBitmapImageFromUri(Uri uri);

        // check if a file is a dictionary
        bool FileIsADictionary(object file);

        // derive mm name
        string DeriveMmNameFromField(FieldInfo thatField);


        object InvokeInstance(Type metadataClass, Type[] argClasses, object[] argObjects);

        FieldInfo GetFieldFromTypeWithName(Type type, String fieldName);
    }
}
