﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDict;
    private static EventManager eventManager;
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("Event error");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }
    void Init()
    {
        if (eventDict == null)
        {
            eventDict = new Dictionary<string, UnityEvent>();
        }
    }
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if(instance.eventDict.TryGetValue(eventName,out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDict.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if(eventManager == null)
        {
            return;
        }
        UnityEvent thisEvent = null;
        if (instance.eventDict.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDict.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}