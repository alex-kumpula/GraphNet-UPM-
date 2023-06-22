using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public class DirectedGraph<TVertexKey, TVertexValue, TEdgeValue> : IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        protected IDictionary<TVertexKey, TVertexValue> _vertices;
        protected IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _successors;
        protected IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _predecessors;

        protected virtual IDictionary<TVertexKey, TVertexValue> _Vertices
        {
            get { return this._vertices; }
            set { this._vertices = value; }
        }

        protected virtual IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _Successors
        {
            get { return this._successors; }
            set { this._successors = value; }
        }

        protected virtual IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _Predecessors
        {
            get { return this._predecessors; }
            set { this._predecessors = value; }
        }

        public DirectedGraph()
        {
            this._vertices = VertexDictionaryFactory();
            this._successors = GraphDictionaryFactory();
            this._predecessors = GraphDictionaryFactory();
        }

        // public DirectedGraph(SerializationInfo info, StreamingContext context) : this()
        // {
        //     this._vertices = info.GetValue("Vertices", typeof(IDictionary<TVertexKey, TVertexValue>)) as IDictionary<TVertexKey, TVertexValue>;
        //     this._successors = info.GetValue("Successors", typeof(IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>>)) as IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>>;

        //     // Reconstruct _predecessors from _successors
        //     foreach (var vertexFrom in this._successors)
        //     {
        //         foreach (var vertexTo in vertexFrom.Value)
        //         {
        //             this._predecessors.TryAdd<TVertexKey, IDictionary<TVertexKey, TEdgeValue>>(vertexTo.Key, this.EdgeDictionaryFactory());
        //             this._predecessors[vertexTo.Key][vertexFrom.Key] = this._successors[vertexFrom.Key][vertexTo.Key];
        //         }
        //     }
        // }

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

        public virtual IEnumerator<KeyValuePair<TVertexKey, TVertexValue>> GetEnumerator()
        {
            return this.Vertices.GetEnumerator();
        }

        public virtual void AddVertex(TVertexKey vertexKey, TVertexValue vertexValue)
        {
            this._vertices[vertexKey] = vertexValue;
            this._successors[vertexKey] = this.EdgeDictionaryFactory();
            this._predecessors[vertexKey] = this.EdgeDictionaryFactory();
        }

        public virtual void AddEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo, TEdgeValue edgeValue)
        {
            if ((this.VertexExists(vertexKeyFrom) || this.VertexExists(vertexKeyTo)) == false)
            {
                throw new System.Exception("Both vertices must exist before making an edge!");
            }
            this._successors[vertexKeyFrom][vertexKeyTo] = edgeValue;
            this._predecessors[vertexKeyTo][vertexKeyFrom] = this._successors[vertexKeyFrom][vertexKeyTo];
        }

        public virtual void RemoveVertex(TVertexKey vertexKey)
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

        public virtual void RemoveEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo)
        {
            this._successors[vertexKeyFrom].Remove(vertexKeyTo);
            this._predecessors[vertexKeyTo].Remove(vertexKeyFrom);
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
            return this._successors.ContainsKey(vertexKeyFrom) && this._successors[vertexKeyFrom].ContainsKey(vertexKeyTo);
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
            return this._successors[vertexKeyFrom][vertexKeyTo];
        }

        public virtual IDirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> this[TVertexKey vertexKey] 
        {
            get { return new DirectedVertexView<TVertexKey, TVertexValue, TEdgeValue>(this, vertexKey); }
        }

        public virtual IEnumerable<TVertexKey> GetSuccessorKeys(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this._successors[vertexKeyFrom].Keys)
            {
                yield return successorKey;
            }
        }

        public virtual IEnumerable<TVertexKey> GetPredecessorKeys(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this._predecessors[vertexKeyTo].Keys)
            {
                yield return predecessorKey;
            }
        }

        public virtual IEnumerable<TVertexValue> GetSuccessorValues(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this._successors[vertexKeyFrom].Keys)
            {
                yield return this.GetVertexValue(successorKey);
            }
        }

        public virtual IEnumerable<TVertexValue> GetPredecessorValues(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this._predecessors[vertexKeyTo].Keys)
            {
                yield return this.GetVertexValue(predecessorKey);
            }
        }

        public virtual IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetSuccessors(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this._successors[vertexKeyFrom].Keys)
            {
                TVertexValue successorValue = this.GetVertexValue(successorKey);
                yield return new KeyValuePair<TVertexKey, TVertexValue>(vertexKeyFrom, successorValue);
            }
        }

        public virtual IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetPredecessors(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this._predecessors[vertexKeyTo].Keys)
            {
                TVertexValue predecessorValue = this.GetVertexValue(predecessorKey);
                yield return new KeyValuePair<TVertexKey, TVertexValue>(vertexKeyTo, predecessorValue);
            }
        }

        public virtual IEnumerable<TEdgeValue> GetSuccessorEdgeValues(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this.GetSuccessorKeys(vertexKeyFrom))
            {
                yield return this.GetEdgeValue(vertexKeyFrom, successorKey);
            }
        }

        public virtual IEnumerable<TEdgeValue> GetPredecessorEdgeValues(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this.GetPredecessorKeys(vertexKeyTo))
            {
                yield return this.GetEdgeValue(predecessorKey, vertexKeyTo);
            }
        }

        public virtual IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetSuccessorEdges(TVertexKey vertexKeyFrom)
        {
            foreach (TVertexKey successorKey in this.GetSuccessorKeys(vertexKeyFrom))
            {
                TEdgeValue successorEdgeValue = this.GetEdgeValue(vertexKeyFrom, successorKey);
                yield return new KeyValuePair<TVertexKey, TEdgeValue>(vertexKeyFrom, successorEdgeValue);
            }
        }

        public virtual IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetPredecessorEdges(TVertexKey vertexKeyTo)
        {
            foreach (TVertexKey predecessorKey in this.GetPredecessorKeys(vertexKeyTo))
            {
                TEdgeValue predecessorEdgeValue = this.GetEdgeValue(predecessorKey, vertexKeyTo);
                yield return new KeyValuePair<TVertexKey, TEdgeValue>(vertexKeyTo, predecessorEdgeValue);
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