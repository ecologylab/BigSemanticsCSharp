//Given mmd, representing the mmd of this page.

var currentMMDField;
//var xpathResult = document.evaluate( xpathExpression, contextNode, namespaceResolver, resultType, result );  
//More info at: https://developer.mozilla.org/en/Introduction_to_using_XPath_in_JavaScript
//Mozilla has better documentation, but we're using chromium.
//Not to worry, we're working with standards here. Both browsers implement the XPath defined in
// http://www.w3.org/TR/2004/NOTE-DOM-Level-3-XPath-20040226/DOM3-XPath.html


function extractMetadata(mmd) 
{
    mmd = mmd.meta_metadata;
    var metadata = recursivelyExtractMetadata(mmd, document, metadata);
    var returnVal = {};
    returnVal[mmd.name] = metadata;

    //return returnVal;
    return JSON.stringify(returnVal);

}

function recursivelyExtractMetadata(mmd, contextNode, metadata) {
    if (metadata == null || metadata == undefined)
        metadata = {}; //Output

    if (mmd.kids == null || mmd.kids.length == 0) {
        console.log("\t\tMMD has no kids: " + mmd.name);
        return; //Nothing to do here.
    }
    if (contextNode == null || contextNode == undefined)
        contextNode = document;
    for (mmdFieldIndex in mmd.kids) {
        var mmdField = mmd.kids[mmdFieldIndex];
        currentMMDField = mmdField;
        console.log("Iterating to Next mmdField");
        console.log(mmdField);

        if (mmdField.scalar != null && mmdField.scalar.xpath != null) {
            console.log("Setting scalar: " + mmdField.scalar.name);
            extractScalar(mmdField.scalar, contextNode, metadata);
        }

        if (mmdField.collection != null) {
            console.log("Setting Collection: " + mmdField.collection.name);
            extractCollection(mmdField.collection, contextNode, metadata);
        }

        if (mmdField.composite != null) {
            console.log("Setting Composite: " + mmdField.composite.name);
            extractComposite(mmdField.composite, contextNode, metadata);
        }
        console.log("Recursive extraction result: ");
        console.info(metadata);
    }
    console.log("Returning Metadata: ");
    console.info(metadata);
    return metadata;
}

function extractScalar(mmdScalarField, contextNode, metadata)
{
    var stringValue = getScalarWithXPath(contextNode, mmdScalarField.xpath);
		stringValue = stringValue.replace(new RegExp('\n', 'g'), "");
		stringValue = stringValue.trim();
    if (mmdScalarField.filter != null)
    {
        var regex = mmdScalarField.filter.regex;
        var replace = mmdScalarField.filter.replace;
        if (replace != undefined && replace != "") // We must replace all newlines if the replacement is not a empty character
				{
						stringValue = stringValue.replace(new RegExp(regex, 'g'), replace);
				}
        else
				{
						stringValue = stringValue.match(new RegExp(regex))[0];
				}
    }

    if (mmdScalarField.tag != null && mmdScalarField.tag != mmdScalarField.name)
        metadata[mmdScalarField.tag] = stringValue;
    else
        metadata[mmdScalarField.name] = stringValue;
}

function extractCollection(mmdCollectionField, contextNode, metadata)
{
    console.log("Setting Collection: " + mmdCollectionField.name);
    //nodeList is a XPathResult type, access items with snapshotItem(index)
    var nodeList = getNodeListWithXPath(contextNode, mmdCollectionField.xpath);
    var metadataCollection = [];

    if (mmdCollectionField.child_type == "hypertext_para") {
        //Special field, for now. Parser needs to change it's ways to handle hypertext
        parseNodeListAsHypertext(mmdCollectionField, nodeList, metadata);
        return; //This collection is special. No further normal processing required.
    }

    var collectionFields = mmdCollectionField.kids;

    if (collectionFields == null || collectionFields == undefined)
        console.log("Oops, collection fields doesn't exist");
    for (var resultIndex = 0; resultIndex < nodeList.snapshotLength; resultIndex++) {
        console.log("\tCollection Result Index: " + resultIndex);
        //recursive call.
        var recursiveContext = nodeList.snapshotItem(resultIndex);
        var metadataCollectionItem = {};
        for (var fieldIndex = 0; fieldIndex < collectionFields.length; fieldIndex++) {
            var recursiveField = collectionFields[fieldIndex];
            console.log("\tCollection Recursive Call: ");
            console.log(recursiveField);
            
            if (recursiveField.scalar != null)
                extractScalar(recursiveField.scalar, recursiveContext, metadataCollectionItem);
            else if (recursiveField.collection != null)
                extractCollection(recursiveField.collection, recursiveContext, metadataCollectionItem);
            else if (recursiveField.composite != null)
                extractComposite(recursiveField.composite, recursiveContext, metadataCollectionItem);
            
            console.log("Metadata Collection Item: ");
            console.info(metadataCollection);
        }
        metadataCollection.push(metadataCollectionItem);
    }
    var extractedCollection = {};
    console.log("Metadata Collection: ");
    console.info(metadataCollection);
    extractedCollection[mmdCollectionField.child_type] = metadataCollection;
    metadata[mmdCollectionField.name] = extractedCollection;
}

function extractComposite(mmdCompositeField, contextNode, metadata)
{
    if (contextNode == null)
        return null;

    console.log("Setting Composite: " + mmdCompositeField.name);

    if (mmdCompositeField.parse_as_hypertext == true || mmdCompositeField.type == "hypertext_para") {
        var paraNode = getNodeWithXPath(contextNode, mmdCompositeField.xpath);
        var parsedPara = parseHypertextParaFromNode(paraNode);
        metadata[mmdCompositeField.name] = parsedPara;
        return;
    }

    compositeMetadata = {};

    console.log("Composite Recursive call: ");
    console.info(mmdCompositeField);
    //Apply xpath of composite node if it exists
    var newContextNode = contextNode;
    if (mmdCompositeField.xpath != null)
        newContextNode = getNodeWithXPath(contextNode, mmdCompositeField.xpath);

    compositeMetadata = recursivelyExtractMetadata(mmdCompositeField, newContextNode, compositeMetadata);

    console.log("Composite Recursive Result ---------- : ");
    console.info(compositeMetadata);
    metadata[mmdCompositeField.name] = compositeMetadata;

}



function parseNodeListAsHypertext(mmdCollectionField, paras, metadata) {

    console.log("Found hypertext nodes: ");
    console.info(paras);

    var parsedParas = [];

    for (var resultIndex = 0; resultIndex < paras.snapshotLength; resultIndex++) {

        var hypertextNode = paras.snapshotItem(resultIndex);
        //console.log("Current Paragraph parsed: ");
        //console.info(hypertextNode);
        //console.log("Number of childNodes : " + hypertextNode.childNodes.length);

        var paraContainer = {};
        paraContainer[mmdCollectionField.child_type] = parseHypertextParaFromNode(hypertextNode);

        
        //console.info(hypertextPara);
        parsedParas.push(paraContainer);
    }
    console.info(parsedParas);

    metadata[mmdCollectionField.name] = parsedParas;
}

function parseHypertextParaFromNode(hypertextNode) {

    //internal functions
    function getLinkRun(node) {
        var link_run = {};
        link_run["text"] = node.textContent;
        link_run["location"] = node.href;
        link_run["title"] = node.title; //Wiki specific !
        return link_run;
    }
    function getTextRun(node, formattedRun) {
        var text_run = formattedRun == null ? {} : formattedRun;
        text_run.text = node.textContent;
        return text_run;
    }

    var hypertextPara = {};
    runs = [];
    hypertextPara["runs"] = runs;

    for (var nodeNum = 0; nodeNum < hypertextNode.childNodes.length; nodeNum++) {
        var curNode = hypertextNode.childNodes[nodeNum];
        var nodeName = curNode.nodeName;
        //console.info(curNode);
        var resNode = {};
        if (nodeName == "#text" || nodeName == "SPAN") {
            //console.log("Text: " + curNode.textContent);
            resNode["text_run"] = getTextRun(curNode);

        }
        else if (nodeName == "A") {
            //No further parsing just pull values out.
            resNode["link_run"] = getLinkRun(curNode);
        }
        else if (nodeName == "B") {
            formattedRun = {};
            var styleInfo = {}

            styleInfo["bold"] = true;
            formattedRun["style_info"] = styleInfo;
            if (curNode.childElementCount == 0) {
                resNode["text_run"] = getTextRun(curNode, formattedRun);
                //console.log("Simple bold : " + curNode.text);
            }
            else {
                console.log("Bold link ?");
                //console.log("NestedBold: ");
                //console.info(curNode);
            }
        }
        else if (nodeName == "I") {
            formattedRun = {};
            var styleInfo = {}
            styleInfo["italics"] = true;
            formattedRun["style_info"] = styleInfo;
            resNode["text_run"] = getTextRun(curNode, formattedRun);
        }
        else {
            console.log("IgnoredNode: ");
            console.info(curNode);
        }
        if (!isEmpty(resNode))
            runs.push(resNode);
    }
    return hypertextPara;
}

//Util functions, to make the above functions a little prettier

function isEmpty(ob) {
    for(var i in ob) {return false;}
    return true;
}

/**
* All scalars can be considered strings. Type holds no value in javascript (yet).
*/
function getScalarWithXPath(contextNode, xpath)
{
    return document.evaluate(xpath, contextNode, null, XPathResult.STRING_TYPE, null).stringValue;
}

/**
* Uses getNodeListWithXPath, but verifies and returns only the first value.
*/
function getNodeWithXPath(contextNode, xpath)
{
    var nodelist = getNodeListWithXPath(contextNode, xpath);
    if (nodelist.snapshotLength == 0)
        return null;
    else
        return nodelist.snapshotItem(0);
    
}

function getNodeListWithXPath(contextNode, xpath) {
    return document.evaluate(xpath, contextNode, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);

}
