using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using ecologylab.semantics.documentparsers;
using ecologylab.semantics.metadata.builtins;
using ecologylab.semantics.metadata.scalar.types;
using ecologylab.semantics.metametadata;
using Simpl.Fundamental.Collections;
using System.Net;

namespace ecologylab.semantics.collecting
{
    public class SemanticsSessionScope : SemanticsGlobalScope
    {
        public SemanticsSessionScope(SimplTypesScope metadataTranslationScope, string repoLocation)
            : base(metadataTranslationScope, repoLocation)
        {
            DownloadMonitor = new DownloadMonitor(this);
        }

        public DownloadMonitor DownloadMonitor { get; private set; }

        public override Document GetOrConstructDocument(ParsedUri location)
        {
            Document doc = base.GetOrConstructDocument(location);
            doc.SemanticsSessionScope = this;
            return doc;
        }

        public async Task<Document> GetDocument(ParsedUri puri)
        {
            if (puri == null)
            {
                Console.WriteLine("Error: empty URL provided.");
                return null;
            }

            Document doc = GetOrConstructDocument(puri);
            DocumentClosure closure = new DocumentClosure(this, doc);
            return await closure.PerformDownload();
        }

        public async static Task<SemanticsSessionScope> InitAsync(SimplTypesScope metadataTranslationScope, string repoLocation)
        {
            return await TaskEx.Run(() => new SemanticsSessionScope(metadataTranslationScope, repoLocation));
        }
    }
}
