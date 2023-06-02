using System.Collections.Generic;
using Newtonsoft.Json;

namespace Spundio.GraphNet
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DirectedGraph<TVertexKey, TVertexValue, TEdgeValue> : IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        [JsonProperty("Vertices")]
        private IDictionary<TVertexKey, TVertexValue> _vertices;
        [JsonProperty("Successors")]
        private IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _successors;
        [JsonProperty("Predecessors")]
        private IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _predecessors;

        public DirectedGraph()
        {
            this._vertices = VertexDictionaryFactory();
            this._successors = GraphDictionaryFactory();
            this._predecessors = GraphDictionaryFactory();
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

        public IEnumerator<KeyValuePair<TVertexKey, TVertexValue>> GetEnumerator()
        {
            return this.Vertices.GetEnumerator();
        }

        public virtual void AddVertex(TVertexKey vertexKey, TVertexValue vertexValue)
        {
            this._vertices[vertexKey] = vertexValue;
            this._successors[vertexKey] = this.EdgeDictionaryFactory();
            this._predecessors[vertexKey] = this.EdgeDictionaryFactory();
        }

        public void AddEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo, TEdgeValue edgeValue)
        {
            if ((this.VertexExists(vertexKeyFrom) || this.VertexExists(vertexKeyTo)) == false)
            {
                throw new System.Exception("Both verticies must exist before making an edge!");
            }
            this._successors[vertexKeyFrom][vertexKeyTo] = edgeValue;
            this._predecessors[vertexKeyTo][vertexKeyFrom] = this._successors[vertexKeyFrom][vertexKeyTo];
        }

        public void RemoveVertex(TVertexKey vertexKey)
        {
            foreach (TVertexKey vertexKeyTo in this.GetSuccessorKeys(vertexKey))
            {
                this.RemoveEdge(vertexKey, vertexKeyTo);
            }
            foreach (TVertexKey vertexKeyFrom in this.GetPredecessorKeys(vertexKey))
            {
                this.RemoveEdge(vertexKeyFrom, vertexKey);
            }
            this._vertices.Remove(vertexKey);
            this._successors.Remove(vertexKey);
            this._predecessors.Remove(vertexKey);
        }

        public void RemoveEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo)
        {
            this._successors[vertexKeyFrom].Remove(vertexKeyTo);
            this._predecessors[vertexKeyTo].Remove(vertexKeyFrom);
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
            return this._successors.ContainsKey(vertexKeyFrom) && this._successors[vertexKeyFrom].ContainsKey(vertexKeyTo);
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
            return this._successors[vertexKeyFrom][vertexKeyTo];
        }

        public IDirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> this[TVertexKey vertexKey] 
        {
            get { return new DirectedVertexView<TVertexKey, TVertexValue, TEdgeValue>(this, vertexKey); }
        }

        public IEnumerable<TVertexKey> GetSuccessorKeys(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this._successors[vertexKeyFrom].Keys)
            {
                yield return successorKey;
            }
        }

        public IEnumerable<TVertexKey> GetPredecessorKeys(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this._predecessors[vertexKeyTo].Keys)
            {
                yield return predecessorKey;
            }
        }

        public IEnumerable<TVertexValue> GetSuccessorValues(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this._successors[vertexKeyFrom].Keys)
            {
                yield return this.GetVertexValue(successorKey);
            }
        }

        public IEnumerable<TVertexValue> GetPredecessorValues(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this._predecessors[vertexKeyTo].Keys)
            {
                yield return this.GetVertexValue(predecessorKey);
            }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetSuccessors(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this._successors[vertexKeyFrom].Keys)
            {
                TVertexValue successorValue = this.GetVertexValue(successorKey);
                yield return new KeyValuePair<TVertexKey, TVertexValue>(vertexKeyFrom, successorValue);
            }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetPredecessors(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this._predecessors[vertexKeyTo].Keys)
            {
                TVertexValue predecessorValue = this.GetVertexValue(predecessorKey);
                yield return new KeyValuePair<TVertexKey, TVertexValue>(vertexKeyTo, predecessorValue);
            }
        }

        public IEnumerable<TEdgeValue> GetSuccessorEdgeValues(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this.GetSuccessorKeys(vertexKeyFrom))
            {
                yield return this.GetEdgeValue(vertexKeyFrom, successorKey);
            }
        }

        public IEnumerable<TEdgeValue> GetPredecessorEdgeValues(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this.GetPredecessorKeys(vertexKeyTo))
            {
                yield return this.GetEdgeValue(predecessorKey, vertexKeyTo);
            }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetSuccessorEdges(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this.GetSuccessorKeys(vertexKeyFrom))
            {
                TEdgeValue successorEdgeValue = this.GetEdgeValue(vertexKeyFrom, successorKey);
                yield return new KeyValuePair<TVertexKey, TEdgeValue>(vertexKeyFrom, successorEdgeValue);
            }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetPredecessorEdges(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this.GetPredecessorKeys(vertexKeyTo))
            {
                TEdgeValue predecessorEdgeValue = this.GetEdgeValue(predecessorKey, vertexKeyTo);
                yield return new KeyValuePair<TVertexKey, TEdgeValue>(vertexKeyTo, predecessorEdgeValue);
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