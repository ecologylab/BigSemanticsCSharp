using System;
using Ecologylab.Semantics.Collecting;
using Ecologylab.Semantics.Generated.Library;
using Ecologylab.Semantics.MetaMetadataNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simpl.Serialization;
using System.Text;
using System.Collections.Generic;

namespace Ecologylab.Semantics.Test
{
    [TestClass]
    public class TestLoadingRepositoryAsOneFile
    {

        SemanticsSessionScope _scope;

        [TestMethod]
        public void TestLoadingRepository()
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            SimplTypesScope.graphSwitch = SimplTypesScope.GRAPH_SWITCH.ON;

            _scope = new SemanticsSessionScope(RepositoryMetadataTranslationScope.Get(),
                                               "TestData/MmdRepo",
                                               null,
                                               delegate { ValidateRepo(); });
            _scope.LoadRepositoryAsync();
        }

        public void ValidateRepo()
        {
            MetaMetadataRepository repo = _scope.MetaMetadataRepository;

            Console.WriteLine(repo.RepositoryByName.Count);
            List<string> mmdNames = new List<string>(repo.RepositoryByName.Keys);
            mmdNames.Sort();
            foreach (string mmdName in mmdNames)
            {
                Console.WriteLine(mmdName);
            }
            Console.WriteLine("--------------------");

            Assert.IsTrue(repo.RepositoryByName.Count > 200);
            Assert.IsNotNull(repo.GetMMByName("metadata"));
            Assert.IsNotNull(repo.GetMMByName("document"));
            Assert.IsNotNull(repo.GetMMByName("amazon_product"));
            Assert.IsNotNull(repo.GetMMByName("google_search"));
            Assert.IsNotNull(repo.GetMMByName("acm_portal"));
            Assert.IsNotNull(repo.GetMMByName("scholarly_article"));
        }

    }
}
