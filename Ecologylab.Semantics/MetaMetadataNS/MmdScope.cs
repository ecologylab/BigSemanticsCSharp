using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Fundamental.Collections;
using Simpl.Serialization.Attributes;
using Simpl.Serialization.Types.Element;

namespace Ecologylab.Semantics.MetaMetadataNS
{
    public class MmdScope : IDictionary<String, MetaMetadata>, IMappable<String>
    {
        [SimplScalar] 
        private String name;

        [SimplMap("mmd")] 
        private MultiAncestorScope<MetaMetadata> mmds;

        public MmdScope()
        {
            mmds = new MultiAncestorScope<MetaMetadata>();
        }

        public MmdScope(params IDictionary<String, MetaMetadata>[] ancestors)
        {
            mmds = new MultiAncestorScope<MetaMetadata>(ancestors);
        }

        public IEnumerator<KeyValuePair<string, MetaMetadata>> GetEnumerator()
        {
            return mmds.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, MetaMetadata> item)
        {
            mmds.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            mmds.Clear();
        }

        public bool Contains(KeyValuePair<string, MetaMetadata> item)
        {
            return mmds.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, MetaMetadata>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, MetaMetadata> item)
        {
            return mmds.Remove(item.Key);
        }

        public int Count { 
            get { return mmds.Count; }
        }

        public bool IsReadOnly { get; private set; }
        
        public void Add(string key, MetaMetadata value)
        {
            mmds.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return mmds.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return mmds.Remove(key);
        }

        public bool TryGetValue(string key, out MetaMetadata value)
        {
            return mmds.TryGetValue(key, out value);
        }

        public MetaMetadata this[string key]
        {
            get { return mmds[key]; }
            set { mmds[key] = value; }
        }

        public ICollection<string> Keys 
        {
            get { return mmds.Keys; }
        }

        public ICollection<MetaMetadata> Values
        {
            get { return mmds.Values; }
        }

        public string Key()
        {
            return name;
        }
    }
}
