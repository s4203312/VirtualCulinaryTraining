using System.Collections.Generic;
using UnityEngine;

public class GameEvent<T> : ScriptableObject                //Generic script for creating game events
{
    private List<GameEventListener<T>> listeners = new();

    public void Raise(T data)
    {
        foreach (var listener in listeners)
        {
            listener.OnEventRaised(data);
        }
    }

    public void RegisterListener(GameEventListener<T> listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener<T> listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
