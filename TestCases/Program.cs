using System;   
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Simpl.Serialization.Attributes;
using Simpl.Serialization.Context;
using ecologylab.serialization;
using ecologylab.semantics.metametadata;
using System.Windows;
using System.IO;
using ecologylab.semantics.metadata.scalar.types;
using System.Text.RegularExpressions;
using ecologylab.semantics.generated.library;
using ecologylab.semantics.metadata.scalar;
using System.Windows.Input;

namespace ecologylabSemantics
{
    public class Tester
    {
        public class TestElementState : ElementState
        {
            [SimplScalar]
            public String SomeString;

            [SimplScalar]
            public ParsedUri uri;

            public TestElementState()
            { }


        }

        public static void Main()
        {
//            TestElementState tES = new TestElementState
//            {
//                SomeString = "This string has \" Quotes\" —  and & > ? > < :" + (char)0x09 + "MORE ",
//                uri = new ParsedUri("http://google.com/search?moresearches=some&more=values")
//            };
//
//            //tES.somethingElse = "New String";
//
//            var buffy = new StringBuilder();
//            tES.serializeToJSON(buffy);
//            Console.WriteLine("buffy:\n" + buffy);
//            TranslationScope scope = new TranslationScope("Temp", new Type[]{typeof(TestElementState)});
//
//            TestElementState deserializeString = (TestElementState) scope.Deserialize(buffy.ToString(),  StringFormat.Json);
//            buffy = new StringBuilder();
//            deserializeString.serialize(buffy, Format.JSON);
//
//            Console.WriteLine("\n" + buffy);
//
//            Console.WriteLine("");
            //new ClassTester();
        }
//
//        static String path = @"C:\Users\damaraju.m2icode\workspace\cSharp\ecologylabSemantics\DomExtraction\javascript\tempJSON\";
//        public class ExpandElement : ICommand
//        {
//            public String GetLabel()
//            {
//                return "Collapse";
//            }
//            public bool CanExecute(object parameter)
//            {
//                return parameter != null;
//            }
//
//            public event EventHandler CanExecuteChanged
//            {
//                add { CommandManager.RequerySuggested += value; }
//                remove { CommandManager.RequerySuggested -= value; }
//            }
//
//            public void Execute(object parameter)
//            {
//                Console.WriteLine("Executing CollapseWikiView command");
//            }
//        }
//
//        class ClassTester : DependencyObject
//        {
//            #region NorthZoneCommand
//            ICommand northZoneCommand;
//
//            public ICommand NorthZoneCommand
//            {
//                get { return northZoneCommand; }
//                set { northZoneCommand = value; }
//            }
//
//            /// <summary>
//            /// NorthZoneCommand Attached Dependency Property
//            /// </summary>
//            public static readonly DependencyProperty NorthZoneCommandProperty =
//                DependencyProperty.RegisterAttached("NorthZoneCommand", typeof(ICommand), typeof(ClassTester),
//                    new FrameworkPropertyMetadata((ICommand)null));
//
//            /// <summary>
//            /// Gets the NorthZoneCommand property. This dependency property 
//            /// indicates ....
//            /// </summary>
//            public static ICommand GetNorthZoneCommand(DependencyObject d)
//            {
//                return (ICommand)d.GetValue(NorthZoneCommandProperty);
//            }
//
//            /// <summary>
//            /// Sets the NorthZoneCommand property. This dependency property 
//            /// indicates ....
//            /// </summary>
//            public static void SetNorthZoneCommand(DependencyObject d, ICommand value)
//            {
//                d.SetValue(NorthZoneCommandProperty, value);
//            }
//
//            #endregion
//
//            public ClassTester()
//            {
//                ICommand command = new ExpandElement();
//                var dp = NorthZoneCommandProperty;
//                PropertyInfo dp2 = this.GetType().GetProperty("NorthZoneCommand");
////                MethodInfo zonePropSetter = this.GetType().GetMethod("SetNorthZoneCommand");
//                dp2.SetValue(this, command, null);
//                Console.WriteLine("NorthZone command is : " + this.NorthZoneCommand);
//            }
//        }
//
//
//      
//
//        public static async Task<ElementState> GetMetadata()
//        {
//
//            String workspace = @"C:\Users\damaraju.m2icode\workspace\";
//            String jsPath = workspace + @"cSharp\ecologylabSemantics\DomExtraction\javascript\";
//            var metadataTScope = GeneratedMetadataTranslations.Get();
//            TaskCompletionSource<ElementState> tcs = new TaskCompletionSource<ElementState>();
//            Console.WriteLine("Thread Name: " + Thread.CurrentThread.Name);
//            ElementState t = await TaskEx.Run(() =>
//            {
//                var tStart = System.DateTime.Now;
//                    Console.WriteLine("Inside TaskEx Before " + Thread.CurrentThread.ManagedThreadId);
//                    ElementState elementState = metadataTScope.deserialize(jsPath + @"tempJSON\lastMetadataCleaned.json", new TranslationContext(), Format.JSON);
//                    TimeSpan timeSpan = (System.DateTime.Now - tStart);
//                    Console.WriteLine("Inside TaskEx After " + Thread.CurrentThread.ManagedThreadId);
//                    Console.WriteLine("Deserialized, time : " + timeSpan);
//                    return elementState;
//                });
//            Console.WriteLine("Done deserializing");
//            tcs.TrySetResult(t);
//
//            return await tcs.Task;
//
//        }
//
//        public static void TestHypertextSerialization()
//        {
//
//            MetadataScalarScalarType.init();
//            HypertextPara p = new HypertextPara();
//            TextRun t = new TextRun();
//            MetadataString s = new MetadataString
//                                   {
//                                       Value =
//                                           "This is the first part of the string. We will end this sentence with a link "
//                                   };
//            t.Text = s;
//
//            LinkRun l = new LinkRun();
//            MetadataString s2 = new MetadataString();
//            s2.Value = "like so";
//            l.Text = s2;
//
//            MetadataParsedURL puri = new MetadataParsedURL();
//            puri.value = new ParsedUri("http://www.google.com");
//            l.Location = puri;
//            p.Runs = new List<Run>();
//            p.Runs.Add(t);
//            p.Runs.Add(l);
//            StringBuilder output = new StringBuilder();
//            p.serializeToJSON(output);
//            Console.WriteLine("output: " + output);
//        }
//
//        public static void TestJSONDeserialization()
//        {
//            
//            MetadataScalarScalarType.init();
//            TranslationScope metadataTScope = GeneratedMetadataTranslations.Get();
//            ElementState es = metadataTScope.deserialize(path + "parsedIMDB.json", new TranslationContext(), Format.JSON);
//            foreach (Object o in es.EnumerableFields)
//            {
//                if (o != null)
//                    Console.WriteLine("-- " + o.ToString());
//                else
//                    Console.WriteLine("Empty");
//            }
//        }
//
//        public static void GetRepoWithPurl()
//        {
//            String workspace = @"C:\Users\damaraju.m2icode\workspace\";
//            MetadataScalarScalarType.init();
//            TranslationScope tScope = MetaMetadataTranslationScope.get();
//
//            TranslationScope metadataTScope = GeneratedMetadataTranslations.Get();
//
//
//            string testFile = @"web\code\java\ecologylabSemantics\repository\";
//            MetaMetadataRepository repo = MetaMetadataRepository.ReadDirectoryRecursively(workspace + testFile, tScope, metadataTScope);
//
//            MetaMetadata mmd = repo.GetDocumentMM(new ParsedUri("http://en.wikipedia.org/wiki/Nizam_of_Hyderabad"));
//            StringBuilder b = new StringBuilder();
//            mmd.serializeToJSON(b, null);
//            Console.WriteLine("Got MMD : " + mmd.Name);
//            Console.WriteLine("MMDJson : " + b);
//
//        }
//
//        public static void testPuris()
//        {
//            Regex r = new Regex("http://imdb.com/[^/]+/something/");
//
//            string shouldSatisfy = "http://imdb.com/somethinghere/something/";
//
//            string shouldNotSatisfy = "http://imdb.com/somethinghere/withsomethingelse/something/";
//
//            Match t = r.Match(shouldSatisfy);
//            Console.WriteLine("Success: " + t.Success);
//            Match s = r.Match(shouldNotSatisfy);
//            Console.WriteLine("Success: " + s.Success);
//
//            ParsedUri uriContext = new ParsedUri("http://www.imdb.com/title/tt1285016/somepic.jpg?Query=someval&someother=something#anchor");
//            String domain = uriContext.Domain;
//            string v = uriContext.GetLeftPart(UriPartial.Path);
//            String domainAgain = uriContext.Suffix;
//            string[] segs = uriContext.Segments;
//            //ElementState es = scope.deserialize(path, Format.JSON, uriContext);
//            Console.WriteLine("Done");
//            //Console.WriteLine("Before:\n\n\n " + txt);
//
//        }
//
//        public static void testJSON()
//        {
//            MetadataScalarScalarType.init();
//            TranslationScope scope =  GeneratedMetadataTranslations.Get();
//            Console.WriteLine("Json to C#");
//            String path = @"C:\Users\damaraju.m2icode\workspace\cSharp\ecologylabSemantics\DomExtraction\javascript\tempJSON\parsedIMDB.json";
//            
//
//            
//            MetaMetadataRepository repo = (MetaMetadataRepository)scope.deserialize(@"C:\Users\damaraju.m2icode\workspace\imdbMMDJson.txt", new TranslationContext(), Format.JSON);
//
//            Console.WriteLine("New repo : " + repo.RepositoryByTagName.Count);
//            StringBuilder buffy = new StringBuilder();
//
//            repo.serializeToJSON(buffy);
//            Console.WriteLine("JSON: \n\n\n" + buffy.ToString());
//        }
//
//        public static void testMMD()
//        {
//            TranslationScope scope = MetaMetadataTranslationScope.get();
//            String filePath = @"C:\Users\damaraju.m2icode\workspace\web\code\java\ecologylabSemantics\repository\repositorySources\";
//            //scope.deserialize(@"C:\Users\damaraju.m2icode\workspace\web\code\java\ecologylabSemantics\testcases\Snippet.xml");
//            MetaMetadataRepository repo = (MetaMetadataRepository)scope.deserialize(filePath + "imdb.xml");
//            //Console.WriteLine(repo.repositoryByTagName.Count);
//            StringBuilder buffy = new StringBuilder();
//            repo.serializeToJSON(buffy, null);
//            String txt = buffy.ToString();
//
//            StreamWriter outfile = new StreamWriter(filePath + "imdb.json");
//
//            outfile.Write(buffy);
//            outfile.Close();
//            StringBuilder outBuffy = new StringBuilder();
//            repo.serializeToXML(outBuffy, null);
//
//            outfile = new StreamWriter(filePath + "imdbAgain.xml");
//            outfile.Write(outBuffy);
//            outfile.Close();
//        }
    }
}
