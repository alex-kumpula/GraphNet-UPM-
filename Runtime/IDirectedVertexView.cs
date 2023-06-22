namespace Spundio.GraphNet
{
    public interface IDirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> : IVertexView<TVertexKey, TVertexValue, TEdgeValue>
    {
        IDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> Graph { get; set; }

        IDirectionalView<TVertexKey, TVertexValue, TEdgeValue> Successors { get; }

        IDirectionalView<TVertexKey, TVertexValue, TEdgeValue> Predecessors { get; }

        TEdgeValue this [TVertexKey vertexKeyTo] { get; set; }
    }
}