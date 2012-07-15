using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.services;

namespace UnitTests
{
    [TestClass]
    public class OODSSTests
    {
        [TestMethod]
        public async void TestMetadataServicesClient()
        {
            Console.WriteLine("Initializing client");
            MetadataServicesClient mmdclient = new MetadataServicesClient(RepositoryMetadataTranslationScope.Get());

            Document d = await mmdclient.GetMetadata("http://www.amazon.com/gp/product/B0050SYS5A/");
            Console.WriteLine("Got second metadata object: {0}", d );


            Document d2 = await mmdclient.GetMetadata("http://www.airbnb.com/rooms/36769");
            Console.WriteLine("Got second metadata object: {0}", d2);

            Console.WriteLine("Terminating test cases");
        }
    }
}
