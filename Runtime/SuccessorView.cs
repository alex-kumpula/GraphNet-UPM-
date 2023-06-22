using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public class SuccessorView<TVertexKey, TVertexValue, TEdgeValue> : IDirectionalView<TVertexKey, TVertexValue, TEdgeValue>
    {
        private IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> _graph;
        private TVertexKey _vertexKey;

        public SuccessorView(IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> graph, TVertexKey vertexKey)
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
            get { return this.Graph.GetSuccessorKeys(this.Key); }
        }

        public IEnumerable<TVertexValue> Values
        {
            get { return this.Graph.GetSuccessorValues(this.Key); }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> Vertices
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
            get { return this.Graph.GetSuccessorEdgeValues(this.Key); }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> Edges
        {
            get { return this.Graph.GetSuccessorEdges(this.Key); }
        }

        public TEdgeValue this [TVertexKey vertexKeyTo]
        {
            get { return this.Graph.GetEdgeValue(this.Key, vertexKeyTo); }
            set { this.Graph.AddEdge(this.Key, vertexKeyTo, value); }
        }

        public IEnumerator<KeyValuePair<TVertexKey, TEdgeValue>> GetEnumerator()
        {
            return this.Graph.GetSuccessorEdges(this.Key).GetEnumerator();
        }
    }
}