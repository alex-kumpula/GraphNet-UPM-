using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public interface IGraphView<TVertexKey, TVertexValue, TEdgeValue, TVertexView> 
        where TVertexView : IVertexView<TVertexKey, TVertexValue, TEdgeValue>
    {
        IGraph<TVertexKey, TVertexValue, TEdgeValue> Graph { get; set; }
        
        IVertexView<TVertexKey, TVertexValue, TEdgeValue> this [TVertexKey vertexKeyFrom] { get; }
        IEnumerator<IVertexView<TVertexKey, TVertexValue, TEdgeValue>> GetEnumerator();
    }


}