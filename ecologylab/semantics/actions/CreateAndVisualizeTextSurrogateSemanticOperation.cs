//
//  CreateAndVisualizeTextSurrogateSemanticAction.cs
//  s.im.pl serialization
//
//  Generated by DotNetTranslator on 11/16/10.
//  Copyright 2010 Interface Ecology Lab. 
//

using System;
using System.Collections.Generic;
using Simpl.Serialization.Attributes;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.scalar;
using ecologylab.semantics.namesandnums;
using ecologylabSemantics.ecologylab.semantics.actions;

namespace ecologylab.semantics.actions 
{
    /// <summary>
    /// This action needs to be implemented by the client.
    /// </summary>
    [SimplInherit]
    [SimplTag("create_and_visualize_text_surrogate")]
    public class CreateAndVisualizeTextSurrogateSemanticOperation : SemanticOperation
    {
        public CreateAndVisualizeTextSurrogateSemanticOperation()
        {
        }

        public override string GetOperationName()
        {
            return SemanticOperationStandardMethods.CreateAndVisualizeTextSurrogate;
        }

        public override void HandleError()
        {
        }

        static readonly int MAX_WORDS_IN_GIST = 8;

        private string CreateGist(string text)
        {
            string[] words = text.Split(' ');
            string returnString = "";
            int wordCount = 0;
            foreach(string word in words)
            {
                if(wordCount > 0)
                    returnString += " ";
                returnString += word;
                wordCount++;
                if(wordCount >= MAX_WORDS_IN_GIST)
                    break;
            }
            return returnString;
        }

        public override object Perform(object obj)
        {
            Console.WriteLine("Adding text clipping");
            bool isSemanticText = GetArgumentBoolean(SemanticOperationNamedArguments.SemanticText, false);

            object contextObject = GetArgumentObject(SemanticOperationNamedArguments.Text);
            string context = (contextObject != null) ? contextObject.ToString() : null; 

            // TODO use html context -- need methods to strip tags to set regular context from it.
            object htmlContextObject = GetArgumentObject(SemanticOperationNamedArguments.HtmlContext);
            string htmlContext = (htmlContextObject != null) ? htmlContextObject.ToString() : null;
            
            if (context != null)
            {
                Document sourceDocument = ResolveSourceDocument();
                // We will do something smarter here later when we have interest vectors.
                TextClipping textClipping = new TextClipping(sessionScope.MetaMetadataRepository.GetMMByName(DocumentParserTagNames.TextTag));
                //textClipping.setText(createGist(context));
                textClipping.Text = new MetadataString(context);
                textClipping.Context = new MetadataString(context);

                textClipping.SourceDoc = sourceDocument;
                
                sourceDocument.AddClipping(textClipping);
            }
            return null;
        }
    }
}
