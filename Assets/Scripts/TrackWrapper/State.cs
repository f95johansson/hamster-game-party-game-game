using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]

[Serializable]
public struct State {

    [Serializable]
    public struct Entity
    {
        public float X, Y, Z;
        public float Hx, Hy, Hz;

        public Entity(Vector3 position, Vector3 handlePosition)
        {
            X = position.x;
            Y = position.y;
            Z = position.z;
            
            Hx = handlePosition.x;
            Hy = handlePosition.y;
            Hz = handlePosition.z;
        }

        public Vector3 Position()
        {
            return new Vector3(X, Y, Z);
        }

        public Vector3 HandlePosition()
        {
            return new Vector3(Hx, Hy, Hz);            
        }
    }
    
    public Entity[] Turners;
    public Entity[] Pushers;
    
    public State(IEnumerable<TurnEffector> turners, IEnumerable<PushEffector> pushers)
    {
        Turners = turners.Select(o => new Entity(o.transform.position, o.Handle.transform.position)).ToArray();
        Pushers = pushers.Select(o => new Entity(o.transform.position, o.Handle.transform.position)).ToArray();
    }
}
