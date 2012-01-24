using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simpl.Fundamental.Generic;
using Simpl.Fundamental.Net;
using ecologylab.semantics.metadata;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metametadata;

namespace ecologylab.semantics.collecting
{
    public class SemanticsGlobalCollection<D> where D : Document
    {
        private Dictionary<ParsedUri, D> _collection = new Dictionary<ParsedUri, D>();

        private Dictionary<ParsedUri, D> Collection
        {
            get { return _collection; }
        }

        public void Remap(D oldDoc, D newDoc)
        {
            var location = oldDoc.Location;
            if (location != null)
            {
                Collection[location.Value] = newDoc;
            }
            var newLocation = newDoc.Location;
            if (newLocation != null)
            {
                Collection[newLocation.Value] = newDoc; // to make sure
            }
        }

        public void TryGetDocument(ParsedUri puri, out D result)
        {
            _collection.TryGetValue(puri, out result);
        }

        public void AddDocument(D doc, ParsedUri location)
        {
            if (doc == null || location == null)
            {
                throw new InvalidOperationException("Cannot add to SemanticsGlobalCollection");

            }
            Collection.Put(location, doc);
        }
    }
}
