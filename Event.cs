using System;
using System.Collections.Generic;
using BetterEventSystem.Exceptions;

namespace BetterEventSystem {

    public class EventArgs {
        string sender;
    }

    [Serializable]
    public class Event {
        public bool AllowAsync;
        public string Name;
        List<Action<EventArgs>> _listeners = new List<Action<EventArgs>>();

        public Event(String name, bool allowAsync = true, bool register = true) {
            this.Name = name;
            this.AllowAsync = allowAsync;
            if (register) { EventSystem.Events.Add(this); }
        }
        
        public void AddListener(Action<EventArgs> listener) {
            _listeners.Add(listener);
        }
        
        public void RemoveListener(Action<EventArgs> listener) {
            _listeners.Remove(listener);
        }

        public void BroadcastAsync(EventArgs args = null) {
            foreach (var item in _listeners) {
                if (AllowAsync) {
                    Task.Run(() => item(args));
                } else {
                    throw new BroadcastException("async not allowed in event");
                }
            }
        }

        public void BroadcastSync(EventArgs args = null) {
            foreach (var item in _listeners) {
                item.Invoke(args);
            }
        }

        public void Broadcast(EventArgs args = null) {
            foreach (var item in _listeners) {
                if (AllowAsync) {
                    Task.Run(() => item(args));
                } else {
                    item.Invoke(args);
                }
            }
        }
    }
}