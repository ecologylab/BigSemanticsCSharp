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
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata.scalar;
namespace ecologylabSemantics
{
    public class Tester
    {

        static String path = @"C:\Users\damaraju.m2icode\workspace\cSharp\ecologylabSemantics\DomExtraction\javascript\tempJSON\";

        public static void Main()
        {
            MetadataScalarScalarType.init();

            TranslationScope metadataTScope = GeneratedMetadataTranslations.Get();
            ElementState s = metadataTScope.deserialize(path + "wikipedia-parsed-hypertext.json", Format.JSON);

            //TestHypertextSerialization();
            Console.WriteLine("Done");
            
        }

        public static void TestHypertextSerialization()
        {

            MetadataScalarScalarType.init();
            HypertextPara p = new HypertextPara();
            TextRun t = new TextRun();
            MetadataString s = new MetadataString();
            s.Value = "This is the first part of the string. We will end this sentence with a link ";
            t.Text = s;

            LinkRun l = new LinkRun();
            MetadataString s2 = new MetadataString();
            s2.Value = "like so";
            l.Text = s2;

            MetadataParsedURL puri = new MetadataParsedURL();
            puri.value = new ParsedUri("http://www.google.com");
            l.Location = puri;
            p.Runs = new List<Run>();
            p.Runs.Add(t);
            p.Runs.Add(l);
            StringBuilder output = new StringBuilder();
            p.serializeToJSON(output);
            Console.WriteLine("output: " + output);
        }

        public static void TestJSONDeserialization()
        {
            
            MetadataScalarScalarType.init();
            TranslationScope metadataTScope = GeneratedMetadataTranslations.Get();
            ElementState es = metadataTScope.deserialize(path + "parsedIMDB.json", Format.JSON);
            foreach (Object o in es.EnumerableFields)
            {
                if (o != null)
                    Console.WriteLine("-- " + o.ToString());
                else
                    Console.WriteLine("Empty");
            }
        }

        public static void GetRepoWithPurl()
        {
            String workspace = @"C:\Users\damaraju.m2icode\workspace\";
            MetadataScalarScalarType.init();
            TranslationScope tScope = MetaMetadataTranslationScope.get();

            TranslationScope metadataTScope = GeneratedMetadataTranslations.Get();


            string testFile = @"web\code\java\ecologylabSemantics\repository\";
            MetaMetadataRepository repo = MetaMetadataRepository.ReadDirectoryRecursively(workspace + testFile, tScope, metadataTScope);

            MetaMetadata mmd = repo.GetDocumentMM(new ParsedUri("http://en.wikipedia.org/wiki/Nizam_of_Hyderabad"));
            StringBuilder b = new StringBuilder();
            mmd.serializeToJSON(b);
            Console.WriteLine("Got MMD : " + mmd.Name);
            Console.WriteLine("MMDJson : " + b);

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
