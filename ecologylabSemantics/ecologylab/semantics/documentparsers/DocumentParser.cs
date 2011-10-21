using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecologylab.semantics.documentparsers
{
  abstract class DocumentParser
  {

    private static Dictionary<string, Type> _registeredDocumentParsers = new Dictionary<string, Type>();

    public static void RegisterDocumentParser(string parserName, Type parserType)
    {
      if (typeof(DocumentParser).IsAssignableFrom(parserType))
      {
        _registeredDocumentParsers[parserName] = parserType;
      }
    }

    static DocumentParser()
    {
      RegisterDocumentParser("xpath", typeof(XPathParser));
    }

      /// <summary>
      /// The main parsing happens here.
      /// This method signature itself doesn't define how meta-metadata is provided and how parsed metadata object is returned.
      /// Instead, each parser type can define their own ways, e.g. using constructors.
      /// </summary>
    public abstract void Parse();

      /// <summary>
      /// This method is called after Parse(), and should try to get more information from what we get from Parse().
      /// Currently, this includes regular expression based filtering (for XPathParser) and field parsers.
      /// </summary>
    public abstract void PostParse();

  }
}
