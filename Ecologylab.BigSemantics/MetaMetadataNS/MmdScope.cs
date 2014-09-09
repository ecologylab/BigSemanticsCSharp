using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpl.Fundamental.Collections;
using Simpl.Serialization.Attributes;
using Simpl.Serialization.Types.Element;

namespace Ecologylab.BigSemantics.MetaMetadataNS
{
    public class MmdScope : IDictionary<String, Object>, IMappable<String>
    {

        private const String NO_ID = "NO_ID";

        private static Dictionary<String, Object> EMPTY_HASH_MAP = new Dictionary<String, object>();

        [SimplScalar]
        private String id;

        [SimplMap("element")]
        [SimplClasses(typeof(MetaMetadata), typeof(MmdGenericTypeVar))]
        private Dictionary<String, Object> local;

        private List<MmdScope> ancestors;

        public MmdScope() : this(NO_ID, null) { }

        public MmdScope(String id) : this(id, null) { }

        public MmdScope(params MmdScope[] ancestors) : this(NO_ID, ancestors) { }

        public MmdScope(String id, params MmdScope[] ancestors)
        {
            this.id = id;
            addAncestors(ancestors);
        }

        public String Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public string Key()
        {
            return id;
        }

        protected List<MmdScope> Ancestors()
        {
            if (ancestors == null)
            {
                ancestors = new List<MmdScope>();
            }
            return ancestors;
        }

        protected List<MmdScope> allAncestors()
        {
            List<MmdScope> result = new List<MmdScope>();
            allAncestorsHelper(result, this);
            return result;
        }

        private void allAncestorsHelper(List<MmdScope> result, MmdScope scope)
        {
            if (scope.ancestors != null)
            {
                foreach (MmdScope ancestor in scope.ancestors)
                {
                    if (!result.Contains(ancestor))
                    {
                        result.Add(ancestor);
                        allAncestorsHelper(result, ancestor);
                    }
                }
            }
        }

        public bool isAncestor(MmdScope scope)
        {
            return ancestors == null ? false : allAncestors().Contains(scope);
        }

        public void addAncestor(MmdScope ancestor)
        {
            if (ancestor != null && ancestor != this && !isAncestor(ancestor))
            {
                this.Ancestors().Add(ancestor);
            }
        }

        public void addAncestors(params MmdScope[] ancestors)
        {
            if (ancestors != null)
            {
                foreach (MmdScope ancestor in ancestors)
                {
                    this.addAncestor(ancestor);
                }
            }
        }

        public void removeImmediateAncestor(MmdScope ancestor)
        {
            if (ancestors != null)
            {
                ancestors.Remove(ancestor);
            }
        }

        public ICollection<String> Keys
        {
            get { return local == null ? EMPTY_HASH_MAP.Keys : local.Keys; }
        }

        public ICollection<Object> Values
        {
            get { return local == null ? EMPTY_HASH_MAP.Values : local.Values; }
        }

        public Object this[String key]
        {
            get
            {
                if (key != null)
                {
                    if (ContainsKeyLocally(key))
                    {
                        return local[key];
                    }
                    if (ancestors != null)
                    {
                        foreach (MmdScope ancestor in allAncestors())
                        {
                            if (ancestor.ContainsKeyLocally(key))
                            {
                                return ancestor.GetLocally(key);
                            }
                        }
                    }
                }
                return null;
            }
            set
            {
                if (key != null)
                {
                    if (local == null)
                    {
                        local = new Dictionary<String, Object>();
                    }
                    local[key] = value;
                }
            }
        }

        public void Add(String key, Object value)
        {
            if (local != null)
            {
                local.Add(key, value);
            }
        }

        public bool Remove(String key)
        {
            return local == null ? false : key == null ? false : local.Remove(key);
        }

        public bool TryGetValue(String key, out Object value)
        {
            if (local == null)
            {
                value = null;
                return false;
            }
            return local.TryGetValue(key, out value);
        }

        public int Count
        {
            get { return local == null ? 0 : local.Count; }
        }

        public bool IsReadOnly { get; private set; }

        public void Add(KeyValuePair<String, Object> item)
        {
            if (local == null)
            {
                local = new Dictionary<String, Object>();
            }
            local.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            local = null;
        }

        public bool Contains(KeyValuePair<String, Object> item)
        {
            return local == null ? false : local.Contains(item);
        }

        public void CopyTo(KeyValuePair<String, Object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<String, Object> item)
        {
            return local == null ? false : local.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<String, Object>> GetEnumerator()
        {
            return local.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsEmpty
        {
            get { return local == null ? true : local.Count == 0; }
        }


        public bool ContainsKey(String key)
        {
            if (key != null)
            {
                if (local != null && local.ContainsKey(key))
                {
                    return true;
                }
                if (ancestors != null)
                {
                    foreach (MmdScope ancestor in allAncestors())
                    {
                        if (ancestor.ContainsKeyLocally(key))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool ContainsValue(Object value)
        {
            if (local != null && local.ContainsValue(value))
            {
                return true;
            }
            if (ancestors != null)
            {
                foreach (MmdScope ancestor in allAncestors())
                {
                    if (ancestor.ContainsValueLocally((Object)value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ContainsKeyLocally(String key)
        {
            return local == null ? false : key == null ? false : local.ContainsKey(key);
        }

        public bool ContainsValueLocally(Object value)
        {
            return local == null ? false : local.ContainsValue(value);
        }

        public Object GetLocally(String key)
        {
            return local == null ? null : key == null ? null : local[key];
        }

        public List<Object> GetAll(String key)
        {
            List<Object> result = new List<Object>();
            if (key != null)
            {
                if (local != null && local.ContainsKey(key))
                {
                    result.Add(local[key]);
                }
                if (ancestors != null)
                {
                    foreach (MmdScope ancestor in allAncestors())
                    {
                        if (ancestor.ContainsKeyLocally(key))
                        {
                            Object value = ancestor.GetLocally(key);
                            result.Add(value);
                        }
                    }
                }
            }
            return result;
        }

        public ICollection<T> valuesOfType<T>()
        {
            List<T> result = new List<T>();
            foreach (Object obj in Values)
            {
                if (obj is T)
                {
                    result.Add((T)obj);
                }
            }
            return result;
        }

        public void PutIfValueNotNull(String key, Object value)
        {
            if (key != null && value != null)
            {
                local[key] = value;
            }
        }

        public void PutAll<T>(IDictionary<String, T> map)
        {
            if (local == null)
            {
                local = new Dictionary<String, Object>();
            }
            foreach (KeyValuePair<String, T> kvp in map)
            {
                local[kvp.Key] = kvp.Value;
            }
        }

        override public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            HashSet<Object> visited = new HashSet<Object>();
            ToStringHelper(sb, "", visited);
            String result = sb.ToString();
            return result;
        }

        private void ToStringHelper(StringBuilder buf, String indent, HashSet<Object> visited)
        {
            buf.Append(this.GetType().ToString());
            buf.Append(".").Append(id == null ? NO_ID : id);
            buf.Append(": [").Append(Count).Append("]");
            buf.Append(local == null ? "{}" : local.ToString());
            if (ancestors != null && ancestors.Count > 0)
            {
                foreach (MmdScope ancestor in ancestors)
                {
                    StringBuilder ancestorStr = new StringBuilder();
                    ancestorStr.Append("\n").Append(indent).Append("    -> ");
                    if (visited.Contains(ancestor))
                    {
                        ancestorStr.Append("(Ref: ");
                        ancestorStr.Append(ancestor.GetType().ToString());
                        ancestorStr.Append(".").Append(ancestor.Id).Append(")");
                    }
                    else
                    {
                        visited.Add(ancestor);
                        ancestor.ToStringHelper(ancestorStr, indent + "    ", visited);
                    }
                    buf.Append(ancestorStr);
                }
            }
        }

        public void Reset()
        {
            id = null;
            local = null;
            ancestors = null;
        }

    }

}
