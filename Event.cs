using System;
using System.Collections.Generic;
using BetterEventSystem.Exceptions;

namespace BetterEventSystem {

    [Serializable]
    public class Event {
        public bool allowAsync;
        public string name;
        List<Action> listeners = new List<Action>();

        public Event(String name, bool allowAsync = true) {
            this.name = name;
            this.allowAsync = allowAsync;
            EventHandler.events.Add(this);
        }
        
        public void AddListener(Action listener) {
            listeners.Add(listener);
        }
        
        public void RemoveListener(Action listener) {
            listeners.Remove(listener);
        }

        public void BroadcastAsync() {
            foreach (var item in listeners) {
                if (allowAsync) {
                    item.BeginInvoke(null, null);
                } else {
                    new BroadcastException("async not allowed in event");
                }
            }
        }

        public void BroadcastSync() {
            foreach (var item in listeners) {
                item.Invoke();
            }
        }

        public void Broadcast() {
            foreach (var item in listeners) {
                if (allowAsync) {
                    item.BeginInvoke(null, null);
                } else {
                    item.Invoke();
                }
            }
        }
    }
}