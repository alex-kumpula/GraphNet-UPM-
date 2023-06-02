using System.Collections.Generic;

namespace GraphNet
{
    public class PredecessorView<TVertexKey, TVertexValue, TEdgeValue> : IDirectionalView<TVertexKey, TVertexValue, TEdgeValue>
    {
        private IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> _graph;
        private TVertexKey _vertexKey;

        public PredecessorView(IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> graph, TVertexKey vertexKey)
        {
            this._graph = graph;
            this._vertexKey = vertexKey;
        }

        public IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> Graph 
        {
            get { return this._graph; }
            set { this._graph = value; }
        }

        public TVertexKey Key
        {
            get { return this._vertexKey; }
        }

        public TVertexValue Value
        {
            get { return this.Graph.GetVertexValue(this.Key); }
            set { this.Graph.AddVertex(this.Key, value); }
        }

        public KeyValuePair<TVertexKey, TVertexValue> Vertex
        {
            get { return new KeyValuePair<TVertexKey, TVertexValue>(this.Key, this.Value); }
        }

        public IEnumerable<TVertexKey> Keys
        {
            get { return this.Graph.GetPredecessorKeys(this.Key); }
        }

        public IEnumerable<TVertexValue> Values
        {
            get { return this.Graph.GetPredecessorValues(this.Key); }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> Verticies
        {
            get
            {
                foreach (TVertexKey vertexKey in this.Keys)
                {
                    TVertexValue vertexValue = this.Graph.GetVertexValue(vertexKey);
                    yield return new KeyValuePair<TVertexKey, TVertexValue>(vertexKey, vertexValue);
                }
            }
        }

        public IEnumerable<TEdgeValue> EdgeValues
        {
            get { return this.Graph.GetPredecessorEdgeValues(this.Key); }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> Edges
        {
            get { return this.Graph.GetPredecessorEdges(this.Key); }
        }

        public TEdgeValue this [TVertexKey vertexKeyFrom]
        {
            get { return this.Graph.GetEdgeValue(vertexKeyFrom, this.Key); }
            set { this.Graph.AddEdge(vertexKeyFrom, this.Key, value); }
        }

        public IEnumerator<KeyValuePair<TVertexKey, TEdgeValue>> GetEnumerator()
        {
            return this.Graph.GetPredecessorEdges(this.Key).GetEnumerator();
        }
    }
}