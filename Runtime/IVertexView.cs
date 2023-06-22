namespace Spundio.GraphNet
{
    public interface IVertexView<TVertexKey, TVertexValue, TEdgeValue>
    {
        TVertexKey Key { get; }
        TVertexValue Value { get; set; }
    }
}