using System.Collections.Generic;
using UnityEngine.Events;

public static class EventManager
{
   private static readonly Dictionary<EventKey, UnityEvent<object>> _events = new();

   public static UnityEvent<object> GetEvent(EventKey eventName)
   {
      return _events.TryGetValue(eventName, value: out var @event) ? @event : null;
   }
   
   public static void RaiseEvent(EventKey eventName, object data = null)
   {
      if (_events.TryGetValue(eventName, out var @event))
      {
         @event?.Invoke(data);
      }
   }

   public static void RegisterEvent(EventKey eventName, UnityAction<object> action)
   {
      if (!_events.ContainsKey(eventName))
      {
         _events.Add(EventKey.None, new());
      }
      _events[eventName].AddListener(action);
   }

   public static void UnregisterEvent(EventKey eventName, UnityAction<object> action)
   {
      if (_events.TryGetValue(eventName, out var @event))
      {
         @event.RemoveListener(action);
      }
   }
}
