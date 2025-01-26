using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class LampTwinkleEvent : MonoBehaviour
{
    public UnityEvent myEvent;
 
    private void Awake() {
        if (myEvent == null)
            myEvent = new UnityEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        myEvent.Invoke();
        
        GetComponent<Collider>().enabled = false;
    }
}
