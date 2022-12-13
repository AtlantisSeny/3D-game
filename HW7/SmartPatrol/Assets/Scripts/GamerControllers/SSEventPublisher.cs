using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSEventPublisher : MonoBehaviour {
    private List<IEventHandler> listeners;

    public SSEventPublisher(){
        listeners = new List<IEventHandler>();
    }

    //通知所有的订阅者
    public void Notify(int e){
        for(int i=0; i<listeners.Count; i++){
            IEventHandler eventHandler = listeners[i];
            eventHandler.Reaction(e);
        }
    }

    public void AddListener(IEventHandler e){
        listeners.Add(e);
    }

    public void RemoveListner(IEventHandler e){
        listeners.Remove(e);
    }
}