using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.serialization;
using ecologylab.semantics.metametadata;
using Cjc.ChromiumBrowser;
using System.Windows;
using System.IO;
using ecologylab.semantics.metadata.scalar.types;
using ecologylab.net;
using ecologylab.semantics.generated;
using System.Text.RegularExpressions;

namespace ecologylabSemantics
{
    public class Tester
    {
        public static void Main()
        {
            String workspace = @"C:\Users\damaraju.m2icode\workspace\";
            MetadataScalarScalarType.init();
            TranslationScope tScope = MetaMetadataTranslationScope.get();

            TranslationScope metadataTScope = GeneratedMetadataTranslations.Get();


            string testFile = @"web\code\java\ecologylabSemantics\repository\";
            MetaMetadataRepository repo = MetaMetadataRepository.ReadDirectoryRecursively(workspace + testFile, tScope, metadataTScope);

            MetaMetadata mmd = repo.getDocumentMM(new ParsedUri("http://www.imdb.com/title/tt1285016/"));
            Console.WriteLine("Got MMD : " + mmd.Name);
        }

        public static void testPuris()
        {
            Regex r = new Regex("http://imdb.com/[^/]+/something/");

            string shouldSatisfy = "http://imdb.com/somethinghere/something/";

            string shouldNotSatisfy = "http://imdb.com/somethinghere/withsomethingelse/something/";

            Match t = r.Match(shouldSatisfy);
            Console.WriteLine("Success: " + t.Success);
            Match s = r.Match(shouldNotSatisfy);
            Console.WriteLine("Success: " + s.Success);

            ParsedUri uriContext = new ParsedUri("http://www.imdb.com/title/tt1285016/somepic.jpg?Query=someval&someother=something#anchor");
            String domain = uriContext.Domain;
            string v = uriContext.GetLeftPart(UriPartial.Path);
            String domainAgain = uriContext.Suffix;
            string[] segs = uriContext.Segments;
            //ElementState es = scope.deserialize(path, Format.JSON, uriContext);
            Console.WriteLine("Done");
            //Console.WriteLine("Before:\n\n\n " + txt);

        }

        public static void testJSON()
        {
            MetadataScalarScalarType.init();
            TranslationScope scope =  GeneratedMetadataTranslations.Get();
            Console.WriteLine("Json to C#");
            String path = @"C:\Users\damaraju.m2icode\workspace\cSharp\ecologylabSemantics\DomExtraction\javascript\tempJSON\parsedIMDB.json";
            

            
            MetaMetadataRepository repo = (MetaMetadataRepository)scope.deserialize(@"C:\Users\damaraju.m2icode\workspace\imdbMMDJson.txt", Format.JSON);

            Console.WriteLine("New repo : " + repo.RepositoryByTagName.Count);
            StringBuilder buffy = new StringBuilder();

            repo.serializeToJSON(buffy);
            Console.WriteLine("JSON: \n\n\n" + buffy.ToString());
        }

        public static void testMMD()
        {
            TranslationScope scope = MetaMetadataTranslationScope.get();
            String filePath = @"C:\Users\damaraju.m2icode\workspace\web\code\java\ecologylabSemantics\repository\repositorySources\";
            //scope.deserialize(@"C:\Users\damaraju.m2icode\workspace\web\code\java\ecologylabSemantics\testcases\Snippet.xml");
            MetaMetadataRepository repo = (MetaMetadataRepository)scope.deserialize(filePath + "imdb.xml");
            //Console.WriteLine(repo.repositoryByTagName.Count);
            StringBuilder buffy = new StringBuilder();
            repo.serializeToJSON(buffy);
            String txt = buffy.ToString();

            StreamWriter outfile = new StreamWriter(filePath + "imdb.json");

            outfile.Write(buffy);
            outfile.Close();
            StringBuilder outBuffy = new StringBuilder();
            repo.serializeToXML(outBuffy);

            outfile = new StreamWriter(filePath + "imdbAgain.xml");
            outfile.Write(outBuffy);
            outfile.Close();
        }
    }
}
