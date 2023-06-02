using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public class UndirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> : IUndirectedVertexView<TVertexKey, TVertexValue, TEdgeValue>
    {
        private IUndirectedGraph<TVertexKey, TVertexValue, TEdgeValue> _graph;
        private TVertexKey _vertexKey;

        public UndirectedVertexView(IUndirectedGraph<TVertexKey, TVertexValue, TEdgeValue> graph, TVertexKey vertexKey)
        {
            this._graph = graph;
            this._vertexKey = vertexKey;
        }

        public IUndirectedGraph<TVertexKey, TVertexValue, TEdgeValue> Graph 
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

        public IEnumerable<TVertexKey> Keys
        {
            get { return this.Graph.GetVertexKeys(this.Key); }
        }

        public IEnumerable<TVertexValue> Values
        {
            get { return this.Graph.GetVertexValues(this.Key); }
        }

        public IEnumerable<TEdgeValue> EdgeValues
        {
            get { return this.Graph.GetEdgeValues(this.Key); }
        }

        public IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> Edges
        {
            get { return this.Graph.GetEdges(this.Key); }
        }

        public TEdgeValue this [TVertexKey vertexKeyTo]
        {
            get { return this.Graph.GetEdgeValue(this.Key, vertexKeyTo); }
            set { this.Graph.AddEdge(this.Key, vertexKeyTo, value); }
        }

        public IEnumerator<KeyValuePair<TVertexKey, TEdgeValue>> GetEnumerator()
        {
            return this.Edges.GetEnumerator();
        }
    }


}