using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecologylab.BigSemantics.PlatformSpecifics
{
    class SemanticsPlatformSpecifics
    {
        private static ISemanticsPlatformSpecifics _iSemanticsPlatformSpecifics;

        private static Boolean _dead = false;

        private static readonly object SyncLock = new object();

        public static void Set(ISemanticsPlatformSpecifics that)
        {
            _iSemanticsPlatformSpecifics = that;
        }

        public static ISemanticsPlatformSpecifics Get()
        {
            if (_dead)
                throw new Exception("Can't initialize SemanticsPlatformSpecifics"); 

            if (_iSemanticsPlatformSpecifics == null)
            {
                lock (SyncLock)
                {
                    if (_iSemanticsPlatformSpecifics == null)
                    {
                        string typeName = "Ecologylab.BigSemantics.PlatformSpecifics.SemanticsPlatformSpecificsImpl, Ecologylab.BigSemantics.DotNet";
                        Type platformSpecificsType = Type.GetType(typeName);
                        if (platformSpecificsType == null)
                        {
                            typeName = "Ecologylab.BigSemantics.PlatformSpecifics.SemanticsPlatformSpecificsImpl, Ecologylab.BigSemantics.WindowsStoreApps";
                            platformSpecificsType = Type.GetType(typeName);
                        }
                        if (platformSpecificsType == null)
                        {
                            _dead = true;
                            throw new Exception("Can't initialize SemanticsPlatformSpecifics");
                        }
                        _iSemanticsPlatformSpecifics =
                            (ISemanticsPlatformSpecifics)Activator.CreateInstance(platformSpecificsType);
                    }
                }
            }

            return _iSemanticsPlatformSpecifics;
        }
    }
}
