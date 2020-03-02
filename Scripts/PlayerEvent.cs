using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Event", menuName = "Player Event", order = 52)]
public class PlayerEvent : ScriptableObject
{

    private List<PlayerEventListener> listeners = new List<PlayerEventListener>();

    public void Raise()
    {
        for(int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(PlayerEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisteredListener(PlayerEventListener listener)
    {
        listeners.Remove(listener);
    }
}
