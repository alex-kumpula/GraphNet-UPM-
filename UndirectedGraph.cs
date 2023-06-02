using System.Collections.Generic;
using Newtonsoft.Json;

namespace GraphNet
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UndirectedGraph<TVertexKey, TVertexValue, TEdgeValue> : IUndirectedGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        [JsonProperty("Vertices")]
        protected IDictionary<TVertexKey, TVertexValue> _vertices;
        [JsonProperty("Edges")]
        protected IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _edges;

        public UndirectedGraph()
        {
            this._vertices = this.VertexDictionaryFactory();
            this._edges = this.GraphDictionaryFactory();
        }

        public IEnumerable<TVertexKey> Keys
        {
            get
            {
                foreach (TVertexKey vertexKey in this._vertices.Keys)
                {
                    yield return vertexKey;
                }
            }
        }

        public IEnumerable<TVertexValue> Values
        {
            get
            {
                foreach (TVertexValue vertexValue in this._vertices.Values)
                {
                    yield return vertexValue;
                }
            }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> Vertices
        {
            get
            {
                foreach (KeyValuePair<TVertexKey, TVertexValue> vertex in this._vertices)
                {
                    yield return vertex;
                }
            }
        }

        public IUndirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> this[TVertexKey vertexKeyFrom]
        {
            get
            {
                return new UndirectedVertexView<TVertexKey, TVertexValue, TEdgeValue>(this, vertexKeyFrom);
            }
        }

        public IEnumerator<KeyValuePair<TVertexKey, TVertexValue>> GetEnumerator()
        {
            return this.Vertices.GetEnumerator();
        }

        public void AddVertex(TVertexKey vertexKey, TVertexValue vertexValue)
        {
            this._vertices[vertexKey] = vertexValue;
            this._edges[vertexKey] = this.EdgeDictionaryFactory();
        }

        public void AddEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo, TEdgeValue edgeValue)
        {
            if ((this.VertexExists(vertexKeyFrom) || this.VertexExists(vertexKeyTo)) == false)
            {
                throw new System.Exception("Both verticies must exist before making an edge!");
            }
            this._edges[vertexKeyFrom][vertexKeyTo] = edgeValue;
            this._edges[vertexKeyTo][vertexKeyFrom] = this._edges[vertexKeyFrom][vertexKeyTo];
        }

        public void RemoveVertex(TVertexKey vertexKey)
        {
            foreach (TVertexKey vertexKeyTo in this[vertexKey].Keys)
            {
                this.RemoveEdge(vertexKey, vertexKeyTo);
            }
            this._edges.Remove(vertexKey);
            this._vertices.Remove(vertexKey);
        }

        public void RemoveEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo)
        {
            this._edges[vertexKeyFrom].Remove(vertexKeyTo);
            this._edges[vertexKeyFrom].Remove(vertexKeyFrom);
        }

        public TVertexValue GetVertexValue(TVertexKey vertexKey)
        {
            if (this.VertexExists(vertexKey) == false)
            {
                throw new KeyNotFoundException();
            }
            return this._vertices[vertexKey];
        }

        public TEdgeValue GetEdgeValue(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo)
        {
            if (this.EdgeExists(vertexKeyFrom, vertexKeyTo) == false)
            {
                throw new KeyNotFoundException();
            }
            return this._edges[vertexKeyFrom][vertexKeyTo];
        }

        public bool VertexExists(TVertexKey vertexKey)
        {
            return this._vertices.ContainsKey(vertexKey);
        }

        public bool EdgeExists(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo)
        {
            if ((this.VertexExists(vertexKeyFrom) && this.VertexExists(vertexKeyTo)) == false)
            {
                return false;
            }
            return this._edges.ContainsKey(vertexKeyFrom) && this._edges[vertexKeyFrom].ContainsKey(vertexKeyTo);
        }

        public IEnumerable<TVertexKey> GetVertexKeys(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                yield return vertexKeyTo;
            }
        }

        public IEnumerable<TVertexValue> GetVertexValues(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                yield return this.GetVertexValue(vertexKeyTo);
            }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetVerticies(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                TVertexValue vertexValueTo = this.GetVertexValue(vertexKeyTo);
                yield return new KeyValuePair<TVertexKey, TVertexValue>(vertexKeyTo, vertexValueTo);
            }
        }
        
        public IEnumerable<TEdgeValue> GetEdgeValues(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                yield return this.GetEdgeValue(vertexKeyFrom, vertexKeyTo);
            }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetEdges(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                TEdgeValue edgeValue = this.GetEdgeValue(vertexKeyFrom, vertexKeyTo);
                yield return new KeyValuePair<TVertexKey, TEdgeValue>(vertexKeyTo, edgeValue);
            }
        }
    
        public IDictionary<TVertexKey, TVertexValue> VertexDictionaryFactory()
        {
            return new Dictionary<TVertexKey, TVertexValue>();
        }

        public IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> GraphDictionaryFactory()
        {
            return new Dictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>>();
        }

        public IDictionary<TVertexKey, TEdgeValue> EdgeDictionaryFactory()
        {
            return new Dictionary<TVertexKey, TEdgeValue>();
        }
    }


}