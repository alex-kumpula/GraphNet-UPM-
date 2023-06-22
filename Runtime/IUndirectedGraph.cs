using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public interface IUndirectedGraph<TVertexKey, TVertexValue, TEdgeValue> : IGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        /// <summary>
        /// The vertex view of the given vertex.
        /// </summary>
        /// <value></value>
        IUndirectedVertexView<TVertexKey, TVertexValue, TEdgeValue> this[TVertexKey vertexKeyFrom] { get; }

        /// <summary>
        /// Gets the value of the edge between two vertices.
        /// </summary>
        /// <param name="vertexKeyFrom">The key of the first vertex.</param>
        /// <param name="vertexKeyTo">The key of the second vertex.</param>
        /// <returns>The value of the edge.</returns>
        TEdgeValue GetEdgeValue(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo);

        /// <summary>
        /// Gets every vertex key that forms an edge
        /// with the given vertex key.
        /// </summary>
        /// <param name="vertexKeyFrom">The vertex key the edge starts with.</param>
        /// <returns>An enumerable of vertex keys.</returns>
        IEnumerable<TVertexKey> GetVertexKeys(TVertexKey vertexKeyFrom);

        /// <summary>
        /// Gets every vertex value of every vertex that forms an edge
        /// with the given vertex key.
        /// </summary>
        /// <param name="vertexKeyFrom">The vertex key the edge starts with.</param>
        /// <returns>An enumerable of vertex values.</returns>
        IEnumerable<TVertexValue> GetVertexValues(TVertexKey vertexKeyFrom);

        /// <summary>
        /// Gets every vertex that forms an edge
        /// with the given vertex key.
        /// </summary>
        /// <param name="vertexKeyFrom">The vertex key the edge starts with.</param>
        /// <returns>An enumerable of vertices (key, value).</returns>
        IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> GetVertices(TVertexKey vertexKeyFrom);
        
        /// <summary>
        /// Gets the value of every edge associated
        /// with the given vertex key.
        /// </summary>
        /// <param name="vertexKeyFrom">The vertex key the edge starts with.</param>
        /// <returns>An enumerable of edge values.</returns>
        IEnumerable<TEdgeValue> GetEdgeValues(TVertexKey vertexKeyFrom);

        /// <summary>
        /// Gets every edge associated
        /// with the given vertex key.
        /// </summary>
        /// <param name="vertexKeyFrom">The vertex key the edge starts with.</param>
        /// <returns>An enumerable of edges (key, value).</returns>
        IEnumerable<KeyValuePair<TVertexKey, TEdgeValue>> GetEdges(TVertexKey vertexKeyFrom);
    }
}