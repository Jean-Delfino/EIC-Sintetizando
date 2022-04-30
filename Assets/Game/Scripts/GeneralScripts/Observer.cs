using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jason Weimann
//Can be used to achievements 
public abstract class Observer : MonoBehaviour{
    public abstract void OnNotify(object value, NotificationType notificationType);
}

public abstract class Subject : MonoBehaviour{
    private List<Observer> observers = new List<Observer>();

    public void RegisterObserver(Observer observer){
        observers.Add(observer);
    }
    
    private void Notify(object value, NotificationType notificationType){
        foreach (var observer in observers){
            observer.OnNotify(value, notificationType);
        }
    }
}

public enum NotificationType{
    Achievement
}