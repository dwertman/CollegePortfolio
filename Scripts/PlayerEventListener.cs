using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventListener : MonoBehaviour
{
    [SerializeField]
    private PlayerEvent playerEvent;
    [SerializeField]
    private UnityEvent response;

    private void OnEnable()
    {
        playerEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        playerEvent.UnregisteredListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
