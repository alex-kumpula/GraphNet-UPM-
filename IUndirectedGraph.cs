using System.Collections.Generic;

namespace GraphNet
{
    public interface IUndirectedGraph<TVertexKey, TVertexValue, TEdgeValue> : IGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        IUndirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> this[TVertexKey vertexKeyFrom] { get; }

        TEdgeValue GetEdgeValue(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo);

        IEnumerable<TVertexKey> GetVertexKeys(TVertexKey vertexKeyFrom);
        IEnumerable<TVertexValue> GetVertexValues(TVertexKey vertexKeyFrom);
        IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetVerticies(TVertexKey vertexKeyFrom);
        
        IEnumerable<TEdgeValue> GetEdgeValues(TVertexKey vertexKeyFrom);
        IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetEdges(TVertexKey vertexKeyFrom);
    }


}