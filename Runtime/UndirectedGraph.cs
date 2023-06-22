using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public class UndirectedGraph<TVertexKey, TVertexValue, TEdgeValue> : IUndirectedGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        protected IDictionary<TVertexKey, TVertexValue> _vertices;
        protected IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _edges;

        public UndirectedGraph()
        {
            this._vertices = this.VertexDictionaryFactory();
            this._edges = this.GraphDictionaryFactory();
        }

        public virtual IEnumerable<TVertexKey> Keys
        {
            get
            {
                foreach (TVertexKey vertexKey in this._vertices.Keys)
                {
                    yield return vertexKey;
                }
            }
        }

        public virtual IEnumerable<TVertexValue> Values
        {
            get
            {
                foreach (TVertexValue vertexValue in this._vertices.Values)
                {
                    yield return vertexValue;
                }
            }
        }

        public virtual IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> Vertices
        {
            get
            {
                foreach (KeyValuePair<TVertexKey, TVertexValue> vertex in this._vertices)
                {
                    yield return vertex;
                }
            }
        }

        public virtual IUndirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> this[TVertexKey vertexKeyFrom]
        {
            get
            {
                return new UndirectedVertexView<TVertexKey, TVertexValue, TEdgeValue>(this, vertexKeyFrom);
            }
        }

        public virtual IEnumerator<KeyValuePair<TVertexKey, TVertexValue>> GetEnumerator()
        {
            return this.Vertices.GetEnumerator();
        }

        public virtual void AddVertex(TVertexKey vertexKey, TVertexValue vertexValue)
        {
            this._vertices[vertexKey] = vertexValue;
            this._edges[vertexKey] = this.EdgeDictionaryFactory();
        }

        public virtual void AddEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo, TEdgeValue edgeValue)
        {
            if ((this.VertexExists(vertexKeyFrom) || this.VertexExists(vertexKeyTo)) == false)
            {
                throw new System.Exception("Both vertices must exist before making an edge!");
            }
            this._edges[vertexKeyFrom][vertexKeyTo] = edgeValue;
            this._edges[vertexKeyTo][vertexKeyFrom] = this._edges[vertexKeyFrom][vertexKeyTo];
        }

        public virtual void RemoveVertex(TVertexKey vertexKey)
        {
            foreach (TVertexKey vertexKeyTo in this[vertexKey].Keys)
            {
                this.RemoveEdge(vertexKey, vertexKeyTo);
            }
            this._edges.Remove(vertexKey);
            this._vertices.Remove(vertexKey);
        }

        public virtual void RemoveEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo)
        {
            this._edges[vertexKeyFrom].Remove(vertexKeyTo);
            this._edges[vertexKeyFrom].Remove(vertexKeyFrom);
        }

        public virtual TVertexValue GetVertexValue(TVertexKey vertexKey)
        {
            if (this.VertexExists(vertexKey) == false)
            {
                throw new KeyNotFoundException();
            }
            return this._vertices[vertexKey];
        }

        public virtual TEdgeValue GetEdgeValue(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo)
        {
            if (this.EdgeExists(vertexKeyFrom, vertexKeyTo) == false)
            {
                throw new KeyNotFoundException();
            }
            return this._edges[vertexKeyFrom][vertexKeyTo];
        }

        public virtual bool VertexExists(TVertexKey vertexKey)
        {
            return this._vertices.ContainsKey(vertexKey);
        }

        public virtual bool EdgeExists(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo)
        {
            if ((this.VertexExists(vertexKeyFrom) && this.VertexExists(vertexKeyTo)) == false)
            {
                return false;
            }
            return this._edges.ContainsKey(vertexKeyFrom) && this._edges[vertexKeyFrom].ContainsKey(vertexKeyTo);
        }

        public virtual IEnumerable<TVertexKey> GetVertexKeys(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                yield return vertexKeyTo;
            }
        }

        public virtual IEnumerable<TVertexValue> GetVertexValues(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                yield return this.GetVertexValue(vertexKeyTo);
            }
        }

        public virtual IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetVertices(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                TVertexValue vertexValueTo = this.GetVertexValue(vertexKeyTo);
                yield return new KeyValuePair<TVertexKey, TVertexValue>(vertexKeyTo, vertexValueTo);
            }
        }
        
        public virtual IEnumerable<TEdgeValue> GetEdgeValues(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                yield return this.GetEdgeValue(vertexKeyFrom, vertexKeyTo);
            }
        }

        public virtual IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetEdges(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey vertexKeyTo in this._edges[vertexKeyFrom].Keys)
            {
                TEdgeValue edgeValue = this.GetEdgeValue(vertexKeyFrom, vertexKeyTo);
                yield return new KeyValuePair<TVertexKey, TEdgeValue>(vertexKeyTo, edgeValue);
            }
        }
    
        public virtual IDictionary<TVertexKey, TVertexValue> VertexDictionaryFactory()
        {
            return new Dictionary<TVertexKey, TVertexValue>();
        }

        public virtual IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> GraphDictionaryFactory()
        {
            return new Dictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>>();
        }

        public virtual IDictionary<TVertexKey, TEdgeValue> EdgeDictionaryFactory()
        {
            return new Dictionary<TVertexKey, TEdgeValue>();
        }
    }
}