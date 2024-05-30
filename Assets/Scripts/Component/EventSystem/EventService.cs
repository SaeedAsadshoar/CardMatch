using System;
using System.Collections.Generic;
using UnityEngine;

namespace Component.EventSystem
{
    public static class EventService
    {
        private static readonly Dictionary<int, List<Delegate>> Events = new();
        private static Delegate _lastFunc;

        public static void Subscribe<T>(int eventId, Action<T> eventClass)
        {
            if (!Events.ContainsKey(eventId))
            {
                Events.Add(eventId, new List<Delegate>());
            }

            Events[eventId].Add(eventClass);
        }

        public static void Unsubscribe<T>(int eventId, Action<T> eventClass)
        {
            if (Events.TryGetValue(eventId, out var eventList))
            {
                eventList.Remove(eventClass);
            }
        }
        
        public static void Invoke<T>(int eventId, T eventClass)
        {
            if (!Events.TryGetValue(eventId, value: out var events)) return;
            foreach (var func in events)
            {
                _lastFunc = func;
                try
                {
                    func.DynamicInvoke(eventClass);
                }
                catch (Exception e)
                {
                    Debug.Log($"Bug in EventId:{eventId} Event : {e.Message} \n " +
                              $"{_lastFunc.Method.Name} \n " +
                              $"{_lastFunc.Method.Module} \n " +
                              $"{_lastFunc.GetType()} \n " +
                              $"{_lastFunc.Target}");
                }
            }
        }
    }
}