using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public interface IDirectionalView<TVertexKey, TVertexValue, TEdgeValue>
    {
        IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> Graph { get; set; }
        TVertexKey Key { get; }
        TVertexValue Value { get; set; }
        KeyValuePair<TVertexKey, TVertexValue> Vertex { get; }

        IEnumerable<TVertexKey> Keys { get; }
        IEnumerable<TVertexValue> Values { get; }
        IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> Vertices { get; }


        IEnumerable<TEdgeValue> EdgeValues { get; }
        IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> Edges { get; }

        TEdgeValue this [TVertexKey vertexKey] { get; set; }
        IEnumerator<KeyValuePair<TVertexKey, TEdgeValue>> GetEnumerator();
    }
}