using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GIB
{
    /// <summary>
    /// A singleton manager that keeps track of, and executes, specified events.
    /// </summary>
    public class EventController : MonoBehaviour
    {
        private static Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();
        public static EventController Instance;

        public List<string> subscriptionList;

        #region Initialization

        private void Awake() => Instance = this;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Subscribes to the specified Event.
        /// </summary>
        /// <param name="eventName">Event to subscribe to.</param>
        /// <param name="listener">Target UnityAction.</param>
        public static void Subscribe(string eventName, UnityAction listener, string listenerName = "anonymous")
        {
            if (!eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent = new UnityEvent();
                eventDictionary.Add(eventName, thisEvent);
            }

            thisEvent.AddListener(listener);
#if DEBUG
            if (Instance)
                Instance.AddToSubscriptionList(eventName, listenerName);
#endif
        }

        /// <summary>
        /// Unsubscribes from the specified Event.
        /// </summary>
        /// <param name="eventName">Event to subscribe to.</param>
        /// <param name="listener">Target UnityAction.</param>
        public static void Unsubscribe(string eventName, UnityAction listener, string listenerName = "anonymous")
        {
            if (eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        /// <summary>
        /// Announces an event to all objects subscribed to it.
        /// </summary>
        /// <param name="eventName">Target event to announce.</param>
        public static void Announce(string eventName)
        {
            if (eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        #endregion

        #region private methods

        #if DEBUG
        private void AddToSubscriptionList(string objName, string evName)
        {
            if (!subscriptionList.Contains(objName + " > " + evName))
            {
                subscriptionList.Add(objName + " > " + evName);
                subscriptionList.Sort();
            }
        }

        private void RemoveFromSubscriptionList(string objName, string evName)
        {
            if (subscriptionList.Contains(objName + " > " + evName))
            {
                subscriptionList.Remove(objName + " > " + evName);
            }
        }
        #endif

#endregion
    }
}
