using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace Spundio.GraphNet.Serialization
{
    [DataContract]
    public class SerializableDirectedGraph<TVertexKey, TVertexValue, TEdgeValue> : DirectedGraph<TVertexKey, TVertexValue, TEdgeValue>
    {
        [DataMember]
        protected override IDictionary<TVertexKey, TVertexValue> _Vertices { get => base._Vertices; set => base._Vertices = value; }

        [DataMember]
        protected override IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _Successors { get => base._Successors; set => base._Successors = value; }

        // [DataMember]
        // protected override IDictionary<TVertexKey, IDictionary<TVertexKey, TEdgeValue>> _Predecessors { get => base._Predecessors; set => base._Predecessors = value; }
    
        [OnDeserialized]
        protected void OnDeserialized(StreamingContext context)
        {
            foreach (var vertexFrom in this._successors)
            {
                foreach (var vertexTo in vertexFrom.Value)
                {
                    this._predecessors.TryAdd<TVertexKey, IDictionary<TVertexKey, TEdgeValue>>(vertexTo.Key, this.EdgeDictionaryFactory());
                    this._predecessors[vertexTo.Key][vertexFrom.Key] = this._successors[vertexFrom.Key][vertexTo.Key];
                }
            }
        }
    }
}