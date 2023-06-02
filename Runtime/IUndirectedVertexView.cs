using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public interface IUndirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> : IVertexView<TVertexKey, TVertexValue, TEdgeValue>
    {
        IUndirectedGraph<TVertexKey, TVertexValue, TEdgeValue> Graph { get; set; }

        IEnumerable<TVertexKey> Keys { get; }
        IEnumerable<TVertexValue> Values { get; }

        IEnumerable<TEdgeValue> EdgeValues { get; }
        IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> Edges { get; }

        TEdgeValue this [TVertexKey vertexKeyTo] { get; set; }
        IEnumerator<KeyValuePair<TVertexKey, TEdgeValue>> GetEnumerator();
    }


}