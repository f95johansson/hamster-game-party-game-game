using System.Collections.Generic;
using UnityEngine;

public class State {

    public struct Entity
    {
        public readonly Vector3 Position;
        public readonly Vector3 HandlePosition;

        public Entity(Vector3 position, Vector3 handlePosition)
        {
            Position = position;
            HandlePosition = handlePosition;
        }
    }

    public readonly HashSet<Entity> Turners = new HashSet<Entity>();
    public readonly HashSet<Entity> Pushers = new HashSet<Entity>();

    public void AddAll(IEnumerable<PushEffector> objects)
    {
        foreach (var o in objects)
        {
            Pushers.Add(new Entity(o.transform.position, o.Handle.transform.position));
        }
    }
    
    public void AddAll(IEnumerable<TurnEffector> objects)
    {
        foreach (var o in objects)
        {
            Turners.Add(new Entity(o.transform.position, o.Handle.transform.position));
        }
    }
}
