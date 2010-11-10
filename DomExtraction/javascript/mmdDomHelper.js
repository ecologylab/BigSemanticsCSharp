//Given mmd, representing the mmd of this page.

var currentMMDField;
function myTest() 
{
    var s = {};
    s.nameString = "name";
    s.val = [];
    s.val.push("string");
    return JSON.stringify(s);    
}

function extractMetadata(mmd) 
{
    mmd = mmd.meta_metadata;
    var metadata = recursivelyExtractMetadata(mmd, document, metadata);
    var returnVal = {};
    returnVal[mmd.name] = metadata;

    return JSON.stringify(returnVal);

}

function recursivelyExtractMetadata(mmd, contextNode, metadata) {
    if (metadata == null || metadata == undefined)
        var metadata = {}; //Output

    if (mmd.kids == null || mmd.kids.length == 0) {
        console.log("\t\tMMD has no kids: " + mmd.name);
        return; //Nothing to do here.
    }
    if (contextNode == null || contextNode == undefined)
        var contextNode = document;
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
            extractCollection(mmdField.collection, contextNode, metadata);
        }

        if (mmdField.composite != null) {
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
    var regex = "\n";
    var replace = "";
    if (mmdScalarField.filter != null)
    {
        regex = mmdScalarField.filter.regex;
        replace = mmdScalarField.filter.replace;
        if (replace != "") //We must replace all newlines if the replacement is not a empty character
            stringValue = stringValue.replace(new RegExp('\n', 'g'), "");
        else
            regex += "|\n";
    }

    stringValue = stringValue.trim().replace(new RegExp(regex, 'g'), replace);

    metadata[mmdScalarField.name] = stringValue;
}

function extractCollection(mmdCollectionField, contextNode, metadata)
{
    console.log("Setting Collection: " + mmdCollectionField.name);
    //nodeList is a XPathResult type, access items with snapshotItem(index)
    var nodeList = getNodeListWithXPath(contextNode, mmdCollectionField.xpath);
    var metadataCollection = [];

    //Must take care of unwrapped collections. 
    //Must be an object with name = mmdField.child_type
    //Collections will always have 1 composite kid 
    //whose children need to be recursed over 
		// WEIRD. JAVA->JSON produced a struction that required mmdCollectionField.kids[0].composite.kids
		// C#->JSON produces a more 'correct' looking structure that requirs mmdCollectionField.kids ...
    // for each result 
    var collectionFields = mmdCollectionField.kids;
    if (collectionFields == null || collectionFields == undefined)
        console.log("Oops, collection fields doesn't exist");
    for (var resultIndex = 0; resultIndex < nodeList.snapshotLength; resultIndex++) {
        console.log("\tCollection Result Index: " + resultIndex);
        //recursive call.
        var collectionItemMetadata = {};
        var recursiveContext = nodeList.snapshotItem(resultIndex);
        for (var fieldIndex = 0; fieldIndex < collectionFields.length; fieldIndex++) {
            var recursiveField = collectionFields[fieldIndex];
            console.log("\tCollection Recursive Call: ");
            console.log(recursiveField);
            if (recursiveField.scalar != null)
                extractScalar(recursiveField.scalar, recursiveContext, collectionItemMetadata);
            else if (recursiveField.collection != null)
                extractCollection(recursiveField.collection, recursiveContext, collectionItemMetadata);
            else if (recursiveField.composite != null)
                extractComposite(recursiveField.composite, recursiveContext, collectionItemMetadata);
            console.log("Metadata Collection Item: ");
            console.info(collectionItemMetadata);
        }
        metadataCollection.push(collectionItemMetadata);
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
