using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public interface IGraphView<TVertexKey, TVertexValue, TEdgeValue, TVertexView> 
        where TVertexView : IVertexView<TVertexKey, TVertexValue, TEdgeValue>
    {
        /// <summary>
        /// The graph that the view is related to.
        /// </summary>
        /// <value></value>
        IGraph<TVertexKey, TVertexValue, TEdgeValue> Graph { get; set; }
        
        /// <summary>
        /// The vertex view of the specified vertex.
        /// </summary>
        /// <value></value>
        IVertexView<TVertexKey, TVertexValue, TEdgeValue> this [TVertexKey vertexKeyFrom] { get; }

        /// <summary>
        /// The vertex views of each vertex in the graph.
        /// </summary>
        /// <returns></returns>
        IEnumerator<IVertexView<TVertexKey, TVertexValue, TEdgeValue>> GetEnumerator();
    }
}