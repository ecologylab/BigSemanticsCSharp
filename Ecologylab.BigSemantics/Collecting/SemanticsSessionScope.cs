using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ecologylab.BigSemantics.MetadataNS;
using Ecologylab.BigSemantics.MetadataNS.Builtins;
using Ecologylab.BigSemantics.Services;
using Simpl.Fundamental.Net;
using Simpl.Serialization;
using Ecologylab.BigSemantics.Documentparsers;
using Ecologylab.BigSemantics.MetadataNS.Scalar.Types;
using Ecologylab.BigSemantics.MetaMetadataNS;
using Simpl.Fundamental.Collections;
using System.Net;

namespace Ecologylab.BigSemantics.Collecting
{
    public class SemanticsSessionScope : SemanticsGlobalScope
    {

        private readonly DispatcherDelegate _extractionDelegate;

        public ParsedUri MetadataServiceUri { get; set; }

        public HttpClient HttpClient { get; set; }

        public delegate void DispatcherDelegate(DocumentClosure closure);

        public SemanticsSessionScope(SimplTypesScope metadataTranslationScope, string repoLocation, ParsedUri serviceUri,
            EventHandler<EventArgs> onCompleted)
            : base(metadataTranslationScope, repoLocation, onCompleted)
        {
            SemanticsSessionScope.Get = this;

            MetadataServiceUri = serviceUri;
            HttpClient = new HttpClient();
        }

        public DownloadMonitor DownloadMonitor { get; private set; }

        public override Document GetOrConstructDocument(ParsedUri location)
        {
            Document doc = base.GetOrConstructDocument(location);
            doc.SemanticsSessionScope = this;
            return doc;
        }

        public async new Task<Document> GetDocument(ParsedUri puri)
        {
            if (puri == null)
            {
                Debug.WriteLine("Error: empty URL provided.");
                return null;
            }

            var doc = base.GetDocument(puri);
            if (doc == null)
            {
                var response = await HttpClient.GetAsync(new Uri(MetadataServiceUri, "metadata.json?url=" + puri.AbsoluteUri));
                if (response.IsSuccessStatusCode)
                {
                    doc = this.MetadataTranslationScope.Deserialize(await response.Content.ReadAsStreamAsync(), Format.Json) as Document;
                }
            }

            return doc;
        }

        public async static Task<SemanticsSessionScope> InitAsync(SimplTypesScope metadataTranslationScope, string repoLocation, ParsedUri serviceUri)
        {
            var scope = await Task.Run(() => new SemanticsSessionScope(metadataTranslationScope, repoLocation, serviceUri, null));
            return scope;
        }

        public static SemanticsSessionScope Get
        {
            get; set;
        }
    }
}
