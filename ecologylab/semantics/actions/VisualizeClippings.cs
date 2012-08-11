using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Serialization.Library.Configuration;
using ecologylab.semantics.actions;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;

namespace ecologylabSemantics.ecologylab.semantics.actions
{
 
    /// <summary>
    /// @author andruid, rhema
    ///  This semantic action visualizes clippings of a compound document.
    ///  For now, we visualize one clipping.
    ///</summary>
    public class VisualizeClippings : SemanticOperation
    {

	    public VisualizeClippings()
	    {
		
	    }

	    public override String GetOperationName()
	    {
		    return SemanticOperationStandardMethods.VISUALIZE_CLIPPINGS;
	    }
	
	    //need a score function for text that I can also use for clippings...
	    public double ScoreTextAtPoint(String text, int x, int y)
	    {
//		    if(!Pref.LookupBoolean("use_situated_search"))
		        return 0;
/*		    //just idf
		    //TermDictionary.getTermForUnsafeWord("sanity")
		    //getTermsFromInteractiveSpace
		    //with 
		
		    InteractiveSpace interactiveSpace = sessionScope.getInteractiveSpace();
		    TermWithScore[] terms = interactiveSpace.getTermScoresAtPoint(x, y);
		    //should add somet sort of stemming here or something... :(
		    HashMap<String, TermWithScore> termHash = new HashMap<String, TermWithScore>();
		    for(TermWithScore term: terms)
		    {
    //			debug(term.getWord());
			    termHash.put(term.getWord(), term);
		    }
		
		    String[] words = text.split(" ");
		    double score = 0;
		    for(String word : words)
		    {
			    word = word.replaceAll("[^A-Za-z]", "");
			    word = word.toLowerCase();
    //			debug("Does the hash contain:"+word+"?");
			    if(termHash.containsKey(word))
			    {
				    score += termHash.get(word).getScore();
    //				debug("yes");
			    }
			    else
			    {
    //				debug("no");
			    }
		    }
		    return score;*/
	    }
	
	    public String GistForTextAndPosition(int x, int y, int numWords, String text)
	    {
		    //debug("make gist at "+x+","+y+" with "+numWords+" and the text "+text);
	        String[] words = text.Split(' ');
		    double bestScore = -1;
		    String bestGist = "";
		    for(int wordOffset=0; wordOffset < Math.Min(words.Length, numWords + words.Length); wordOffset++)
		    {
			    String wholeGist = "";
			    int wordCount = 0;
			    //this should be repeated with different indexes
			    //calkins1942@gmail.com
			    for(int i=wordOffset; i<words.Length; i++)
			    {
				    String word = words[i];
				    if(wordCount > 0)
				    wholeGist += " ";
				    wholeGist += word;
				    wordCount++;
				    if(wordCount >= numWords)
					    break;
			    }
			    ///add up total score here...
			
			    double gScore = ScoreTextAtPoint(wholeGist, x, y);
			    //debug("check dist:"+wholeGist +" gets the score:"+gScore);
			    if(gScore > bestScore)
			    {
				    bestScore = gScore;
				    bestGist = wholeGist;
			    }
		    }
		    return bestGist;
	    }

	    public override void HandleError()
	    {
	    }

	    enum SelectionMethod
	    {
		    FIFO, SITUATED_SIMILARITY
	    }
	    //TODO add  arguments:
	    // selection method: default fifo
	    // filters: text only, picture only....
	    // number of clippings added default 1...  for searches mainly.
	    // max_extent:
	    //method for display. Currently using FIFO.  Also add filters: like text only

	
	    public override Object Perform(Object obj)// throws IOException
	    {
//		    InteractiveSpace interactiveSpace = sessionScope.getInteractiveSpace();
		    Document sourceDocument = ResolveSourceDocument();
//		    DocumentClosure closure = documentParser.GetDocumentClosure();
		    SelectionMethod selectionMethod = SelectionMethod.FIFO;
/*		    if (closure.IsDnd && interactiveSpace != null)
		    {
			    debug("This is a drop.  Finding clippings to visualize.");
			    //TODO make something that waits for images to be downloaded
			    CompoundDocument compoundSource = null;
			    if(sourceDocument is CompoundDocument)
			    {
				    compoundSource = (CompoundDocument) sourceDocument;
			    }
			
			    TextClipping bestTextClipping = null;
			    ImageClipping bestImageClipping = null;
			    double bestTextClippingScore = -1;

			    List<Clipping> clippings = compoundSource.getClippings();
			    if(clippings != null)
			    {
				    foreach (Clipping clipping in clippings)
				    {
					    if (clipping is TextClipping)
					    {
						    /*
						    //debug("Found text clipping");
						    if(selectionMethod == SelectionMethod.FIFO)
						    {
							    if(bestTextClipping == null)
							    {
								    bestTextClipping = (TextClipping)clipping;
							    }
						    }
						    */
/*						    double textClippingScore = ScoreTextAtPoint(((TextClipping)clipping).getText(), closure.getDndPoint().getX(), closure.getDndPoint().getY());
						    if(textClippingScore > bestTextClippingScore)//tbd, normalize on lenght
						    {
							     bestTextClippingScore = textClippingScore;
							     bestTextClipping = (TextClipping) clipping;
						    }
					    }
					    else if(clipping is ImageClipping)
					    {
						    //debug("Found image clipping");
						    if(selectionMethod == SelectionMethod.FIFO)
						    {
							    if(bestImageClipping == null)
							    {
								    bestImageClipping = (ImageClipping)clipping;
							    }
						    }
					    }
				    }
				    //TODO: Handle piles et cetera
				    if(bestImageClipping != null)
				    {

					    //try to download or make closure et cetera...
					    sessionScope.getOrConstructImage(bestImageClipping.getMedia().getLocation());
					    bestImageClipping.getMedia().getOrConstructClosure().addContinuation(new DropImageContinuation(bestImageClipping));
					    bestImageClipping.getMedia().getOrConstructClosure().setDndPoint(closure.getDndPoint());
					    bestImageClipping.getMedia().getOrConstructClosure().queueDownload();

					    if(bestImageClipping.getMedia().isDownloadDone())
					    {
						    debug("image already exists");
						    interactiveSpace.createAndAddClipping((ImageClipping)bestImageClipping, closure.getDndPoint().getX(), closure.getDndPoint().getY());
					    }

					    return null;
				    }
				    if(bestTextClipping != null)
				    {
					    //preprocess at this point to make smaller...
					    TextClipping gistWithContext = (TextClipping)bestTextClipping;
					    gistWithContext.setText(gistForTextAndPosition(closure.getDndPoint().getX(),closure.getDndPoint().getY(),10,gistWithContext.getText()));
					    interactiveSpace.createAndAddClipping(gistWithContext, closure.getDndPoint().getX(), closure.getDndPoint().getY());
					    return null;
				    }
			    }
			    else
			    {
				    debug("No clippings were found. Nothing visualized.");
			    }
			    debug("J");
		    }
		    else
		    {
			    debug("Ignore because not a drop");
		    }*/
		    return null;
	    }

    }
/*        class DropImageContinuation : Continuation<DocumentClosure>
	    {
		    ImageClipping bestImageClipping;
		    public DropImageContinuation()
		    {
			    super();
		    }
		    public DropImageContinuation(ImageClipping imageClipping)
		    {
			    super();
			    bestImageClipping = imageClipping;
		    }
		
		    @Override
		    public void callback(DocumentClosure o)
		    {
			    //debug("Here is the url for the document that should be showing up as the source:"+bestImageClipping.getSourceDoc().getLocation());
         // debug(""+bestImageClipping.getSourceDoc().setTitle(title));
      
			    //TODO we may want to download all of the images first before picking one.
			    //for now, we assume that we just display anything that uses this as its continuation.
			    //bestImageClipping.getSourceDoc().setTitle("SWIINGERS ARE FUNNY!!!");
			    //bestImageClipping.setSourceDoc(sessionScope.getOrConstructDocument((bestImageClipping.getSourceDoc().getDownloadLocation())));

			    if(bestImageClipping.getSourceDoc() == null)
				    debug("There is no source document for this image");
			    InteractiveSpace interactiveSpace = sessionScope.getInteractiveSpace();
			    interactiveSpace.createAndAddClipping((ImageClipping)bestImageClipping, o.getDndPoint().getX(), o.getDndPoint().getY());
			    debug("Dropped image");
		    }
	    }
    }*/
}
