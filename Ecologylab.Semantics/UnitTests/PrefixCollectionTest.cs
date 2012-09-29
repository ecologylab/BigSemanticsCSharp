using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simpl.Fundamental.Collections;
using Simpl.Fundamental.Net;

namespace UnitTests
{
    /// <summary>
    /// test the functionality of the ported from Java: PrefixCollection, PrefixPhrase, ChildPrefixMap
    /// </summary>
    [TestClass]
    public class PrefixCollectionTest
    {
        ParsedUri[] TEST_ADD =
        {
            new ParsedUri("http://nytimes.com"), 
            new ParsedUri("http://www.nytimes.com/2008"),
            new ParsedUri("http://nytimes.com/pages/sports/foo/bar/baz/bloch"),
            new ParsedUri("http://nytimes.com/pages/sports/"),
            new ParsedUri("http://nytimes.com/pages/sports/foo/"),
            new ParsedUri("http://nytimes.com/pages/sports/foo/bar/baz/bloch"),
            new ParsedUri("http://nytimes.com/pages/arts/interactive"),
            new ParsedUri("http://nytimes.com/pages/sports/foo"),
            new ParsedUri("http://nytimes.com/pages/arts"),
            new ParsedUri("http://www.nytimes.com/2008/01/26/sports/football/26giants.html?ref=sports"),
            new ParsedUri("http://www.amazon.com/*/lm")
        };

        ParsedUri[] TEST_MATCH =
        {
            new ParsedUri("http://nytimes.com/pages/sports/hoops"),
            new ParsedUri("http://nytimes.com/pages/"),
            new ParsedUri("http://nytimes.com/pages/sports/"),
            new ParsedUri("http://nytimes.mom/"),
            new ParsedUri("http://nytimes.com/pages/arts/interactive"),
            new ParsedUri("http://nytimes.com/"),
            new ParsedUri("http://nytimes.com/pages/arts/"),
            new ParsedUri("http://www.nytimes.com/2008/01/26/sports/baseball"),
            new ParsedUri("http://www.amazon.com/Cook-Books-amp-more/lm/R1PADW7FZALCHA")
        };
        
        [TestMethod]
        public void Test()
        {
            char separator = '/';
            PrefixCollection<Object> pc = new PrefixCollection<Object>(separator);

            StringBuilder buffy = new StringBuilder(32);

            for (int i = 0; i < TEST_ADD.Length; i++)
            {
                //			println(TEST[i].directoryString());
                PrefixPhrase<Object> pp = pc.Add(TEST_ADD[i]);
                buffy.Clear();
                pp.ToStringBuilder(buffy, separator);
                Console.WriteLine(buffy);
            }
            Console.WriteLine("\n");

            for (int i = 0; i < TEST_MATCH.Length; i++)
            {
                ParsedUri purl = TEST_MATCH[i];

                Console.WriteLine(purl.ToString() + "\t" + pc.Match(purl));
            }
            Console.WriteLine("\n");

            foreach (String phrase in pc.Values())
            {
                Console.WriteLine(phrase);
            }
        }
    }
}
