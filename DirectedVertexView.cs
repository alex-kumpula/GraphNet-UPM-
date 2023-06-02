namespace GraphNet
{
    public class DirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> : IDirectedVertexView<TVertexKey, TVertexValue, TEdgeValue>
    {
        private IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> _graph;
        private TVertexKey _vertexKey;

        public DirectedVertexView(IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> graph, TVertexKey vertexKey)
        {
            this._graph = graph;
            this._vertexKey = vertexKey;
        }

        public TEdgeValue this [TVertexKey vertexKeyTo]
        {
            get { return this.Successors[vertexKeyTo]; }
            set { this.Successors[vertexKeyTo] = value; }
        }

        public IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> Graph 
        {
            get { return this._graph; }
            set { this._graph = value; }
        }

        public TVertexKey Key
        {
            get { return this._vertexKey; }
            set { this._vertexKey = value; }
        }

        public TVertexValue Value
        {
            get { return this.Graph.GetVertexValue(this.Key); }
            set { this.Graph.AddVertex(this.Key, value); }
        }

        public IDirectionalView<TVertexKey, TVertexValue, TEdgeValue> Successors
        {
            get { return new SuccessorView<TVertexKey, TVertexValue, TEdgeValue>(this.Graph, this.Key); }
        }

        public IDirectionalView<TVertexKey, TVertexValue, TEdgeValue> Predecessors
        {
            get { return new PredecessorView<TVertexKey, TVertexValue, TEdgeValue>(this.Graph, this.Key); }
        }
    }


}