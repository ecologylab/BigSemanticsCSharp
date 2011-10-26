using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Simpl.Fundamental.Collections;

namespace ecologylab.semantics.documentparsers
{
    public class BrowserPool : ResourcePool<IBrowserWrapper>
    {
        public delegate IBrowserWrapper BrowserFactoryMethod();

        private static BrowserFactoryMethod _factoryMethod;

        public static void registerBrowserFactoryMethod(BrowserFactoryMethod factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }

        public BrowserPool(int initialCapacity) : base(initialCapacity) { }

        protected override IBrowserWrapper GenerateNewResource()
        {
            if (_factoryMethod == null)
                return null;
            IBrowserWrapper browser = _factoryMethod();
            return browser;
        }
    }
}
