using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public interface IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> : IGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        IDirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> this[TVertexKey vertexKey] { get; }

        TEdgeValue GetEdgeValue(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo);

        IEnumerable<TVertexKey> GetSuccessorKeys(TVertexKey vertexKeyFrom);
        IEnumerable<TVertexKey> GetPredecessorKeys(TVertexKey vertexKeyTo);

        IEnumerable<TVertexValue> GetSuccessorValues(TVertexKey vertexKeyFrom);
        IEnumerable<TVertexValue> GetPredecessorValues(TVertexKey vertexKeyTo);

        IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetSuccessors(TVertexKey vertexKeyFrom);
        IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetPredecessors(TVertexKey vertexKeyTo);

        IEnumerable<TEdgeValue> GetSuccessorEdgeValues(TVertexKey vertexKeyFrom);
        IEnumerable<TEdgeValue> GetPredecessorEdgeValues(TVertexKey vertexKeyTo);

        IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetSuccessorEdges(TVertexKey vertexKeyFrom);
        IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetPredecessorEdges(TVertexKey vertexKeyTo);
    }


}