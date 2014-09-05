using System;
using Ecologylab.Semantics.Collecting;
using Ecologylab.Semantics.Generated.Library;
using Ecologylab.Semantics.MetaMetadataNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simpl.Serialization;
using System.Text;
using System.Collections.Generic;
using Ecologylab.Semantics.MetadataNS.Scalar.Types;
using System.IO;
using System.Threading.Tasks;

namespace Ecologylab.Semantics.Test
{
    [TestClass]
    public class TestLoadingRepositoryAsOneFile
    {

        SemanticsSessionScope _scope;

        [TestMethod]
        public void TestLoadingPostInheritanceRepository()
        {
            SimplTypesScope.graphSwitch = SimplTypesScope.GRAPH_SWITCH.ON;
            MetadataScalarType.init();

            String workingDirPath = System.IO.Directory.GetCurrentDirectory();
            FileInfo repoFile = new FileInfo(workingDirPath + "\\..\\..\\..\\..\\BigSemanticsWrapperRepository\\BigSemanticsWrappers\\PostInheritanceRepository\\post-inheritance-repository.xml");
            Assert.IsTrue(repoFile.Exists);
            MetaMetadataRepositoryInit repoInit =
                new MetaMetadataRepositoryInit(RepositoryMetadataTranslationScope.Get(),
                                               repoFile.FullName,
                                               null);
            Task<MetaMetadataRepository> task = repoInit.LoadRepositoryFromCache(repoFile);
            task.Wait();
            ValidateRepo(task.Result);
        }

        public void ValidateRepo(MetaMetadataRepository repo)
        {
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
