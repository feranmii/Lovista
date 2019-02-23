using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{

    public class EventManager : MonoBehaviour
    {
        private static EventManager _instance;

        public static EventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EventManager>();
                }

                return _instance;
            }
        }

        delegate void EventListener(EventInfo e);

        private Dictionary<System.Type, List<EventListener>> eventListeners;
        
        
        private void Awake()
        {
            _instance = this;
        }

        public void RegisterListener<T>(System.Action<T> listener) where T : EventInfo
        {
            System.Type eventType = typeof(T);

            if (eventListeners == null)
            {
                eventListeners = new Dictionary<Type, List<EventListener>>();
            }

            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<EventListener>();
            }

            EventListener wrapper = (ei) => { listener((T) ei); };
            
            eventListeners[eventType].Add(wrapper);
            
        }


        public void FireEvent(EventInfo eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                //If there is no listener then it shows that we are done..
                return;
            }

            foreach (EventListener el in eventListeners[trueEventInfoClass])
            {
                el(eventInfo);
            }
        }
    }
    
    
}
