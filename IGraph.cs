using System.Collections.Generic;

namespace GraphNet
{
    public interface IGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        IEnumerable<TVertexKey> Keys { get; }
        IEnumerable<TVertexValue> Values { get; }
        IEnumerable<KeyValuePair<TVertexKey, TVertexValue>> Vertices { get; }

        IEnumerator<KeyValuePair<TVertexKey, TVertexValue>> GetEnumerator();

        void AddVertex(TVertexKey vertexKey, TVertexValue vertexValue);
        void AddEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo, TEdgeValue edgeValue);
        void RemoveVertex(TVertexKey vertexKey);
        void RemoveEdge(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo);
        
        bool VertexExists(TVertexKey vertexKey);
        bool EdgeExists(TVertexKey vertexKeyFrom, TVertexKey vertexKeyTo);

        TVertexValue GetVertexValue(TVertexKey vertexKey);
    }


}