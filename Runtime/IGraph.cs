using System.Collections.Generic;

namespace Spundio.GraphNet
{
    public interface IGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        /// <summary>
        /// An enumerable of every vertex key in this graph.
        /// </summary>
        /// <value></value>
        IEnumerable<TVertexKey> Keys { get; }

        /// <summary>
        /// An enumerable of every vertex value in this graph.
        /// </summary>
        /// <value></value>
        IEnumerable<TVertexValue> Values { get; }

        /// <summary>
        /// An enumerable of every vertex (key, value) in this graph.
        /// </summary>
        /// <value></value>
        IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> Vertices { get; }

        /// <summary>
        /// An enumerator of every vertex (key, value) in this graph.
        /// </summary>
        /// <value></value>
        IEnumerator<KeyValuePair<TVertexKey, TVertexValue>> GetEnumerator();

        /// <summary>
        /// Adds a vertex to the graph.
        /// </summary>
        /// <param name="vertexKey">The key of the vertex.</param>
        /// <param name="vertexValue">The value of the vertex.</param>
        void AddVertex(TVertexKey vertexKey, TVertexValue vertexValue);

        /// <summary>
        /// Adds an edge between two verticies to the graph.
        /// </summary>
        /// <param name="vertexKeyFrom">The key of the first vertex.</param>
        /// <param name="vertexKeyTo">The key of the second vertex.</param>
        /// <param name="edgeValue">The value of the edge.</param>
        void AddEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo, TEdgeValue edgeValue);

        /// <summary>
        /// Removes a vertex from the graph.
        /// </summary>
        /// <param name="vertexKey">The key of the vertex to remove.</param>
        void RemoveVertex(TVertexKey vertexKey);

        /// <summary>
        /// Removes an edge between two verticies from the graph.
        /// </summary>
        /// <param name="vertexKeyFrom">The key of the first vertex.</param>
        /// <param name="vertexKeyTo">The key of the second vertex.</param>
        void RemoveEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo);
        
        /// <summary>
        /// Checks if a vertex exists in the graph.
        /// </summary>
        /// <param name="vertexKey">The key of the vertex to check for.</param>
        /// <returns>True if the vertex exists, else False.</returns>
        bool VertexExists(TVertexKey vertexKey);

        /// <summary>
        /// Checks if an edge between two verticies exists in the graph.
        /// </summary>
        /// <param name="vertexKeyFrom">The key of the first vertex.</param>
        /// <param name="vertexKeyTo">The key of the second vertex.</param>
        /// <returns>True if the edge exists, else False.</returns>
        bool EdgeExists(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo);

        /// <summary>
        /// Gets the value of the vertex at the given key.
        /// </summary>
        /// <param name="vertexKey">The key of the vertex.</param>
        /// <returns>The value of the vertex.</returns>
        TVertexValue GetVertexValue(TVertexKey vertexKey);
    }
}